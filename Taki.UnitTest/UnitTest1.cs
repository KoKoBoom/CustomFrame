using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Taki.Common;

namespace Taki.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Taki.Common.Extensions.IsBetween(2, 1, 3), true);
            var json = new { A = 1.1f, B = "XXX" }.ToJson();
            string str = null;
            str.ToObject<dynamic>(false);
            var aa = json.TrimEnd(new char[] { '}' }).ToObject<dynamic>(true);
        }
    }
}
