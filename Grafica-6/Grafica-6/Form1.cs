namespace Grafica_3
{
    public partial class Form1 : Form
    {
        public enum Caz
        {
            INTERIOR = 0b0000,
            STANGA = 0b0001,
            DREAPTA = 0b0010,
            JOS = 0b0100,
            SUS = 0b1000
        }

        float xmin = 100, ymin = 100, xmax = 300, ymax = 200;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Clipping - Cohen-Sutherland";
            this.Width = 800;
            this.Height = 800;
            this.Paint += Desenare;
        }

        Caz CodRegiune(float x, float y) {
            Caz c = Caz.INTERIOR;
            if (x < xmin) c |= Caz.STANGA;
            else if (x > xmax) c |= Caz.DREAPTA;

            if (y < ymin) c |= Caz.JOS;
            else if (y > ymax) c |= Caz.SUS;
            return c;
        }

        void Clipping(Graphics graphics, Segment s, Pen normal) { 
            Caz c1 = CodRegiune(s.X1, s.Y1), c2 = CodRegiune(s.X2, s.Y2);
            bool acceptata = false;

            while (true)
            {
                if ((c1 | c2) == 0)
                {
                    acceptata = true;
                    break;
                } else if ((c1 & c2) != 0)
                {
                    break;
                } else
                {
                    Caz cExt = (c1 != 0) ? c1 : c2;
                    float x = 0, y = 0;

                    if ((cExt & Caz.SUS) != 0)
                    {
                        x = s.X1 + (s.X2 - s.X1) * (ymax - s.Y1) / (s.Y2 - s.Y1);
                        y = ymax;
                    }
                    else if ((cExt & Caz.JOS) != 0)
                    {
                        x = s.X1 + (s.X2 - s.X1) * (ymin - s.Y1) / (s.Y2 - s.Y1);
                        y = ymin;
                    }
                    else if ((cExt & Caz.DREAPTA) != 0)
                    {
                        y = s.Y1 + (s.Y2 - s.Y1) * (xmax - s.X1) / (s.X2 - s.X1);
                        x = xmax;
                    }
                    else if ((cExt & Caz.STANGA) != 0)
                    {
                        y = s.Y1 + (s.Y2 - s.Y1) * (xmin - s.X1) / (s.X2 - s.X1);
                        x = xmin;
                    }

                    if (cExt == c1)
                    {
                        s.X1 = x;
                        s.Y1 = y;
                        c1 = CodRegiune(s.X1, s.Y1);
                    }
                    else
                    {
                        s.X2 = x;
                        s.Y2 = y;
                        c2 = CodRegiune(s.X2, s.Y2);
                    }
                }
            }
            if (acceptata)
            {
               graphics.DrawLine(normal, s.X1, s.Y1, s.X2, s.Y2);
            }
        }

        void Desenare(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 2);
            g.DrawRectangle(p, xmin, ymin, xmax - xmin, ymax - ymin);
            g.DrawLine(new Pen(Color.Green, 1), 150, 150, 600, 300);

            Clipping(g, new Segment(new(150, 150), new(600, 300)), new Pen(Color.Red, 3));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
