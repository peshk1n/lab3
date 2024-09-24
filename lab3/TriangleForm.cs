using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

namespace lab3
{
    public partial class TriangleForm : Form
    {
        private Point[] trianglePoints = new Point[3];
        private Color[] triangleColors = new Color[3];
        private int pointCount = 0;

        public TriangleForm()
        {
            InitializeComponent();

            this.Text = "Gradient Triangle Rasterization";
            this.ClientSize = new Size(600, 600);
            this.BackColor = Color.White;
            this.MouseClick += OnMouseClick;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (pointCount < 3)
            {
                trianglePoints[pointCount] = e.Location;
                ColorDialog colorDialog = new ColorDialog();

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    triangleColors[pointCount] = colorDialog.Color;
                    pointCount++;
                    this.Invalidate();
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < pointCount; i++)
            {
                DrawPoint(e.Graphics, trianglePoints[i], triangleColors[i]);
            }

            if (pointCount == 3)
            {
                RasterizeTriangle(e.Graphics);
            }
        }


        private void DrawPoint(Graphics g, Point point, Color color)
        {
            int pointRadius = 5;
            Rectangle rect = new Rectangle(point.X - pointRadius, point.Y - pointRadius, pointRadius * 2, pointRadius * 2);
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, rect);
        }


        public class PointComparer : IComparer<Point>
        {
            public int Compare(Point p1, Point p2)
            {
                return p1.Y.CompareTo(p2.Y);
            }
        }


        private void RasterizeTriangle(Graphics g)
        {
            Array.Sort(trianglePoints, triangleColors, new PointComparer());

            int minY = trianglePoints[0].Y;
            int maxY = trianglePoints[2].Y;

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            for (int y = minY; y <= maxY; y++)
            {
                var xLeft = int.MaxValue;
                var xRight = int.MinValue;
                var cLeft = Color.Empty;
                var cRight = Color.Empty;

                for (int i = 0; i < 3; i++)
                {
                    var p1 = trianglePoints[i];
                    var p2 = trianglePoints[(i + 1) % 3];

                    if ((p1.Y <= y && p2.Y > y) || (p2.Y <= y && p1.Y > y))
                    {
                        float t = (float)(y - p1.Y) / (p2.Y - p1.Y);
                        int x = (int)(p1.X + t * (p2.X - p1.X));

                        Color interpolatedColor = InterpolateColor(triangleColors[i], triangleColors[(i + 1) % 3], t);

                        if (x < xLeft)
                        {
                            xLeft = x;
                            cLeft = interpolatedColor;
                        }
                        if (x > xRight)
                        {
                            xRight = x;
                            cRight = interpolatedColor;
                        }
                    }
                }

                if (xLeft < xRight)
                {
                    for (int x = xLeft; x <= xRight; x++)
                    {
                        float t = (float)(x - xLeft) / (xRight - xLeft);
                        Color color = InterpolateColor(cLeft, cRight, t);
                        bmp.SetPixel(x, y, color);
                    }
                }
            }

            g.DrawImage(bmp, 0, 0);
        }


        private Color InterpolateColor(Color c1, Color c2, float t)
        {
            int r = (int)(c1.R + t * (c2.R - c1.R));
            int g = (int)(c1.G + t * (c2.G - c1.G));
            int b = (int)(c1.B + t * (c2.B - c1.B));
            return Color.FromArgb(r, g, b);
        }


        private void clearButton_Click(object sender, EventArgs e)
        {
            pointCount = 0;
            this.Invalidate();
        }
    }
}
