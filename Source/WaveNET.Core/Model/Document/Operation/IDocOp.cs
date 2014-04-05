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
    }
}