using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HideInImage.Properties;

namespace HideInImage
{
    public partial class ProcessingForm : Form
    {
        public ProcessingForm()
        {
            InitializeComponent();
            pictureBox1.Image = Resources.Circle;
            TopMost = true;
        }

        internal void SetImage(Bitmap image, Size formSize, Point location)
        {
            Size = formSize;
            pictureBox1.Image = image;
            StartPosition = FormStartPosition.Manual;
            Location = location;
        }
    }
}
