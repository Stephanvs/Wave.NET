using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Operation
{
    [Serializable]
    public class TransformException : Exception
    {
        public TransformException()
        {
        }

        public TransformException(string message) : base(message)
        {
        }

        public TransformException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TransformException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}