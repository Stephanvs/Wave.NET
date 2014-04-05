using System;

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
            throw new NotImplementedException();
            //if (clientOperation is WaveletDocumentOperation && serverOperation is WaveletDocumentOperation)
            //{
            //    var clientWaveDocOp = (WaveletDocumentOperation) clientOperation;
            //    var serverWaveDocOp = (WaveletDocumentOperation) serverOperation;
            //    if (clientWaveDocOp.DocumentId.Equals(serverWaveDocOp.DocumentId))
            //    {
            //        // Transform document operations
            //        IBufferedDocOp clientMutation = clientWaveDocOp.Operation;
            //        IBufferedDocOp serverMutation = serverWaveDocOp.Operation;
            //        OperationPair<BufferedDocOp> transformedDocOps = Transformer.Transform(clientMutation,
            //            serverMutation);

            //        // Only recreate boxes if transform did something.  Yes, this is != not !.equals
            //        if (transformedDocOps.ClientOperation != clientMutation)
            //        {
            //            clientOperation = transformedDocOps.ClientOperation != null
            //                ? new WaveletDocumentOperation(clientWaveDocOp.DocumentId, transformedDocOps.ClientOperation)
            //                : null;
            //        }
            //        if (transformedDocOps.ServerOperation != serverMutation)
            //        {
            //            serverOperation = transformedDocOps.ServerOperation != null
            //                ? new WaveletDocumentOperation(serverWaveDocOp.DocumentId, transformedDocOps.ServerOperation)
            //                : null;
            //        }
            //    }
            //}
            //else
            //{
            //    if (serverOperation is RemoveParticipantOperation)
            //    {
            //        if (clientOperation is RemoveParticipantOperation)
            //        {
            //            ParticipantId clientParticipant = ((RemoveParticipantOperation) clientOperation).Participant;
            //            ParticipantId serverParticipant = ((RemoveParticipantOperation) serverOperation).Participant;
            //            if (clientParticipant.Equals(serverParticipant))
            //            {
            //                clientOperation = null;
            //                serverOperation = null;
            //            }
            //        }
            //        else if (clientOperation is AddParticipantOperation)
            //        {
            //            CheckParticipantRemovalAndAddition((RemoveParticipantOperation) serverOperation,
            //                (AddParticipantOperation) clientOperation);
            //        }
            //    }
            //    else if (serverOperation is AddParticipantOperation)
            //    {
            //        if (clientOperation is AddParticipantOperation)
            //        {
            //            ParticipantId clientParticipant = ((AddParticipantOperation) clientOperation).Participant;
            //            ParticipantId serverParticipant = ((AddParticipantOperation) serverOperation).Participant;
            //            if (clientParticipant.Equals(serverParticipant))
            //            {
            //                clientOperation = null;
            //                serverOperation = null;
            //            }
            //        }
            //        else if (clientOperation is RemoveParticipantOperation)
            //        {
            //            CheckParticipantRemovalAndAddition((RemoveParticipantOperation) clientOperation,
            //                (AddParticipantOperation) serverOperation);
            //        }
            //    }
            //}
            //// Apply identity transform by default
            //return new OperationPair<WaveletOperation>(clientOperation, serverOperation);
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
            throw new NotImplementedException();
            //if (removeOperation.Participant.Equals(addOperation.Participant))
            //    throw new TransformException("Transform error involving participant: " +
            //                                 removeOperation.Participant.GetAddress());
        }
    }
}