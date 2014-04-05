namespace WaveNET.Core.Model.Document.Operation
{
    public interface IEvaluatingDocOpCursor<out T> : IDocOpCursor
    {
        T Finish();
    }
}