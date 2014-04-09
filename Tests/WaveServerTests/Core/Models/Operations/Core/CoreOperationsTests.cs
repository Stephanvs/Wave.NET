using System;
using FakeItEasy;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Wave;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Core
{
    public class CoreOperationsTests
    {
        private const String Domain = "example.com";
        private static WaveId _waveId = WaveId.Of(Domain, "hello");
        private static WaveletId _waveletId = WaveletId.Of(Domain, "world");
        private static ParticipantId _participantId = new ParticipantId("test@" + Domain);
        private static String DOC_ID = "doc";
        private static String TEXT = "hi";

        [Fact]
        public void AddParticipantTest()
        {
            var op = new AddParticipantOperation(A.Fake<WaveletOperationContext>(), _participantId);
            
        }
    }
}