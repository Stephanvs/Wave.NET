using System;
using FluentAssertions;
using WaveNET.Core.Model.Id;
using Xunit;

namespace WaveNET.Tests.Core.Models.Ids
{
    [Trait("Category", "IdTests")]
    [Trait("Speed", "Fast")]
    public class IdTests
    {
        public static WaveId CreateWaveId(string domain, string id)
        {
            return WaveId.Of(domain, id);
        }

        public static WaveletId CreateWaveletId(string domain, string id)
        {
            return WaveletId.Of(domain, id);
        }

        public class WaveIdEqualityTests
        {
            [Fact]
            public void SameDomainAndIdShoudEqual()
            {
                WaveId x = CreateWaveId("domain", "id");
                WaveId y = CreateWaveId("domain", "id");

                x.Equals(y).Should().BeTrue();
                y.Equals(x).Should().BeTrue();
            }

            [Fact]
            public void DifferentDomainSameIdShouldNotEqual()
            {
                WaveId x = CreateWaveId("one", "id");
                WaveId y = CreateWaveId("two", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }

            [Fact]
            public void DifferentIdsSameDomainShoudNotEqual()
            {
                WaveId x = CreateWaveId("one", "sa");
                WaveId y = CreateWaveId("one", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }
        }

        public class WaveIdTests
        {
            [Fact]
            public void TestNullWaveIdDomainRejected()
            {
                Assert.Throws<ArgumentNullException>(() => { WaveId waveId = CreateWaveId(null, "id"); });
            }

            [Fact]
            public void TestEmptyWaveIdDomainRejected()
            {
                Assert.Throws<ArgumentNullException>(() => { WaveId waveId = CreateWaveId("", "id"); });
            }
        }

        public class WaveletIdEqualityTests
        {
            [Fact]
            public void SameDomainAndIdShoudEqual()
            {
                WaveletId x = CreateWaveletId("domain", "id");
                WaveletId y = CreateWaveletId("domain", "id");

                x.Equals(y).Should().BeTrue();
                y.Equals(x).Should().BeTrue();
            }

            [Fact]
            public void DifferentDomainSameIdShouldNotEqual()
            {
                WaveletId x = CreateWaveletId("one", "id");
                WaveletId y = CreateWaveletId("two", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }

            [Fact]
            public void DifferentIdsSameDomainShoudNotEqual()
            {
                WaveletId x = CreateWaveletId("one", "sa");
                WaveletId y = CreateWaveletId("one", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }
        }

        public class WaveletIdTests
        {
            [Fact]
            public void TestNullWaveletIdDomainRejected()
            {
                Assert.Throws<ArgumentNullException>(() => { WaveletId waveletId = CreateWaveletId(null, "id"); });
            }

            [Fact]
            public void TestEmptyWaveletIdDomainRejected()
            {
                Assert.Throws<ArgumentNullException>(() => { WaveletId waveletId = CreateWaveletId("", "id"); });
            }
        }
    }
}