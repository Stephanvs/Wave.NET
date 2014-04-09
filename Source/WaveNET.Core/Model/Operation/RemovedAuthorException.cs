using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Operation
{
    [Serializable]
    public class RemovedAuthorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RemovedAuthorException()
        {
        }

        public RemovedAuthorException(string message) : base(message)
        {
        }

        public RemovedAuthorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RemovedAuthorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}