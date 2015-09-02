namespace KohonenNetwork
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
            this.Temoin = new System.Windows.Forms.PictureBox();
            this.ShowBuild = new System.Windows.Forms.CheckBox();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.Position = new System.Windows.Forms.PictureBox();
            this.TrainNumber = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Calcule = new System.Windows.Forms.PictureBox();
            this.Origine = new System.Windows.Forms.PictureBox();
            this.Y = new System.Windows.Forms.NumericUpDown();
            this.X = new System.Windows.Forms.NumericUpDown();
            this.Entrees = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Temoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Calcule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Origine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Entrees)).BeginInit();
            this.SuspendLayout();
            // 
            // Temoin
            // 
            this.Temoin.Location = new System.Drawing.Point(455, 244);
            this.Temoin.Name = "Temoin";
            this.Temoin.Size = new System.Drawing.Size(75, 50);
            this.Temoin.TabIndex = 18;
            this.Temoin.TabStop = false;
            // 
            // ShowBuild
            // 
            this.ShowBuild.AutoSize = true;
            this.ShowBuild.Location = new System.Drawing.Point(750, 80);
            this.ShowBuild.Name = "ShowBuild";
            this.ShowBuild.Size = new System.Drawing.Size(136, 21);
            this.ShowBuild.TabIndex = 17;
            this.ShowBuild.Text = "Voir construction";
            this.ShowBuild.UseVisualStyleBackColor = true;
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(558, 131);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(723, 23);
            this.Progress.TabIndex = 16;
            this.Progress.Visible = false;
            // 
            // Position
            // 
            this.Position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Position.Location = new System.Drawing.Point(27, 576);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(400, 400);
            this.Position.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Position.TabIndex = 15;
            this.Position.TabStop = false;
            // 
            // TrainNumber
            // 
            this.TrainNumber.Location = new System.Drawing.Point(558, 73);
            this.TrainNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.TrainNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TrainNumber.Name = "TrainNumber";
            this.TrainNumber.Size = new System.Drawing.Size(120, 22);
            this.TrainNumber.TabIndex = 13;
            this.TrainNumber.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 33);
            this.button3.TabIndex = 12;
            this.button3.Text = "Entrainer N fois";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(747, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Sources";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(335, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 40);
            this.button2.TabIndex = 11;
            this.button2.Text = "Entrainer une fois";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(750, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Random";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Calcule
            // 
            this.Calcule.Location = new System.Drawing.Point(558, 169);
            this.Calcule.Name = "Calcule";
            this.Calcule.Size = new System.Drawing.Size(723, 723);
            this.Calcule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Calcule.TabIndex = 7;
            this.Calcule.TabStop = false;
            // 
            // Origine
            // 
            this.Origine.Location = new System.Drawing.Point(27, 170);
            this.Origine.Name = "Origine";
            this.Origine.Size = new System.Drawing.Size(400, 400);
            this.Origine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Origine.TabIndex = 6;
            this.Origine.TabStop = false;
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(147, 107);
            this.Y.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(120, 22);
            this.Y.TabIndex = 5;
            this.Y.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(147, 67);
            this.X.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(120, 22);
            this.X.TabIndex = 4;
            this.X.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Entrees
            // 
            this.Entrees.Enabled = false;
            this.Entrees.Location = new System.Drawing.Point(147, 25);
            this.Entrees.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.Entrees.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Entrees.Name = "Entrees";
            this.Entrees.Size = new System.Drawing.Size(120, 22);
            this.Entrees.TabIndex = 3;
            this.Entrees.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "X:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Matrice entrée:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 811);
            this.Controls.Add(this.Temoin);
            this.Controls.Add(this.ShowBuild);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.TrainNumber);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Calcule);
            this.Controls.Add(this.Origine);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Controls.Add(this.Entrees);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kohonen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Temoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Calcule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Origine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Entrees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Temoin;
        private System.Windows.Forms.CheckBox ShowBuild;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.PictureBox Position;
        private System.Windows.Forms.NumericUpDown TrainNumber;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox Calcule;
        private System.Windows.Forms.PictureBox Origine;
        private System.Windows.Forms.NumericUpDown Y;
        private System.Windows.Forms.NumericUpDown X;
        private System.Windows.Forms.NumericUpDown Entrees;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

