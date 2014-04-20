using System;
using FakeItEasy;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Util;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Wave.Data;
using WaveNET.Tests.Testing;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class BlipContentOperationTest
        : OperationTestBase
    {
        private static readonly IDocOp docOp = new DocOpBuilder().Characters("Hello").Build();

        [Fact]
        public void Apply()
        {
            var op = new BlipContentOperation(Context, docOp);
            var holderFactory = WaveNET.Core.Model.Wave.Data.WaveletData.WaveletDataFactory.Create(A.Fake<IDocumentFactory>());
            var wavelet = WaveletDataFactory<IWaveletData>.Of(holderFactory).Create();

            var blip = wavelet.CreateDocument("root", Jane, NoParticipants, EmptyDocument.Empty, new DateTime(), 0L);

            op.Apply(blip);

            // editing the document makes the op creator a blip contributor
            blip.Contributors.Should().Contain(Fred);

            // the op eventually reached the document
            //docOp.Should().Be(blip.Content);

            //// the op eventually reached the document
            //assertEquals(docOp, ((FakeDocument)blip.getContent()).getConsumed());
            //// editing the document makes the op creator a blip contributor
            //assertEquals(Collections.singleton(fred), blip.getContributors());
        }

        [Fact]
        public void ReverseRestoresContent()
        {
            var op = new BlipContentOperation(Context, docOp);
            var blip = WaveletData.CreateDocument("root", Fred, NoParticipants, EmptyDocument.Empty, new DateTime(), 0L);

            var reverseOps = op.ApplyAndReturnReverse(blip);

            foreach (var reverse in reverseOps)
            {
                reverse.Apply(blip);
            }

            blip.Contributors.Should().BeEmpty();
            //blip.Content.Should().Be("");
        }
    }
}