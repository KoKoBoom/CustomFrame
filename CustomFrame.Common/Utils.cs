﻿using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace CustomFrame.Common
{
    public static class Utils
    {
        #region ============= HtmlHelper ===============

        #region 获取压缩的html字符串
        /// <summary>
        /// 获取压缩的html字符串
        /// </summary>
        /// <returns></returns>
        public static string GetZipHtml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 30000;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //设置连接超时时间
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result = "";
            using (Stream streamReceive = response.GetResponseStream())
            {
                using (GZipStream zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                using (StreamReader sr = new StreamReader(zipStream, encoding))
                    result = sr.ReadToEnd();
            }
            return result;
        }
        #endregion

        #endregion
    }
}
