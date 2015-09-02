#region using
using BoiteAOutils;
using System;
using System.Diagnostics;
using System.Linq;
#endregion

namespace MultiCouche
{
    /// <summary>
    /// Réseau multicouche 
    /// </summary>
    sealed class DeepNetwork : Reseau
    {
        #region Constructor
        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="nbEntrees">Taille de la matrice d'entrée</param>
        /// <param name="topologie">Topologie du réseau</param>
        public DeepNetwork(int nbEntrees, params int[] topologie)
            : base(FonctionsTransfert.Sigmoide, 0.5, FonctionsTransfert.SigmoideDerivee, nbEntrees, topologie)
        {

        }
        #endregion

        #region Calculer
        /// <summary>
        /// Evaluer une entrée fournie par le réseau
        /// </summary>
        /// <param name="jeuEssai"></param>
        public void Calculer(double[] jeuEssai)
        {
            // on en fait une copie locale
            double[] entraineur = new double[jeuEssai.Length];
            Array.Copy(jeuEssai, entraineur, jeuEssai.Length);

            // première dimension: une valeur par neurone de la couche
            // deuxième dimension: le vecteur des entrées appliquée au neurone
            double[][] testeurs = new double[Couches[0].Neurones.Length][];
            for (int i = 0; i < Couches[0].Neurones.Length; i++)
            {
                testeurs[i] = new double[] { entraineur[i] };
            }

            foreach (Couche couche in Couches)
            {
                if (testeurs == null)
                {
                    // on réamorce la boucle de calcul
                    testeurs = new double[couche.Neurones.Length][];
                    for (int i = 0; i < couche.Neurones.Length; i++)
                    {
                        // les neurones de chaque couche interne reçoivent le même jeu d'entrée contrairement aux neurones de la couche d'entrée
                        testeurs[i] = entraineur;
                    }
                }

                for (int n = 0; n < couche.Neurones.Length; n++)
                {
                    Neurone neurone = couche.Neurones[n];
                    neurone.CalculerSortie(testeurs[n]);
                }

                // on récupère les sorties des neurones de la couche courante
                entraineur = new double[couche.Neurones.Length];
                for (int i = 0; i < couche.Neurones.Length; i++)
                {
                    Neurone neurone = couche.Neurones[i];
                    entraineur[i] = neurone.Sortie;
                }

                testeurs = null;
            }
        }
        #endregion

        #region Entrainer
        /// <summary>
        /// Entraine le réseau
        /// </summary>
        /// <param name="maxIteration">Nombre d'itérations max</param>
        /// <param name="pas">Pas d'apprentissage</param>
        /// <param name="jeuEssai">Jeu d'essai pour entraîner le réseau. Il contient la valeur à appliquer à chaque neurone de la couche d'entrée</param>
        /// <param name="biais">Biais initial</param>
        /// <returns>L'erreur du réseau à la fin de son entraînement</returns>
        public double Entrainer(double[][] tests, double[][] cibles, int maxIteration, double pas, double maxErreur)
        {
            // Note: pour la suite on supposera qu'il n'y a qu'une seule couche cachée

            // inspiré de:
            // https://www4.rgu.ac.uk/files/chapter3%20-%20bp.pdf
            // https://www.youtube.com/watch?v=I2I5ztVfUSE

            Couche coucheSortie = Couches[Couches.Length - 1];
            Couche coucheCachee = Couches[Couches.Length - 2];
            Couche coucheEntree = Couches[0];
            // erreur faite par le réseau
            double erreurReseau = 0;

            // important: ne pas inverser les deux boucles for
            // sinon on aura au final entraîné le réseau uniquement pour le dernier jeu de test et non pas le jeu complet
            for (int m = 0; m < maxIteration; m++)
            {
                Debug.WriteLine("Itération: " + m.ToString());

                erreurReseau = 0;
                for (int t = 0; t < tests.Length; t++)
                {
                    #region Passe avant
                    // calcul d'un état du réseau (Somme et Sortie)
                    Calculer(tests[t]);
                    //Helper.AfficherReseau(this); 
                    #endregion

                    #region Passe rétro
                    // calcul des gradiants de la couche de sortie
                    CalculerDeltaO(cibles[t]);
                    //CalculerDeltaO(cibles[c]);
                    // calculer des radiants de la couche cachée
                    CalculerDeltaH(coucheCachee, coucheSortie);

                    // recalculer les poids et les biais de la couche de sortie
                    Propager(coucheCachee, coucheSortie, pas);
                    // recalculer les poids et les biais de la couche cachée
                    Propager(coucheEntree, coucheCachee, pas);
                    #endregion

                    //Helper.AfficherReseau(this);
                } // pour chaque test

                // le réseau a t'il convergé?
                for (int t = 0; t < tests.Length; t++)
                {
                    Calculer(tests[t]);

                    // calcule l'erreur qui doit converger vers la valeur cible 'maxErreur'
                    double erreur = 0.0;
                    for (int i = 0; i < coucheSortie.Neurones.Length; i++)
                    {
                        // TODO: en l'état Cibles n'est pas adapté au cas de réseau avec une couche de sortie à plusieurs neurones
                        //double ecart = cibles[c][i] - coucheSortie.Neurones[i].Sortie;
                        double ecart = cibles[t][i] - coucheSortie.Neurones[i].Sortie;
                        erreur += ecart * ecart;
                    }
                    erreurReseau += erreur;
                }
                erreurReseau = 0.5 * Math.Sqrt(erreurReseau / tests.Length);
                //Console.WriteLine("Erreur réseau: {0}", erreurReseau);
                if (erreurReseau <= maxErreur)
                {
                    break;
                }

            } // pour chaque itération

            return erreurReseau;
        }
        #endregion

        #region Propager (private)
        /// <summary>
        /// Propagation des erreurs entre couche1 et couche2 sur les poids et les biais
        /// </summary>
        /// <param name="pas">Pas d'apprentissage</param>
        /// <param name="couche1"></param>
        /// <param name="couche2"></param>
        /// <remarks>
        /// couche1 => couche2
        /// </remarks>
        private void Propager(Couche couche1, Couche couche2, double pas)
        {
            for (int c2 = 0; c2 < couche2.Neurones.Length; c2++)
            {
                Neurone neurone2 = couche2.Neurones[c2];
                for (int c1 = 0; c1 < couche1.Neurones.Length; c1++)
                {
                    neurone2.Poids[c1] += pas * neurone2.Erreur * couche1.Neurones[c1].Sortie;
                }

                neurone2.Biais += pas * neurone2.Erreur;
            }
        } 
        #endregion

        #region CalculerDeltaH (private)
        /// <summary>
        /// Calcule de l'erreur (gradiant local) des neurones des couches cachées
        /// </summary>
        /// <param name="couche1">Couche cachée N</param>
        /// <param name="couche2">Couche N + 1</param>
        /// <returns></returns>
        private void CalculerDeltaH(Couche couche1, Couche couche2)
        {
            for (int k = 0; k < couche1.Neurones.Length; k++)
            {
                Neurone neuroneH = couche1.Neurones[k];

                double somme = 0.0;
                for (int i = 0; i < couche2.Neurones.Length; i++)
                {
                    Neurone neurone2 = couche2.Neurones[i];
                    somme += neurone2.Poids[k] * neurone2.Erreur;
                }

                neuroneH.Erreur = neuroneH.TransfertDerivee(neuroneH.Somme + neuroneH.Biais) * somme;
                //Debug.WriteLine("Gradiant H: " + neuroneH.Erreur.ToString());
            }
        }
        #endregion

        #region CalculerDeltaO (private)   
        /// <summary>
        /// Calcule des erreurs (gradiant local) des neurones de la couche de sortie
        /// </summary>
        /// <param name="tests">Jeu de tests</param>
        /// <param name="cible">Cible (valeur désirée) pour chaque jeu de test</param>
        private void CalculerDeltaO(double[] cible)
        {
            Couche coucheSortie = Couches[Couches.Length - 1];
            for (int k = 0; k < coucheSortie.Neurones.Length; k++)
            {
                Neurone neuroneO = coucheSortie.Neurones[k];
                double ecart = 0.0;

                ecart = cible[k] - neuroneO.Sortie;
                neuroneO.Erreur = neuroneO.TransfertDerivee(neuroneO.Somme + neuroneO.Biais) * ecart;
                //Debug.WriteLine("Gradiant O: " + neuroneO.Erreur.ToString());
            }
        }
        #endregion

        #region Initialiser (protected)
        /// <summary>
        /// Initialiser le réseau
        /// </summary>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="nbEntrees">Nb d'entrées des neurones de la couche d'entrée</param>
        /// <param name="transfertDerivee">Null ou dérivée de la fonction de transfert</param>
        /// <param name="seuil">Seuil de la fonction de transfert de la couche</param>
        /// <param name="topologie">Topologie du réseau</param>
        protected override void Initialiser(Func<double, double> transfert, double seuil, Func<double, double> transfertDerivee, int nbEntrees, int[] topologie)
        {
            Couches = new Couche[topologie.Length];

            // couche d'entrée
            Couches[0] = new Couche(topologie[0], nbEntrees, FonctionsTransfert.Identite, 0);
            foreach (Neurone neurone in Couches[0].Neurones)
            {
                for (int i = 0; i < neurone.Poids.Length; i++)
                {
                    neurone.Poids[i] = 1;
                }
            }

            // couches cachées
            for (int c = 1; c < Couches.Length - 1; c++)
            {
                //Couches[c] = new Couche(topologie[c], Couches[c - 1].Neurones.Length, FonctionsTransfert.HyperTan, 0, FonctionsTransfert.HyperTanDerivee);
                Couches[c] = new Couche(topologie[c], Couches[c - 1].Neurones.Length, FonctionsTransfert.Sigmoide, 0.5, FonctionsTransfert.SigmoideDerivee);
                Couches[c].Position = c;
            }

            // couche de sortie
            Couches[Couches.Length - 1] = new Couche(topologie[Couches.Length - 1], Couches[Couches.Length - 2].Neurones.Length, FonctionsTransfert.Sigmoide, 0.5, FonctionsTransfert.SigmoideDerivee);
            Couches[Couches.Length - 1].Position = Couches.Length - 1;
        }
        #endregion
        public override bool Predire(double[] dataVector)
        {
            throw new NotImplementedException();
        }
    }
}
