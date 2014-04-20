using System;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Operation.Algorithm;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     The class for transforming operations as in the Jupiter system.
    ///     The Jupiter algorithm takes 2 operations S and C and produces S' and C'.
    ///     Where the operations, S + C' = C + S'
    /// </summary>
    public static class Transformer
    {
        /// <summary>
        ///     Transforms a pair of operations.
        /// </summary>
        /// <param name="clientOperation">The client's operation.</param>
        /// <param name="serverOperation">The server's operation.</param>
        /// <returns>The resulting transformed client and server operations.</returns>
        public static OperationPair<WaveletOperation> Transform(WaveletOperation clientOperation,
            WaveletOperation serverOperation)
        {
            Preconditions.CheckNotNull(clientOperation);
            Preconditions.CheckNotNull(serverOperation);

            if (clientOperation is WaveletBlipOperation && serverOperation is WaveletBlipOperation)
            {
                var clientWaveBlipOp = (WaveletBlipOperation) clientOperation;
                var serverWaveBlipOp = (WaveletBlipOperation) serverOperation;

                if (clientWaveBlipOp.BlipId.Equals(serverWaveBlipOp.BlipId))
                {
                    // Transform document operations
                    BlipOperation clientBlipOp = clientWaveBlipOp.BlipOp;
                    BlipOperation serverBlipOp = serverWaveBlipOp.BlipOp;

                    OperationPair<BlipOperation> transformedBlipOps = Transform(clientBlipOp, serverBlipOp);

                    clientOperation = new WaveletBlipOperation(clientWaveBlipOp.BlipId,
                        transformedBlipOps.ClientOperation);
                    serverOperation = new WaveletBlipOperation(serverWaveBlipOp.BlipId,
                        transformedBlipOps.ServerOperation);
                }
            }
            else
            {
                if (serverOperation is RemoveParticipantOperation)
                {
                    var serverRemoveOp = serverOperation as RemoveParticipantOperation;
                    CheckParticipantRemoval(serverRemoveOp, clientOperation);

                    if (clientOperation is RemoveParticipantOperation)
                    {
                        var clientRemoveOp = clientOperation as RemoveParticipantOperation;
                        if (clientRemoveOp.ParticipantId.Equals(serverRemoveOp.ParticipantId))
                        {
                            clientOperation = new NoOp(clientRemoveOp.Context);
                            serverOperation = new NoOp(serverRemoveOp.Context);
                        }
                    }
                    else if (clientOperation is AddParticipantOperation)
                    {
                        CheckParticipantRemovalAndAddition(serverRemoveOp, clientOperation as AddParticipantOperation);
                    }
                }
                else if (serverOperation is AddParticipantOperation)
                {
                    var serverAddOp = serverOperation as AddParticipantOperation;

                    if (clientOperation is AddParticipantOperation)
                    {
                        var clientAddOp = clientOperation as AddParticipantOperation;

                        if (clientAddOp.ParticipantId.Equals(serverAddOp.ParticipantId))
                        {
                            clientOperation = new NoOp(clientAddOp.Context);
                            serverOperation = new NoOp(serverAddOp.Context);
                        }
                    }
                    else if (clientOperation is RemoveParticipantOperation)
                    {
                        CheckParticipantRemovalAndAddition(
                            clientOperation as RemoveParticipantOperation,
                            serverOperation as AddParticipantOperation);
                    }
                }
            }

            // Apply identity transform by default
            return new OperationPair<WaveletOperation>(clientOperation, serverOperation);
        }

        public static OperationPair<BlipOperation> Transform(BlipOperation clientOperation,
            BlipOperation serverOperation)
        {
            if (clientOperation is BlipContentOperation && serverOperation is BlipContentOperation)
            {
                var clientBlipContentOp = (BlipContentOperation) clientOperation;
                var serverBlipContentOp = (BlipContentOperation) serverOperation;
                IDocOp clientContentOp = clientBlipContentOp.ContentOp;
                IDocOp serverContentOp = serverBlipContentOp.ContentOp;

                OperationPair<IDocOp> transformedDocOps = Transform(clientContentOp, serverContentOp);

                clientOperation = new BlipContentOperation(clientBlipContentOp.Context,
                    transformedDocOps.ClientOperation);
                serverOperation = new BlipContentOperation(serverBlipContentOp.Context,
                    transformedDocOps.ServerOperation);
            }

            // Apply identity transform by default
            return new OperationPair<BlipOperation>(clientOperation, serverOperation);
        }

        /// <summary>
        ///     Transforms a pair of operations.
        /// </summary>
        /// <param name="clientOperation">the operation from the client</param>
        /// <param name="serverOperation">the operation from the server</param>
        /// <returns>the transformed pair of operations</returns>
        public static OperationPair<IDocOp> Transform(IDocOp clientOperation, IDocOp serverOperation)
        {
            // The transform process consists of decomposing the client and server
            // operations into two constituent operations each and performing four
            // transforms structured as in the following diagram:
            //     ci0     cn0
            // si0     si1     si2
            //     ci1     cn1
            // sn0     sn1     sn2
            //     ci2     cn2
            //
            try
            {
                Tuple<IDocOp, IDocOp> c = Decomposer.Decompose(clientOperation);
                Tuple<IDocOp, IDocOp> s = Decomposer.Decompose(serverOperation);

                var r1 = new InsertionTransformer().TransformOperations(c.Item1, s.Item1);
                var r2 = new InsertionNoninsertionTransformer().TransformOperations(r1.ClientOperation, s.Item2);
                var r3 = new InsertionNoninsertionTransformer().TransformOperations(r1.ServerOperation, c.Item2);
                var r4 = new NoninsertionTransformer().TransformOperations(r3.ServerOperation, r2.ServerOperation);

                return new OperationPair<IDocOp>(
                    Composer.Compose(r2.ClientOperation, r4.ClientOperation),
                    Composer.Compose(r3.ClientOperation, r4.ServerOperation));
            }
            catch (OperationException e)
            {
                throw new TransformException(e.Message, e);
            }
        }

        private static void CheckParticipantRemoval(RemoveParticipantOperation removeOperation,
            WaveletOperation operation)
        {
            ParticipantId participantId = removeOperation.ParticipantId;
            if (participantId.Equals(operation.Context.Creator))
            {
                throw new RemovedAuthorException(participantId.Address);
            }
        }

        /// <summary>
        ///     Checks to see if a participant is being removed by one operation and added
        ///     by another concurrent operation. In such a situation, at least one of the
        ///     operations is invalid.
        /// </summary>
        /// <param name="removeOperation">The operation to remove a participant.</param>
        /// <param name="addOperation">The operation to add a participant.</param>
        /// <exception cref="TransformException">
        ///     TransformException if the same participant is being concurrently added and
        ///     removed.
        /// </exception>
        private static void CheckParticipantRemovalAndAddition(RemoveParticipantOperation removeOperation,
            AddParticipantOperation addOperation)
        {
            if (removeOperation.ParticipantId.Equals(addOperation.ParticipantId))
                throw new TransformException("Transform error involving participant: " +
                                             removeOperation.ParticipantId.Address);
        }
    }
}