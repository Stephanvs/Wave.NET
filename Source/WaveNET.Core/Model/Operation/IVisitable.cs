namespace WaveNET.Core.Model.Operation
{
    /// <summary>
    ///     Generic interface for objects that accept a visitor.
    /// </summary>
    /// <typeparam name="TVisitor">type of accepted visitor</typeparam>
    public interface IVisitable<in TVisitor>
        where TVisitor : IVisitor
    {
        void AcceptVisitor(TVisitor visitor);
    }
}