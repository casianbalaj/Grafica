using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica_1234
{
    public class Matrice
    {
        private float[,] values;

        public float this[int i, int j]
        {
            get => values[i, j];
            set => values[i, j] = value;
        }
        public int N { 
            get => values.GetLength(0);
        }

        public int M {
            get => values.GetLength(1);
        }

        public Matrice(float[,] values)
        {
            this.values = values;
        }

        public Matrice(int N, int M)
        {
            this.values = new float[N, M];
        }

        public static Matrice operator +(Matrice a, Matrice b)
        {
            if (a.M != b.M || a.N != b.N) return null;
            Matrice res = new Matrice(a.N, a.M);
            for (int i = 0; i < a.N; i++)
            {
                for (int j = 0; j < b.M; j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }
            return res;
        }

        public static Matrice operator *(Matrice a, Matrice b)
        {

            if (a.M != b.N)
            {
                return null;
            }
            Matrice res = new Matrice(a.N, b.M);
            for (int i = 0; i < a.N; i++)
            {

                for (int j = 0; j < b.M; j++)
                {
                    res[i, j] = 0;


                    for (int k = 0; k < a.M; k++)
                    {
                        res[i, j] += a.values[i, k] * b.values[k, j];
                    }
                }
            }
            return res;
        }

        public static Matrice operator -(Matrice a, Matrice b)
        {
            return a + (-1 * b);
        }

        public static Matrice operator *(float a, Matrice b)
        {
            Matrice res = new Matrice(b.N, b.M);
            for (int i = 0; i < b.N; i++)
            {
                for (int j = 0; j < b.M; j++)
                {
                    res[i, j] = a * b[i, j];
                }
            }
            return res;
        }

        public static Matrice operator *(Matrice b, float a)
        {
            Matrice res = new Matrice(b.N, b.M);
            for (int i = 0; i < b.N; i++)
            {
                for (int j = 0; j < b.M; j++)
                {
                    res[i, j] = a * b[i, j];
                }
            }
            return res;
        }

        public PointF[] Transform()
        {
            if (this.N != 2) return null;
            PointF[] res = new PointF[this.M];
            for (int i = 0; i < this.M; i++)
            {
                res[i].X = this.values[0, i];
                res[i].Y = this.values[1, i];
            }
            return res;
        }

        public static Matrice RotationMatrix(float angle)
        {
            Matrice m = new Matrice(2, 2);
            m[0, 0] = (float)Math.Cos(angle);
            m[0, 1] = -(float)Math.Sin(angle);
            m[1, 0] = (float)Math.Sin(angle);
            m[1, 1] = (float)Math.Cos(angle);
            return m;
        }
    }
}
