using System;

namespace BoiteAOutils
{
    public static class Helper
    {
        #region AfficherReseau
        /// <summary>
        /// Afficher le contenu d'un réseau
        /// </summary>
        public static void AfficherReseau(Reseau reseau)
        {
            Console.WriteLine();

            // couche d'entrée traitée à part
            Couche couche = reseau.Couches[0];
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Couche entrée ({0} neurones)", couche.Neurones.Length);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sortie:");

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Neurone neurone in couche.Neurones)
            {
                Console.WriteLine(neurone.Sortie.ToString("+0.0000;-0.0000"));
                Console.WriteLine();
            }
            Console.ResetColor();

            for (int i = 1; i < reseau.Couches.Length; i++)
            {
                couche = reseau.Couches[i];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couche {0} ({1} neurones)", i , couche.Neurones.Length);
                Console.ResetColor();
                Console.Write("Poids, ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("biais, ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("somme, ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("sortie, ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Gradiant:");

                Console.ResetColor();
                foreach (Neurone neurone in couche.Neurones)
                {
                    for (int n = 0; n < neurone.Poids.Length; n++)
                    {
                        Console.Write("{0} ", neurone.Poids[n].ToString("+0.0000;-0.0000"));
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("{0} ", neurone.Biais.ToString("+0.0000;-0.0000"));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0} ", neurone.Somme.ToString("+0.0000;-0.0000"));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("{0} ", neurone.Sortie.ToString("+0.0000;-0.0000"));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(neurone.Erreur.ToString("+0.0000;-0.0000"));

                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }
        #endregion

        #region AfficherSorties
        public static void AfficherSorties(Neurone[] neurones, string titre)
        {
            string sortie = titre;
            foreach (Neurone neurone in neurones)
            {
                sortie += neurone.Sortie.ToString("-0.0,0.0") + ",";
            }

            Console.WriteLine(sortie.Trim(','));
        } 
        #endregion

        #region AfficherVecteur
        /// <summary>
        /// Affiche un vecteur fournit
        /// </summary>
        /// <param name="vecteur"></param>
        /// <param name="libelle"></param>
        public static void AfficherVecteur(double[] vecteur, string libelle)
        {
            Console.WriteLine(libelle);
            for (int i = 0; i < vecteur.Length; i++)
            {
                Console.Write("{0} ", vecteur[i].ToString("0.00000;-0.00000"));
            }
            Console.WriteLine();
        }
        #endregion

        #region PoidsAleatoires
        /// <summary>
        /// Règle une valeur aléatoire comprise entre -1 et +1 pour les poids des couches du réseau autres que la couche d'entrée
        /// </summary>
        /// <param name="reseau"></param>
        public static void PoidsAleatoires(Reseau reseau, bool bias)
        {
            Random rnd = new Random();

            // on laisse les valeurs par défaut (1) des poids de la couche d'entrée
            // d'ou le démarrage à 1
            for (int c = 1; c < reseau.Couches.Length; c++)
            {
                Couche couche = reseau.Couches[c];
                foreach (Neurone neurone in couche.Neurones)
                {
                    for (int i = 0; i < neurone.Poids.Length; i++)
                    {
                        neurone.Poids[i] = 2 * rnd.NextDouble() - 1;
                        if (bias)
                        {
                            neurone.Biais = 2 * rnd.NextDouble() - 1;
                        }
                    }
                }
            }
        }
        #endregion

        #region Distance
        /// <summary>
        /// Calcule la distance entre deux vecteurs
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double Distance(double[] v1, double[] v2)
        {
            double somme = 0.0;
            for (int i = 0; i < v1.Length; i++)
            {
                double ecart = v1[i] - v2[i];
                somme += ecart * ecart;
            }

            double distance = Math.Sqrt(somme);

            return distance;
        }
        #endregion

        #region NormaliserCouleur 
        /// <summary>
        /// Normalise un entier dans le segment [0,255] en un double dans le segment [-1,1]
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        public static double NormaliserCouleur(int valeur)
        {
            valeur = 2 * valeur / 255 - 1;
            return valeur;
        }
        #endregion
    }
}
