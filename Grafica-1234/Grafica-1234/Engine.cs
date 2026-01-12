using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grafica_1234;

namespace Grafica_1234
{
    public class Engine
    {
        public static Matrice PolygonToMatrix(Polygon p) => new(p.Transform());
        public static Polygon MatrixToPolygon(Matrice m) => new(m.Transform());

        public static Polygon ScaleTransform(Polygon polygon, float by)
        {
            Matrice P = Engine.PolygonToMatrix(polygon);
            return Engine.MatrixToPolygon(by * P);
        }

        public static Polygon TranslationTransform(Polygon polygon, PointF by)
        {
            Matrice P = Engine.PolygonToMatrix(polygon);
            Matrice C = Engine.PointToMatrix(by, polygon.N);
            return Engine.MatrixToPolygon(P + C);
        }

        public static Polygon RotationTransform(Polygon polygon, PointF point, float angle)
        {
            Matrice P = Engine.PolygonToMatrix(polygon);
            Matrice C = Engine.PointToMatrix(point, polygon.N);
            Matrice R = Matrice.RotationMatrix(angle);
            Matrice Pprim = R * (P - C) + C;
            return Engine.MatrixToPolygon(Pprim);
        }

        public static Polygon RotationTransform(Polygon polygon, float angle)
        {
            Matrice P = Engine.PolygonToMatrix(polygon);
            Matrice R = Matrice.RotationMatrix(angle);
            Matrice Pprim = R * P;
            return Engine.MatrixToPolygon(Pprim);
        }

        public static Matrice PointToMatrix(PointF p, int n)
        {
            float[,] values = new float[2, n];
            for (int i = 0; i < n; i++)
            {
                values[0, i] = p.X;
                values[1, i] = p.Y;
            }
            return new Matrice(values);
        }

        public static Polygon ShearTransform(Polygon polygon, float angle) {
            List<PointF> p7 = new List<PointF>();
            foreach (PointF p in polygon.points) {
                p7.Add(new(p.X+p.Y*(float)Math.Tan(angle), p.Y));
            }
            return new Polygon(p7.ToArray());
        }
    }
}
