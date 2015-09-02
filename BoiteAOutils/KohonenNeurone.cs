#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Neurone pour un réseau de Kohonen
    /// </summary>
    [DebuggerDisplay("({X},{Y})")]
    public class KohonenNeurone : Neurone
    {
        #region Constructeur
        public KohonenNeurone(int nbEntrees)
    : base(nbEntrees, null, null)
        {

        }
        #endregion

        /// <summary>
        /// Distance du neurone au neurone gagant
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Traduction de la sortie en couleur
        /// </summary>
        public Color Couleur { get; set; }

        /// <summary>
        /// Nombre de fois où ce neurone a été un gagnant
        /// </summary>
        public int NbGagnant { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
