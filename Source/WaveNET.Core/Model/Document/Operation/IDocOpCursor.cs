namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     The callback interface used by DocOp's apply method.
    /// </summary>
    public interface IDocOpCursor : IDocInitializationCursor
    {
        void Retain(int itemCount);
        
        void DeleteCharacters(string characters);
        
        void DeleteElementStart(string type, IAttributes attributes);
        
        void DeleteElementEnd();
        
        void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes);
        
        void UpdateAttributes(IAttributesUpdate attributesUpdate);
    }
}