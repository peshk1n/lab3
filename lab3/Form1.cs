namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TriangleForm form = new TriangleForm();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task1 task1 = new Task1();
            task1.ShowDialog();
        }
    }
}