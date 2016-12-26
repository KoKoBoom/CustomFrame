using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFrame.Common
{
    public class Utils
    {
        #region File操作
        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="fileAbsolutePath"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReadImageFileFromFileStream(string fileAbsolutePath)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(fileAbsolutePath))
            {
                int fileLength = 0;
                fileLength = (int)fs.Length; //获得文件长度 
                Byte[] image = new Byte[fileLength]; //建立一个字节数组 
                fs.Read(image, 0, fileLength); //按字节流读取 
                Image result = Image.FromStream(fs);
                return result;
            }
        }

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

        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }
            return photo;
        }
        #endregion
    }
}
