using System;

namespace WaveNET.Core.Model.Document.Operation
{
    public class InitializationCursorAdapter
        : IDocOpCursor
    {
        private readonly IDocInitializationCursor _inner;

        private InitializationCursorAdapter(IDocInitializationCursor inner)
        {
            _inner = inner;
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            _inner.AnnotationBoundary(map);
        }

        public void Characters(string characters)
        {
            _inner.Characters(characters);
        }

        public void ElementStart(string type, IAttributes attributes)
        {
            _inner.ElementStart(type, attributes);
        }

        public void ElementEnd()
        {
            _inner.ElementEnd();
        }

        public void Retain(int itemCount)
        {
            throw new NotSupportedException("Retain");
        }

        public void DeleteCharacters(string characters)
        {
            throw new NotSupportedException("DeleteCharacters");
        }

        public void DeleteElementStart(string type, IAttributes attributes)
        {
            throw new NotSupportedException("DeleteElementStart");
        }

        public void DeleteElementEnd()
        {
            throw new NotSupportedException("DeleteElementEnd");
        }

        public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
        {
            throw new NotSupportedException("ReplaceAttributes");
        }

        public void UpdateAttributes(IAttributesUpdate attributesUpdate)
        {
            throw new NotSupportedException("UpdateAttributes");
        }

        public static IDocOpCursor Adapt(IDocInitializationCursor initializationCursor)
        {
            if (initializationCursor is IDocOpCursor)
            {
                return (IDocOpCursor) initializationCursor;
            }
            return new InitializationCursorAdapter(initializationCursor);
        }
    }
}