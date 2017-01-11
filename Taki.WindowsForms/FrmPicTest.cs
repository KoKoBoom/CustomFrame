using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Taki.Common;
using static Taki.Common.FileExtend;

namespace Taki.WindowsForms
{
    public partial class FrmPicTest : Form
    {
        public FrmPicTest()
        {
            InitializeComponent();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {

            Taki.Logging.LoggerFactory.Create().Debug("11111111", "FrmPicTest.pictureEdit1_Click");

            var pic = sender as DevExpress.XtraEditors.PictureEdit;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    NewPath = SaveImage(ofd.FileName);
                    //pic.Image = GetImageFromReadAllBytes(NewPath);
                    pic.Image = GetImage(NewPath);
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
