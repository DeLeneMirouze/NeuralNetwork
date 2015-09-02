#region using
using BoiteAOutils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace KohonenNetwork
{
    public partial class Form1 : Form
    {
        KohonenMap2D kohonen;

        public Form1()
        {
            InitializeComponent();
        }

        private Color[] Couleurs()
        {
            Color[] couleurs = new Color[] { Color.Purple, Color.Gray, Color.Orange, Color.White, Color.Red, Color.Green, Color.Blue, Color.Yellow };
            return couleurs;
        }

        #region ObtenirCibles
        private double[][] ObtenirCibles()
        {
            double[][] cibles = new double[8][];
            Color[] couleurs = Couleurs();
            for (int i = 0; i < cibles.Length; i++)
            {
                Color couleur = couleurs[i];
                double[] cible = new double[] { couleur.R, couleur.G, couleur.B };
                cibles[i] = cible;
            }

            return cibles;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // Mode random

            // création du réseau
            CreerReseau();
            Calcule.Image = null;
            Position.Image = null;
        }

        private void CreerReseau()
        {
            kohonen = new KohonenMap2D((int)Entrees.Value, (int)X.Value, (int)Y.Value, Pas, Couleurs());
            // une carte aléatoire est toujours initialisée avec un nouveau réseau
            DisplayColor(Origine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // entrainer une fois
            double[][] cibles = ObtenirCibles();
            Random rnd = new Random();
            int position = rnd.Next(cibles.Length);
            double[] cible = cibles[position];
            kohonen.Entrainer(cible, 1, 1);

            DisplayColor(Calcule);
        }

        #region DisplayColor (private)
        private void DisplayColor(PictureBox box, FormeType formeType = FormeType.Rectangle)
        {
            Graphics g = box.CreateGraphics();
            int factorX = (int)box.Width / kohonen.X;
            int factorY = (int)box.Height / kohonen.Y;

            double[][] cibles = ObtenirCibles();
            // recalcule les couleurs
            foreach (KohonenNeurone neurone in kohonen.Grille)
            {
                if (neurone.Couleur == Color.Transparent)
                {
                    kohonen.CalculerCouleurDepuisPoids(neurone, cibles);
                }

                Brush brush = new SolidBrush(neurone.Couleur);
                Pen pen = new Pen(Color.Black, 2);

                if (formeType == FormeType.Rectangle)
                {
                    Rectangle rectangle = new Rectangle(factorX * neurone.X, factorY * neurone.Y, factorX, factorY);
                    g.FillRectangle(brush, rectangle);
                    g.DrawRectangle(pen, rectangle);
                }
                else
                {
                    // inspiré de :
                    // http://www.codeproject.com/Articles/14948/Hexagonal-grid-for-games-and-other-projects-Part
                    // mais les données sont a et b dans notre cas
                }
            }
        }
        #endregion

        private double Pas = 1;

        private void button3_Click(object sender, EventArgs e)
        {
            // on entraine N fois

            int trainNumber = (int)TrainNumber.Value;
            double[][] cibles = ObtenirCibles();

            Progress.Maximum = trainNumber;
            Progress.Minimum = 1;
            Progress.Visible = true;

            for (int i = 1; i <= trainNumber; i++)
            {
                Progress.Value = i;

                for (int c = 0; c < cibles.Length; c++)
                {
                    double[] cible = cibles[c];
                    Color color = Color.FromArgb((int)cible[0], (int)cible[1], (int)cible[2]);
                    Temoin.CreateGraphics().Clear(color);

                    kohonen.Entrainer(cible, trainNumber, i);

                    if (ShowBuild.Checked)
                    {
                        DisplayColor(Calcule);
                    }
                }
            }

            DisplayColor(Calcule);

            // affiche les gagnants des gagnants
            WinnerBitmap(kohonen);
            Progress.Visible = false;
        }

        public void WinnerBitmap(KohonenMap2D kohonen)
        {
            List<KohonenNeurone> neurones = new List<KohonenNeurone>();
            foreach (KohonenNeurone neurone in kohonen.Grille)
            {
                if (neurone.NbGagnant > 0)
                {
                    neurones.Add(neurone);
                }
            }

            neurones = neurones.OrderByDescending(n => n.NbGagnant).ToList();
            List<KohonenNeurone> gagnants = new List<KohonenNeurone>();
            foreach (KohonenNeurone neurone in neurones)
            {
                if (!gagnants.Any(w => w.Couleur == neurone.Couleur))
                {
                    gagnants.Add(neurone);

                    if (gagnants.Count == 8)
                    {
                        break;
                    }
                }
            }

            Graphics g = Position.CreateGraphics();
            int factorX = (int)Position.Width / kohonen.X;
            int factorY = (int)Position.Height / kohonen.Y;

            Brush brush = Brushes.White;
            g.FillRectangle(brush, Position.ClientRectangle);
            foreach (var neurone in gagnants)
            {
                brush = new SolidBrush(neurone.Couleur);
                g.FillRectangle(brush, factorX * neurone.X, factorY * neurone.Y, factorX, factorY);
            }
        }
    }
}
