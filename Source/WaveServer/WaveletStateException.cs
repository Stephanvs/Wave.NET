using System;
using System.Runtime.Serialization;

namespace WaveNET.Server
{
    [Serializable]
    public class WaveletStateException : Exception
    {
        public WaveletStateException()
        {
        }

        public WaveletStateException(string message) : base(message)
        {
        }

        public WaveletStateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WaveletStateException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}