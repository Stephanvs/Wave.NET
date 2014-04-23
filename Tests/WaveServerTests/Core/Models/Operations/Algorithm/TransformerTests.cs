using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Operation;
using WaveNET.Core.Model.Operation.Wave;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Algorithm
{
    public class TransformerTests
    {
        [Fact]
        public void ClientOpLongerThanServerOp()
        {
            var ex =
                Assert.Throws<TransformException>(
                    () => Transformer.Transform(new DocOpBuilder().Retain(1).Build(), new DocOpBuilder().Build()));

            ex.Message.Should().Be("gramsdf");
        }
    }
}