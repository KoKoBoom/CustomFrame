/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/14 15:46:55
** Desc：	方法的调用链表
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Taki.Logging
{
    public class StackTraceLinkedList
    {
        public static string GetStrLinkedList(int depth, int maxDepth = 5)
        {
            List<string> listInfo = new List<string>();
            while (true)
            {
                if (depth > maxDepth) { break; }
                StackFrame st = new StackTrace(true).GetFrame(depth++);
                if (st == null)
                {
                    break;
                }
                var fileName = st.GetFileName();
                var lineNumber = st.GetFileLineNumber();
                //if (lineNumber == 0)
                //{
                //    break;
                //}
                var method = st.GetMethod();
                var fullName = string.Format("{0}.{1}({2})", method.ReflectedType.FullName, method.Name, string.Join(",", method.GetParameters().Select(o => string.Format("{0} {1}", o.ParameterType.ToString().Split('.').LastOrDefault(), o.Name)).ToArray()));
                listInfo.Add(String.Format("\r\t在{0} 位置{1} 行号：{2}", fullName, fileName, lineNumber));
            }
            return string.Join("\r\n", listInfo);
        }
    }
}
