namespace StartCSE
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SitesColumnLbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddressColumnLbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.MSlabel1 = new System.Windows.Forms.Label();
            this.MStextBox1 = new System.Windows.Forms.TextBox();
            this.MScheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PasteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.ProgressgroupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxPDM = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NewProjecttabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.ProgressgroupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.NewProjecttabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SitesColumnLbl,
            this.AddressColumnLbl});
            this.dataGridView1.Location = new System.Drawing.Point(21, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.Size = new System.Drawing.Size(543, 332);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // SitesColumnLbl
            // 
            this.SitesColumnLbl.HeaderText = "Sites";
            this.SitesColumnLbl.Name = "SitesColumnLbl";
            this.SitesColumnLbl.Width = 200;
            // 
            // AddressColumnLbl
            // 
            this.AddressColumnLbl.HeaderText = "Address";
            this.AddressColumnLbl.Name = "AddressColumnLbl";
            this.AddressColumnLbl.Width = 300;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(90, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "START JOB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MSlabel1
            // 
            this.MSlabel1.AutoSize = true;
            this.MSlabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSlabel1.Location = new System.Drawing.Point(20, 24);
            this.MSlabel1.Name = "MSlabel1";
            this.MSlabel1.Size = new System.Drawing.Size(90, 18);
            this.MSlabel1.TabIndex = 2;
            this.MSlabel1.Text = "Job Name:";
            this.MSlabel1.Visible = false;
            // 
            // MStextBox1
            // 
            this.MStextBox1.Location = new System.Drawing.Point(136, 22);
            this.MStextBox1.Name = "MStextBox1";
            this.MStextBox1.Size = new System.Drawing.Size(219, 21);
            this.MStextBox1.TabIndex = 3;
            this.MStextBox1.Visible = false;
            // 
            // MScheckBox
            // 
            this.MScheckBox.AutoSize = true;
            this.MScheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MScheckBox.Location = new System.Drawing.Point(415, 24);
            this.MScheckBox.Name = "MScheckBox";
            this.MScheckBox.Size = new System.Drawing.Size(137, 22);
            this.MScheckBox.TabIndex = 4;
            this.MScheckBox.Text = "Multiple Sites?";
            this.MScheckBox.UseVisualStyleBackColor = true;
            this.MScheckBox.CheckedChanged += new System.EventHandler(this.MScheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(400, 592);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "V1.0.0";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(174, 592);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "V1.0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 503);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 592);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "LOCAL CSE VERSION:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 592);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "SERVER CSE VERSION:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(206, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 42);
            this.label6.TabIndex = 2;
            this.label6.Text = "StartCSE";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(14, 632);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(127, 23);
            this.UpdateButton.TabIndex = 6;
            this.UpdateButton.Text = "Update Code";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(174, 607);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "V1.0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 607);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "SERVER BOS VERSION:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(400, 607);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "V1.0.0";
            this.label9.Click += new System.EventHandler(this.label1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(237, 607);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 15);
            this.label10.TabIndex = 5;
            this.label10.Text = "LOCAL BOS VERSION:";
            // 
            // PasteButton
            // 
            this.PasteButton.Location = new System.Drawing.Point(376, 431);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(75, 23);
            this.PasteButton.TabIndex = 7;
            this.PasteButton.Text = "Paste";
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(477, 431);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 8;
            this.ClearButton.Text = "Clear Table";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(331, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(27, 66);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(55, 15);
            this.progressLabel.TabIndex = 10;
            this.progressLabel.Text = "label11";
            this.progressLabel.Visible = false;
            // 
            // ProgressgroupBox1
            // 
            this.ProgressgroupBox1.Controls.Add(this.progressBar1);
            this.ProgressgroupBox1.Controls.Add(this.progressLabel);
            this.ProgressgroupBox1.Location = new System.Drawing.Point(90, 202);
            this.ProgressgroupBox1.Name = "ProgressgroupBox1";
            this.ProgressgroupBox1.Size = new System.Drawing.Size(411, 100);
            this.ProgressgroupBox1.TabIndex = 11;
            this.ProgressgroupBox1.TabStop = false;
            this.ProgressgroupBox1.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 2;
            this.label11.Text = "PDM:";
            // 
            // comboBoxPDM
            // 
            this.comboBoxPDM.FormattingEnabled = true;
            this.comboBoxPDM.Items.AddRange(new object[] {
            "Dan Leary",
            "Brent Eskay",
            "Lauren Harris"});
            this.comboBoxPDM.Location = new System.Drawing.Point(136, 46);
            this.comboBoxPDM.Name = "comboBoxPDM";
            this.comboBoxPDM.Size = new System.Drawing.Size(219, 23);
            this.comboBoxPDM.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NewProjecttabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 525);
            this.tabControl1.TabIndex = 12;
            // 
            // NewProjecttabPage
            // 
            this.NewProjecttabPage.Controls.Add(this.dataGridView1);
            this.NewProjecttabPage.Controls.Add(this.comboBoxPDM);
            this.NewProjecttabPage.Controls.Add(this.button1);
            this.NewProjecttabPage.Controls.Add(this.ProgressgroupBox1);
            this.NewProjecttabPage.Controls.Add(this.MSlabel1);
            this.NewProjecttabPage.Controls.Add(this.ClearButton);
            this.NewProjecttabPage.Controls.Add(this.label11);
            this.NewProjecttabPage.Controls.Add(this.PasteButton);
            this.NewProjecttabPage.Controls.Add(this.MStextBox1);
            this.NewProjecttabPage.Controls.Add(this.MScheckBox);
            this.NewProjecttabPage.Location = new System.Drawing.Point(4, 24);
            this.NewProjecttabPage.Name = "NewProjecttabPage";
            this.NewProjecttabPage.Padding = new System.Windows.Forms.Padding(3);
            this.NewProjecttabPage.Size = new System.Drawing.Size(581, 497);
            this.NewProjecttabPage.TabIndex = 0;
            this.NewProjecttabPage.Text = "New Project";
            this.NewProjecttabPage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(629, 669);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(645, 676);
            this.Name = "Form1";
            this.Text = "StartCSE";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ProgressgroupBox1.ResumeLayout(false);
            this.ProgressgroupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.NewProjecttabPage.ResumeLayout(false);
            this.NewProjecttabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label MSlabel1;
        private System.Windows.Forms.TextBox MStextBox1;
        private System.Windows.Forms.CheckBox MScheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SitesColumnLbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressColumnLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button PasteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.GroupBox ProgressgroupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxPDM;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NewProjecttabPage;
    }
}

