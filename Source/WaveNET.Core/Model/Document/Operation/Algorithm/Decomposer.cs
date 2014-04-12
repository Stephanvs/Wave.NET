using System;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public static class Decomposer
    {
        public static Tuple<IDocOp, IDocOp> Decompose(IDocOp docOp)
        {
            var target = new Target();
            docOp.Apply(target);
            return target.Finish();
        }

        private class Target
            : IEvaluatingDocOpCursor<Tuple<IDocOp, IDocOp>>
        {
#warning Use Dependency Injection Container for resolving OperationNormalizer
            private readonly IEvaluatingDocOpCursor<IDocOp> _insertionOp =
                OperationNormalizer.CreateNormalizer(new DocOpBuffer());

#warning Use Dependency Injection Container for resolving OperationNormalizer
            private readonly IEvaluatingDocOpCursor<IDocOp> _noninsertionOp =
                OperationNormalizer.CreateNormalizer(new DocOpBuffer());

            public Tuple<IDocOp, IDocOp> Finish()
            {
                return new Tuple<IDocOp, IDocOp>(_insertionOp.Finish(), _noninsertionOp.Finish());
            }

            public void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                _noninsertionOp.AnnotationBoundary(map);
            }

            public void Characters(string characters)
            {
                _insertionOp.Characters(characters);
                _noninsertionOp.Retain(characters.Length);
            }

            public void ElementStart(string type, IAttributes attributes)
            {
                _insertionOp.ElementStart(type, attributes);
                _noninsertionOp.Retain(1);
            }

            public void ElementEnd()
            {
                _insertionOp.ElementEnd();
                _noninsertionOp.Retain(1);
            }

            public void Retain(int itemCount)
            {
                _insertionOp.Retain(itemCount);
                _noninsertionOp.Retain(itemCount);
            }

            public void DeleteCharacters(string characters)
            {
                _insertionOp.Retain(characters.Length);
                _noninsertionOp.DeleteCharacters(characters);
            }

            public void DeleteElementStart(string type, IAttributes attributes)
            {
                _insertionOp.Retain(1);
                _noninsertionOp.DeleteElementStart(type, attributes);
            }

            public void DeleteElementEnd()
            {
                _insertionOp.Retain(1);
                _noninsertionOp.DeleteElementEnd();
            }

            public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                _insertionOp.Retain(1);
                _noninsertionOp.ReplaceAttributes(oldAttributes, newAttributes);
            }

            public void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                _insertionOp.Retain(1);
                _noninsertionOp.UpdateAttributes(attributesUpdate);
            }
        }
    }
}