using System;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class InsertionTransformer
    {
        public OperationPair<IDocOp> TransformOperations(IDocOp clientOperation, IDocOp serverOperation)
        {
            var positionTracker = new PositionTracker();

            IRelativePosition clientPosition = positionTracker.GetPosition1();
            IRelativePosition serverPosition = positionTracker.GetPosition2();

            // The target responsible for processing components of the client operation.
            var clientTarget = new Target(clientPosition);

            // The target responsible for processing components of the server operation.
            var serverTarget = new Target(serverPosition);

            clientTarget.SetOtherTarget(serverTarget);
            serverTarget.SetOtherTarget(clientTarget);

            // Incrementally apply the two operations in a linearly-ordered interleaving fashion.
            ApplyInsertionTransformation(clientOperation, serverOperation, clientTarget, clientPosition, serverTarget);

            clientOperation = clientTarget.Finish();
            serverOperation = serverTarget.Finish();

            return new OperationPair<IDocOp>(clientOperation, serverOperation);
        }

        private static void ApplyInsertionTransformation(IDocOp clientOperation, IDocOp serverOperation,
                                                         Target clientTarget, IRelativePosition clientPosition,
                                                         Target serverTarget)
        {
            int clientIndex = 0;
            int serverIndex = 0;

            while (clientIndex < clientOperation.Size())
            {
                clientOperation.ApplyComponent(clientIndex++, clientTarget);

                while (clientPosition.Get() > 0)
                {
                    if (serverIndex >= serverOperation.Size())
                    {
                        string msg = string.Format(
                            "Ran out of {0} server op components after {1} of {2} client op components, with {3} spare positions",
                            serverOperation.Size(), clientIndex, clientOperation.Size(), clientPosition.Get());

                        throw new TransformException(msg);
                    }
                    serverOperation.ApplyComponent(serverIndex++, serverTarget);
                }
            }
            while (serverIndex < serverOperation.Size())
            {
                serverOperation.ApplyComponent(serverIndex++, serverTarget);
            }
        }

        private sealed class Target
            : IEvaluatingDocOpCursor<IDocOp>
        {
            private readonly IRelativePosition _relativePosition;

            private readonly IEvaluatingDocOpCursor<IDocOp> _targetDocument =
                new RangeNormalizer<IDocOp>(new DocOpBuffer());

            private Target _otherTarget;

            public Target(IRelativePosition relativePosition)
            {
                _relativePosition = relativePosition;
            }

            public IDocOp Finish()
            {
                return _targetDocument.Finish();
            }

            public void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void Characters(string characters)
            {
                _targetDocument.Characters(characters);
                _otherTarget._targetDocument.Retain(characters.Length);
            }

            public void ElementStart(string type, IAttributes attributes)
            {
                _targetDocument.ElementStart(type, attributes);
                _otherTarget._targetDocument.Retain(1);
            }

            public void ElementEnd()
            {
                _targetDocument.ElementEnd();
                _otherTarget._targetDocument.Retain(1);
            }

            public void Retain(int itemCount)
            {
                int oldPosition = _relativePosition.Get();
                _relativePosition.Increase(itemCount);

                if (_relativePosition.Get() < 0)
                {
                    _targetDocument.Retain(itemCount);
                    _otherTarget._targetDocument.Retain(itemCount);
                }
                else if (oldPosition < 0)
                {
                    _targetDocument.Retain(-oldPosition);
                    _otherTarget._targetDocument.Retain(-oldPosition);
                }
            }

            public void DeleteCharacters(string characters)
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void DeleteElementEnd()
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new InvalidOperationException("This method should never be called.");
            }

            public void SetOtherTarget(Target other)
            {
                _otherTarget = other;
            }
        }
    }
}