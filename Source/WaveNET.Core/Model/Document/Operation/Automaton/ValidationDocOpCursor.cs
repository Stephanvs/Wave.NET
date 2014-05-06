namespace WaveNET.Core.Model.Document.Operation.Automaton
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Contained within DocOpValidator class in java source</remarks>
    public class ValidationDocOpCursor
        : IDocOpCursor
    {
        private readonly ViolationCollector _collector;
        private readonly DocOpAutomaton _automaton;
        private readonly ValidationResult[] _accu;

        private static readonly IllFormedException IllFormedException = 
            new IllFormedException("Preallocated exception with a meaningless stack trace");

        public ValidationDocOpCursor(ViolationCollector collector, DocOpAutomaton automaton, ValidationResult[] accu)
        {
            _collector = collector;
            _automaton = automaton;
            _accu = accu;
        }

        private void AbortIfIllFormed()
        {
            if (_accu[0] == ValidationResult.IllFormed)
            {
                throw IllFormedException;
            }
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            throw new System.NotImplementedException();
        }

        public void Characters(string characters)
        {
            _accu[0] = _accu[0].MergeWith(_automaton.CheckCharacters(characters, _collector));
        }

        public void ElementStart(string type, IAttributes attributes)
        {
            _accu[0] = _accu[0].MergeWith(_automaton.CheckElementStart(type, attributes, _collector));
            AbortIfIllFormed();
            _automaton.DoElementStart(type, attributes);
        }

        public void ElementEnd()
        {
            throw new System.NotImplementedException();
        }

        public void Retain(int itemCount)
        {
            _accu[0] = _accu[0].MergeWith(_automaton.CheckRetain(itemCount, _collector));
            AbortIfIllFormed();
            _automaton.DoRetain(itemCount);
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