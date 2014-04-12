using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation
{
    public abstract class AbstractDocInitialization
        : IDocInitialization
    {
        void IOperation<IDocOpCursor>.Apply(IDocOpCursor target)
        {
            Apply(target);
        }

        public abstract int Size();

        public abstract DocOpComponentType GetType(int i);
        
        public abstract void ApplyComponent(int i, IDocOpCursor cursor);
        
        public abstract IAnnotationBoundaryMap GetAnnotationBoundary(int i);
        
        public abstract string GetCharactersString(int i);
        
        public abstract string GetElementStartTag(int i);
        
        public abstract IAttributes GetElementStartAttributes(int i);
        
        public abstract int GetRetainItemCount(int i);
        
        public abstract string GetDeleteCharactersString(int i);
        
        public abstract string GetDeleteElementStartTag(int i);
        
        public abstract IAttributes GetDeleteElementStartAttributes(int i);
        
        public abstract IAttributes GetReplaceAttributesOldAttributes(int i);
        
        public abstract IAttributes GetReplaceAttributesNewAttributes(int i);
        
        public abstract IAttributesUpdate GetUpdateAttributesUpdate(int i);

        public abstract void Apply(IDocOpCursor cursor);
        
        public abstract void Apply(IDocInitializationCursor cursor);
        
        public abstract void ApplyComponent(int i, IDocInitializationCursor cursor);
        
        public abstract DocInitializationComponentType GetComponentType(int i);
    }
}