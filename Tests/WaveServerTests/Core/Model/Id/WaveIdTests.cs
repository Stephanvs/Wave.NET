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
        public void TestEmptyDomainRejected()
        {
            AssertIdRejected(null, "id");
            AssertIdRejected("", "id");
        }

        private static void AssertIdRejected(string domain, string id)
        {
            var x = new WaveId(domain, id);
        }
    }
}
