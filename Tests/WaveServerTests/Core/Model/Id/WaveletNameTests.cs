using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveNET.Core.Model.Id;

namespace WaveNET.Tests.Core.Model.Id
{
    [TestClass]
    public class WaveletNameTests
    {
        [TestMethod]
        public void CreatingWaveletNameResultsInCorrectToString()
        {
            var waveId = WaveId.Of("example.com", "w+abcd1234");
            var waveletId = WaveletId.Of("acmewave.com", "conv+blah");

            WaveletName
                .Of(waveId, waveletId)
                .ToString()
                .Should()
                .Be("[WaveletName example.com/w+abcd1234/acmewave.com/conv+blah]");
        }
    }
}
