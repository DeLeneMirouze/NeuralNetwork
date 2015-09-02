#region using
using System;
#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Bibliothèque de fonctions de transfert
    /// </summary>
    public static class FonctionsTransfert
    {
        #region Tangeante hyperbolique
        /// <summary>
        /// Fonction tangeante hyperbolique
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        /// <remarks>
        /// Seuil: 0
        /// </remarks>
        public static double HyperTan(double valeur)
        {
            if (valeur < -45.0)
            {
                return -1.0;
            }
            else if (valeur > 45.0)
            {
                return 1.0;
            }

            valeur = -2 * valeur;
            double retour = (1 - Math.Pow(Math.E, valeur))/ (1 + Math.Pow(Math.E, valeur));

            return retour;
        }

        /// <summary>
        /// Fonction tangeante dérivée
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        /// <remarks>
        /// Seuil: 0
        /// </remarks>
        public static double HyperTanDerivee(double valeur)
        {
            double calcul = (1 - valeur) * (1 + valeur);
            return calcul;
        } 
        #endregion
        public static double Identite(double valeur)
        {
            return valeur;
        }

        /// <summary>
        /// Signe d'une valeur fournie
        /// </summary>
        /// <param name="valeur">Valeur à évaluer</param>
        /// <returns></returns>
        /// <remarks>
        /// Le seuil est 0
        /// </remarks>
        public static double Signe(double valeur)
        {
            return Math.Sign(valeur);
        }

        #region Escalier
        /// <summary>
        /// Fonction en escalier (Heaviside)
        /// </summary>
        /// <param name="valeur">Valeur à filtrer</param>
        /// <returns></returns>
        /// <remarks>
        /// Le seuil est 0
        /// </remarks>
        public static double Escalier(double valeur)
        {
            return (valeur > 0) ? 1 : 0;
        }

        /// <summary>
        /// Fonction en escalier dérivée (Heaviside)
        /// </summary>
        /// <param name="valeur">Valeur à filtrer</param>
        /// <returns></returns>
        /// <remarks>
        /// Le seuil est 0
        /// </remarks>
        public static double EscalierDerivee(double valeur)
        {
            return 1;
        }
        #endregion

        #region Sigmoide
        /// <summary>
        /// Fonction sigmoïde
        /// </summary>
        /// <param name="valeur">Valeur à filtrer</param>
        /// <returns></returns>
        /// <remarks>
        /// Le seuil est 0.5
        /// </remarks>
        public static double Sigmoide(double valeur)
        {
            // on évite les débordements
            if (valeur < -20.0)
            {
                return 0.0;
            }
            else if (valeur > 20.0)
            {
                return 1.0;
            }

            double retour = 1.0 / (1.0 + Math.Exp(-valeur));
            return retour;
        }

        /// <summary>
        /// Fonction sigmoïde dérivée
        /// </summary>
        /// <param name="valeur">Valeur à filtrer</param>
        /// <returns></returns>
        /// <remarks>
        /// Le seuil est 0.5
        /// </remarks>
        public static double SigmoideDerivee(double valeur)
        {
            valeur = Sigmoide(valeur);
            double retour = valeur * (1 - valeur);
            return retour;
        } 
        #endregion
    }
}
