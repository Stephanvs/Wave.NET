using System;
using System.Collections.Generic;
using System.Linq;

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
            if (!string.IsNullOrEmpty(characters))
            {
                FlushAnnotations();
                _target.Characters(characters);
            }
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
                var key = change.Key;
                var values = change.Value;
                var previousValues = _annotationTracker[key];

                if (values == null)
                {
                    if (previousValues != null)
                    {
                        _annotationTracker.Remove(key);
                        ends.Add(key);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                    //if (previousValues == null || 
                    //    !ValueUtils.Equals(values.OldValue, previousValues.OldValue) ||
                    //    !ValueUtils.Equals(values.NewValue, previousValues.NewValue))
                    //{
                    //    _annotationTracker.Add(key, values);
                    //    changes.Add(new AnnotationChange(key, values.OldValue, values.NewValue));
                    //}
                }

                if (changes.Any() || ends.Any())
                {
                    throw new NotImplementedException();
                    //_target.AnnotationBoundary(new AnnotationBoundaryMap()
                    //{
                    //    @Override
                    //    public int changeSize() {
                    //      return changes.size();
                    //    }

                    //    @Override
                    //    public String getChangeKey(int changeIndex) {
                    //      return changes.get(changeIndex).key;
                    //    }

                    //    @Override
                    //    public String getOldValue(int changeIndex) {
                    //      return changes.get(changeIndex).oldValue;
                    //    }

                    //    @Override
                    //    public String getNewValue(int changeIndex) {
                    //      return changes.get(changeIndex).newValue;
                    //    }

                    //    @Override
                    //    public int endSize() {
                    //      return ends.size();
                    //    }

                    //    @Override
                    //    public String getEndKey(int endIndex) {
                    //      return ends.get(endIndex);
                    //    }

                    //  });
                    //}
                }

                _annotationChanges.Clear();
            }
        }
    }
}