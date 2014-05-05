using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Document.Operation.Automation
{
    [Serializable]
    public class IllFormedException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public IllFormedException()
        {
        }

        public IllFormedException(string message) : base(message)
        {
        }

        public IllFormedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IllFormedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}