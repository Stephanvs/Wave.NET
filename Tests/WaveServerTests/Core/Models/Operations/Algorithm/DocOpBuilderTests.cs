using System;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Algorithm
{
    public class DocOpBuilderTests
    {
        [Fact(Skip = "DocOpBuilder probably not working like this")]
        public void OpBuilder()
        {
            IDocOp docOp = new DocOpBuilder()
                .Retain(8)
                .Characters("can ")
                .Retain(21)
                .Characters(", usually")
                .Build();

            docOp.Size().Should().Be(4);

            docOp.GetRetainItemCount(0).Should().Be(8);
            docOp.GetCharactersString(1).Should().Be("can ");
            docOp.GetRetainItemCount(2).Should().Be(21);
            docOp.GetCharactersString(3).Should().Be(", usually");
        }

        [Fact(Skip = "DocOpBuilder probably not working like this")]
        [Trait("Inconclusive", "")]
        public void DocOpBuilder_NormalizeString()
        {
            IDocOp docOp = new DocOpBuilder()
                .Retain(8)
                .Characters("can ")
                .Retain(21)
                .Characters(", usually")
                .Build();

            IDocInitialization result = DocOpUtil.Normalize(new BufferedDocInitialization(docOp));

            //result.Should().Be("WaveNET.Core.Model.Document.Operation.BufferedDocInitialization {{ }}.");
        }

        [Fact(Skip = "DocOpBuilder probably not working like this")]
        public void Test()
        {
            IDocOp docOp = new DocOpBuilder()
                .Retain(8)
                .Characters("can ")
                .Retain(21)
                .Characters(", usually")
                .Build();

            var result = DocOpUtil.Normalize(docOp);

            Console.WriteLine("Result: {0}", result);
        }
    }
}