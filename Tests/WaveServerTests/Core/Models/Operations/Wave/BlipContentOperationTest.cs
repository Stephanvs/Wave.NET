using System;
using FakeItEasy;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Util;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Wave.Data;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class BlipContentOperationTest
        : OperationTestBase
    {
         private static IDocOp docOp = new DocOpBuilder().Characters("Hello").Build();

        [Fact]
         public void Apply()
        {
            var op = new BlipContentOperation(Context, docOp);
            //var blip = WaveletData.CreateDocument("root", Jane, NoParticipants, EmptyDocument.Empty, new DateTime(), 0L);
            var blip = A.Fake<IBlipData>();

            op.Apply(blip);

            docOp.Should().Be(blip.Content);

            //// the op eventually reached the document
            //assertEquals(docOp, ((FakeDocument)blip.getContent()).getConsumed());
            //// editing the document makes the op creator a blip contributor
            //assertEquals(Collections.singleton(fred), blip.getContributors());
        }
    }
}