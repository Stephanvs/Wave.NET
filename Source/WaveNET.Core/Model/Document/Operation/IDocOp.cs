using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A document operation.
    ///     This interface only offers a visitor pattern ('apply') to enumerate the
    ///     components; the data structure behind the operation remains opaque.
    /// </summary>
    public interface IDocOp : IOperation<IDocOpCursor>
    {
        new void Apply(IDocOpCursor cursor);
        
        int Size();
        
        DocOpComponentType GetType(int i);
        
        // A method get(int) that returns a DocOpComponent would be more intuitive,
        // but with accessors like the ones below, we can avoid reifying the component
        // objects.
        void ApplyComponent(int i, IDocOpCursor cursor);
        
        IAnnotationBoundaryMap GetAnnotationBoundary(int i);
        
        string GetCharactersString(int i);
        
        string GetElementStartTag(int i);
        
        IAttributes GetElementStartAttributes(int i);
        
        int GetRetainItemCount(int i);
        
        string GetDeleteCharactersString(int i);
        
        string GetDeleteElementStartTag(int i);
        
        IAttributes GetDeleteElementStartAttributes(int i);
        
        IAttributes GetReplaceAttributesOldAttributes(int i);
        
        IAttributes GetReplaceAttributesNewAttributes(int i);
        
        IAttributesUpdate GetUpdateAttributesUpdate(int i);
    }
}