using System.Numerics;

namespace Grafica_5
{
    public partial class Form1 : Form
    {
        double xmin = -2.0d, xmax = 0.47d;
        double ymin = -1.12d, ymax = 1.12d; 
        double screenHeight, screenWidth;

        int maxIterations = 105;

        Graphics graphics;
        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            screenHeight = pictureBox1.Height;
            screenWidth = pictureBox1.Width;
            bitmap = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            graphics = Graphics.FromImage(bitmap);
        }

        public class PointD
        {
            public double X;
            public double Y;

            public PointD(double X, double Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public Complex MandelbrotIterate(Complex z, Complex c)
        {
            return z * z + c;
        }

        public PointD ComplexToPointD(Complex z) {
            double a = (-xmin + z.Real) * screenWidth  / (xmax - xmin);
            double b = (ymin + z.Imaginary) * screenHeight / (ymax - ymin);
            return new(a, b);
        }

        //public PointD ComplexToPointD(Complex z) {
        //    double dx = xmax - xmin;
        //    double dy = ymax - ymin;
        //    double ux = (double) (dx / screenWidth);
        //    double uy = (double) (dy / screenHeight);
        //    return new PointD(xmin + z.Real * ux, ymin - z.Imaginary * uy);
        //}

        private int clampX(int x) { 
            if (x < 0) return 0;
            if (x >= screenWidth) return (int)screenWidth - 1;
            return x;
        }

        private int clampY(int y)
        {
            if (y < 0) return 0;
            if (y >= screenHeight) return (int)screenHeight - 1;
            return y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (double a = xmin; a <= xmax; a += 0.002) {
                for (double b = ymin; b <= ymax; b += 0.002) {
                    Complex z = new Complex(a, b);
                    Complex c = new Complex(a, b);
                    int iteration = 0;
                    while (iteration <= maxIterations && z.Magnitude <= 2) {
                        z = MandelbrotIterate(z, c);
                        iteration++;
                    }

                    PointD aa = ComplexToPointD(z);
                    Color color = (iteration <= maxIterations) ? Color.Black : Color.FromArgb((int)iteration/maxIterations, (int)iteration / maxIterations, (int)iteration / maxIterations);
                    bitmap.SetPixel(clampX((int)aa.X), clampY((int)aa.Y), color);
                }
            }
            pictureBox1.Image = bitmap;
        }
    }
}
