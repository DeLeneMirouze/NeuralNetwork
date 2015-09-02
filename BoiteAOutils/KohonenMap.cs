#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Réseau de Kohonen linéaire avec 2 poids par réseau
    /// </summary>
    public class KohonenMap: KohonenMapBase<KohonenNeurone>
    {
        #region Constructeur
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbPoints">Nombre de points sur la ligne</param>
        /// <param name="pas">Pas d'apprentissage initial</param>
        /// <param name="largeur">Valeur max du poids en X (index 0)</param>
        /// <param name="hauteur">Valeur max du poids en Y (index 1)</param>
        public KohonenMap(int nbPoints, double pas, int largeur, int hauteur)
            :base(nbPoints, pas, largeur, hauteur, 2)
        {
            //RayonCarte = 16;
        }
        #endregion

        #region Entrainer
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

            Pas = PasInitial * Math.Exp(-((double)iterationCourante / iterations));
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
                    neurone.Poids[p] = (int)neurone.Poids[p];

                    if (neurone.Poids[p] < 0)
                    {
                        neurone.Poids[p] = 0;
                    }
                    if (neurone.Poids[p] > LargeurMax && p == 0)
                    {
                        neurone.Poids[p] = LargeurMax;
                    }
                    if (neurone.Poids[p] > HauteurMax && p == 1)
                    {
                        neurone.Poids[p] = HauteurMax;
                    }
                }
            }
        }
        #endregion

        #region RechercherGagnant (private)
        private KohonenNeurone RechercherGagnant(double[] cible)
        {
            double distanceMin = double.MaxValue;
            KohonenNeurone gagnant = null;

            for (int x = 0; x < NbPoints; x++)
            {
                KohonenNeurone neurone = Line[x] as KohonenNeurone;

                // pour chaque neurone on calcule sa distance euclidienne avec l'entrée
                double distance = Helper.Distance(cible, neurone.Poids);
                // est-ce le gagnant?
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    gagnant = neurone;
                }
            }

            gagnant.NbGagnant++;
            return gagnant;
        }
        #endregion

        #region Initialiser (protected)
        protected override void Initialiser()
        {
            Random rnd = new Random();
            Line = new KohonenNeurone[NbPoints];

            for (int i = 0; i < Line.Length; i++)
            {
                KohonenNeurone neurone = new KohonenNeurone(NbEntrees);
                neurone.X = i;

                neurone.Poids[0] = rnd.Next(LargeurMax);
                neurone.Poids[1] = rnd.Next(HauteurMax);

                Line[i] = neurone;
            }
        }
        #endregion

        #region Voisinage (protected)
        protected List<KohonenNeurone> Voisinage(KohonenNeurone gagnant, int totalIteration, int iterationCourante)
        {
            // on commence par calculer le rayon
            //rayon varie entre RayonCarte et 4
            int rayon = RayonCarte - iterationCourante * (RayonCarte - 3) / totalIteration;
            if (rayon < 4)
            {
                rayon = 4;
            }
            Debug.WriteLine("Rayon: " + rayon.ToString() + ", Itération: " + iterationCourante.ToString());

            // on recherche les neurones présents dans le rayon
            // leurs poids seront ajustés
            List<KohonenNeurone> voisinage = new List<KohonenNeurone>();
            for (int x = 0; x < Line.Length; x++)
            {
                if (Math.Abs(x - gagnant.X) < rayon)
                {
                    KohonenNeurone neurone = Line[x];
                    neurone.Distance = Distance(neurone, gagnant);

                    voisinage.Add(neurone);
                }
            }

            Debug.WriteLine("Voisinage: " + voisinage.Count.ToString());
            return voisinage;
        }
        #endregion

        #region Distance (protected)
        protected override double Distance(Neurone v1, Neurone v2)
        {
            ///Distance euclidienne
            double distance = Math.Sqrt(
                ((KohonenNeurone)v1).X - (((KohonenNeurone)v2).X) * (((KohonenNeurone)v1).X - ((KohonenNeurone)v2).X) + (((KohonenNeurone)v1).Y - ((KohonenNeurone)v2).Y) * (((KohonenNeurone)v1).Y - ((KohonenNeurone)v2).Y)
                );
            return distance;
        }
        #endregion

        /// <summary>
        /// Chemin initial
        /// </summary>
        public Point[] CheminInitial { get; set; }

    }
}
