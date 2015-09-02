#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Description d'un réseau
    /// </summary>
    public abstract class Reseau
    {
        #region Constructeur
        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="topologie">Topologie du réseau</param>
        /// <param name="seuil">Seuil de la fonction de transfert</param>
        /// <param name="transfertDerivee">Dérivée de la fonction de transfert ou null</param>
        /// <param name="nbEntrees">Taille de la matrice d'entrée</param>
        public Reseau(Func<double, double> transfert, double seuil, Func<double, double> transfertDerivee, int nbEntrees, params int[] topologie)
        {
            Initialiser(transfert, seuil, transfertDerivee, nbEntrees, topologie);
        }
        #endregion

        /// <summary>
        /// Couches du réseau
        /// </summary>
        public Couche[] Couches { get; set; }

        /// <summary>
        /// Initialiser le réseau
        /// </summary>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="nbEntrees">Nb d'entrées des neurones de la couche d'entrée</param>
        /// <param name="seuil">Seuil de la fonction de transfert de la couche</param>
        /// <param name="transfertDerivee">Null ou dérivée de la fonction de transfert</param>
        /// <param name="topologie">Topologie du réseau</param>
        protected abstract void Initialiser(Func<double, double> transfert, double seuil, Func<double, double> transfertDerivee, int nbEntrees, int[] topologie);

        /// <summary>
        /// Effectue une prédiction sur un modèle
        /// </summary>
        /// <param name="dataVector"></param>
        /// <returns>true si le vecteur d'entrée correspond au modèle pour lequel le réseau a été entraîné</returns>
        public abstract bool Predire(double[] dataVector);
    }
}
