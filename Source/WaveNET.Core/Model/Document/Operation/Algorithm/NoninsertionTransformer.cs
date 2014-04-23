using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public sealed class NoninsertionTransformer
    {
        private readonly IEvaluatingDocOpCursor<IDocOp> _clientOperation;
        private readonly IEvaluatingDocOpCursor<IDocOp> _serverOperation;
        private readonly AnnotationTracker _clientAnnotationTracker;
        private readonly AnnotationTracker _serverAnnotationTracker;

        public NoninsertionTransformer()
        {
            _clientOperation = OperationNormalizer.CreateNormalizer(new DocOpBuffer());
            _serverOperation = OperationNormalizer.CreateNormalizer(new DocOpBuffer());

            _clientAnnotationTracker = new ClientAnnotationTracker(_clientOperation);
            _serverAnnotationTracker = new ServerAnnotationTracker(_serverOperation);
        }

        /// <summary>
        /// Transforms a pair of insertion-free operations.
        /// </summary>
        /// <param name="clientOperation">the operation from the client</param>
        /// <param name="serverOperation">the operation from the server</param>
        /// <returns>the transformed pair of operations</returns>
        public OperationPair<IDocOp> TransformOperations(IDocOp clientOperation, IDocOp serverOperation)
        {
            try
            {
                var positionTracker = new PositionTracker();

                var clientPosition = positionTracker.GetPosition1();
                var serverPosition = positionTracker.GetPosition2();

                var clientTarget = new Target(_clientOperation, clientPosition, _clientAnnotationTracker);

                var serverTarget = new Target(_serverOperation, serverPosition, _serverAnnotationTracker);

                clientTarget.OtherTarget = serverTarget;
                serverTarget.OtherTarget = clientTarget;

                // Incrementally apply the two operations in a linearly-ordered interleaving fashion
                int clientIndex = 0;
                int serverIndex = 0;

                while (clientIndex < clientOperation.Size())
                {
                    clientOperation.ApplyComponent(clientIndex++, clientTarget);

                    while (clientPosition.Get() > 0)
                    {
                        if (serverIndex >= serverOperation.Size())
                        {
                            throw new TransformException("Ran out of " + serverOperation.Size()
                                                         + " server op components after " + clientIndex + " of " +
                                                         clientOperation.Size()
                                                         + " client op components, with " + clientPosition.Get() +
                                                         " spare positions");
                        }
                        serverOperation.ApplyComponent(serverIndex++, serverTarget);
                    }
                }

                while (serverIndex < serverOperation.Size())
                {
                    serverOperation.ApplyComponent(serverIndex++, serverTarget);
                }

                clientOperation = clientTarget.Finish();
                serverOperation = serverTarget.Finish();
            }
            catch (InternalTransformException ex)
            {
                throw new TransformException(ex.Message, ex);
            }

            return new OperationPair<IDocOp>(clientOperation, serverOperation);
        }

        private abstract class AnnotationTracker
        {
            public IDocOpCursor Output { get; private set; }

            public AnnotationTracker(IDocOpCursor output)
            {
                Output = output;
            }
        }

        private class ClientAnnotationTracker
            : AnnotationTracker
        {
            public ClientAnnotationTracker(IDocOpCursor output)
                : base(output)
            {
            }
        }

        private class ServerAnnotationTracker
            : AnnotationTracker
        {
            public ServerAnnotationTracker(IDocOpCursor output)
                : base(output)
            {
            }
        }

        private sealed class Target
            : IEvaluatingDocOpCursor<IDocOp>
        {
            private IEvaluatingDocOpCursor<IDocOp> _targetDocument;
            private IRelativePosition _relativePosition;
            private AnnotationTracker _annotationTracker;

            public Target OtherTarget { get; set; }

            public Target(IEvaluatingDocOpCursor<IDocOp> targetDocument, IRelativePosition relativePosition, AnnotationTracker annotationTracker)
            {
                _targetDocument = targetDocument;
                _relativePosition = relativePosition;
                _annotationTracker = annotationTracker;
            }

            public IDocOp Finish()
            {
                return _targetDocument.Finish();
            }

            public void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                throw new System.NotImplementedException();
            }

            public void Characters(string characters)
            {
                throw new System.NotImplementedException();
            }

            public void ElementStart(string type, IAttributes attributes)
            {
                throw new System.NotImplementedException();
            }

            public void ElementEnd()
            {
                throw new System.NotImplementedException();
            }

            public void Retain(int itemCount)
            {
                throw new System.NotImplementedException();
            }

            public void DeleteCharacters(string characters)
            {
                throw new System.NotImplementedException();
            }

            public void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new System.NotImplementedException();
            }

            public void DeleteElementEnd()
            {
                throw new System.NotImplementedException();
            }

            public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new System.NotImplementedException();
            }

            public void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}