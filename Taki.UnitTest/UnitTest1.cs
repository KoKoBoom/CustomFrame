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

            var s = "ldp615df33".Matches("[0-9]+");

            FileExtend.Mp3ToWav("http://122.228.233.115/m10.music.126.net/20170116173128/86b6099e941062333abd8da3056123e8/ymusic/0198/3c2f/e61a/9f2444a064380651a955fa867f86f899.mp3?wshc_tag=1&wsts_tag=587c8d14&wsid_tag=3baf218e&wsiphost=ipdbm", "d:\\");


            string str = "的撒娇公开的世界观fdsakgjd";
            str.ToSBC();
            var lenth = "".LengthReal();

            var minDate = DateTime.MinValue;
            var maxDate = DateTime.MaxValue;

            //Assert.AreEqual("2017-01-13".GetToDayBeginDateTime(), "2017-01-13 00:00:00".To<DateTime>());
            //Assert.AreEqual("2017-01-13 11:11:11".To<DateTime>().GetToDayBeginDateTime(), "2017-01-13 00:00:00".To<DateTime>());
            //Assert.AreEqual("2017-01-13 11:11:11fff".GetToDayBeginDateTime(), "1970-01-01 00:00:00".To<DateTime>());
            //test2();

            //Assert.AreEqual("2017-01-13d".GetToDayEndDateTime("2017-01-13 23:59:59", true), "2017-01-13 23:59:59".To<DateTime>());
            //Assert.AreEqual("2017-01-13 11:11:11".To<DateTime>().GetToDayEndDateTime(), "2017-01-13 23:59:59".To<DateTime>());


            //Assert.AreEqual(Taki.Common.Extensions.IsBetween(2, 1, 3), true);
            //var json = new { A = 1.1f, B = "XXX" }.ToJson();
            //string str = null;
            //str.ToObject<dynamic>(true);
            //var aa = json.TrimEnd(new char[] { '}' }).ToObject<dynamic>(true);

        }

        public void test2()
        {
            Assert.AreEqual("2017-01-13 11:11:11fff".GetToDayBeginDateTime("2017-01-133"), "1970-01-01 00:00:00".To<DateTime>());
        }
    }
}
