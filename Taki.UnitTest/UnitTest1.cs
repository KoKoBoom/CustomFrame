using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taki.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Taki.Common.Extensions.IsBetween(2, 1, 3), true);
        }
    }
}
