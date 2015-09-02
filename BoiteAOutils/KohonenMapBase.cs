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
    public abstract class KohonenMapBase<T>
        where T :Neurone
    {
        #region Constructeur
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbPoints">Nombre de points sur la ligne</param>
        /// <param name="pas">Pas d'apprentissage initial</param>
        /// <param name="largeur">Valeur max du poids en X (index 0)</param>
        /// <param name="hauteur">Valeur max du poids en Y (index 1)</param>
        /// <param name="nbEntrees">Taille de la couche d'entrée</param>
        public KohonenMapBase(int nbPoints, double pas, int largeur, int hauteur, int nbEntrees)
        {
            PasInitial = pas;
            Pas = pas;
            NbEntrees = nbEntrees;
            NbPoints = nbPoints;
            RayonCarte = NbPoints / 2;
            LargeurMax = largeur;
            HauteurMax = hauteur;

            Initialiser();
        }
        #endregion

        protected abstract void Initialiser();

        #region Distance (protected)
        protected abstract double Distance(Neurone v1, Neurone v2);
        #endregion

        protected int LargeurMax { get; set; }
        protected int HauteurMax { get; set; }
        /// <summary>
        /// Valeur initiale du pas d'apprentissage
        /// </summary>
        protected double PasInitial { get; set; }
        protected double Pas { get; set; }
        /// <summary>
        /// Taille de la couche d'entrée
        /// </summary>
        protected int NbEntrees { get; set; }
        /// <summary>
        /// Nombre de points de la ligne
        /// </summary>
        protected int NbPoints { get; set; }
        /// <summary>
        /// La matrice à optimiser
        /// </summary>
        public T[] Line { get; set; }
        /// <summary>
        /// Rayon de la carte de Kohonen
        /// </summary>
        protected int RayonCarte { get; set; }
    }
}
