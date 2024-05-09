namespace SimpleVoronoi
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.deleteEdit = new System.Windows.Forms.RadioButton();
            this.addEdit = new System.Windows.Forms.RadioButton();
            this.noneEdit = new System.Windows.Forms.RadioButton();
            this.DeletePoints = new System.Windows.Forms.Button();
            this.numberOfPoints = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.numberOfPoints);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1067, 694);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.deleteEdit);
            this.groupBox1.Controls.Add(this.addEdit);
            this.groupBox1.Controls.Add(this.noneEdit);
            this.groupBox1.Controls.Add(this.DeletePoints);
            this.groupBox1.Location = new System.Drawing.Point(318, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(445, 80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Points";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(285, 55);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(160, 22);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // deleteEdit
            // 
            this.deleteEdit.AutoSize = true;
            this.deleteEdit.Location = new System.Drawing.Point(178, 23);
            this.deleteEdit.Margin = new System.Windows.Forms.Padding(4);
            this.deleteEdit.Name = "deleteEdit";
            this.deleteEdit.Size = new System.Drawing.Size(101, 20);
            this.deleteEdit.TabIndex = 2;
            this.deleteEdit.TabStop = true;
            this.deleteEdit.Text = "Delete Point";
            this.deleteEdit.UseVisualStyleBackColor = true;
            // 
            // addEdit
            // 
            this.addEdit.AutoSize = true;
            this.addEdit.Location = new System.Drawing.Point(77, 23);
            this.addEdit.Margin = new System.Windows.Forms.Padding(4);
            this.addEdit.Name = "addEdit";
            this.addEdit.Size = new System.Drawing.Size(93, 20);
            this.addEdit.TabIndex = 1;
            this.addEdit.TabStop = true;
            this.addEdit.Text = "Add Points";
            this.addEdit.UseVisualStyleBackColor = true;
            // 
            // noneEdit
            // 
            this.noneEdit.AutoSize = true;
            this.noneEdit.Checked = true;
            this.noneEdit.Location = new System.Drawing.Point(8, 23);
            this.noneEdit.Margin = new System.Windows.Forms.Padding(4);
            this.noneEdit.Name = "noneEdit";
            this.noneEdit.Size = new System.Drawing.Size(61, 20);
            this.noneEdit.TabIndex = 0;
            this.noneEdit.TabStop = true;
            this.noneEdit.Text = "None";
            this.noneEdit.UseVisualStyleBackColor = true;
            // 
            // DeletePoints
            // 
            this.DeletePoints.Location = new System.Drawing.Point(311, 19);
            this.DeletePoints.Margin = new System.Windows.Forms.Padding(4);
            this.DeletePoints.Name = "DeletePoints";
            this.DeletePoints.Size = new System.Drawing.Size(100, 28);
            this.DeletePoints.TabIndex = 0;
            this.DeletePoints.Text = "Delete Points";
            this.DeletePoints.UseVisualStyleBackColor = true;
            this.DeletePoints.Click += new System.EventHandler(this.DeletePoints_Click);
            // 
            // numberOfPoints
            // 
            this.numberOfPoints.Location = new System.Drawing.Point(136, 17);
            this.numberOfPoints.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfPoints.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numberOfPoints.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numberOfPoints.Name = "numberOfPoints";
            this.numberOfPoints.Size = new System.Drawing.Size(160, 22);
            this.numberOfPoints.TabIndex = 1;
            this.numberOfPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1067, 570);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 694);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numberOfPoints;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton deleteEdit;
        private System.Windows.Forms.RadioButton addEdit;
        private System.Windows.Forms.RadioButton noneEdit;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button DeletePoints;
    }
}

