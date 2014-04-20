using System;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public sealed class DocOpInverter<T> 
        : IEvaluatingDocOpCursor<T>
    {
        private readonly IEvaluatingDocOpCursor<T> _target;

        public DocOpInverter(IEvaluatingDocOpCursor<T> target)
        {
            _target = target;
        }

        public T Finish()
        {
            return _target.Finish();
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            _target.AnnotationBoundary(map);
        }

        public void Characters(string characters)
        {
            _target.DeleteCharacters(characters);
        }

        public void ElementStart(string type, IAttributes attributes)
        {
            throw new NotImplementedException();
        }

        public void ElementEnd()
        {
            throw new NotImplementedException();
        }

        public void Retain(int itemCount)
        {
            throw new NotImplementedException();
        }

        public void DeleteCharacters(string characters)
        {
            throw new NotImplementedException();
        }

        public void DeleteElementStart(string type, IAttributes attributes)
        {
            throw new NotImplementedException();
        }

        public void DeleteElementEnd()
        {
            throw new NotImplementedException();
        }

        public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttributes(IAttributesUpdate attributesUpdate)
        {
            throw new NotImplementedException();
        }

        public static IDocOp Invert(IDocOp input)
        {
            var inverter = new DocOpInverter<IDocOp>(new DocOpBuffer());
            input.Apply(inverter);
            return inverter.Finish();
        }
    }
}