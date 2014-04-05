using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Operation
{
    [Serializable]
    public class OperationException : Exception
    {
        public OperationException()
        {
        }

        public OperationException(string message) : base(message)
        {
        }

        public OperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OperationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}