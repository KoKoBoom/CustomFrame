using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using CustomFrame.Common;
using static CustomFrame.Common.FileExtend;

namespace CustomFrame.WindowsForms
{
    public partial class FrmPicTest : Form
    {
        public FrmPicTest()
        {
            InitializeComponent();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            var pic = sender as DevExpress.XtraEditors.PictureEdit;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    NewPath = SaveImage(ofd.FileName);
                    pic.Image = GetImageFromReadAllBytes(NewPath);
                }
            }
        }

        public string NewPath { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteFile(NewPath);
            pictureEdit1.Image = null;
            NewPath = string.Empty;
        }


        public string SaveImage(string path)
        {
            string newPath = Application.StartupPath + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetExtension(path);
            using (Bitmap bitmap = new Bitmap(path))
            {
                bitmap.Save(newPath);
            }
            return newPath;
        }
    }
}
