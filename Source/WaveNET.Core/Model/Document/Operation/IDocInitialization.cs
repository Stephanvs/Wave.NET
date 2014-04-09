namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A document initialization.
    ///     Document initializations are document operations that apply to the empty
    ///     document; they do not contain update or deletion components.
    ///     This interface only offers a visitor pattern ('apply') to enumerate the
    ///     components; the data structure behind the operation remains opaque.
    /// </summary>
    public interface IDocInitialization : IDocOp
    {
        void Apply(IDocInitializationCursor cursor);

        void ApplyComponent(int i, IDocInitializationCursor cursor);

        DocInitializationComponentType GetComponentType(int i);
    }
}