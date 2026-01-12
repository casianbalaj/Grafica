using System.Diagnostics;

namespace Grafica_1234
{
    public partial class Lab4 : Form
    {
        //private List<Point> points = new List<Point> { 
        //    new(100, 100),
        //    new(200, 80),
        //    new(250, 150),
        //    new(180, 200),
        //    new(120, 180)
        //};

        private List<Point> points = new List<Point> {
            new(100, -41),
            new(-94, -27),
            new(126, 111),
            new(-18, 135)
        };


        public Lab4()
        {
            InitializeComponent();
            this.Text = "Scanline Simplu";
            this.Width = 800;
            this.Height = 800;
            this.Paint += DeseneazaPoligonUmplut;
        }

        private void DeseneazaPoligonUmplut(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Brush b = Brushes.LightSkyBlue;

            int yMin = int.MaxValue, yMax = int.MinValue;
            foreach (Point point in points) {
                yMin = Math.Min(yMin, point.Y);
                yMax = Math.Max(yMax, point.Y);
            }

            for (int y = yMin; y <= yMax; y++) { 
                List<int> intersectii = new List<int>();

                for (int i = 0; i < points.Count; i++) { 
                    Point p1 = points[i];
                    Point p2 = points[(i + 1) % points.Count];

                    if ((y >= p1.Y && y < p2.Y) || (y >= p2.Y && y < p1.Y)) {
                        float x = p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        intersectii.Add((int)x);
                    }
                }
                intersectii.Sort();
                for (int i = 0; i < intersectii.Count - 1; i += 2) {
                    g.DrawLine(Pens.Blue, intersectii[i], y, intersectii[i + 1], y);
                }

                g.DrawPolygon(Pens.Black, points.ToArray());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
