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
    public class RobotNeurone : Neurone
    {
        #region Constructeur
        public RobotNeurone(int nbEntrees)
    : base(nbEntrees, null, null)
        {
            Valeurs = new double[NbEntrees];
            Activites = new double[NbEntrees];
        }
        #endregion

        /// <summary>
        /// Aptitude à prendre une décision
        /// </summary>
        /// <remarks>
        /// Les valeurs apparaissent dans la game [-1,+1]
        /// 
        /// Les entrées contiennent une valeur liée à chaque senseur dans l'ordre:
        /// Gauche, devant, droit (par rapport au robot)
        /// 
        /// Chaque valeur représente "l'intérêt" qu'aura le robot à suivre les indications du senseur correspondant
        /// cad le fait que ce soit bon ou pas
        /// </remarks>
        public double[] Valeurs { get; set; }
        /// <summary>
        /// Plus l'activité est élevée, plus le neurone est proche d'une zone qui a déjà été visitée
        /// </summary>
        public double[] Activites { get; set; }

        public double A { get; set; }
        public double V { get; set; }

        public int X { get; set; }
        public double Distance { get; set; }

        /// <summary>
        /// Pour la recherche du voisinage
        /// </summary>
        public double F { get; set; }

        #region CalculProbas
        public double[] CalculProbas()
        {
            // http://members.ozemail.com.au/~dekker/robot.pdf

            double[] probas = new double[NbEntrees];
            Random rnd = new Random();

            // commence par classer les valeurs
            double vx = Valeurs.Min();
            double vz = Valeurs.Max();

            // calcul du paramètre q
            double[] q = new double[NbEntrees];
            for (int j = 0; j < Valeurs.Length; j++)
            {
                q[j] = (0.03 + Valeurs[j] - vx) / (0.03 + vz - vx);
                q[j] = q[j] * q[j];
            }

            // calcul des probas
            double denominateur = q.Sum();
            for (int j = 0; j < NbEntrees; j++)
            {
                probas[j] = q[j] / denominateur;
            }

            Helper.AfficherVecteur(probas, "Probas:");

            return probas;
        } 
        #endregion
    }
}
