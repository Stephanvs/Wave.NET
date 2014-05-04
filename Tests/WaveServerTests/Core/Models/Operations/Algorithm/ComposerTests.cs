using System;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Operation.Algorithm;
using WaveNET.Core.Model.Operation;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Algorithm
{
    public class ComposerTests
    {
        [Fact]
        public void DocumentLengthMismatch()
        {
            var ex = Assert.Throws<OperationException>(
                () =>
                    Composer.Compose(new DocOpBuilder().Build(), new DocOpBuilder().Retain(1).Build()));

            ex.Message.Should().Be("Illegal composition");
        }

        [Fact]
        public void DocumentLengthMismatchInverted()
        {
            var ex = Assert.Throws<OperationException>(
                () =>
                    Composer.Compose(new DocOpBuilder().Retain(1).Build(), new DocOpBuilder().Build()));

            ex.Message.Should().Be("Document size mismatch: op1 resulting length=1, op2 initial length=0");
        }

        [Fact]
        public void ComposerChecking()
        {
            IDocOp @checked = new DocOpBuilder().Build();
            IDocOp @unchecked = new DocOpBuilder()
                .ElementStart(".!$*.4!(,", Attributes.Empty)
                .ElementEnd()
                .BuildUnchecked();

            var ex = Assert.Throws<InvalidOperationException>(() => Composer.Compose(@checked, @unchecked));

            ex.Message.Should().Be("");
        }
    }
}