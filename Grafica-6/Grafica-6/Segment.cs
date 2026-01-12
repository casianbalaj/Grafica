using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica_3
{
    public class Segment(Point p1, Point p2)
    {
        private PointF p1 = p1, p2 = p2;


        public float X1
        {
            get { return p1.X; }
            set { p1.X = value; }
        }

        public float X2
        {
            get { return p2.X; }
            set { p2.X = value; }
        }

        public float Y1
        {
            get { return p1.Y; }
            set { p1.Y = value; }
        }

        public float Y2
        {
            get { return p2.Y; }
            set { p2.Y = value; }
        }
    }
}
