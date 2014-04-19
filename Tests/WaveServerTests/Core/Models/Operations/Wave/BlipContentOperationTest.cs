﻿using System;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Util;
using WaveNET.Core.Model.Operation.Wave;
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
            var blip = WaveletData.CreateDocument("root", Jane, NoParticipants, EmptyDocument.Empty,
                new DateTime(), 0L);

            op.Apply(blip);

            // editing the document makes the op creator a blip contributor
            blip.Contributors.Should().Contain(Fred);

            // the op eventually reached the document
            docOp.Should().Be(blip.Content);

            //// the op eventually reached the document
            //assertEquals(docOp, ((FakeDocument)blip.getContent()).getConsumed());
            //// editing the document makes the op creator a blip contributor
            //assertEquals(Collections.singleton(fred), blip.getContributors());
        }
    }
}