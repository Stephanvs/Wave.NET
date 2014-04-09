namespace WaveNET.Core.Model.Document.Operation
{
    public abstract class AbstractBufferedDocInitialization
        : AbstractDocInitialization
    {
        public override void Apply(IDocOpCursor cursor)
        {
            Apply((IDocInitializationCursor) cursor);
        }

        
    }
}