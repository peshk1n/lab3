namespace lab3
{
    partial class Task1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.clean_area = new System.Windows.Forms.Button();
            this.draw_border = new System.Windows.Forms.Button();
            this.fill_color = new System.Windows.Forms.Button();
            this.fill_picture = new System.Windows.Forms.Button();
            this.find_border = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(316, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 870);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // clean_area
            // 
            this.clean_area.Location = new System.Drawing.Point(12, 12);
            this.clean_area.Name = "clean_area";
            this.clean_area.Size = new System.Drawing.Size(279, 58);
            this.clean_area.TabIndex = 1;
            this.clean_area.Text = "Очистить область";
            this.clean_area.UseVisualStyleBackColor = true;
            this.clean_area.Click += new System.EventHandler(this.clean_area_Click);
            // 
            // draw_border
            // 
            this.draw_border.Location = new System.Drawing.Point(12, 76);
            this.draw_border.Name = "draw_border";
            this.draw_border.Size = new System.Drawing.Size(279, 108);
            this.draw_border.TabIndex = 2;
            this.draw_border.Text = "Нарисовать границу";
            this.draw_border.UseVisualStyleBackColor = true;
            this.draw_border.Click += new System.EventHandler(this.draw_border_Click);
            // 
            // fill_color
            // 
            this.fill_color.Location = new System.Drawing.Point(12, 190);
            this.fill_color.Name = "fill_color";
            this.fill_color.Size = new System.Drawing.Size(279, 95);
            this.fill_color.TabIndex = 3;
            this.fill_color.Text = "Залить область цветом";
            this.fill_color.UseVisualStyleBackColor = true;
            this.fill_color.Click += new System.EventHandler(this.fill_color_Click);
            // 
            // fill_picture
            // 
            this.fill_picture.Location = new System.Drawing.Point(12, 291);
            this.fill_picture.Name = "fill_picture";
            this.fill_picture.Size = new System.Drawing.Size(279, 95);
            this.fill_picture.TabIndex = 4;
            this.fill_picture.Text = "Залить область картинкой";
            this.fill_picture.UseVisualStyleBackColor = true;
            this.fill_picture.Click += new System.EventHandler(this.fill_picture_Click);
            // 
            // find_border
            // 
            this.find_border.Location = new System.Drawing.Point(12, 392);
            this.find_border.Name = "find_border";
            this.find_border.Size = new System.Drawing.Size(279, 58);
            this.find_border.TabIndex = 5;
            this.find_border.Text = "Выделить границу";
            this.find_border.UseVisualStyleBackColor = true;
            this.find_border.Click += new System.EventHandler(this.find_border_Click);
            // 
            // Task1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 894);
            this.Controls.Add(this.find_border);
            this.Controls.Add(this.fill_picture);
            this.Controls.Add(this.fill_color);
            this.Controls.Add(this.draw_border);
            this.Controls.Add(this.clean_area);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Task1";
            this.Text = "Task1";
            this.Load += new System.EventHandler(this.Task1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private Button clean_area;
        private Button draw_border;
        private Button fill_color;
        private Button fill_picture;
        private Button find_border;
    }
}