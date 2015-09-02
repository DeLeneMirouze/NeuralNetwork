#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BoiteAOutils
{

    /// <summary>
    /// Modélisation d'une couche de neurones
    /// </summary>
    [DebuggerDisplay("Position: {Position}")]
    public class Couche
    {
        #region Constructeur
        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="tailleCouche">Nombre de neurones de la couche</param>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="seuil">Seuil de la fonction de transfert</param>
        /// <param name="nbEntrees">Nombre d'entrée de chaque neurone</param>
        /// <param name="transfertDerivee">Null ou dérivée de la fonction de transfert</param>
        public Couche(int tailleCouche, int nbEntrees, Func<double, double> transfert, double seuil, Func<double, double> transfertDerivee = null)
        {
            Seuil = seuil;
            Neurones = new Neurone[tailleCouche];
            for (int i = 0; i < tailleCouche; i++)
            {
                Neurone neurone = new Neurone(nbEntrees, transfert, transfertDerivee);
                Neurones[i] = neurone;
            }
        }
        #endregion

        /// <summary>
        /// Neurones de la couche
        /// </summary>
        public Neurone[] Neurones { get; private set; }

        /// <summary>
        /// Seuil de la fonction de transfert
        /// </summary>
        public double Seuil { get; set; }

        /// <summary>
        /// Position de la couche
        /// </summary>
        /// <remarks>
        /// La couche d'entrée a la position 0
        /// </remarks>
        public int Position { get; set; }
    }
}
