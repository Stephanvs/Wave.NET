namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class AnnotationsNormalizer<T> : IEvaluatingDocOpCursor<T>
    {
        public AnnotationsNormalizer(RangeNormalizer<T> rangeNormalizer)
        {
            throw new System.NotImplementedException();
        }

        public T Finish()
        {
            throw new System.NotImplementedException();
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