using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveNET.Core.Model.Id;

namespace WaveNET.Tests.Core.Model.Id
{
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

        [TestClass]
        public class WaveIdEqualityTests
        {
            [TestMethod]
            public void SameDomainAndIdShoudEqual()
            {
                WaveId x = CreateWaveId("domain", "id");
                WaveId y = CreateWaveId("domain", "id");

                x.Equals(y).Should().BeTrue();
                y.Equals(x).Should().BeTrue();
            }

            [TestMethod]
            public void DifferentDomainSameIdShouldNotEqual()
            {
                WaveId x = CreateWaveId("one", "id");
                WaveId y = CreateWaveId("two", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }

            [TestMethod]
            public void DifferentIdsSameDomainShoudNotEqual()
            {
                WaveId x = CreateWaveId("one", "sa");
                WaveId y = CreateWaveId("one", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }
        }

        [TestClass]
        public class WaveIdTests
        {
            [TestMethod]
            [ExpectedException(typeof (ArgumentException), AllowDerivedTypes = true)]
            public void TestNullWaveIdDomainRejected()
            {
                WaveId waveId = CreateWaveId(null, "id");
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentException), AllowDerivedTypes = true)]
            public void TestEmptyWaveIdDomainRejected()
            {
                WaveId waveId = CreateWaveId("", "id");
            }
        }

        [TestClass]
        public class WaveletIdEqualityTests
        {
            [TestMethod]
            public void SameDomainAndIdShoudEqual()
            {
                WaveletId x = CreateWaveletId("domain", "id");
                WaveletId y = CreateWaveletId("domain", "id");

                x.Equals(y).Should().BeTrue();
                y.Equals(x).Should().BeTrue();
            }

            [TestMethod]
            public void DifferentDomainSameIdShouldNotEqual()
            {
                WaveletId x = CreateWaveletId("one", "id");
                WaveletId y = CreateWaveletId("two", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }

            [TestMethod]
            public void DifferentIdsSameDomainShoudNotEqual()
            {
                WaveletId x = CreateWaveletId("one", "sa");
                WaveletId y = CreateWaveletId("one", "id");

                x.Equals(y).Should().BeFalse();
                y.Equals(x).Should().BeFalse();
            }
        }

        [TestClass]
        public class WaveletIdTests
        {
            [TestMethod]
            [ExpectedException(typeof (ArgumentException), AllowDerivedTypes = true)]
            public void TestNullWaveletIdDomainRejected()
            {
                WaveletId waveletId = CreateWaveletId(null, "id");
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentException), AllowDerivedTypes = true)]
            public void TestEmptyWaveletIdDomainRejected()
            {
                WaveletId waveletId = CreateWaveletId("", "id");
            }
        }
    }
}