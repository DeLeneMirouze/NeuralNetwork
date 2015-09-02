#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Implémentation d'un réseau de Kohonen 2D
    /// </summary>
    public sealed class KohonenMap2D
    {
        #region Constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="pas">Pas d'apprentissage initial</param>
        /// <param name="nbEntrees">Taille de la couche d'entrée</param>
        /// <param name="couleurs">Couleurs pour initialiser la carte</param>
        /// <param name="x">Dimension de la carte</param>
        /// <param name="y">Dimension de la carte</param>
        public KohonenMap2D(int nbEntrees, int x, int y, double pas, Color[] couleurs)
        {
            X = x;
            Y = y;
            NbEntrees = nbEntrees;
            PasInitial = pas;
            Pas = pas;
            Initialiser(nbEntrees, x, y, couleurs);
        }
        #endregion

        /// <summary>
        /// Valeur initiale du pas d'apprentissage
        /// </summary>
        private double PasInitial { get; set; }
        private double Pas { get; set; }

        /// <summary>
        /// Carte
        /// </summary>
        public KohonenNeurone[,] Grille { get; set; }
        /// <summary>
        /// Taille de la couche d'entrée
        /// </summary>
        public int NbEntrees { get; private set; }
        /// <summary>
        /// Rayon du réseau
        /// </summary>
        private double RayonCarte { get; set; }

        /// <summary>
        /// Bitmap originel
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// Nombre de neurones en X
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Nombre de neurones en Y
        /// </summary>
        public int Y { get; private set; }

        #region Entrainer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cible">Jeux d'essai</param>
        /// <param name="iterationCourante">Itération courante</param>
        /// <param name="iterations">Nombre d'itération prévues pour la phase d'entrainement</param>
        public void Entrainer(double[] cible, int iterations, int iterationCourante)
        {
            // recherche du meilleur neurone
            KohonenNeurone gagnant = RechercherGagnant(cible);

            // récupère le voisinage du gagnant
            List<KohonenNeurone> voisinage = Voisinage(gagnant, iterations, iterationCourante);
            if (voisinage.Count == 0)
            {
                return;
            }

            // ajuste les poids
            AjusterPoids(voisinage, cible, iterations, iterationCourante);

            Pas = PasInitial * Math.Exp(-(iterationCourante / iterations));
        }
        #endregion

        #region AjusterPoids (private)
        /// <summary>
        /// Ajuste les poids de la carte
        /// </summary>
        /// <param name="voisinage">Voisinage du réseau gagnant pour l'itération en cours</param>
        /// <param name="iterations">Nombre max d'itérations prévues</param>
        /// <param name="iterationCourante">Iteration en cours</param>
        /// <param name="entrees">Entrées du réseau</param>
        private void AjusterPoids(IEnumerable<KohonenNeurone> voisinage, double[] entrees, int iterations, int iterationCourante)
        {
            foreach (KohonenNeurone neurone in voisinage)
            {
                // le facteur d'atténuation (diminue avec la distance au neurone gagnant)
                double facteur = FonctionsAttenuation.Gaus(neurone.Distance, RayonCarte, iterations, iterationCourante);
                for (int p = 0; p < neurone.Poids.Length; p++)
                {
                    double ecart = facteur * Pas * (entrees[p] - neurone.Poids[p]);
                    neurone.Poids[p] += ecart;
                }

                neurone.Couleur = Color.Transparent;
            }
        }
        #endregion

        #region Voisinage (private)
        /// <summary>
        /// Renvoie le voisinage du gagnant
        /// </summary>
        /// <param name="gagnant"></param>
        /// <param name="totalIteration"></param>
        /// <param name="iterationCourante"></param>
        /// <returns></returns>
        private List<KohonenNeurone> Voisinage(KohonenNeurone gagnant, int totalIteration, int iterationCourante)
        {
            // on commence par calculer le rayon
            double rayon = FonctionsAttenuation.Sigma(RayonCarte, totalIteration, iterationCourante);

            // on recherche les neurones présents dans le rayon
            // leurs poids seront ajustés
            List<KohonenNeurone> voisinage = new List<KohonenNeurone>();
            for (int x = 0; x < Grille.GetLength(0); x++)
            {
                for (int y = 0; y < Grille.GetLength(1); y++)
                {
                    KohonenNeurone neurone = Grille[x, y];
                    neurone.Distance = Distance(neurone, gagnant);

                    if (neurone.Distance <= rayon)
                    {
                        voisinage.Add(neurone);
                    }
                }
            }

            Debug.WriteLine("Voisinage: " + voisinage.Count.ToString());
            return voisinage;
        }
        #endregion

        #region RechercherGagnant (private)
        private KohonenNeurone RechercherGagnant(double[] cible)
        {
            double distanceMin = double.MaxValue;
            KohonenNeurone gagnant = null;

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    KohonenNeurone neurone = Grille[x,y] as KohonenNeurone;

                    // pour chaque neurone on calcule sa distance euclidienne avec l'entrée
                    double distance = Helper.Distance(cible, neurone.Poids);
                    // est-ce le gagnant?
                    if (distance < distanceMin )
                    {
                        distanceMin = distance;
                        gagnant = neurone;
                    }
                }
            }

            gagnant.NbGagnant++;
            return gagnant;
        }
        #endregion

        #region Distance (private)
        private double Distance(KohonenNeurone v1, KohonenNeurone v2)
        {
            ///Distance euclidienne
            double distance = Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
            return distance;
        }
        #endregion

        #region Initialiser (private)
        private void Initialiser(int nbEntrees, int x, int y, Color[] couleurs)
        {
            Grille = new KohonenNeurone[x, y];
            Random rnd = new Random();

            for (int cx = 0; cx < x; cx++)
            {
                for (int cy = 0; cy < y; cy++)
                {
                    KohonenNeurone neurone = new KohonenNeurone(nbEntrees);
                    neurone.X = cx;
                    neurone.Y = cy;

                    if (couleurs.Length != 0)
                    {
                        int position = rnd.Next(couleurs.Length);
                        Color couleur = couleurs[position];

                        neurone.Couleur = couleur;
                        neurone.Poids[0] = Helper.NormaliserCouleur(neurone.Couleur.R);
                        neurone.Poids[1] = Helper.NormaliserCouleur(neurone.Couleur.G);
                        neurone.Poids[2] = Helper.NormaliserCouleur(neurone.Couleur.B);
                    }
                    else
                    {
                        neurone.Poids[0] = Helper.NormaliserCouleur(rnd.Next(255) / 255);
                        neurone.Poids[1] = Helper.NormaliserCouleur(rnd.Next(255) / 255);
                        neurone.Poids[2] = Helper.NormaliserCouleur(rnd.Next(255) / 255);

                        CalculerCouleurDepuisPoids(neurone);
                    }

                    Grille[cx, cy] = neurone;
                }
            }

            // rayon du réseau
            RayonCarte = 0.5 * Math.Max(x, y);
        }
        #endregion

        #region CalculerCouleurDepuisPoids
        public void CalculerCouleurDepuisPoids(KohonenNeurone neurone)
        {
            // on suppose que l'entrée a une taille de 3, sinon faut trouver un autre algo

            // & 0xff pour le cas où le poids devient plus grand que 1
            int r = (int)(255 * neurone.Poids[0]);
            int g = (int)(255 * neurone.Poids[1]);
            int b = (int)(255 * neurone.Poids[2]);
            neurone.Couleur = Color.FromArgb(r & 0xff, g & 0xff, b & 0xff);
        }

        public void CalculerCouleurDepuisPoids(KohonenNeurone neurone, double[][] couleurs)
        {
            CalculerCouleurDepuisPoids(neurone);

            double plusProche = double.MaxValue;
            double[] meilleureCouleur = null;

            for (int i = 0; i < couleurs.Length; i++)
            {
                double distance = Helper.Distance(couleurs[i], neurone.Poids);
                if (distance < plusProche)
                {
                    plusProche = distance;
                    meilleureCouleur = couleurs[i];
                }
            }

            neurone.Couleur = Color.FromArgb((int)meilleureCouleur[0], (int)meilleureCouleur[1], (int)meilleureCouleur[2]);
        }
        #endregion
    }
}
