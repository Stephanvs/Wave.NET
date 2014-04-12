namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A DocOp whose components are in a random-access buffer.
    ///     Implementations MUST be immutable.
    /// </summary>
    public interface IBufferedDocOp : IDocOp
    {
    }
}