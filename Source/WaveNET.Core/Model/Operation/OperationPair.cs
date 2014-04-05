namespace WaveNET.Core.Model.Operation
{
    public sealed class OperationPair<O>
    {
        private readonly O _clientOperation;
        private readonly O _serverOperation;

        public OperationPair(O clientOperation, O serverOperation)
        {
            _clientOperation = clientOperation;
            _serverOperation = serverOperation;
        }

        /// <summary>
        ///     Gets the client's operation.
        /// </summary>
        public O ClientOperation
        {
            get { return _clientOperation; }
            private set { }
        }

        /// <summary>
        ///     Gets the server's operation.
        /// </summary>
        public O ServerOperation
        {
            get { return _serverOperation; }
            private set { }
        }
    }
}