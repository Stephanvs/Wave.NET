namespace WaveNET.Core.Model.Document.Operation
{
    public class AnnotationsUpdate
        : IAnnotationsUpdate
    {
        public int ChangeSize()
        {
            throw new System.NotImplementedException();
        }

        public string GetChangeKey(int changeIndex)
        {
            throw new System.NotImplementedException();
        }

        public string GetOldValue(int changeIndex)
        {
            throw new System.NotImplementedException();
        }

        public string GetNewValue(int changeIndex)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotationsUpdate ComposeWith(IAnnotationsUpdate mutation)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotationsUpdate ComposeWith(IAnnotationBoundaryMap map)
        {
            throw new System.NotImplementedException();
        }
    }
}