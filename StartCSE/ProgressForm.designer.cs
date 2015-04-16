namespace StartCSE
{
    partial class ProgressForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.currentStatusLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(29, 23);
            this.progressBar1.MaximumSize = new System.Drawing.Size(509, 40);
            this.progressBar1.MinimumSize = new System.Drawing.Size(509, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(509, 40);
            this.progressBar1.TabIndex = 0;
            // 
            // currentStatusLbl
            // 
            this.currentStatusLbl.AutoSize = true;
            this.currentStatusLbl.Location = new System.Drawing.Point(26, 78);
            this.currentStatusLbl.Name = "currentStatusLbl";
            this.currentStatusLbl.Size = new System.Drawing.Size(55, 13);
            this.currentStatusLbl.TabIndex = 1;
            this.currentStatusLbl.Text = "Initiating...";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 112);
            this.Controls.Add(this.currentStatusLbl);
            this.Controls.Add(this.progressBar1);
            this.Name = "ProgressForm";
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label currentStatusLbl;
    }
}