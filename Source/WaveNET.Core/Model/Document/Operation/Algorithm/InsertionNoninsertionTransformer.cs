using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class InsertionNoninsertionTransformer
    {
        public OperationPair<IDocOp> TransformOperations(IDocOp insertionOp, IDocOp noninsertionOp)
        {
            var positionTracker = new PositionTracker();

            IRelativePosition insertionPosition = positionTracker.GetPosition1();
            IRelativePosition noninsertionPosition = positionTracker.GetPosition2();

            // The target responsible for processing components of the insertion operation.
            var insertionTarget = new InsertionTarget(insertionPosition);

            // The target responsible for processing components of the insertion-free operation.
            var noninsertionTarget = new NoninsertionTarget(noninsertionPosition);

            insertionTarget.SetOtherTarget(noninsertionTarget);
            noninsertionTarget.SetOtherTarget(insertionTarget);

            // Incrementally apply the two operations in a linearly-ordered interleaving fashion.
            int insertionIndex = 0;
            int noninsertionIndex = 0;

            while (insertionIndex < insertionOp.Size())
            {
                insertionOp.ApplyComponent(insertionIndex++, insertionTarget);
                while (insertionPosition.Get() > 0)
                {
                    if (noninsertionIndex >= noninsertionOp.Size())
                    {
                        throw new TransformException("Ran out of " + noninsertionOp.Size()
                            + " noninsertion op components after " + insertionIndex + " of " + insertionOp.Size()
                            + " insertion op components, with " + insertionPosition.Get() + " spare positions");
                    }
                    noninsertionOp.ApplyComponent(noninsertionIndex++, noninsertionTarget);
                }
            }
            while (noninsertionIndex < noninsertionOp.Size())
            {
                noninsertionOp.ApplyComponent(noninsertionIndex++, noninsertionTarget);
            }
            insertionOp = insertionTarget.Finish();
            noninsertionOp = noninsertionTarget.Finish();

            return new OperationPair<IDocOp>(insertionOp, noninsertionOp);
        }

        private void ApplyInsertionNoninsertionTransformation()
        {
        }

        abstract class Target : IEvaluatingDocOpCursor<IDocOp>
        {
            private readonly IEvaluatingDocOpCursor<IDocOp> _targetDocument;
            private readonly IRelativePosition _relativePosition;

            public Target(IEvaluatingDocOpCursor<IDocOp> targetDocument, IRelativePosition relativePosition)
            {
                _targetDocument = targetDocument;
                _relativePosition = relativePosition;
            }

            public virtual IDocOp Finish()
            {
                return _targetDocument.Finish();
            }
            public abstract void AnnotationBoundary(IAnnotationBoundaryMap map);
            public abstract void Characters(string characters);
            public abstract void ElementStart(string type, IAttributes attributes);
            public abstract void ElementEnd();
            public abstract void Retain(int itemCount);
            public abstract void DeleteCharacters(string characters);
            public abstract void DeleteElementStart(string type, IAttributes attributes);
            public abstract void DeleteElementEnd();
            public abstract void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes);
            public abstract void UpdateAttributes(IAttributesUpdate attributesUpdate);
        }
    }
}