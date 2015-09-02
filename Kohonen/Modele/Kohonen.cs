#region using
using BoiteAOutils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace KohonenSOM.Modele
{
    public class Kohonen 
    {
        #region Constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nbEntrees">Taille de la couche d'entrée</param>
        /// <param name="x">Dimension de la carte</param>
        /// <param name="y">Dimension de la carte</param>
        public Kohonen(int nbEntrees, int x, int y)
        {
            NbEntrees = nbEntrees;
            Initialiser(nbEntrees, x, y);
        } 
        #endregion

        /// <summary>
        /// Carte
        /// </summary>
        public Neurone[,] Grille { get; set; }
        /// <summary>
        /// Taille de la couche d'entrée
        /// </summary>
        private int NbEntrees { get; set; }
        /// <summary>
        /// Rayon du réseau
        /// </summary>
        private double  RayonCarte { get; set; }

        #region Entrainer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entree"></param>
        public void Entrainer(double[] entree,int iteration)
        {
            // recherche du meilleur neurone
            Neurone gagnant = RechercherGagnant(entree);


            // récupère le voisinage du gagnant
            CalculerRayon(gagnant, iteration, 100);






        }
        #endregion

        #region CalculerRayon (private)
        private void CalculerRayon(Neurone neuroneCible,int totalIteration, int iterationCourante)
        {
            // on commence par calculer le rayon
            double lambda = totalIteration / Math.Log(RayonCarte);
            double rayon = RayonCarte * Math.Exp(-1 * iterationCourante / lambda);

            // on recherche les neurones présents dans le rayon
            // leurs poids seront ajustés

            foreach (Neurone neurone in Grille)
            {

            }

        }
        #endregion

        #region RechercherGagnant (private)
        private Neurone RechercherGagnant(double[] entree)
        {
            double distanceMin = double.MaxValue;
            Neurone gagnant = null;
            for (int n = 0; n < Grille.Length; n++)
            {
                Neurone neurone = Grille.GetValue(n) as Neurone;

                // pour chaque neurone on calcule sa distance euclidienne avec l'entrée
                double somme = 0.0;
                for (int i = 0; i < NbEntrees; i++)
                {
                    double ecart = entree[i] - neurone.Poids[i];
                    somme += ecart * ecart;
                }

                double distance = Math.Sqrt(somme);

                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    gagnant = neurone;
                }
            }

            return gagnant;
        } 
        #endregion

        #region Initialiser (private)
        private void Initialiser(int nbEntrees, int x, int y)
        {
            Grille = new Neurone[x, y];

            Random rnd = new Random();

            for (int i = 0; i < Grille.Length; i++)
            {
                Neurone neurone = new Neurone(nbEntrees, null);
                for (int p = 0; p < neurone.Poids.Length; p++)
                {
                    neurone.Poids[p] = rnd.NextDouble();
                }
            }

            // rayon du réseau
            RayonCarte = 0.5 * Math.Max(x, y);
        }
        #endregion

        #region ToBitmap 
        /// <summary>
        /// Sauvegarde un réseau de Kohonen 2D dans un bitmap
        /// </summary>
        /// <param name="name">Nom du fichier sans extension ni chemin</param>
        public string ToBitmap(string name)
        {
            int X = Grille.GetLength(0);
            int Y = Grille.GetLength(1);

            Bitmap bitMap = new Bitmap(X, Y);

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    Neurone neurone = Grille[x, y];
                    Color couleur = Color.FromArgb((int)neurone.Poids[0], (int)neurone.Poids[1], (int)neurone.Poids[2]);

                    bitMap.SetPixel(x, y, couleur);
                }
            }

            string modele = @"c:\temp\{0}.bmp";
            string fichier = string.Format(modele, name);
            bitMap.Save(fichier);

            return fichier;
        }
        #endregion
    }
}
