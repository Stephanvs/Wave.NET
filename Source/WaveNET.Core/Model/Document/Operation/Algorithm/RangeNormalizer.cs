using System.Text;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class RangeNormalizer<T> : IEvaluatingDocOpCursor<T>
    {
        private readonly IEvaluatingDocOpCursor<T> _target;

        public RangeNormalizer(IEvaluatingDocOpCursor<T> target)
        {
            _target = target;

            _skipCache = new SkipCache(this);
            _charactersCache = new CharactersCache(this);
            _deleteCharactersCache = new DeleteCharactersCache(this);
        }

        public T Finish()
        {
            cache.Flush();
            return _target.Finish();
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            cache.Flush();
            _target.AnnotationBoundary(map);
        }

        public void Characters(string characters)
        {
            if (!string.IsNullOrEmpty(characters))
            {
                cache.Characters(characters);
            }
        }

        public void ElementStart(string type, IAttributes attributes)
        {
            cache.Flush();
            _target.ElementStart(type, attributes);
        }

        public void ElementEnd()
        {
            cache.Flush();
            _target.ElementEnd();
        }

        public void Retain(int itemCount)
        {
            if (itemCount > 0)
            {
                cache.Skip(itemCount);
            }
        }

        public void DeleteCharacters(string characters)
        {
            if (!string.IsNullOrEmpty(characters))
            {
                cache.DeleteCharacters(characters);
            }
        }

        public void DeleteElementStart(string type, IAttributes attributes)
        {
            cache.Flush();
            _target.DeleteElementStart(type, attributes);
        }

        public void DeleteElementEnd()
        {
            cache.Flush();
            _target.DeleteElementEnd();
        }

        public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
        {
            cache.Flush();
            _target.ReplaceAttributes(oldAttributes, newAttributes);
        }

        public void UpdateAttributes(IAttributesUpdate attributesUpdate)
        {
            cache.Flush();
            _target.UpdateAttributes(attributesUpdate);
        }

        protected static Cache cache = new EmptyCache();
        protected static readonly Cache _emptyCache = new EmptyCache();
        protected static Cache _skipCache;
        protected static Cache _charactersCache;
        protected static Cache _deleteCharactersCache;

        protected abstract class Cache
        {
            public abstract void Flush();

            public virtual void Skip(int distance)
            {
                Flush();
                cache = _skipCache;
                cache.Skip(distance);
            }

            public virtual void Characters(string characters)
            {
                Flush();
                cache = _charactersCache;
                cache.Characters(characters);
            }

            public virtual void DeleteCharacters(string characters)
            {
                Flush();
                cache = _deleteCharactersCache;
                cache.DeleteCharacters(characters);
            }
        }

        protected class EmptyCache
            : Cache
        {
            public override void Flush()
            {
            }
        }

        protected class SkipCache : Cache
        {
            private readonly RangeNormalizer<T> _rangeNormalizer;
            private int _dist = 0;

            public SkipCache(RangeNormalizer<T> rangeNormalizer)
            {
                _rangeNormalizer = rangeNormalizer;
            }

            public override void Flush()
            {
                _rangeNormalizer.Retain(_dist);
                _dist = 0;
                cache = _emptyCache;
            }

            public override void Skip(int distance)
            {
                _dist += _dist;
            }
        }

        protected class CharactersCache
            : Cache
        {
            private readonly RangeNormalizer<T> _rangeNormalizer;
            StringBuilder _chars = new StringBuilder();

            public CharactersCache(RangeNormalizer<T> rangeNormalizer)
            {
                _rangeNormalizer = rangeNormalizer;
            }

            public override void Flush()
            {
                _rangeNormalizer.Characters(_chars.ToString());
                _chars = new StringBuilder();
                cache = _emptyCache;
            }

            public override void Characters(string characters)
            {
                _chars.Append(characters);
            }
        }

        protected class DeleteCharactersCache
            : Cache
        {
            private readonly RangeNormalizer<T> _rangeNormalizer;
            private StringBuilder _chars = new StringBuilder();

            public DeleteCharactersCache(RangeNormalizer<T> rangeNormalizer)
            {
                _rangeNormalizer = rangeNormalizer;
            }

            public override void Flush()
            {
                _rangeNormalizer.Characters(_chars.ToString());
                _chars = new StringBuilder();
                cache = _emptyCache;
            }

            public override void DeleteCharacters(string characters)
            {
                _chars.Append(characters);
            }
        }
    }
}