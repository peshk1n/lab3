namespace lab3
{
    partial class FormTask2
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
            this.panel = new System.Windows.Forms.Panel();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.comboBoxAlgorithm);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1343, 771);
            this.panel.TabIndex = 0;
            this.panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(3, 3);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(151, 28);
            this.comboBoxAlgorithm.TabIndex = 0;
            this.comboBoxAlgorithm.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlgorithm_SelectedIndexChanged);
            // 
            // FormTask2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 771);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTask2";
            this.Text = "FormTask2";
            this.Load += new System.EventHandler(this.FormTask2_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel;
        private ComboBox comboBoxAlgorithm;
    }
}