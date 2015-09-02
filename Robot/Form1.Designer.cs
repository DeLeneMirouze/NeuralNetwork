namespace Robot
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
            this.Piece = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Liste = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.NbIterations = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.Position = new System.Windows.Forms.Label();
            this.Sensor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Piece)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NbIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // Piece
            // 
            this.Piece.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Piece.Location = new System.Drawing.Point(4, 13);
            this.Piece.Name = "Piece";
            this.Piece.Size = new System.Drawing.Size(600, 600);
            this.Piece.TabIndex = 0;
            this.Piece.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Initialiser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Liste
            // 
            this.Liste.Location = new System.Drawing.Point(660, 149);
            this.Liste.Multiline = true;
            this.Liste.Name = "Liste";
            this.Liste.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Liste.Size = new System.Drawing.Size(259, 417);
            this.Liste.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(660, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Itérer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // NbIterations
            // 
            this.NbIterations.Location = new System.Drawing.Point(799, 78);
            this.NbIterations.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NbIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NbIterations.Name = "NbIterations";
            this.NbIterations.Size = new System.Drawing.Size(120, 22);
            this.NbIterations.TabIndex = 4;
            this.NbIterations.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(660, 101);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 27);
            this.button3.TabIndex = 5;
            this.button3.Text = "Pas à pas";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(959, 149);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(660, 586);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(259, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Effacer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Position
            // 
            this.Position.AutoSize = true;
            this.Position.Location = new System.Drawing.Point(4, 629);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(0, 17);
            this.Position.TabIndex = 8;
            // 
            // Sensor
            // 
            this.Sensor.AutoSize = true;
            this.Sensor.Location = new System.Drawing.Point(396, 620);
            this.Sensor.Name = "Sensor";
            this.Sensor.Size = new System.Drawing.Size(46, 17);
            this.Sensor.TabIndex = 9;
            this.Sensor.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 658);
            this.Controls.Add(this.Sensor);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.NbIterations);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Liste);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Piece);
            this.Name = "Form1";
            this.Text = "Robot";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Piece)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NbIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Piece;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Liste;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown NbIterations;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label Position;
        private System.Windows.Forms.Label Sensor;
    }
}

