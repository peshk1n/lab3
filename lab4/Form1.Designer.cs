﻿namespace lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clear = new System.Windows.Forms.Button();
            this.draw_polygon = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(0, 0);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(241, 104);
            this.clear.TabIndex = 0;
            this.clear.Text = "Очистить область";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // draw_polygon
            // 
            this.draw_polygon.Location = new System.Drawing.Point(0, 110);
            this.draw_polygon.Name = "draw_polygon";
            this.draw_polygon.Size = new System.Drawing.Size(241, 105);
            this.draw_polygon.TabIndex = 1;
            this.draw_polygon.Text = "Нарисовать полигон";
            this.draw_polygon.UseVisualStyleBackColor = true;
            this.draw_polygon.Click += new System.EventHandler(this.draw_polygon_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(247, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1169, 804);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 806);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.draw_polygon);
            this.Controls.Add(this.clear);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button clear;
        private Button draw_polygon;
        private PictureBox pictureBox1;
    }
}