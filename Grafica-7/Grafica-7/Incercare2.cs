using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabSapt7Grafica.Utility2;

namespace Grafica_4
{
    public partial class Incercare2 : Form
    {
        //caz degenerat: e doar o linie
        public void DrawDegree1Bezier(Pen p, PointF p1, PointF p2, Graphics g, int resolution=100) {
            List<PointF> points = [];
            float t = 0, dt = 1.0f/resolution;
            for (int _ = 0; _ < resolution; _++) {
                points.Add(Utility2.lerp(p1,p2,t));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++) {
                g.DrawLine(p, points[i], points[i+1]);
            }
        }

        public void DrawDegree2Bezier(Pen p, PointF p1, PointF p2, PointF p3, Graphics g, int resolution=100) {
            List<PointF> points = [];
            float t = 0, dt = 1.0f/resolution;
            for (int _ = 0; _ < resolution; _++) {
                PointF a = Utility2.lerp(p1, p2, t);
                PointF b = Utility2.lerp(p2, p3, t);
                points.Add(Utility2.lerp(a, b, t));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++) {
                g.DrawLine(p, points[i], points[i+1]);
            }
        }

        public void DrawDegree3Bezier(Pen p, PointF p1, PointF p2, PointF p3, PointF p4, Graphics g, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ < resolution; _++)
            {
                PointF l_p1p2 = Utility2.lerp(p1, p2, t);
                PointF l_p2p3 = Utility2.lerp(p2, p3, t);
                PointF l_p3p4 = Utility2.lerp(p3, p4, t);
                PointF l_p1p2_p2p3 = Utility2.lerp(l_p1p2, l_p2p3, t);
                PointF l_p2p3_p3p4 = Utility2.lerp(l_p2p3, l_p3p4, t);
                //PointF l_final = Utility2.lerp(l_p1p2_p2p3, l_p2p3_p3p4, t);
                points.Add(Utility2.lerp(l_p1p2_p2p3, l_p2p3_p3p4, t));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(Pens.Black, points[i], points[i + 1]);
            }
        }

        public List<PointF> __underlying_bezier_lerp(List<PointF> points, float t) {
            if (points.Count == 2) return [Utility2.lerp(points[0], points[1], t)];
            else {
                List<PointF> res = [];
                for (int i = 0; i < points.Count-1; i++) {
                    PointF p_i = Utility2.lerp(points[i], points[i + 1], t);
                    res.Add(p_i);
                }
                return __underlying_bezier_lerp(res, t);
            }
        }

        public void DrawDegreeNBezier(Pen p, List<PointF> controlPoints, Graphics g, int resolution = 100) {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ < resolution; _++) {
                points.Add(__underlying_bezier_lerp(controlPoints, t)[0]);
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i], points[i + 1]);
            }
        }

        public Incercare2()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 800;
            this.Name = "Hello World!";
        }

        List<PointF> points; 

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            #region Gradul 1
            points = [new PointF(186, 508), new PointF(617, 598)];
            DrawDegree1Bezier(Pens.Black, points[0], points[1], g);
            #endregion
            #region Gradul 2
            points = [new PointF(186, 508), new PointF(447, 350), new PointF(617, 598)];
            DrawDegree2Bezier(Pens.Blue, points[0], points[1], points[2], g);
            #endregion
            #region Gradul 3
            points = [new PointF(186, 508), new PointF(447, 350), new PointF(517, 370), new PointF(617, 598)];
            DrawDegree3Bezier(Pens.Green, points[0], points[1], points[2], points[3], g);
            #endregion
            #region Gradul 4
            points = [new PointF(186, 508), new PointF(447, 350), new PointF(517, 370), new PointF(522, 512), new PointF(617, 598)];
            DrawDegreeNBezier(Pens.Red, points, g);
            #endregion
        }

        private void Incercare2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
