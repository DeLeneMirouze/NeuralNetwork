#region using
using System;
using System.Diagnostics;
#endregion

namespace BoiteAOutils
{
    /// <summary>
    /// Modélisation d'un neurone formel
    /// </summary>
    [DebuggerDisplay("Sortie: {Sortie}")]
    public class Neurone
    {
        #region Constructeur
        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="nbEntrees">Nombre d'entrées du neurone</param>
        /// <param name="transfert">Fonction de transfert</param>
        /// <param name="transfertDerivee">Null ou dérivée de la fonction de transfert</param>
        public Neurone(int nbEntrees, Func<double,double> transfert, Func<double, double> transfertDerivee = null)
        {
            Transfert = (transfert == null) ? FonctionsTransfert.Identite : transfert;
            TransfertDerivee = (transfertDerivee == null) ? FonctionsTransfert.Identite : transfertDerivee;
            NbEntrees = nbEntrees;
            Poids = new double[NbEntrees];
        }
        #endregion

        /// <summary>
        /// Erreur attachée au neurone
        /// </summary>
        public double Erreur { get; set; }
        /// <summary>
        /// Valeur du biais
        /// </summary>
        public double Biais { get; set; }
        /// <summary>
        /// Nombre d'entrées du neurone (dendrites)
        /// </summary>
        public int NbEntrees { get; private set; }
        /// <summary>
        ///Coefficients synaptiques
        /// </summary>
        public double[] Poids { get; set; }
        /// <summary>
        /// Fonction de transfert
        /// </summary>
        public Func<double, double> Transfert { get; private set; }
        /// <summary>
        /// Fonction dérivée de la fonction de transfert
        /// </summary>
        public Func<double, double> TransfertDerivee { get; private set; }

        #region CalculerSortie
        /// <summary>
        /// Calcul de la sortie d'un neurone
        /// </summary>
        /// <param name="entrees">Valeur d'entrée </param>
        /// <param name="biais">Biais</param>
        /// <returns></returns>
        public double CalculerSortie(double[] entrees)
        {
            if (entrees.Length != NbEntrees)
            {
                string message = "Nombre d'entrées attendues: {0}, mais {1} reçues";
                throw new InvalidOperationException(string.Format(message, NbEntrees, entrees.Length));
            }

            Somme = CalculPoidsSynaptique(entrees);
            Sortie = Transfert(Somme + Biais);

            return Sortie;
        }

        /// <summary>
        /// Somme pondérée sans le biais
        /// </summary>
        public double Somme { get; set; }
        public double Sortie { get; set; }
        #endregion

        #region CalculPoidsSynaptique (private)
        /// <summary>
        /// Calcul du poids synaptique
        /// </summary>
        /// <param name="entrees"></param>
        /// <returns></returns>
        private double CalculPoidsSynaptique(double[] entrees)
        {
            double poids = 0;
            for (int i = 0; i < NbEntrees; i++)
            {
                poids += entrees[i] * Poids[i];
            }

            return poids;
        }
        #endregion
    }
}
