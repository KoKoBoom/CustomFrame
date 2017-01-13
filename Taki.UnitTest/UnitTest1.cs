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

            var minDate = DateTime.MinValue;
            var maxDate = DateTime.MaxValue;

            Assert.AreEqual("2017-01-13".GetToDayBeginDateTime(), "2017-01-13 00:00:00".To<DateTime>());
            Assert.AreEqual("2017-01-13 11:11:11".To<DateTime>().GetToDayBeginDateTime(), "2017-01-13 00:00:00".To<DateTime>());
            Assert.AreEqual("2017-01-13 11:11:11fff".GetToDayBeginDateTime(), "1970-01-01 00:00:00".To<DateTime>());
            test2();

            Assert.AreEqual("2017-01-13".GetToDayEndDateTime(), "2017-01-13 23:59:59".To<DateTime>());
            Assert.AreEqual("2017-01-13 11:11:11".To<DateTime>().GetToDayEndDateTime(), "2017-01-13 23:59:59".To<DateTime>());


            Assert.AreEqual(Taki.Common.Extensions.IsBetween(2, 1, 3), true);
            var json = new { A = 1.1f, B = "XXX" }.ToJson();
            string str = null;
            str.ToObject<dynamic>(true);
            var aa = json.TrimEnd(new char[] { '}' }).ToObject<dynamic>(true);
        }

        public void test2() {
            Assert.AreEqual("2017-01-13 11:11:11fff".GetToDayBeginDateTime("2017-01-133"), "1970-01-01 00:00:00".To<DateTime>());
        }
    }
}
