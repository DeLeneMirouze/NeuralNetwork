#region using
using BoiteAOutils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace MultiCouche
{
    /// <summary>
    /// Implémentation du Perceptron multicouche
    /// </summary>
    /// <remarks>
    /// Le code ne gère que le cas d'une seule couche cachée
    /// Tel qu'il est écrit le code supose que la couche de sortie n'a qu'un seul neurone, mais il n'est pas difficile de corriger le problème
    /// </remarks>
    class Program
    {
        /// <summary>
        /// jeu d'entrainement du réseau
        /// </summary>
        /// <remarks>
        /// Première dimension: chaque jeu de test
        /// Deuxième dimension: pour chaque jeu, liste des valeurs de test
        /// </remarks>
        static double[][] entrainement = null;
        /// <summary>
        /// les cibles pour l'entraînement
        /// </summary>
        static double[][] cibleEntrainement = null;

        /// <summary>
        /// jeu de test
        /// </summary>
        static double[][] tests = null;
        /// <summary>
        /// cibles de test
        /// </summary>
        static double[][] cibleTest = null;

        static void Main(string[] args)
        {
            string selection = null;

            while (selection != "1" && selection != "2")
            {
                Console.Clear();
                Console.WriteLine("1) Cartographie");
                Console.WriteLine("2) Fleurs d'iris");
                Console.WriteLine("Quel jeu de test?");

                selection = Console.ReadLine();
                selection = selection.Trim();
            }

            #region Création
            Console.Clear();
            Console.WriteLine("Création du réseau");
            int[] topologie;
            if (selection == "1")
            {
                topologie = new int[] { 2, 2, 1 };
            }
            else
            {
                topologie = new int[] { 4, 7, 3 };
            }
            DeepNetwork deepNetwork = new DeepNetwork(1, topologie);

            Console.WriteLine("Nb de couches: {0}", deepNetwork.Couches.Length);
            Console.WriteLine("Topologie du réseau: {0}", string.Join("x", topologie));
            int nbPoids = 0;
            for (int i = 1; i < topologie.Length; i++)
            {
                nbPoids += topologie[i] * topologie[i - 1];
            }
            Console.WriteLine("Nombre de poids: {0}", nbPoids);
            Console.WriteLine("Nombre de biais/neurones: {0}", topologie[1] + topologie[topologie.Length - 1]);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            #endregion

            Helper.PoidsAleatoires(deepNetwork, false);

            // vitesse d'apprentissage
            double pas = 0.5;
            double maxErreur = 0.03;
            int maxIteration = 5000;
            if (selection == "1")
            {
                Cartographie();
            }
            else
            {
                // iris
                pas = 0.05;
                maxErreur = 0.001;
                maxIteration = 500;
                Iris();
            }

            deepNetwork.Calculer(entrainement[0]);
            Helper.AfficherReseau(deepNetwork);

            // entraînement
            Console.WriteLine("Entraînement du réseau sur {0} tests et {1} itérations max", entrainement.Length, maxIteration);
            Console.WriteLine("Pas: {0}", pas);
            double erreur = deepNetwork.Entrainer(entrainement, cibleEntrainement, maxIteration, pas, maxErreur);
            Console.WriteLine("Erreur en fin d'apprentissage: {0}", erreur);
            Helper.AfficherReseau(deepNetwork);

            // jeu de test
            Couche coucheSortie = deepNetwork.Couches[2];
            for (int i = 0; i < tests.Length; i++)
            {
                deepNetwork.Calculer(tests[i]);

                Console.WriteLine("Test {0}", i);
                Helper.AfficherVecteur(tests[i], "Entrées:");

                Helper.AfficherSorties(coucheSortie.Neurones, "Calculé: ");
                Helper.AfficherVecteur(cibleTest[i], "Cible:");
                Console.WriteLine();
            }

            Console.WriteLine("Terminé");
            Console.ReadLine();
        }

        #region Iris (private)
        private static void Iris()
        {
            // http://archive.ics.uci.edu/ml/datasets/Iris
            // https://visualstudiomagazine.com/Articles/2013/09/01/Neural-Network-Training-Using-Back-Propagation.aspx?Page=1

            List<Iris> listeIris = ChargerIris();

            // jeu de test: 2 éléments de chaque classe
            List<Iris> listeTest = listeIris.Where(i => i.Espece == "Iris-virginica").Take(2).ToList();
            listeTest.AddRange(listeIris.Where(i => i.Espece == "Iris-setosa").Take(2).ToList());
            listeTest.AddRange(listeIris.Where(i => i.Espece == "Iris-versicolor").Take(2).ToList());

            // on les supprime du jeu d'entrainement
            List<Iris> training = listeIris.Where(i => i.Espece == "Iris-virginica").Skip(2).ToList();
            training.AddRange(listeIris.Where(i => i.Espece == "Iris-setosa").Skip(2).ToList());
            training.AddRange(listeIris.Where(i => i.Espece == "Iris-versicolor").Skip(2).ToList());

            // monte les données pour le réseau
            // entrée:
            // dans cet ordre: PetaleLargeur,PetaleLongueur,SepaleLargeur,SepaleLongueur
            // sortie:
            // 1,0,0 => Iris-virginica
            // 0,0,1 => Iris-setosa
            // 0,1,0 => Iris-versicolor

            entrainement = new double[training.Count][];
            cibleEntrainement = new double[training.Count][];
            for (int i = 0; i < training.Count; i++)
            {
                Iris iris = training[i];
                entrainement[i] = new double[] { iris.PetaleLargeur, iris.PetaleLongueur, iris.SepaleLargeur, iris.SepaleLongueur };

                if (iris.Espece == "Iris-virginica") // Iris-virginica
                {
                    cibleEntrainement[i] = new double[] { 1, 0, 0 };
                }
                if (iris.Espece == "Iris-versicolor") // Iris-versicolor
                {
                    cibleEntrainement[i] = new double[] { 0, 1, 0 };
                }
                if (iris.Espece == "Iris-setosa") // Iris-setosa
                {
                    cibleEntrainement[i] = new double[] { 0, 0, 1 };
                }
            }

            tests = new double[listeTest.Count][];
            cibleTest = new double[listeTest.Count][];
            for (int i = 0; i < listeTest.Count; i++)
            {
                Iris iris = listeTest[i];
                tests[i] = new double[] { iris.PetaleLargeur, iris.PetaleLongueur, iris.SepaleLargeur, iris.SepaleLongueur };

                if (iris.Espece == "Iris-virginica")
                {
                    cibleTest[i] = new double[] { 1, 0, 0 };
                }
                if (iris.Espece == "Iris-versicolor")
                {
                    cibleTest[i] = new double[] { 0, 1, 0 };
                }
                if (iris.Espece == "Iris-setosa")
                {
                    cibleTest[i] = new double[] { 0, 0, 1 };
                }
            }
        }
        #endregion

        #region Cartographie (private)
        private static void Cartographie()
        {
            // http://dynamicnotions.blogspot.fr/2008/09/single-layer-perceptron.html
            // (2x2x1)

            List<Carte> cartes = ChargerZones();
            int testSize = 5;
            int testGap = 11;

            entrainement = new double[cartes.Count - testSize][];
            cibleEntrainement = new double[cartes.Count - testSize][];

            tests = new double[testSize][];
            cibleTest = new double[testSize][];

            for (int i = 0; i < cartes.Count; i++)
            {
                if (i < testGap)
                {
                    entrainement[i] = new double[] { cartes[i].X, cartes[i].Y };
                    cibleEntrainement[i] = new double[] { cartes[i].Cible };
                }
                else if (i > testGap + testSize - 1)
                {
                    entrainement[i - testSize] = new double[] { cartes[i].X, cartes[i].Y };
                    cibleEntrainement[i - testSize] = new double[] { cartes[i].Cible };
                }
                else
                {
                    tests[i - testGap] = new double[] { cartes[i].X, cartes[i].Y };
                    cibleTest[i - testGap] = new double[] { cartes[i].Cible };
                }
            }

            //entrainement = new double[1][] { donnees[0] };
            //cibleEntrainement = new double[] { cibles[0] };
        } 
        #endregion

        #region ChargerZones (private)
        /// <summary>
        /// Charge le fichier des lettres
        /// </summary>
        private static List<Carte> ChargerZones()
        {
            // lecture du jeu complet
            List<Carte> cartes = new List<Carte>();

            using (StreamReader reader = File.OpenText(@"zones2.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string ligne = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }

                    string[] splitted = ligne.Split(';');
                    Carte carte = new Carte();

                    carte.X = Convert.ToDouble(splitted[0]);
                    carte.Y = Convert.ToDouble(splitted[1]);
                    carte.Cible = Convert.ToDouble(splitted[2]);

                    cartes.Add(carte);

                } // while
            }

            return cartes;
        }
        #endregion

        #region ChargerIris (private)
        /// <summary>
        /// Charge le fichier des données Iris Flower
        /// </summary>
        private static List<Iris> ChargerIris()
        {
            // lecture du jeu complet
            List<Iris> cartes = new List<Iris>();

            using (StreamReader reader = File.OpenText(@"iris.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string ligne = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }

                    string[] splitted = ligne.Split(';');
                    Iris iris = new Iris();

                    iris.SepaleLongueur = Convert.ToDouble(splitted[0]);
                    iris.SepaleLargeur = Convert.ToDouble(splitted[1]);
                    iris.PetaleLongueur = Convert.ToDouble(splitted[2]);
                    iris.PetaleLargeur = Convert.ToDouble(splitted[3]);
                    iris.Espece = splitted[4];

                    cartes.Add(iris);

                } // while
            }

            return cartes;
        }
        #endregion
    }
}
