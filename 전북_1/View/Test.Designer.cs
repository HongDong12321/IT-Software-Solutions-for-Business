namespace 전북_1.View
{
    partial class Test
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
            this.bufferedPanel1 = new 전북_1.View.BufferedPanel();
            this.SuspendLayout();
            // 
            // bufferedPanel1
            // 
            this.bufferedPanel1.Location = new System.Drawing.Point(31, 45);
            this.bufferedPanel1.Name = "bufferedPanel1";
            this.bufferedPanel1.Size = new System.Drawing.Size(460, 332);
            this.bufferedPanel1.TabIndex = 0;
            this.bufferedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bufferedPanel1);
            this.Name = "Test";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BufferedPanel bufferedPanel1;
    }
}