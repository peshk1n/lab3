using System.Drawing;

namespace lab4
{
    public partial class Form1 : Form
    {
        private List<List<PointF>> polygons = new List<List<PointF>>();
        private int currentPolygon = -1;
        private bool isSelectingPolygon = false;
        private PointF transformCenter;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.White;
            OpenTransformMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                transformCenter = e.Location;
                RedrawPolygons();
            }
            else
            {
                if (isSelectingPolygon)
                {
                    SelectPolygon(e.Location);
                    isSelectingPolygon = false;
                }
                else if (currentPolygon != -1)
                {
                    polygons[currentPolygon].Add(e.Location);
                    transformCenter = GetPolygonCenter(currentPolygon);
                    RedrawPolygons();
                    if(polygons[currentPolygon].Count >= 2)
                    {
                        int c = polygons[currentPolygon].Count;
                        DrawIntersection(polygons[currentPolygon][c - 2], polygons[currentPolygon][c - 1]);
                    }
                }
            }
        }

        // Выделение полигона  
        // ==========================================================================================
        private void selectButton_Click(object sender, EventArgs e)
        {
            isSelectingPolygon = true;
        }


        private void SelectPolygon(Point clickPoint)
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                if (IsPointInPolygon(clickPoint, polygons[i]))
                {
                    currentPolygon = i;
                    transformCenter = GetPolygonCenter(i);
                    RedrawPolygons();
                    return;
                }
            }
        }

        private bool IsPointInPolygon(PointF point, List<PointF> polygon)
        {
            int i, j = polygon.Count - 1;
            bool inside = false;

            for (i = 0; i < polygon.Count; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        // Создание полигонов через клики мышью 
        // ==========================================================================================
        private void RedrawPolygons()
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                var pen = Pens.Black;
                var brush = Brushes.Black;

                for (int i = 0; i < polygons.Count; i++)
                {
                    if (i == currentPolygon)
                    {
                        pen = Pens.Red;
                        brush = Brushes.Red;
                    }
                    else
                    {
                        pen = Pens.Black;
                        brush = Brushes.Black;
                    }

                    var polygon = polygons[i];

                    if (polygon.Count > 1)
                    {
                        for (int j = 1; j < polygon.Count; ++j)
                        {
                            g.DrawLine(pen, polygon[j - 1], polygon[j]);
                        }
                        g.DrawLine(pen, polygon[polygon.Count - 1], polygon[0]);
                    }

                    foreach (PointF p in polygon)
                    {
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                    }
                }

                if (currentPolygon != -1 && polygons[currentPolygon].Count > 0)
                {
                    g.FillEllipse(Brushes.Orange, transformCenter.X - 3, transformCenter.Y - 3, 6, 6);
                }
            }

            pictureBox1.Invalidate();
        }

        private void draw_polygon_Click(object sender, EventArgs e)
        {
            polygons.Add(new List<PointF>());
            currentPolygon = polygons.Count - 1;
            RedrawPolygons();
        }

        // Очистка сцены
        // ==========================================================================================
        private void clear_Click(object sender, EventArgs e)
        {
            polygons.Clear();
            currentPolygon = -1;
            if (pictureBox1.Image != null)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.Clear(Color.White);
                }

                pictureBox1.Invalidate();
            }
        }

        // Применение аффинных преобразований 
        // ==========================================================================================
        private void OpenTransformMenu()
        {
            this.Width += 225;

            Label lblOffset = new Label { Text = "Смещение", Location = new Point(this.ClientSize.Width - 220, 20) };
            Label lblRotation = new Label { Text = "Поворот", Location = new Point(this.ClientSize.Width - 220, 60) };
            Label lblScale = new Label { Text = "Масштаб", Location = new Point(this.ClientSize.Width - 220, 100) };

            TextBox txtOffsetX = new TextBox { Text = "0", Location = new Point(this.ClientSize.Width - 120, 20), Width = 50 };
            TextBox txtOffsetY = new TextBox { Text = "0", Location = new Point(this.ClientSize.Width - 60, 20), Width = 50 };

            TextBox txtRotation = new TextBox { Text = "0", Location = new Point(this.ClientSize.Width - 120, 60), Width = 110 };

            TextBox txtScaleX = new TextBox { Text = "1", Location = new Point(this.ClientSize.Width - 120, 100), Width = 50 };
            TextBox txtScaleY = new TextBox { Text = "1", Location = new Point(this.ClientSize.Width - 60, 100), Width = 50 };

            txtOffsetX.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (int.TryParse(txtOffsetX.Text, out int dx) && int.TryParse(txtOffsetY.Text, out int dy))
                    {
                        ApplyTranslation(dx, dy);
                    }
                }
            };

            txtOffsetY.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (int.TryParse(txtOffsetX.Text, out int dx) && int.TryParse(txtOffsetY.Text, out int dy))
                    {
                        ApplyTranslation(dx, dy);
                    }
                }
            };

            txtRotation.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (double.TryParse(txtRotation.Text, out double angle))
                    {
                        ApplyRotation(angle);
                    }
                }
            };

            txtScaleX.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (double.TryParse(txtScaleX.Text, out double scaleFactorX) && double.TryParse(txtScaleY.Text, out double scaleFactorY))
                    {
                        ApplyScaling(scaleFactorX, scaleFactorY);
                    }
                }
            };

            txtScaleY.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (double.TryParse(txtScaleX.Text, out double scaleFactorX) && double.TryParse(txtScaleY.Text, out double scaleFactorY))
                    {
                        ApplyScaling(scaleFactorX, scaleFactorY);
                    }
                }
            };

            this.Controls.Add(lblOffset);
            this.Controls.Add(txtOffsetX);
            this.Controls.Add(txtOffsetY);

            this.Controls.Add(lblRotation);
            this.Controls.Add(txtRotation);

            this.Controls.Add(lblScale);
            this.Controls.Add(txtScaleX);
            this.Controls.Add(txtScaleY);
        }

        private void ApplyTranslation(int dx, int dy)
        {
            var matrix = TransformationMatrix.CreateTranslationMatrix(dx, dy);
            if (TransformCurrentPolygon(matrix))
                transformCenter = matrix.Transform(transformCenter);
        }

        private void ApplyRotation(double angle)
        {
            var matrix = TransformationMatrix.CreateRotationMatrix(angle, transformCenter);
            TransformCurrentPolygon(matrix);
        }

        private void ApplyScaling(double scaleX, double scaleY)
        {
            var matrix = TransformationMatrix.CreateScalingMatrix(scaleX, scaleY, transformCenter);
            TransformCurrentPolygon(matrix);
        }

        private bool TransformCurrentPolygon(TransformationMatrix transformationMatrix)
        {
            if (currentPolygon != -1 && polygons[currentPolygon].Count > 0)
            {
                var polygon = polygons[currentPolygon];
                for (int i = 0; i < polygon.Count; i++)
                {
                    polygon[i] = transformationMatrix.Transform(polygon[i]);
                }
                RedrawPolygons();
                return true;
            }
            return false;
        }

        private PointF GetPolygonCenter(int polygonIndex)
        {
            var polygon = polygons[polygonIndex];
            float sumX = 0, sumY = 0;
            foreach (var point in polygon)
            {
                sumX += point.X;
                sumY += point.Y;
            }
            return new PointF(sumX / polygon.Count, sumY / polygon.Count);
        }

        // Поиск точки пересечения двух ребер
        // ==========================================================================================
        public static PointF? FindIntersection(PointF A, PointF B, PointF C, PointF D)
        {
            float dxAB = B.X - A.X;
            float dyAB = B.Y - A.Y;
            float dxCD = D.X - C.X;
            float dyCD = D.Y - C.Y;

            float nX = -dyCD;
            float nY = dxCD;

            float dxAC = A.X - C.X;
            float dyAC = A.Y - C.Y;
            float numerator = nX * dxAC + nY * dyAC;
            float denominator = nX * dxAB + nY * dyAB;

            if (denominator == 0)
            {
                return null; 
            }

            float t = -numerator / denominator;

            if (t < 0 || t > 1)
            {
                return null; 
            }

            float intersectionX = A.X + t * dxAB;
            float intersectionY = A.Y + t * dyAB;

            float dxIntersectC = intersectionX - C.X;
            float dyIntersectC = intersectionY - C.Y;

            float dotProduct = dxIntersectC * dxCD + dyIntersectC * dyCD;
            float lengthCD = dxCD * dxCD + dyCD * dyCD;

            if (dotProduct >= 0 && dotProduct <= lengthCD)
            {
                return new PointF(intersectionX, intersectionY);
            }

            return null; 
        }


        public void DrawIntersection(PointF x, PointF y)
        {
            for(int p = 0; p < polygons.Count; ++p){
                if (p == currentPolygon)
                    continue;
                for(int i = 0; i < polygons[p].Count; ++i)
                {
                    PointF? intersection = FindIntersection(polygons[p][i], polygons[p][(i + 1) % polygons[p].Count], x, y);
                    if (intersection != null)
                    {
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            g.FillEllipse(Brushes.Orchid, intersection.Value.X - 4, intersection.Value.Y - 4, 8, 8);
                        }
                    }
                }
            }

            this.Invalidate();
        }
    }


    // Класс для матрицы афинных преобразований
    // ==========================================================================================
    public class TransformationMatrix
    {
        public double[,] matrix = new double[3, 3];

        public TransformationMatrix()
        {
            matrix[0, 0] = 1;
            matrix[1, 1] = 1;
            matrix[2, 2] = 1;
        }


        public PointF Transform(PointF p)
        {
            double x = matrix[0, 0] * p.X + matrix[1, 0] * p.Y + matrix[2, 0];
            double y = matrix[0, 1] * p.X + matrix[1, 1] * p.Y + matrix[2, 1];
            return new PointF((float)x, (float)y);
        }


        public static TransformationMatrix Multiply(TransformationMatrix a, TransformationMatrix b)
        {
            TransformationMatrix result = new TransformationMatrix();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                    }
                }
            }
            return result;
        }

        public static TransformationMatrix CreateTranslationMatrix(double dx, double dy)
        {
            TransformationMatrix result = new TransformationMatrix();
            result.matrix[2, 0] = dx;
            result.matrix[2, 1] = dy;
            return result;
        }


        public static TransformationMatrix CreateRotationMatrix(double angle)
        {
            double radians = angle * Math.PI / 180;
            TransformationMatrix result = new TransformationMatrix();
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            result.matrix[0, 0] = cos;
            result.matrix[0, 1] = sin;
            result.matrix[1, 0] = -sin;
            result.matrix[1, 1] = cos;
            return result;
        }


        public static TransformationMatrix CreateRotationMatrix(double angle, PointF center)
        {
            var translateToOrigin = CreateTranslationMatrix(-center.X, -center.Y);
            var rotationMatrix = CreateRotationMatrix(angle);
            var translateBack = CreateTranslationMatrix(center.X, center.Y);

            return Multiply(Multiply(translateToOrigin, rotationMatrix), translateBack);
        }


        public static TransformationMatrix CreateScalingMatrix(double scaleX, double scaleY)
        {
            TransformationMatrix result = new TransformationMatrix();
            result.matrix[0, 0] = scaleX;
            result.matrix[1, 1] = scaleY;
            return result;
        }


        public static TransformationMatrix CreateScalingMatrix(double scaleX, double scaleY, PointF center)
        {
            var translateToOrigin = CreateTranslationMatrix(-center.X, -center.Y);
            var scalingMatrix = CreateScalingMatrix(scaleX, scaleY);
            var translateBack = CreateTranslationMatrix(center.X, center.Y);

            return Multiply(Multiply(translateToOrigin, scalingMatrix), translateBack);
        }
    }
}
