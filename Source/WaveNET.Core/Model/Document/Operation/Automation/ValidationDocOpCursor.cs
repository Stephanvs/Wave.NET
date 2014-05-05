using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation.Automation
{
    public class ValidationDocOpCursor
        : IDocOpCursor
    {
        private readonly ViolationCollector _collector;
        private readonly DocOpAutomation _automation;
        private readonly ValidationResult[] _accu;

        public ValidationDocOpCursor(ViolationCollector collector, DocOpAutomation automation, ValidationResult[] accu)
        {
            _collector = collector;
            _automation = automation;
            _accu = accu;
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            throw new System.NotImplementedException();
        }

        public void Characters(string characters)
        {
            _accu[0] = _accu[0].MergeWith(_automation.CheckCharacters(characters, _collector));
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