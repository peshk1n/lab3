using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class FormTask2 : Form
    {
        private List<Point> points = new List<Point>();
        private Bitmap canvasBitmap;

        public FormTask2()
        {
            InitializeComponent();
            comboBoxAlgorithm.Items.Add("Wu");
            comboBoxAlgorithm.Items.Add("Bresenham");
            comboBoxAlgorithm.SelectedIndex = 0;
            comboBoxAlgorithm.SelectedIndexChanged += comboBoxAlgorithm_SelectedIndexChanged;
            canvasBitmap = new Bitmap(panel.Width, panel.Height);
            panel.MouseClick += panel1_MouseClick;
            panel.Paint += panel_Paint; 
        }

        private void FormTask2_Load(object sender, EventArgs e)
        {

        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvasBitmap, 0, 0);
        }
        private void comboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем холст
            canvasBitmap = new Bitmap(panel.Width, panel.Height);
            points = new List<Point>();
            panel.Invalidate(); // Перерисовываем панель

            // Перерисовываем все точки
            //using (Graphics g = Graphics.FromImage(canvasBitmap))
            //{
            //    foreach (Point point in points)
            //    {
            //        int pointSize = 10;
            //        g.FillEllipse(Brushes.Black, point.X - pointSize / 2, point.Y - pointSize / 2, pointSize, pointSize);
            //    }
            //}
        }

        void DrawWuLine(Bitmap bmp, int x0, int y0, int x1, int y1)
        {
            Color pointColor = Color.Black;
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            if (dx > dy)
            {
                float gradient = dx != 0 ? (float)dy / dx : 1;
                float y = y0 + gradient * sy;

                for (int x = x0 + sx; x != x1 + sx; x += sx)
                {
                    float a = y - (int)y;
                    int c1 = (int)(255 * (1 - a));
                    int c2 = (int)(255 * a);

                    bmp.SetPixel(x, (int)y, Color.FromArgb(c1, Color.Black)); // Используем SetPixel
                    bmp.SetPixel(x, (int)y + 1, Color.FromArgb(c2, Color.Black));

                    y += gradient * sy;
                }
            }
            else
            {
                float gradient = dy != 0 ? (float)dx / dy : 1;
                float x = x0 + gradient * sx;
                for (int y = y0 + sy; y != y1 + sy; y += sy)
                {
                    float a = x - (int)x;
                    int c1 = (int)(255 * (1 - a));
                    int c2 = (int)(255 * a);

                    bmp.SetPixel((int)x, y, Color.FromArgb(c1, Color.Black)); // Используем SetPixel
                    bmp.SetPixel((int)x + 1, y, Color.FromArgb(c2, Color.Black));

                    x += gradient * sx;
                }
            }
        }
        private void DrawLineBresenham(Bitmap bmp, int x0, int y0, int x1, int y1)
        {

            Color pointColor = Color.Black;
            

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            if (dx > dy)
            {
                int d0 = 2 * dy - dx;

                int y = y0;
                for (int x = x0; x != x1 + sx; x += sx)
                {
                    bmp.SetPixel(x, y, pointColor);
                    if (d0 < 0)
                    {
                        d0 += 2 * dy;
                    }
                    else
                    {
                        y += sy;
                        d0 += 2 * (dy - dx);
                    }
                }
            }
            else
            {
                int d0 = 2 * dx - dy;

                int x = x0;
                for (int y = y0; y != y1 + sy; y += sy)

                {

                    bmp.SetPixel(x, y, pointColor);
                    if (d0 < 0)
                    {
                        d0 += 2 * dx;
                    }
                    else
                    {
                        x += sx;
                        d0 += 2 * (dx - dy);
                    }
                }

            }

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(e.Location);
            using (Graphics g = Graphics.FromImage(canvasBitmap))
            {
                int pointSize = 10;
                g.FillEllipse(Brushes.Black, e.Location.X - pointSize / 2, e.Location.Y - pointSize / 2, pointSize, pointSize);
            }

            if (points.Count > 1)
            {
                Point prevPoint = points[points.Count - 2];
                Point newPoint = points[points.Count - 1];

                if (comboBoxAlgorithm.SelectedItem.ToString() == "Wu")
                {
                    DrawWuLine(canvasBitmap, prevPoint.X, prevPoint.Y, newPoint.X, newPoint.Y);
                }
                else if (comboBoxAlgorithm.SelectedItem.ToString() == "Bresenham")
                {
                    DrawLineBresenham(canvasBitmap, prevPoint.X, prevPoint.Y, newPoint.X, newPoint.Y);
                }
            }

            panel.Invalidate();
        }

    }

    //public partial class FormTask2 : Form
    //{
    //    private List<Point> points = new List<Point>();
    //    public FormTask2()
    //    {
    //        InitializeComponent();

    //        panel.MouseClick+=panel1_MouseClick;

    //    }

    //    private void FormTask2_Load(object sender, EventArgs e)
    //    {

    //    }


    //    void DrawWuLine(Graphics g, int x0, int y0, int x1, int y1)
    //    {
    //        Color pointColor = Color.Black;

    //        int dx = Math.Abs(x1 - x0);
    //        int dy = Math.Abs(y1 - y0);
    //        int sx = x0 < x1 ? 1 : -1;
    //        int sy = y0 < y1 ? 1 : -1;

    //        if (dx > dy)
    //        {
    //            float gradient = dx != 0 ? (float)dy / dx : 1;
    //            float y = y0 + gradient * sy;

    //            for (int x = x0 + sx; x != x1 + sx; x += sx)
    //            {
    //                float a = y - (int)y;
    //                int c1 = (int)(255 * (1 - a));
    //                int c2 = (int)(255 * a);

    //                g.FillRectangle(new SolidBrush(Color.FromArgb(c1, Color.Black)), x, (int)y, 1, 1);
    //                g.FillRectangle(new SolidBrush(Color.FromArgb(c2, Color.Black)), x, (int)y + 1, 1, 1);

    //                y += gradient * sy;
    //            }
    //        }
    //        else
    //        {
    //            float gradient = dy != 0 ? (float)dx / dy : 1;
    //            float x = x0 + gradient * sx;
    //            for (int y = y0 + sy; y != y1 + sy; y += sy)
    //            {
    //                float a = x - (int)x;
    //                int c1 = (int)(255 * (1 - a));
    //                int c2 = (int)(255 * a);

    //                g.FillRectangle(new SolidBrush(Color.FromArgb(c1, Color.Black)), (int)x, y, 1, 1);
    //                g.FillRectangle(new SolidBrush(Color.FromArgb(c2, Color.Black)), (int)x + 1, y, 1, 1);
    //                x += gradient * sx;
    //            }
    //        }
    //    }





    //    private void panel1_MouseClick(object sender, MouseEventArgs e)
    //    {
    //        points.Add(e.Location);
    //        using (Graphics g = panel.CreateGraphics())
    //        {
    //            Brush brush = Brushes.Black;
    //            int pointSize = 10;
    //            g.FillEllipse(brush, e.Location.X - pointSize / 2, e.Location.Y - pointSize / 2, pointSize, pointSize);
    //            if (points.Count > 1)
    //            {
    //                Point prevPoint = points[points.Count - 2];
    //                Point newPoint = points[points.Count - 1];

    //                DrawWuLine(g, prevPoint.X, prevPoint.Y, newPoint.X, newPoint.Y);
    //            }
    //        }
    //    }
    //    private void DrawLineBresenham(Graphics g, int x0, int y0, int x1, int y1)
    //    {

    //        Color pointColor = Color.Black;
    //        Brush brush = new SolidBrush(pointColor);

    //        int dx = Math.Abs(x1 - x0);
    //        int dy = Math.Abs(y1 - y0);

    //        int sx = x0 < x1 ? 1 : -1;
    //        int sy = y0 < y1 ? 1 : -1;

    //        if (dx > dy)
    //        {
    //            int d0 = 2 * dy - dx;

    //            int y = y0;
    //            for (int x = x0; x != x1 + sx; x += sx)
    //            {
    //                g.FillRectangle(brush, x, y, 1, 1);
    //                if (d0 < 0)
    //                {
    //                    d0 += 2 * dy;
    //                }
    //                else
    //                {
    //                    y += sy;
    //                    d0 += 2 * (dy - dx);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            int d0 = 2 * dx - dy;

    //            int x = x0;
    //            for (int y = y0; y != y1 + sy; y += sy)

    //            {

    //                g.FillRectangle(brush, x, y, 1, 1);
    //                if (d0 < 0)
    //                {
    //                    d0 += 2 * dx;
    //                }
    //                else
    //                {
    //                    x += sx;
    //                    d0 += 2 * (dx - dy);
    //                }
    //            }

    //        }

    //    }


    //}
}
