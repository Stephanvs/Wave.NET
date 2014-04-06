using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Id
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidIdException()
        {
        }

        public InvalidIdException(string message) : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidIdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}