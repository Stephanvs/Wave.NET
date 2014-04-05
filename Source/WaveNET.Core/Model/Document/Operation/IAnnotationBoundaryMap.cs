namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     Information required by an annotation boundary operation component.
    ///     Implementations must be immutable
    /// </summary>
    public interface IAnnotationBoundaryMap
    {
        int EndSize();
        string GetEndKey(int index);

        int ChangeSize();
        string GetChangeKey(int index);
        string GetOldValue(int index);
        string GetNewValue(int index);
    }
}