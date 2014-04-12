using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class OperationTestBase
    {
        protected ParticipantId Creator = new ParticipantId("lars@example.com");
        protected ParticipantId Fred = new ParticipantId("fred@example.com");
        protected ParticipantId Jane = new ParticipantId("jane@example.com");
        protected List<ParticipantId> NoParticipants = new List<ParticipantId>();

        protected WaveletOperationContext Context;
        protected WaveletData WaveletData;

        protected static readonly DateTime CreationTimestamp = DateTime.UtcNow;
        protected static readonly DateTime LastModifiedTimestamp = CreationTimestamp.AddSeconds(10);
        protected static readonly DateTime ContextTimestamp = LastModifiedTimestamp.AddSeconds(5);
        protected const long ContextVersion = 4L;
        //protected static HashedVersion ContextHashedVersion = HashedVersion.Of(ContextVersion, new byte[] { 4, 4, 4, 4 });

        public OperationTestBase()
        {
            Context = new WaveletOperationContext(Fred, ContextTimestamp, 1);
        }
    }
}