using FluentAssertions;
using WaveNET.Core.Model.Id;
using Xunit;

namespace WaveNET.Tests.Core.Models.Ids
{
    public class WaveletNameTests
    {
        public static WaveletName CreateWaveletName(string domain, string id)
        {
            WaveId waveId = WaveId.Of(domain, id);
            WaveletId waveletId = WaveletId.Of(domain, id);

            return WaveletName.Of(waveId, waveletId);
        }

        public class WaveletNameEqualityTests
        {
            [Fact]
            public void SameDomainAndIdShoudEqual()
            {
                WaveletName one = CreateWaveletName("example.com", "w+abcd1234");
                WaveletName two = CreateWaveletName("example.com", "w+abcd1234");

                one.Equals(two).Should().BeTrue();
                two.Equals(one).Should().BeTrue();
            }
        }

        public class WaveletNameSerializationTests
        {
            [Fact]
            public void CreatingWaveletNameResultsInCorrectToString()
            {
                WaveId waveId = WaveId.Of("example.com", "w+abcd1234");
                WaveletId waveletId = WaveletId.Of("acmewave.com", "conv+blah");

                WaveletName
                    .Of(waveId, waveletId)
                    .ToString()
                    .Should()
                    .Be("[WaveletName example.com/w+abcd1234/acmewave.com/conv+blah]");
            }
        }
    }
}