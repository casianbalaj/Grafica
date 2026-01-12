using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica_4
{
    public static class Utility2
    {
        public static float lerp(float a, float b, float t /* t e din [0,1] */) {
            if (!(t is >= 0 and <= 1)) throw new ArgumentException("t trebuie sa fie din [0,1]");
            return (1 - t) * a + t * b;
        }

        public static PointF lerp(PointF p1, PointF p2, float t) { 
            return new PointF(lerp(p1.X, p2.X, t), lerp(p1.Y, p2.Y, t));
        }
    }
}
