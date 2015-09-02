#region using
using BoiteAOutils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace KohonenPath
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Zone dans laquelle pousser le chemin
        /// </summary>
        Point[] zone;
        KohonenMap KohonenMap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Initialiser

            KohonenMap = new KohonenMap((int)NbPoints.Value, 0.5, Schema.Width, Schema.Height);

            Afficher();

            Entrainer.Visible = true;
            Liste.Visible = true;
        }

        #region Afficher
        private void Afficher()
        {
            Graphics g = Schema.CreateGraphics();
            g.Clear(Color.White);

            zone = new Point[7];
            zone[0] = new Point(0, 0);
            zone[1] = new Point(0, Schema.Height);
            zone[2] = new Point(Schema.Width, Schema.Height);
            zone[3] = new Point(Schema.Width, Schema.Height - 100);
            zone[4] = new Point(100, Schema.Height - 100);
            zone[5] = new Point(100, 0);

            Pen pen = new Pen(Color.Black, 2);
            Brush brush = Brushes.Azure;
            g.FillPolygon(brush, zone);
            g.DrawPolygon(pen, zone);

            // affiche chemin courant
            AfficherChemin(KohonenMap.Line);

            Liste.Text = null;
            for (int i = 0; i < KohonenMap.Line.Length; i++)
            {
                KohonenNeurone neurone = KohonenMap.Line[i];

                string message = string.Format("{0}: ({1},{2})", i, neurone.Poids[0], neurone.Poids[1]);
                Liste.Text += message + Environment.NewLine;
            }
        }
        #endregion

        #region AfficherChemin (private)
        private void AfficherChemin(KohonenNeurone[] line)
        {
            Graphics g = Schema.CreateGraphics();
            Point[] chemin = new Point[line.Length];
            for (int i = 0; i < line.Length; i++)
            {
                KohonenNeurone neurone = line[i];
                chemin[i] = new Point((int)neurone.Poids[0], (int)neurone.Poids[1]);
            }

            Pen pen = new Pen(Color.Red, 2);
            g.DrawLines(pen, chemin);
            Brush brush = new SolidBrush(Color.DarkBlue);
            for (int i = 0; i < chemin.Length; i++)
            {
                g.FillEllipse(brush, chemin[i].X, chemin[i].Y, 8, 8);
            }
        }
        #endregion

        private async void button2_Click(object sender, EventArgs e)
        {
            // entrainer 

            if (KohonenMap == null)
            {
                return;
            }

            // région contenant le L
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(zone);
            Region region = new Region(path);

            progressBar1.Maximum = (int)TrainNumber.Value;
            double[] cible = null;
            for (int i = 0; i < (int)TrainNumber.Value; i++)
            {
                progressBar1.Value = i;
                label3.Text = i.ToString();

                // sélectionne un point dans la zone
                cible = ObtenirCible();
                Debug.WriteLine("Point essai: " + cible[0].ToString() + "/" + cible[1].ToString());

                KohonenMap.Entrainer(cible, (int)TrainNumber.Value, i);

                // affiche la situation
                Afficher();
                // affiche la cible
                Graphics g = Schema.CreateGraphics();
                g.FillEllipse(Brushes.Orange, (float)cible[0], (float)cible[1], 8, 8);

                bool termine = TestFinCalculs(region);
                if (termine)
                {
                    break;
                }

                if (Ralenti.Checked)
                {
                    await Task.Delay(1000);
                }
            }
        }

        #region TestFinCalculs (private)
        /// <summary>
        /// Détermine si le chemin est entièrement dans le L
        /// </summary>
        /// <returns></returns>
        private bool TestFinCalculs(Region region)
        {
            foreach (KohonenNeurone neurone in KohonenMap.Line)
            {
                Point point = new Point((int)neurone.Poids[0], (int)neurone.Poids[1]);
                if (!region.IsVisible(point))
                {
                    return false;
                }
            }

            return true;
        } 
        #endregion

        #region ObtenirCible (private)
        /// <summary>
        /// Obtient un point situé dans le L du dessin
        /// </summary>
        /// <returns></returns>
        private double[] ObtenirCible()
        {
            Random rnd = new Random();
            int x = rnd.Next(Schema.Width);
            int y = 0;
            if (x <= 100)
            {
                y = rnd.Next(Schema.Height);
            }
            else
            {
                y = rnd.Next(Schema.Height - 100, Schema.Height);         
            }

            double[] point = new double[2];
            point[0] = x;
            point[1] = y;

            return point;
        }
        #endregion
    }
}
