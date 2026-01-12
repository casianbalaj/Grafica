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
    public partial class Lab2 : Form
    {
        Random rnd = new Random();
        Graphics graphics;
        Bitmap bitmap;
        bool[] state = new bool[3];

        public Lab2()
        {
            InitializeComponent();
            this.Text = "Semafor Lab 2";
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
        }

        void drawCirclePolar(Graphics g, float x0, float y0, float r, Brush b)
        {
            float step = 0.01f;
            float angle = 0f;

            List<PointF> points = new();

            while (angle <= 2 * Math.PI)
            {
                float x = x0 + r * (float)Math.Cos(angle);
                float y = y0 + r * (float)Math.Sin(angle);
                //g.DrawRectangle(p, x, y, 1, 1);
                points.Add(new PointF(x, y));
                angle += step;
            }
            g.FillPolygon(b, points.ToArray());
        }

        void drawStopLight(Graphics g, float x0, float y0, float w, float h, Brush b)
        {
            g.FillRectangle(b, new RectangleF(x0, y0, w, h));
        }

        private void Lab2_Load(object sender, EventArgs e)
        {
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            timer1.Tick += new EventHandler(TimerEventProcessor);
            timer1.Interval = 400;
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float marginX = 100, marginY = 70, stopLightHeight = 200, radius = 45;
            Color[] colors = [Color.Plum, Color.Peru, Color.PapayaWhip, Color.PowderBlue, Color.PeachPuff];
            Color c = colors[rnd.Next(colors.Length - 1)];
            Brush b = new SolidBrush(Color.Gray);
            drawStopLight(graphics, pictureBox1.Width / 2 - marginX, marginY, stopLightHeight, 400, b);
            drawCirclePolar(graphics, pictureBox1.Width / 2, marginY + stopLightHeight / 3 + 20, radius, new SolidBrush(c));
            c = colors[rnd.Next(colors.Length - 1)];
            drawCirclePolar(graphics, pictureBox1.Width / 2, marginY + 2 * stopLightHeight / 3 + 20 + radius, radius, new SolidBrush(c));
            c = colors[rnd.Next(colors.Length - 1)];
            drawCirclePolar(graphics, pictureBox1.Width / 2, marginY + stopLightHeight + 20 + 2 * radius, radius, new SolidBrush(c));
            g.DrawImage(bitmap, new Point(0, 0));


        }

        private void TimerEventProcessor(Object o, EventArgs e)
        {
            timer1.Stop();
            pictureBox1.Invalidate();
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
