namespace KohonenPath
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
            this.Schema = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Entrainer = new System.Windows.Forms.Button();
            this.TrainNumber = new System.Windows.Forms.NumericUpDown();
            this.NbPoints = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Liste = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Ralenti = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Schema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NbPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // Schema
            // 
            this.Schema.Location = new System.Drawing.Point(97, 147);
            this.Schema.Name = "Schema";
            this.Schema.Size = new System.Drawing.Size(543, 538);
            this.Schema.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Schema.TabIndex = 8;
            this.Schema.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 36);
            this.button1.TabIndex = 9;
            this.button1.Text = "Initialiser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Entrainer
            // 
            this.Entrainer.Location = new System.Drawing.Point(97, 90);
            this.Entrainer.Name = "Entrainer";
            this.Entrainer.Size = new System.Drawing.Size(133, 36);
            this.Entrainer.TabIndex = 10;
            this.Entrainer.Text = "Entrainer";
            this.Entrainer.UseVisualStyleBackColor = true;
            this.Entrainer.Visible = false;
            this.Entrainer.Click += new System.EventHandler(this.button2_Click);
            // 
            // TrainNumber
            // 
            this.TrainNumber.Location = new System.Drawing.Point(364, 72);
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
            this.TrainNumber.TabIndex = 14;
            this.TrainNumber.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // NbPoints
            // 
            this.NbPoints.Location = new System.Drawing.Point(364, 31);
            this.NbPoints.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NbPoints.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NbPoints.Name = "NbPoints";
            this.NbPoints.Size = new System.Drawing.Size(120, 22);
            this.NbPoints.TabIndex = 15;
            this.NbPoints.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Taille chaîne";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Nb itérations";
            // 
            // Liste
            // 
            this.Liste.Location = new System.Drawing.Point(668, 147);
            this.Liste.Multiline = true;
            this.Liste.Name = "Liste";
            this.Liste.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Liste.Size = new System.Drawing.Size(296, 538);
            this.Liste.TabIndex = 18;
            this.Liste.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(639, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(325, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 19;
            // 
            // Ralenti
            // 
            this.Ralenti.AutoSize = true;
            this.Ralenti.Checked = true;
            this.Ralenti.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ralenti.Location = new System.Drawing.Point(260, 105);
            this.Ralenti.Name = "Ralenti";
            this.Ralenti.Size = new System.Drawing.Size(74, 21);
            this.Ralenti.TabIndex = 20;
            this.Ralenti.Text = "Ralenti";
            this.Ralenti.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(639, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 17);
            this.label3.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 699);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Ralenti);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Liste);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NbPoints);
            this.Controls.Add(this.TrainNumber);
            this.Controls.Add(this.Entrainer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Schema);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Résolution de chemin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Schema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NbPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Schema;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Entrainer;
        private System.Windows.Forms.NumericUpDown TrainNumber;
        private System.Windows.Forms.NumericUpDown NbPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Liste;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox Ralenti;
        private System.Windows.Forms.Label label3;
    }
}

