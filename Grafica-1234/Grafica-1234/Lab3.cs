using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica_1234 //light: sayori light
                         //dark: maple dark
{
    public partial class Lab3 : Form
    {
        private List<Point> points;
        private List<Point> points3;

        Polygon polygon;
        Polygon polygon2;

        public Lab3()
        {
            InitializeComponent();

            points = new List<Point> {
                new(100, -41),
                new(-94, -27),
                new(126, 111),
                new(-18, 135)
            };

            List<PointF> points2 = new List<PointF>();

            foreach (Point p in points) { 
                points2.Add(new PointF(p.X, p.Y));
            }

            polygon = new Polygon(points2.ToArray());

            this.Text = "Transformari in Plan";
            this.Width = 800;
            this.Height = 800;
            this.Paint += DeseneazaPoligonUmplut;
        }

        private void DeseneazaPoligonUmplut(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush b = Brushes.LightSkyBlue;

            int yMin = int.MaxValue, yMax = int.MinValue;
            foreach (Point point in points3)
            {
                yMin = Math.Min(yMin, (int) point.Y);
                yMax = Math.Max(yMax, (int) point.Y);
            }

            for (int y = yMin; y <= yMax; y++)
            {
                List<int> intersectii = new List<int>();

                for (int i = 0; i < points3.Count; i++)
                {
                    Point p1 = points3[i];
                    Point p2 = points3[(i + 1) % points3.Count];

                    if ((y >= p1.Y && y < p2.Y) || (y >= p2.Y && y < p1.Y))
                    {
                        float x = p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        intersectii.Add((int)x);
                    }
                }
                intersectii.Sort();
                for (int i = 0; i < intersectii.Count - 1; i += 2)
                {
                    g.DrawLine(Pens.Blue, intersectii[i], y, intersectii[i + 1], y);
                }

                g.DrawPolygon(Pens.Black, points3.ToArray());
                g.DrawPolygon(Pens.Green, polygon2.points);
            }
        }

        private void Lab3_Load(object sender, EventArgs e)
        {
            polygon = Engine.TranslationTransform(polygon, new(400f, 400f));
            polygon2 = Engine.ShearTransform(polygon, 0.7f);
            polygon2 = Engine.TranslationTransform(polygon2, new(-400f, 0f));
            points3 = new List<Point>();
            foreach (PointF p in polygon.points) {
                points3.Add(new Point((int)p.X, (int)p.Y));
            }
        }
    }
}
