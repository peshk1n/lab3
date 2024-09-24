using System;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Windows.Forms;

namespace lab3
{
    public partial class Task1 : Form
    {
        private bool isDrawing = false;
        private bool canDraw = false;
        private bool isFillColor = false;
        private bool isFindBorder = false;
        private bool isFillImage = false; 
        private PointF startPoint;
        private Bitmap drawingBitmap;
        private Color fillColor = Color.Black;
        private Bitmap fillImage; 
        private PointF initialClickPoint;
        private Point rightpoint;

        private const int MaxDepth = 2500; 
        private int currentDepth = 0;      

        public Task1()
        {
            InitializeComponent();
            drawingBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = drawingBitmap;
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Invalidate();
        }

        private void Task1_Load(object sender, EventArgs e)
        {

        }

        private void clean_area_Click(object sender, EventArgs e)
        {
            canDraw = false;
            isFillColor = false;
            isFillImage = false;
            isFindBorder = false;
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Invalidate();
        }

        private void draw_border_Click(object sender, EventArgs e)
        {
            canDraw = true;
            isFillColor = false;
            isFillImage = false;
            isFillColor = false;
            isFindBorder = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                isDrawing = true;
                startPoint = e.Location; 
            }
            if (isFindBorder)
            {
                rightpoint = e.Location;
                FindBorder();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDraw && isDrawing)
            {
                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    Pen pen = new Pen(Color.Black, 1);

                    float dx = e.Location.X - startPoint.X;
                    float dy = e.Location.Y - startPoint.Y;
                    int steps = (int)Math.Max(Math.Abs(e.Location.X - startPoint.X), 
                        Math.Abs(e.Location.Y - startPoint.Y));
                    float xInc = dx / steps;
                    float yInc = dy / steps;

                    float x = startPoint.X;
                    float y = startPoint.Y;

                    for (int i = 0; i <= steps; i++)
                    {
                        g.DrawRectangle(pen, (int)Math.Round(x), (int)Math.Round(y), 1, 1);
                        x += xInc;
                        y += yInc;
                    }
                }

                pictureBox1.Invalidate();
                startPoint = e.Location;
            }
        }



        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (canDraw) isDrawing = false;
        }

        private void fill_color_Click(object sender, EventArgs e)
        {
            canDraw = false;
            isFillColor = true;
            isFillImage = false;
            isFindBorder = false;

            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    fillColor = colorDialog.Color;
                }
            }
        }

        private void fill_picture_Click(object sender, EventArgs e)
        {
            canDraw = false;
            isFillColor = false;
            isFillImage = true; 
            isFindBorder = false;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fillImage = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFillColor)
            {
                Color color = drawingBitmap.GetPixel(e.X, e.Y);
                FillAreaWithColor(new PointF(e.X, e.Y), color, fillColor);
            }
            else if (isFillImage && fillImage != null)
            {
                initialClickPoint=new Point(e.X, e.Y);
                Color color = drawingBitmap.GetPixel(e.X, e.Y);
                FillAreaWithImage(new PointF(e.X, e.Y), color);
            }
        }

        private bool IsColorSimilar(Color color1, Color color2, int tolerance = 10)
        {
            return color1 == color2;
        }


        private void FillAreaWithImage(PointF point, Color startColor)
        {
            if (currentDepth > MaxDepth)
                return;

            currentDepth++;

            if (!IsWithinBounds(point))
            {
                currentDepth--;
                return;
            }
            Color originalColor = drawingBitmap.GetPixel((int)point.X, (int)point.Y);

            if (!IsColorSimilar(originalColor, startColor))
            {
                currentDepth--;
                return;
            }

            var lr = FindBorders(point, startColor);
            Point left = lr.Item1;
            Point right = lr.Item2;

            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                for (int i = left.X; i <= right.X; i++)
                {
                    int offsetX = (i - (int)initialClickPoint.X);
                    int offsetY = ((int)point.Y - (int)initialClickPoint.Y);

                    int imgX = (offsetX % fillImage.Width + fillImage.Width) % fillImage.Width;
                    int imgY = (offsetY % fillImage.Height + fillImage.Height) % fillImage.Height;


                    Color imageColor = fillImage.GetPixel(imgX, imgY);

                    g.FillRectangle(new SolidBrush(imageColor), i, (int)point.Y, 1, 1);
                }
            }


            pictureBox1.Invalidate();

            for (int i = left.X; i <= right.X; i++)
            {
                FillAreaWithImage(new PointF(i, point.Y + 1), startColor);
                FillAreaWithImage(new PointF(i, point.Y - 1), startColor);
            }

            currentDepth--;
        }


        private void FillAreaWithColor(PointF point, Color startColor, Color fillColor)
        {
            if (currentDepth > MaxDepth)
                return;

            currentDepth++;

            if (!IsWithinBounds(point))
            {
                currentDepth--;
                return;
            }

            Color originalColor = drawingBitmap.GetPixel((int)point.X, (int)point.Y);

            if (!IsColorSimilar(originalColor, startColor))
            {
                currentDepth--;
                return;
            }

            var lr = FindBorders(point, startColor);
            Point left = lr.Item1;
            Point right = lr.Item2;

            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.DrawLine(new Pen(fillColor, 1), left, right);
                for (int i = left.X; i <= right.X; i++)
                {
                    drawingBitmap.SetPixel(i, (int)point.Y, fillColor);
                }
            }

            pictureBox1.Invalidate();

            for (int i = left.X; i <= right.X; i++)
            {
                FillAreaWithColor(new PointF(i, point.Y + 1), startColor, fillColor);
                FillAreaWithColor(new PointF(i, point.Y - 1), startColor, fillColor);
            }

            currentDepth--;
        }
        private Tuple<Point, Point> FindBorders(PointF point, Color targetColor)
        {
            int x = (int)point.X;
            int y = (int)point.Y;
            int width = drawingBitmap.Width;

            int right = x;
            while (right < width && IsColorSimilar(drawingBitmap.GetPixel(right, y), targetColor))
            {
                right++;
            }

            int left = x;
            while (left >= 0 && IsColorSimilar(drawingBitmap.GetPixel(left, y), targetColor))
            {
                left--;
            }
            
            return Tuple.Create(new Point(left + 1, y), new Point(right - 1, y));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void find_border_Click(object sender, EventArgs e)
        {
            isFindBorder = true;
            canDraw = false;
            isFillColor = false;
            isFillImage = false;
        }

        private void FindBorder()
        {
            var point = FindBorders(rightpoint, drawingBitmap.GetPixel(rightpoint.X, rightpoint.Y)).Item2;
            int x = point.X+1;
            int y = point.Y;
            Point startPoint = new Point((int)x, (int)y);

            Color boundaryColor = drawingBitmap.GetPixel(startPoint.X, startPoint.Y);

            List<PointF> borderPoints = FindBorderPoints(startPoint, boundaryColor);

            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                Pen pen = new Pen(Color.Red, 1); 
                g.DrawLines(pen, borderPoints.ToArray());
            }

            pictureBox1.Invalidate();
        }

        private List<PointF> FindBorderPoints(Point startPoint, Color boundaryColor)
        {
            List<PointF> boundaryPoints = new List<PointF>();
            Point currentPoint = startPoint;
            int currentDirection = 6; 

            Point[] directions = new Point[]
            {
                new Point(1, 0),   // 0 - вправо
                new Point(1, -1),  // 1 - вправо-вверх
                new Point(0, -1),  // 2 - вверх
                new Point(-1, -1), // 3 - влево-вверх
                new Point(-1, 0),  // 4 - влево
                new Point(-1, 1),  // 5 - влево-вниз
                new Point(0, 1),   // 6 - вниз
                new Point(1, 1)    // 7 - вправо-вниз
            };

            boundaryPoints.Add(new Point(
                        currentPoint.X - 1,
                        currentPoint.Y 
                    ));

            do
            {
                bool foundNext = false;
                int nextDirection = (currentDirection + 6) % 8;  

                for (int i = 0; i < 8; i++)
                {
                    int testDirection = (nextDirection + i) % 8;
                    Point nextPoint = new Point(
                        currentPoint.X + directions[testDirection].X,
                        currentPoint.Y + directions[testDirection].Y
                    );

                    if (IsWithinBounds(nextPoint) && IsColorSimilar(drawingBitmap.GetPixel(nextPoint.X, nextPoint.Y), boundaryColor))
                    {
                        Point addpoint = new Point(
                        currentPoint.X + directions[(testDirection+7)%8].X,
                        currentPoint.Y + directions[(testDirection+7)%8].Y
                    );
                        currentPoint = nextPoint;
                        boundaryPoints.Add(addpoint);


                        currentDirection = testDirection;
                        foundNext = true;
                        break;
                    }
                }

                if (!foundNext)
                {
                    break;
                }

            } while (!currentPoint.Equals(startPoint)); 

            return boundaryPoints;
        }

        private bool IsWithinBounds(PointF p)
        {
            return p.X >= 0 && p.X < drawingBitmap.Width && p.Y >= 0 && p.Y < drawingBitmap.Height;
        }

    }
}
