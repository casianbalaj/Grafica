namespace Grafica_1234
{
    public class Polygon
    {
        public PointF[] points;

        public int N
        {
            get { return points.Length; }
        }

        public Polygon() { }

        public Polygon(int n) { points = new PointF[n]; }

        public Polygon(PointF[] points) { this.points = points; }

        public Polygon(string fileName)
        {
            List<string> data = new List<string>();
            TextReader load = new StreamReader(fileName);
            string buffer;
            while ((buffer = load.ReadLine()) != null)
            {
                data.Add(buffer);
            }
            load.Close();
            points = new PointF[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                string[] split = data[i].Split([' '], StringSplitOptions.RemoveEmptyEntries);
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                points[i] = new PointF(x, y);
            }
        }



        public void Draw(Pen strokeCol, Brush fillCol, Graphics handler)
        {
            if (points.Length > 1) handler.DrawPolygon(strokeCol, points);
            foreach (PointF _p in points)
            {
                handler.FillEllipse(fillCol, _p.X - 5, _p.Y - 5, 11, 11);
                handler.DrawEllipse(strokeCol, _p.X - 5, _p.Y - 5, 11, 11);
            }
        }

        public float Area()
        {
            float res = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                res += points[i].X * points[i + 1].Y - points[i + 1].X * points[i].Y;
            }
            res += points[points.Length - 1].X * points[0].Y - points[0].X * points[points.Length - 1].Y;
            return Math.Abs(res / 2);
        }

        public Polygon VarignonProcess(float k1=1, float k2=1) // P<>S(P)
        {
            float x, y;
            Polygon p = new(this.points.Length);
            for (int i = 0; i < points.Length - 1; i++)
            {
                x = (k1 * this.points[i].X + k2 * this.points[i + 1].X) / (k1 + k2);
                y = (k1 * this.points[i].Y + k2 * this.points[i + 1].Y) / (k1 + k2);
                p.points[i] = new PointF(x, y);
            }
            x = (k1 * this.points[points.Length - 1].X + k2 * this.points[0].X) / (k1 + k2);
            y = (k1 * this.points[points.Length - 1].Y + k2 * this.points[0].Y) / (k1 + k2);
            p.points[points.Length - 1] = new PointF(x, y);
            return p;
        }

        public PointF CentruGreautate()
        {
            float x = 0, y = 0;
            foreach (PointF p in this.points)
            {
                x += p.X;
                y += p.Y;
            }
            return new PointF(x / points.Length, y / points.Length);
        }

        public float[,] Transform()
        {
            float[,] res = new float[2, this.N];
            for (int i = 0; i < this.N; i++)
            {
                res[0, i] = points[i].X;
                res[1, i] = points[i].Y;
            }
            return res;
        }

        //public
    }
}
