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
    public partial class FrmPicTest : Form
    {
        public FrmPicTest()
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
                    NewPath = SaveImage(ofd.FileName);
                    pictureEdit1.Image = CustomFrame.Common.Utils.ReadImageFileFromFileStream(NewPath);
                }
            }
        }

        public string NewPath { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(NewPath);
            pictureEdit1.Image = null;
            NewPath = string.Empty;
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
    }
}
