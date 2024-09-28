namespace lab4
{
    public partial class Form1 : Form
    {
        private List<List<Point>> polygons = new List<List<Point>>(); 
        private List<Point> currentPolygon = new List<Point>(); 

        public Form1()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentPolygon != null)
            {
                currentPolygon.Add(e.Location); 
                RedrawPolygons(); 
            }
        }

        private void RedrawPolygons()
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height); 
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White); 

                foreach (var polygon in polygons)
                {
                    if (polygon.Count > 1)
                    {
                        for (int i = 1; i < polygon.Count; i++)
                        {
                            g.DrawLine(Pens.Black, polygon[i - 1], polygon[i]); 
                        }
                        g.DrawLine(Pens.Black, polygon[polygon.Count - 1], polygon[0]); 
                    }

                    foreach (Point p in polygon)
                    {
                        g.FillEllipse(Brushes.Black, p.X - 3, p.Y - 3, 6, 6); 
                    }
                }

                if (currentPolygon.Count > 1)
                {
                    for (int i = 1; i < currentPolygon.Count; i++)
                    {
                        g.DrawLine(Pens.Black, currentPolygon[i - 1], currentPolygon[i]); 
                    }
                    g.DrawLine(Pens.Black, currentPolygon[currentPolygon.Count - 1], currentPolygon[0]); 
                }

                foreach (Point p in currentPolygon)
                {
                    g.FillEllipse(Brushes.Black, p.X - 3, p.Y - 3, 6, 6); 
                }
            }

            pictureBox1.Invalidate(); 
        }

        private void draw_polygon_Click(object sender, EventArgs e)
        {
            if (currentPolygon.Count > 0)
            {
                polygons.Add(new List<Point>(currentPolygon)); 
                currentPolygon.Clear(); 
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            polygons.Clear(); 
            currentPolygon.Clear(); 
            if (pictureBox1.Image != null)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.Clear(Color.White); 
                }

                pictureBox1.Invalidate(); 
            }
        }

        
    }
}
