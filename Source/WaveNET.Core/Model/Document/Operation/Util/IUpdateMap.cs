namespace WaveNET.Core.Model.Document.Operation.Util
{
    /// <summary>
    ///     Defines a reversible change on a map.
    /// </summary>
    public interface IUpdateMap
    {
        int ChangeSize();

        string GetChangeKey(int changeIndex);

        string GetOldValue(int changeIndex);

        string GetNewValue(int changeIndex);
    }
}