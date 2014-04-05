﻿using System;
using System.Runtime.Serialization;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    [Serializable]
    public class ComposeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ComposeException()
        {
        }

        public ComposeException(string message) : base(message)
        {
        }

        public ComposeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ComposeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}