using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    [Serializable]
    public class InternalTransformException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InternalTransformException()
        {
        }

        public InternalTransformException(string message) : base(message)
        {
        }

        public InternalTransformException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InternalTransformException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}