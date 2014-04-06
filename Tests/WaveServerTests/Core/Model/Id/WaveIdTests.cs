using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveNET.Core.Model.Id;

namespace WaveNET.Tests.Core
{
    [TestClass]
    public class WaveIdTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void TestNullWaveIdDomainRejected()
        {
            var waveId = new WaveId(null, "id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void TestEmptyWaveIdDomainRejected()
        {
            var waveId = new WaveId("", "id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void TestNullWaveletIdDomainRejected()
        {
            var waveletId = new WaveletId(null, "id");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException), AllowDerivedTypes = true)]
        public void TestEmptyWaveletIdDomainRejected()
        {
            var waveletId = new WaveletId("", "id");
        }
    }
}
