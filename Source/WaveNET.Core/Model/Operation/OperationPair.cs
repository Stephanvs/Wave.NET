namespace WaveNET.Core.Model.Operation
{
    public sealed class OperationPair<TOperation>
    {
        private readonly TOperation _clientOperation;
        private readonly TOperation _serverOperation;

        public OperationPair(TOperation clientOperation, TOperation serverOperation)
        {
            _clientOperation = clientOperation;
            _serverOperation = serverOperation;
        }

        /// <summary>
        ///     Gets the client's operation.
        /// </summary>
        public TOperation ClientOperation
        {
            get { return _clientOperation; }
            private set { }
        }

        /// <summary>
        ///     Gets the server's operation.
        /// </summary>
        public TOperation ServerOperation
        {
            get { return _serverOperation; }
            private set { }
        }
    }
}