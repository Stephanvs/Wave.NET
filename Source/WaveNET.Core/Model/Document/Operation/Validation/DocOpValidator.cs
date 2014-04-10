using WaveNET.Core.Model.Document.Operation.Automation;

namespace WaveNET.Core.Model.Document.Operation.Validation
{
    public static class DocOpValidator
    {
        public static bool IsWellformed(ViolationCollector collector, IBufferedDocOp docOp)
        {
            // Todo: implement actual validation
            return true;
        }
    }
}