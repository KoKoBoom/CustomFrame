using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomFrame.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bitmap = new Bitmap(ofd.FileName);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        pictureEdit1.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
            }
        }

        public string NewPath { get; set; }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    NewPath = SaveImage(ofd.FileName);
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    Bitmap bitmap = new Bitmap(NewPath);
                    //    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //pictureBox1.Image = GetImageByBytes(ReadImage(NewPath));
                    pictureBox1.Image = CustomFrame.Common.Utils.ReadImageFileFromFileStream(NewPath);
                    //}
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(NewPath);
        }


        public string SaveImage(string path)
        {
            string newPath = Application.StartupPath + DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetExtension(path);
            using (Bitmap bitmap = new Bitmap(path))
            {
                bitmap.Save(newPath);
            }
            return newPath;
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
    }
}
