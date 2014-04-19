namespace WaveNET.Core.Model.Operation
{
    public sealed class OperationPair<TOperation>
    {
        public OperationPair(TOperation clientOperation, TOperation serverOperation)
        {
            ClientOperation = clientOperation;
            ServerOperation = serverOperation;
        }

        /// <summary>
        ///     Gets the client's operation.
        /// </summary>
        public TOperation ClientOperation { get; private set; }

        /// <summary>
        ///     Gets the server's operation.
        /// </summary>
        public TOperation ServerOperation { get; private set; }
    }
}