namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class OperationNormalizer
    {
        public static IEvaluatingDocOpCursor<T> CreateNormalizer<T>(IEvaluatingDocOpCursor<T> target)

        {
            return new AnnotationsNormalizer<T>(new RangeNormalizer<T>(target));
        }
    }
}