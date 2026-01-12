namespace Grafica_9
{
    public partial class Form1 : Form
    {
        const int gridSize = 100;
        float[,] heightMap = new float[gridSize, gridSize];

        float lightX = 0.5f;
        float lightY = 0.5f;
        float lightZ = -1f;
        float kd = 1.0f;
        float IL = 1.0f;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ClientSize = new Size(600, 600);
            GenerateSurface();
        }

        void GenerateSurface()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    float dx = x - gridSize / 2f;
                    float dy = y - gridSize / 2f;
                    heightMap[x, y] = (float)(50 * Math.Sin(dx * 0.1f) * Math.Cos(dy * 0.1f));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawLambert(e.Graphics);
        }

        void DrawLambert(Graphics g)
        {
            int cell = Math.Min(ClientSize.Width, ClientSize.Height) / gridSize;

            for (int x = 1; x < gridSize - 1; x++)
            {
                for (int y = 1; y < gridSize - 1; y++)
                {
                    float xL = x - 1, yL = y, zL = heightMap[x - 1, y];
                    float xR = x + 1, yR = y, zR = heightMap[x + 1, y];
                    float xD = x, yD = y - 1, zD = heightMap[x, y - 1];
                    float xU = x, yU = y + 1, zU = heightMap[x, y + 1];

                    float dxX = xR - xL;
                    float dxY = yR - yL;
                    float dxZ = zR - zL;

                    float dyX = xU - xD;
                    float dyY = yU - yD;
                    float dyZ = zU - zD;

                    float nX = dxY * dyZ - dxZ * dyY;
                    float nY = dxZ * dyX - dxX * dyZ;
                    float nZ = dxX * dyY - dxY * dyX;

                    float length = (float)Math.Sqrt(nX * nX + nY * nY + nZ * nZ);
                    if (length != 0)
                    {
                        nX /= length;
                        nY /= length;
                        nZ /= length;
                    }

                    float ndotl = Math.Max(0f, nX * lightX + nY * lightY + nZ * lightZ);
                    float intensity = kd * IL * ndotl;

                    int shade = (int)(intensity * 255);
                    if (shade < 0) shade = 0;
                    if (shade > 255) shade = 255;

                    using (SolidBrush b = new SolidBrush(Color.FromArgb(shade, shade, shade)))
                    {
                        g.FillRectangle(b, x * cell, y * cell, cell, cell);
                    }
                }
            }
        }

        public static void CalculateTriangleNormal(
            float Ax, float Ay, float Az,
            float Bx, float By, float Bz,
            float Cx, float Cy, float Cz,
            out float nx, out float ny, out float nz)
        {
            float ux = Bx - Ax;
            float uy = By - Ay;
            float uz = Bz - Az;

            float vx = Cx - Ax;
            float vy = Cy - Ay;
            float vz = Cz - Az;

            nx = uy * vz - uz * vy;
            ny = uz * vx - ux * vz;
            nz = ux * vy - uy * vx;

            float length = (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
            if (length != 0)
            {
                nx /= length;
                ny /= length;
                nz /= length;
            }
        }
    }
}
