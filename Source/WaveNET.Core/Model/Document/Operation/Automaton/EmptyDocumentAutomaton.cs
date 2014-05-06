namespace WaveNET.Core.Model.Document.Operation.Automaton
{
    public class EmptyDocumentAutomaton : IAutomatonDocument
    {
        public string ElementStartingAt(int position)
        {
            throw new System.NotImplementedException();
        }

        public IAttributes AttributesAt(int position)
        {
            throw new System.NotImplementedException();
        }

        public string ElementEndingAt(int position)
        {
            throw new System.NotImplementedException();
        }

        public int CharAt(int position)
        {
            throw new System.NotImplementedException();
        }

        public string NthEnclosingElementTag(int insertionPoint, int depth)
        {
            throw new System.NotImplementedException();
        }

        public int RemainingCharactersInElement(int insertionPoint)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotationMap AnnotationsAt(int position)
        {
            throw new System.NotImplementedException();
        }

        public string GetAnnotations(int position, string key)
        {
            throw new System.NotImplementedException();
        }

        public int Length()
        {
            return 0;
        }

        public int FirstAnnotationChange(int start, int end, string key, string fromValue)
        {
            throw new System.NotImplementedException();
        }
    }
}