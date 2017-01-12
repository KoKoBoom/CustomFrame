using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Taki.Common;
using System.Linq;
using System.Collections.Generic;

namespace Taki.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> list = new List<string> { "1", "2", "3" };
            var list1 = list.Join(",");
            var list2 = list.Join("");


            Assert.AreEqual(Taki.Common.Extensions.IsBetween(2, 1, 3), true);
            var json = new { A = 1.1f, B = "XXX" }.ToJson();
            string str = null;
            str.ToObject<dynamic>(true);
            var aa = json.TrimEnd(new char[] { '}' }).ToObject<dynamic>(true);
        }
    }
}
