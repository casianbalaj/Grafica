namespace Grafica_4
{
    public partial class Form1 : Form
    {
        private PointF[] controlPoints = new PointF[]
        {
            new PointF(50, 300),
            new PointF(150, 50),
            new PointF(300, 400),
            new PointF(500, 200),
        };

        override protected void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            // Draw control points
            foreach (var point in controlPoints)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10);
            }
            // Draw Bezier curve
            //g.DrawBezier(Pens.Blue, controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3]);
            BezierCurveRenderer.DrawBezier(new List<PointF>(controlPoints), g);
        }

        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
