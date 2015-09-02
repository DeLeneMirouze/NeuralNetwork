#region using
using BoiteAOutils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Perceptron
{
    class Program
    {
        // définition de la matrice d'encodage des lettres
        const int Lignes = 5;
        const int Colonnes = 6;

        static void Main(string[] args)
        {
            #region Création
            Console.WriteLine("Création du Perceptron");
            //Perceptron perceptron = new Perceptron(FonctionsTransfert.Escalier, 0, Colonnes * Lignes);
            //Perceptron perceptron = new Perceptron(FonctionsTransfert.Signe, 0, Colonnes * Lignes);
            Perceptron perceptron = new Perceptron(FonctionsTransfert.Sigmoide, 0.5, Colonnes * Lignes);
            Console.WriteLine("Nombre de neurones: {0}", perceptron.Couches[0].Neurones.Length);
            Console.WriteLine("Biais initial: {0}", perceptron.Couches[0].Neurones[0].Biais);
            Console.WriteLine();
            #endregion

            // Lettre pour laquelle le réseau est entraîné
            string lettreTest = "B";

            #region obtient le jeu d'entrainement
            Dictionary<string, double[]> entrainement = ChargerAlphabet();
            //Dictionary<string, int[]> entrainement = ChargerLettre(lettreTest);
            // affiche ce qui a été chargé
            foreach (string lettre in entrainement.Keys)
            {
                Afficher(entrainement[lettre]);
            }
            #endregion

            #region Entraînement
            int maxIteration = 100;
            double pas = 0.05;
            Console.WriteLine("Après entraînement");
            Console.WriteLine("Pas d'apprentissage: {0}, nombre max d'itégrations: {1}", pas, maxIteration);
            // on entraîne le réseau pour reconnaître la lettre B
            double[] erreur = perceptron.Entrainer(entrainement, lettreTest, maxIteration, pas, 0.05);
            Console.WriteLine("Erreur: {0}", erreur[0]);
            Console.WriteLine("Biais: {0}", perceptron.Couches[0].Neurones[0].Biais);
            Console.WriteLine("Poids synaptiques");

            Neurone neurone = perceptron.Couches[0].Neurones[0];
            for (int l = 0; l < Lignes; l++)
            {
                for (int c = 0; c < Colonnes; c++)
                {
                    Console.Write(neurone.Poids[c + l * Colonnes].ToString("+0.000;-0.000") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            #endregion

            while (true)
            {
                Console.WriteLine("Test du réseau");
                // On construit des jeux d'essai avec un bruit de plus en plus important

                for (int entropie = 0; entropie < 50; entropie++)
                {
                    double[] vecteur = new double[Lignes * Colonnes];
                    Array.Copy(entrainement[lettreTest], vecteur, vecteur.Length);

                    double[] test = Degrader(vecteur, entropie);
                    bool prediction = perceptron.Predire(test);

                    Afficher(test);
                    Console.WriteLine("Entropie: {0}", entropie);
                    Console.WriteLine("Reconnu?: {0}", prediction);
                    Console.WriteLine();

                    if (!prediction)
                    {
                        break;
                    }
                }

                Console.WriteLine("Recommencer? (O/N)");
                string ligne = Console.ReadLine();
                if (ligne == "N" || ligne == "n")
                {
                    break;
                }

                Console.Clear();
            }

            Console.WriteLine("Terminé!");
        }

        #region Degrader
        /// <summary>
        /// Altère aléatoirement un  vecteur
        /// </summary>
        /// <param name="vecteur">Vecteur</param>
        /// <param name="modifier">Nombre de case à modifier au hasard</param>
        /// <returns></returns>
        private static double[] Degrader(double[] vecteur, int modifier)
        {
            Random rnd = new Random(DateTime.Now.GetHashCode());

            // sélectionne 'modifier' cases dans le 'vecteur'
            List<int> candidats = new List<int>();
            while (modifier > 0)
            {
                int candidat = rnd.Next(0, Lignes * Colonnes);
                if (!candidats.Any(c => c == candidat))
                {
                    candidats.Add(candidat);
                    modifier--;
                }
            }
            foreach (int item in candidats)
            {
                vecteur[item] = (vecteur[item] == 0) ? 1 : 0;
            }

            return vecteur;
        }
        #endregion

        #region ChargerLettre (private)
        private static Dictionary<string, double[]> ChargerLettre(string lettreTest)
        {
            Dictionary<string, double[]> alphabet = ChargerAlphabet();
            double[] lettre = new double[Lignes * Colonnes];
            Array.Copy(alphabet[lettreTest], lettre, lettre.Length);

            int modifier = 1;
            Dictionary<string, double[]> retour = new Dictionary<string, double[]>();
            foreach (string item in alphabet.Keys)
            {
                if (item == lettreTest)
                {
                    retour[item] = lettre;
                    continue;
                }
                double[] copie = new double[lettre.Length];
                Array.Copy(lettre, copie, copie.Length);
                retour[item] = Degrader(copie, modifier);

                modifier++;
            }

            return retour;
        } 
        #endregion

        #region ChargerAlphabet (private)
        /// <summary>
        /// Charge le fichier des lettres
        /// </summary>
        private static Dictionary<string, double[]> ChargerAlphabet()
        {
            // chaque lettre est modélisée dans une matrice 5x6
            Dictionary<string, double[]> alphabet = new Dictionary<string, double[]>();

            // lecture du jeu complet
            using (StreamReader reader = File.OpenText(@"Alphabet.txt"))
            {
                string lettre = null;
                int numeroLigne = 0;

                while (!reader.EndOfStream)
                {
                    string ligne = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }

                    if (ligne.Trim() == "STOP")
                    {
                        break;
                    }

                    if (ligne.Trim().StartsWith("="))
                    {
                        // nom de la lettre qui est encodée plus loin

                        ligne = ligne.Trim();
                        string[] splitted = ligne.Split('>');
                        lettre = splitted[splitted.Length - 1].Trim();
                        alphabet[lettre] = new double[Lignes * Colonnes];
                        numeroLigne = 0;
                        continue; ;
                    }

                    for (int i = 0; i < ligne.Length; i++)
                    {
                        alphabet[lettre][i + numeroLigne * Colonnes] = (ligne[i] == 'X') ? 1 : 0;
                    }
                    numeroLigne++;
                } // while
            }

            return alphabet;
        }
        #endregion

        #region Afficher (private)
        /// <summary>
        /// Affiche une lettre
        /// </summary>
        /// <param name="data"></param>
        static void Afficher(double[] data)
        {
            for (int ligne = 0; ligne < Lignes; ligne++)
            {
                for (int colonne = 0; colonne < Colonnes; colonne++)
                {
                    double code = data[ligne * Colonnes + colonne];
                    string encodage = (code == 1) ? "X" : " ";
                    Console.Write(encodage);
                }
                Console.WriteLine();
            }
       
            Console.WriteLine();
        }
        #endregion
    }
}
