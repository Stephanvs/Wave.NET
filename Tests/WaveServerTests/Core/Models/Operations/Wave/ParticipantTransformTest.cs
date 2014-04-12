using System;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Operation;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Wave;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class ParticipantTransformTest
    {
        private static readonly ParticipantId Fred = new ParticipantId("fred@example.com");
        private static readonly ParticipantId John = new ParticipantId("john@example.com");
        private static readonly ParticipantId Marc = new ParticipantId("marc@example.com");

        /// <summary>
        /// Tests that the correct exception is thrown when a removed participant issues an operation.
        /// </summary>
        [Fact]
        public void RemovedAuthorException()
        {
            var contextA = new WaveletOperationContext(Fred, DateTime.UtcNow.AddMinutes(-2), 1L);
            var contextB = new WaveletOperationContext(John, DateTime.UtcNow.AddMinutes(-1), 1L);

            var x = new DocOpBuilder().Characters("x").Build();
            
            var clientOperation = new WaveletBlipOperation("dummy", new BlipContentOperation(contextB, x));
            var serverOperation = new RemoveParticipantOperation(contextA, John);

            var ex = Assert.Throws<RemovedAuthorException>(() => Transformer.Transform(clientOperation, serverOperation));
            //ex.Message.Should().Be("Transform error involving participant: " + John.Address);
        }
    }
}