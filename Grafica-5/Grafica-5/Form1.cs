namespace Grafica_2
{
    public partial class Form1 : Form
    {
        private Image? sourceImage;
        private Rectangle clippingWindow;
        private Point startPoint;
        private bool isDrawing = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Lab 5 - Vector Clipping from Image";
            this.Width = 1000;
            this.Height = 700;
            clippingWindow = new Rectangle(100, 100, 300, 200);
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isDrawing = true;
            }
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDrawing)
            {
                isDrawing = false;
                this.Invalidate();
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            g.DrawRectangle(Pens.Blue, clippingWindow);
            g.DrawString("Clipping Window", this.Font, Brushes.Blue, clippingWindow.X, clippingWindow.Y - 20);

            if (sourceImage != null)
            {
                g.DrawImage(sourceImage, 500, 100, 400, 400);
                
                Rectangle imageClipArea = new Rectangle(150, 50, 200, 150);
                var clippedResult = ClipImageRegion(sourceImage, imageClipArea);
                if (clippedResult != null)
                {
                    g.DrawImage(clippedResult, 500, 520);
                    g.DrawString("Clipped Region", this.Font, Brushes.Green, 500, 500);
                }
            }

            if (isDrawing && startPoint != Point.Empty)
            {
                Point currentPoint = this.PointToClient(Cursor.Position);
                Pen originalPen = new Pen(Color.Gray, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                g.DrawLine(originalPen, startPoint, currentPoint);

                var clippedLine = CohenSutherlandClip(startPoint, currentPoint, clippingWindow);
                if (clippedLine.HasValue)
                {
                    g.DrawLine(new Pen(Color.Red, 3), clippedLine.Value.Item1, clippedLine.Value.Item2);
                }
            }

            g.DrawString("Lab 5: Vector Clipping - Draw lines with mouse", this.Font, Brushes.Black, 10, 10);
            g.DrawString("Left click and drag to draw lines", this.Font, Brushes.DarkGray, 10, 30);
            g.DrawString("Red = Clipped line | Gray = Original line", this.Font, Brushes.DarkGray, 10, 50);
        }

        private enum OutCode
        {
            INSIDE = 0,
            LEFT = 1,
            RIGHT = 2,
            BOTTOM = 4,
            TOP = 8
        }

        private OutCode ComputeOutCode(Point point, Rectangle rect)
        {
            OutCode code = OutCode.INSIDE;

            if (point.X < rect.Left)
                code |= OutCode.LEFT;
            else if (point.X > rect.Right)
                code |= OutCode.RIGHT;

            if (point.Y < rect.Top)
                code |= OutCode.TOP;
            else if (point.Y > rect.Bottom)
                code |= OutCode.BOTTOM;

            return code;
        }

        private (Point, Point)? CohenSutherlandClip(Point p1, Point p2, Rectangle rect)
        {
            OutCode outcode1 = ComputeOutCode(p1, rect);
            OutCode outcode2 = ComputeOutCode(p2, rect);
            bool accept = false;

            float x1 = p1.X, y1 = p1.Y;
            float x2 = p2.X, y2 = p2.Y;

            while (true)
            {
                if ((outcode1 | outcode2) == OutCode.INSIDE)
                {
                    accept = true;
                    break;
                }
                else if ((outcode1 & outcode2) != OutCode.INSIDE)
                {
                    break;
                }
                else
                {
                    float x = 0, y = 0;
                    OutCode outcodeOut = outcode1 != OutCode.INSIDE ? outcode1 : outcode2;

                    if ((outcodeOut & OutCode.TOP) != OutCode.INSIDE)
                    {
                        x = x1 + (x2 - x1) * (rect.Top - y1) / (y2 - y1);
                        y = rect.Top;
                    }
                    else if ((outcodeOut & OutCode.BOTTOM) != OutCode.INSIDE)
                    {
                        x = x1 + (x2 - x1) * (rect.Bottom - y1) / (y2 - y1);
                        y = rect.Bottom;
                    }
                    else if ((outcodeOut & OutCode.RIGHT) != OutCode.INSIDE)
                    {
                        y = y1 + (y2 - y1) * (rect.Right - x1) / (x2 - x1);
                        x = rect.Right;
                    }
                    else if ((outcodeOut & OutCode.LEFT) != OutCode.INSIDE)
                    {
                        y = y1 + (y2 - y1) * (rect.Left - x1) / (x2 - x1);
                        x = rect.Left;
                    }

                    if (outcodeOut == outcode1)
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutCode(new Point((int)x1, (int)y1), rect);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        outcode2 = ComputeOutCode(new Point((int)x2, (int)y2), rect);
                    }
                }
            }

            if (accept)
                return (new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
            else
                return null;
        }

        private Image? ClipImageRegion(Image source, Rectangle clipRect)
        {
            try
            {
                Bitmap srcBitmap = new Bitmap(source);
                Bitmap clipped = new Bitmap(clipRect.Width, clipRect.Height);

                for (int x = 0; x < clipRect.Width; x++)
                {
                    for (int y = 0; y < clipRect.Height; y++)
                    {
                        int srcX = clipRect.X + x;
                        int srcY = clipRect.Y + y;

                        if (srcX >= 0 && srcX < srcBitmap.Width && srcY >= 0 && srcY < srcBitmap.Height)
                        {
                            clipped.SetPixel(x, y, srcBitmap.GetPixel(srcX, srcY));
                        }
                    }
                }
                return clipped;
            }
            catch
            {
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string imagePath = "../../../test_image.png";
                if (File.Exists(imagePath))
                {
                    sourceImage = Image.FromFile(imagePath);
                }
            }
            catch
            {
            }
        }
    }
}
