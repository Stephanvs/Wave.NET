using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Wave.Data
{
    public interface IDocumentOperationSink
        : IOperationSink<IDocOp>
    {
    }
}