namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A DocOp whose components are in a random-access buffer.
    ///     Implementations MUST be immutable.
    /// </summary>
    public interface IBufferedDocOp : IDocOp
    {
        int Size();
        DocOpComponentType GetType(int i);

        // A method get(int) that returns a DocOpComponent would be more intuitive,
        // but with accessors like the ones below, we can avoid reifying the component
        // objects.
        // http://www.google.com/codesearch/p?hl=en&sa=N&cd=1&ct=rc#BP5se_RPVvg/src/org/waveprotocol/wave/model/document/operation/BufferedDocOp.java&q=Operation%3C%20package:http://wave-protocol\.googlecode\.com
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