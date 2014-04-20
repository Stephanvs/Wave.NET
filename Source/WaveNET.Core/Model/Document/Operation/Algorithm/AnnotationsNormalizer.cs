using System;
using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    internal sealed class AnnotationChange
    {
        public string Key { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public AnnotationChange(string key, string oldValue, string newValue)
        {
            Key = key;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    internal sealed class AnnotationChangeValues
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public AnnotationChangeValues(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class AnnotationsNormalizer<T> : IEvaluatingDocOpCursor<T>
    {
        private readonly IEvaluatingDocOpCursor<T> _target;

        private readonly IDictionary<string, AnnotationChangeValues> _annotationTracker =
            new Dictionary<string, AnnotationChangeValues>();

        private readonly IDictionary<string, AnnotationChangeValues> _annotationChanges =
            new Dictionary<string, AnnotationChangeValues>();

        public AnnotationsNormalizer(IEvaluatingDocOpCursor<T> target)
        {
            _target = target;
        }

        public T Finish()
        {
            FlushAnnotations();
            return _target.Finish();
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
            if (itemCount > 0)
            {
                FlushAnnotations();
                _target.Retain(itemCount);
            }
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

        private void FlushAnnotations()
        {
            var changes = new List<AnnotationChange>();
            var ends = new List<string>();

            foreach (var change in _annotationChanges)
            {
                // todo: Implement
                throw new NotImplementedException();
            }
        }
    }
}