using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica_1234
{
    public partial class Lab1 : Form
    {
        public Random random = new Random();
        List<Point> points = new List<Point>();
        Bitmap canvas;
        public Lab1()
        {
            InitializeComponent();
            this.Text = "Pixeli si Linii pe Bitmap";
            this.Width = 800;
            this.Height = 800;
            this.Paint += new PaintEventHandler(DeseneazaPuncte);

            canvas = new Bitmap(this.Width, this.Height);

            for (int x = 0; x < this.Width; x++) {
                for (int y = 0; y < this.Height; y++)
                {
                    for (int _=0; _<100; _++)
                    {
                        canvas.SetPixel(x,y,
                            (x*y%100!=0 && random.NextDouble() < 0.3) 
                            ? Color.Black
                            : Color.White);
                        if (x * y % 200 == 0 && random.NextDouble() is >= 0.22 and <= 0.6) {
                            points.Add(new(x, y));
                        }
                    }
                }
            }

            for (int i=0; i<points.Count; i++)
            {
                DeseneazaLinie(canvas, points[i], points[(i+1)% points.Count], Color.Red);
            }

            canvas.Save(@"../../../puncte.png");
        }

        private void DeseneazaPuncte(object sender, PaintEventArgs e) {
            e.Graphics.DrawImage(canvas, 0, 0);
        }

        private void DeseneazaLinie(Bitmap bmp, Point a, Point b, Color color) {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float p = 2 * dy - dx;

            bmp.SetPixel(a.X, a.Y, color);
            float x = a.X, y = a.Y;
            while (x < b.X) {
                if (p < 0)
                {
                    x += 1;
                    p += 2 * dy;
                }
                else {
                    x += 1;
                    y += 1;
                    p += 2 * dy - 2 * dx;
                }
                bmp.SetPixel((int)x, (int)y, color);
            }
        }

        private void Lab1_Load(object sender, EventArgs e)
        {

        }
    }
}
