using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica_3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Text = "Clipping pe Imagini";
            this.Paint += DeseneazaClippedInainteSiDupa;
        }

        private (Image, Point) ClipImage(Image src, Rectangle clippingArea) { 
            Bitmap _src = new Bitmap(src);
            Bitmap dest = new Bitmap(clippingArea.Width, clippingArea.Height);
            for (int x = clippingArea.X; x < clippingArea.X + clippingArea.Width; x++) {
                for (int y = clippingArea.Y; y < clippingArea.Y + clippingArea.Height; y++)
                {
                    dest.SetPixel(x - clippingArea.X, y-clippingArea.Y, _src.GetPixel(x, y));
                }
            }
            return (dest, new(clippingArea.X, clippingArea.Y));
        }

        private void DeseneazaClippedInainteSiDupa(object? sender, PaintEventArgs e)
        {
            string imagePath = @"../../../lion.png";
            if (!File.Exists(imagePath))
            {
                MessageBox.Show("Image file 'lion.png' not found.", "Missing Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Image src = Image.FromFile(imagePath);
            pictureBox1.Image = src;
            Image i;
            Point p;
            (i, p) = ClipImage(src, new(150, 13, 314, 355));
            pictureBox2.Paint += (s, ev) => {
                ev.Graphics.DrawImage(i, p);
            };
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
