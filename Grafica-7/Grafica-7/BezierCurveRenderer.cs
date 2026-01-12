using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica_4
{
    public static class BezierCurveRenderer
    {
        public static void DrawBezier(List<PointF> controlPoints, Graphics g, int resolution = 10) {
            List<PointF> result = new List<PointF>();
            List<double> vals = new List<double>(); 
            List<BernsteinPolynomial> berns = new List<BernsteinPolynomial>();
            double du = 1.0 / resolution;
            double u = 0;
            int n = controlPoints.Count - 1;
            for (int k=0; k<=resolution; k++) {
                PointF sum_p = new PointF(0,0);
                for (int i=0; i<n; i++)
                {
                    var b = new BernsteinPolynomial(i, n);
                    berns.Add(b);
                    float val = (float)b.Eval(u);
                    vals.Add(val);
                    sum_p.X += controlPoints[i].X * val;
                    sum_p.Y += controlPoints[i].Y * val;
                    result.Add(sum_p);
                    u += du;
                }
            }
            for (int i = 0; i < result.Count - 1; i++)
            {
                g.DrawLine(Pens.Black, result[i], result[i + 1]);
            }
        }
    }
}
