using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomFrame.Common
{
    public class Utils
    {
        #region ============= File操作 =============

        #region 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static Image ReadImageFileFromFileStream(string fileAbsolutePath)
        {
            using (FileStream fs = File.OpenRead(fileAbsolutePath))
            {
                int fileLength = 0;
                fileLength = (int)fs.Length; //获得文件长度 
                Byte[] image = new Byte[fileLength]; //建立一个字节数组 
                fs.Read(image, 0, fileLength); //按字节流读取 
                using (Image result = Image.FromStream(fs))
                {
                    return result;
                }
            }
        }
        #endregion

        #region 读取文件到 byte[]
        /// <summary>
        /// 读取文件到 byte[]
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public byte[] ReadFileToBytes(string fileAbsolutePath)
        {
            byte[] buffer = null;
            if (File.Exists(fileAbsolutePath))
            {
                using (FileStream fs = new FileStream(fileAbsolutePath, FileMode.Open))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, int.Parse(fs.Length.ToString()));
                }
            }
            return buffer;
        }
        #endregion

        #region 将image转化为二进制 
        /// <summary>
        /// 将image转化为二进制 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] GetByteImage(Image img)
        {
            byte[] bt = null;
            if (!img.Equals(null))
            {
                using (MemoryStream mostream = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(mostream, img.RawFormat);//将图像以指定的格式存入缓存内存流
                    bt = new byte[mostream.Length];
                    mostream.Position = 0;//设置留的初始位置
                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));
                }
            }
            return bt;
        }
        #endregion

        #region 读取byte[]并转化为图片
        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Image GetImageByBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                using (var photo = Image.FromStream(ms, true))
                {
                    return photo;
                }
            }
        }
        #endregion

        #region 读取文本文件
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static string ReadTextFile(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                return File.ReadAllText(fileAbsolutePath, Encoding.Default);
            }
            return null;
        }
        #endregion

        #region 写入文本文件 如果存在则覆盖
        /// <summary>
        /// 写入文本文件 如果存在则覆盖
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static void WriteTextFile(string fileAbsolutePath, string contents)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileAbsolutePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileAbsolutePath));
            }
            File.WriteAllText(fileAbsolutePath, contents, Encoding.Default);
        }
        #endregion

        #region 删除指定目录文件
        /// <summary>
        /// 删除指定目录文件
        /// </summary>
        public static void DeleteFile(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath))
            {
                File.Delete(fileAbsolutePath);
            }
        }
        #endregion

        #endregion

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
