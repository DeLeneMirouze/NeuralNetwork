#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
#endregion

namespace BoiteAOutils
{
    public static class FonctionsAttenuation
    {
        #region ChapeauFrancais
        public static double ChapeauFrancais(double distance, double largeur)
        {
            double calcul = 0;
            if (distance <= largeur)
            {
                calcul = 1;
            }
            else if (distance < largeur && distance <= 3 * largeur)
            {
                calcul = -1 / 3;
            }
            else if (distance > 3 * largeur)
            {
                calcul = 0;
            }

            return calcul;
        }
        #endregion

        #region ChapeauMexicain
        public static double ChapeauMexicain(double distance, double rayonCarte, int totalIteration, int iterationCourante)
        {
            double sigma = Sigma(rayonCarte, totalIteration, iterationCourante);
            double calcul = Math.Exp(-(distance * distance) / Math.Pow(sigma, 2)) * (1 - (2 / Math.Pow(sigma, 2)) * (distance * distance));
            return calcul;
        }
        #endregion

        #region Gaus
        public static double Gaus(double distance, double rayonCarte, int totalIteration, int iterationCourante)
        {
            double rayon = Sigma(rayonCarte, totalIteration, iterationCourante);
            double ratio = distance * distance / (2 * rayon * rayon);
            double calcul = Math.Exp(-1 * ratio);
            return calcul;
        } 
        #endregion

        #region Sigma (private)
        public static double Sigma(double rayonCarte, int totalIteration, int iterationCourante)
        {
            double lambda = (double)totalIteration / Math.Log(rayonCarte);
            double sigma = rayonCarte * Math.Exp(-1 * (double)iterationCourante / lambda);

            return sigma;
        }
        #endregion
    }
}
