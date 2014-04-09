namespace WaveNET.Core.Model.Operation
{
    public interface IOperationSink<in T>
        //where T: IOperation<T>
    {
        void Consume(T op);
    }
}