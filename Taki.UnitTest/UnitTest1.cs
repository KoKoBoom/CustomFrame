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
            Assert.IsTrue("0".IsMatch(Regexs.Intege), $"Regexs.Intege:0:{"0".IsMatch(Regexs.Intege)}");
            Assert.IsTrue("1".IsMatch(Regexs.Intege), $"Regexs.Intege:1:{"1".IsMatch(Regexs.Intege)}");
            Assert.IsTrue("-1".IsMatch(Regexs.Intege), $"Regexs.Intege:-1:{"-1".IsMatch(Regexs.Intege)}");

            Assert.IsFalse("0".IsMatch(Regexs.UIntege), $"Regexs.Intege:0:{"0".IsMatch(Regexs.UIntege)}");
            Assert.IsTrue("1".IsMatch(Regexs.UIntege), $"Regexs.Intege:1:{"1".IsMatch(Regexs.UIntege)}");
            Assert.IsFalse("-1".IsMatch(Regexs.UIntege), $"Regexs.Intege:-1:{"-1".IsMatch(Regexs.UIntege)}");

            Assert.IsFalse("0".IsMatch(Regexs.NegateIntege), $"Regexs.Intege:0:{"0".IsMatch(Regexs.NegateIntege)}");
            Assert.IsFalse("1".IsMatch(Regexs.NegateIntege), $"Regexs.Intege:1:{"1".IsMatch(Regexs.NegateIntege)}");
            Assert.IsTrue("-1".IsMatch(Regexs.NegateIntege), $"Regexs.Intege:-1:{"-1".IsMatch(Regexs.NegateIntege)}");


            //FileExtend.Mp3ToWav("http://122.228.233.115/m10.music.126.net/20170116173128/86b6099e941062333abd8da3056123e8/ymusic/0198/3c2f/e61a/9f2444a064380651a955fa867f86f899.mp3?wshc_tag=1&wsts_tag=587c8d14&wsid_tag=3baf218e&wsiphost=ipdbm", "d:\\");
        }

        public void test2()
        {
            Assert.AreEqual("2017-01-13 11:11:11fff".GetToDayBeginDateTime("2017-01-133"), "1970-01-01 00:00:00".To<DateTime>());
        }
    }
}
