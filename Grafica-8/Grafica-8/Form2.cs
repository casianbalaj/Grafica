using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica_5
{
    public partial class Form2 : Form
    {
        const int LATIME = 1080;
        const int INALTIME = 720;
        const int MAX_ITER = 500;

        double minRe = -2.4;
        double maxRe = 0.8;
        double minIm = -1.5;
        double maxIm = 1.5;


        public static Color FromOklch(float l, float c, float h)
        {
            
            float hRad = h * (MathF.PI / 180.0f);

            float L = l;
            float a = c * MathF.Cos(hRad);
            float b = c * MathF.Sin(hRad);

          
            float l_ = L + 0.3963377774f * a + 0.2158037573f * b;
            float m_ = L - 0.1055613458f * a - 0.0638541728f * b;
            float s_ = L - 0.0894841775f * a - 1.2914855480f * b;

      
            float l_lin = l_ * l_ * l_;
            float m_lin = m_ * m_ * m_;
            float s_lin = s_ * s_ * s_;

            float rLinear = +4.0767416621f * l_lin - 3.3077115913f * m_lin + 0.2309699292f * s_lin;
            float gLinear = -1.2684380046f * l_lin + 2.6097574011f * m_lin - 0.3413193965f * s_lin;
            float bLinear = -0.0041960863f * l_lin - 0.7034186147f * m_lin + 1.7076147010f * s_lin;

            byte r = GammaCorrect(rLinear);
            byte g = GammaCorrect(gLinear);
            byte bb = GammaCorrect(bLinear);

            return Color.FromArgb(255, r, g, bb);
        }

        private static byte GammaCorrect(float component)
        {
            
            float abs = Math.Abs(component);
            float corrected = abs <= 0.0031308f
                ? 12.92f * abs
                : 1.055f * MathF.Pow(abs, 1.0f / 2.4f) - 0.055f;

           
            int val = (int)(corrected * 255.0f);

            if (val < 0) return 0;
            if (val > 255) return 255;
            return (byte)val;
        }

        public float lerp(float minVal, float maxVal, float amt) { 
            return minVal + (maxVal - minVal) * amt;
        }
    

        public Form2()
        {
            InitializeComponent();
            this.Text = "Setul Mandelbrot Frumos";
            this.Size = new Size(LATIME, INALTIME);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            

            Bitmap bmp = new Bitmap(LATIME, INALTIME);

            for (int px = 0; px < LATIME; px++)
            {
                for (int py = 0; py < INALTIME; py++)
                {
                    double cr = minRe + px * (maxRe - minRe) / LATIME;
                    double ci = maxIm - py * (maxIm - minIm) / INALTIME;

                    double zr = 0;
                    double zi = 0;
                    int iter = 0;
                    double zr2 = 0;
                    double zi2 = 0;

                    while (zr2 + zi2 < 16 && iter < MAX_ITER)
                    {
                        zi = 2 * zr * zi + ci;
                        zr = zr2 - zi2 + cr;
                        zr2 = zr * zr;
                        zi2 = zi * zi;
                        iter++;
                    }

                    if (iter == MAX_ITER)
                    { 
                        bmp.SetPixel(px, py, Color.Black);
                    }
                    else
                    {
                        double log_zn = Math.Log(zr2 + zi2) / 2.0;
                        double nu = Math.Log(log_zn / Math.Log(2.0)) / Math.Log(2.0);
                        double smoothIter = iter + 1.0 - nu;
                        float L = 0.6f + 0.2f * MathF.Sin((float)smoothIter * 0.5f);
                        float C = 0.18f;
                        float H = (float)(smoothIter * 6.0 + 200.0) % 360.0f;
                        bmp.SetPixel(px, py, FromOklch(L, C, H));
                    }
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
