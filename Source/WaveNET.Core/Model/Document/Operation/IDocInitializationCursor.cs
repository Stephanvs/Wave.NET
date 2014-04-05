namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     The callback interface used by DocInitialization's apply method.
    /// </summary>
    public interface IDocInitializationCursor
    {
        void AnnotationBoundary(IAnnotationBoundaryMap map);
        void Characters(string characters);
        void ElementStart(string type, IAttributes attributes);
        void ElementEnd();
    }
}