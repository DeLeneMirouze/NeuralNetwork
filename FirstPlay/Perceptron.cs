#region using
using BoiteAOutils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#endregion

namespace Perceptron
{
    /// <summary>
    /// Description d'un Perceptron
    /// </summary>
    sealed class Perceptron : Reseau
    {
        #region Constructeur
        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="seuil">Seuil de la fonction de transfert</param>
        /// <param name="nbEntrees">Taille de la matrice d'essai</param>
        public Perceptron(Func<double, double> transfert, double seuil, int nbEntrees)
            : base(transfert, seuil, null, nbEntrees, new int[] { 1 })
        {

        }
        #endregion

        #region Entrainer
        /// <summary>
        /// Entraine le réseau
        /// </summary>
        /// <param name="maxIteration">Nombre d'itérations max</param>
        /// <param name="pas">Pas d'apprentissage</param>
        /// <param name="lettreCible">Lettre pour laquelle on entraîne le réseau</param>
        /// <param name="jeuEssai">Jeu d'essai pour entraîner le réseau</param>
        /// <param name="biais">Biais initial</param>
        /// <returns>L'erreur du réseau à la fin de son entraînement</returns>
        public double[] Entrainer(Dictionary<string, double[]> jeuEssai, string lettreCible, int maxIteration, double pas, double biais)
        {
            // on entraîne le réseau
            Neurone neurone = Couches[0].Neurones[0]; // un seul neurone dans le Perceptron

            double erreurCible = 0;
            double[] erreurCourante = new double[] { double.MaxValue };
            int iteration = 0;
            // valeurs initiales
            neurone.Poids = new double[neurone.NbEntrees];
            neurone.Biais = biais;

            while (iteration < maxIteration && erreurCourante[0] > erreurCible)
            {
                // pour chaque élément de test
                foreach (string lettre in jeuEssai.Keys)
                {
                    // récupère le jeu d'entraînement courant
                    double[] entraineur = jeuEssai[lettre];
                    // détermine si c'est la valeur cible (1) ou pas (-1)
                    int valeurCible = (lettre == lettreCible) ? 1 : 0; // la fonction de transfert doit donc produire des -1 ou des 1

                    double sortie = neurone.CalculerSortie(entraineur);
                    // de combien la sortie s'écarte de la cible
                    double ecart = valeurCible - sortie;

                    if (ecart != 0)
                    {
                        // réévalue le poids de chaque entrée (règle de RosenBlatt)
                        for (int p = 0; p < neurone.NbEntrees; p++)
                        {
                            neurone.Poids[p] = neurone.Poids[p] + (pas * ecart * entraineur[p]);
                        }
                        // réévalue le biais
                        // le biais est considéré comme une entrée supplémentaire avec un coefficient toujours égal à 1
                        neurone.Biais = neurone.Biais + (pas * ecart);
                    }
                }

                ++iteration;
                Debug.WriteLine("Itération: " + iteration.ToString());

                // on a un biais et un jeu de poids candidat
                // calcule l'erreur faite par le réseau dans l'estimation de notre jeu d'essai avec notre candidat
                double[][] tests = jeuEssai.Select(jeu => jeu.Value).ToArray();
                double[] cibles = jeuEssai.Select(jeu => (jeu.Key == lettreCible) ? 1.0 : 0.0).ToArray();
                double[][] c = jeuEssai.Select(jeu => cibles).ToArray();
                erreurCourante = CalculerErreur(tests, cibles);
            }

            return erreurCourante;
       }
        #endregion

        #region Predire
        /// <summary>
        /// Effectue une prédiction sur un modèle
        /// </summary>
        /// <param name="dataVector"></param>
        /// <returns>true si le vecteur d'entrée correspond au modèle pour lequel le réseau a été entraîné</returns>
        public override bool Predire(double[] dataVector)
        {
            Neurone neurone = Couches[0].Neurones[0];
            double sortie = neurone.CalculerSortie(dataVector);
            return (sortie > Couches[0].Seuil);
        }
        #endregion

        #region Initialiser (protected)
        /// <summary>
        /// Initialise la couche
        /// </summary>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="transfertDerivee">Nom utilisé</param>
        /// <param name="topologie">Topologie du réseau</param>
        /// <param name="seuil">Seuil de la fonction de transfert</param>
        /// <param name="nbEntrees">Nb d'entrées des neurones de la couche d'entrée</param>
        protected override void Initialiser(Func<double, double> transfert, double seuil, Func<double, double> transfertDerivee, int nbEntrees, int[] topologie)
        {
            Couches = new Couche[1];
            Couches[0] = new Couche(topologie[0], nbEntrees, transfert, seuil);
        }
        #endregion

        #region CalculerErreur (protected)   
        /// <summary>
        /// Cacule la matrice des erreurs effectuées par le réseau sur le jeu de tests fournis
        /// </summary>
        /// <param name="tests">Jeu de tests</param>
        /// <param name="cibles">Cible pour chaque jeu de test</param>
        /// <returns>
        /// Matrice des erreurs contenant l'erreur commise par chaque neurone de la couche de sortie
        /// </returns>
        private double[] CalculerErreur(double[][] tests, double[] cibles)
        {
            Couche coucheSortie = Couches[Couches.Length - 1];
            double[] erreurs = new double[coucheSortie.Neurones.Length];

            for (int n = 0; n < coucheSortie.Neurones.Length; n++)
            {
                Neurone neurone = coucheSortie.Neurones[n];
                double somme = 0.0;

                for (int i = 0; i < tests.Length; i++)
                {
                    double sortie = neurone.CalculerSortie(tests[i]);
                    double ecart = cibles[i] - sortie;
                    // calcule l'erreur faite par le réseau lors de l'estimation de la cible
                    // sur le neurone en cours de la dernière couche
                    somme += ecart * ecart;
                }

                erreurs[n] = 0.5 * somme;
            }

            return erreurs;
        }
        #endregion
    }
}
