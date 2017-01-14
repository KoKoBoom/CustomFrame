using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taki.Common;
using Taki.Logging;

namespace Taki.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Test1();
            Console.ReadLine();
        }

        public static void Test1()
        {
            Test2<string>("a", "b");
        }

        public static void Test2<T>(string a, string b)
        {
            try
            {

                "fds".To<int>();
            }
            catch (Exception ex)
            {
                LoggerFactory.Create()?.Error("转换出错", ex);
            }

            //Thread thread1 = new Thread(() =>
            //{
            //for (var i = 0; i < 10; i++)
            "fsd".GetToDayBeginDateTime("fd");
            try
            {
                var aa = 0;
                var bb = 0;
                var cc = aa / bb;
            }
            catch (Exception ex) { LoggerFactory.Create()?.Error(ex); }
            //});
            //thread1.Start();

            //Thread thread2 = new Thread(() =>
            //{
            //    for (var i = 10; i < 20; i++)
            //        "fsd".GetToDayBeginDateTime("fd");
            //});

            //thread2.Start();

        }
    }
}
