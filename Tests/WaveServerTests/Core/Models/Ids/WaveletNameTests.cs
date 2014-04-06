using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveNET.Core.Model.Id;

namespace WaveNET.Tests.Core.Models.Ids
{
    
    public class WaveletNameTests
    {
        [TestClass]
        public class WaveletNameSerializationTests
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

        [TestClass]
        public class WaveletNameEqualityTests
        {
            [TestMethod]
            public void SameDomainAndIdShoudEqual()
            {
                var one = CreateWaveletName("example.com", "w+abcd1234");
                var two = CreateWaveletName("example.com", "w+abcd1234");

                one.Equals(two).Should().BeTrue();
                two.Equals(one).Should().BeTrue();
            }
        }

        public static WaveletName CreateWaveletName(string domain, string id)
        {
            var waveId = WaveId.Of(domain, id);
            var waveletId = WaveletId.Of(domain, id);

            return WaveletName.Of(waveId, waveletId);
        }
    }
}
