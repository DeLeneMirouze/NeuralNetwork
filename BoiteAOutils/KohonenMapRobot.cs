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
    /// Réseau de Kohonen linéaire avec 3 poids par neurones
    /// </summary>
    public class KohonenMapRobot: KohonenMapBase<RobotNeurone>
    {
        #region Constructeur
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbPoints">Nombre de points sur la ligne</param>
        /// <param name="pas">Pas d'apprentissage initial</param>
        /// <param name="largeur">Valeur max du poids en X (index 0)</param>
        /// <param name="hauteur">Valeur max du poids en Y (index 1)</param>
        public KohonenMapRobot(int nbPoints, double pas, int largeur, int hauteur)
            :base(nbPoints, pas, largeur, hauteur, 3)
        {
        }
        #endregion

        #region Entrainer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="senseurs"></param>
        /// <param name="iteraionCourante"></param>
        /// <param name="totalIterations"></param>
        /// <returns>
        /// Action demandée au robot
        /// </returns>
        public int Entrainer(double[] senseurs, int iterationCourante, int totalIterations)
        {
            // recherche du meilleur neurone
            RobotNeurone gagnant = RechercherGagnant(senseurs);
            double[] probas = gagnant.CalculProbas();

            // recherche l'action ayant la proba la plus forte
            int bestAction = -1;
            double bestProbas = probas.Max();
            for (int i = 0; i < probas.Length; i++)
            {
                if (probas[i] == bestProbas)
                {
                    bestAction = i;
                    break;
                }
            }

            // récupère le voisinage du gagnant
            List<RobotNeurone> voisinage = Voisinage(gagnant, totalIterations, iterationCourante);

            // recalcule les poids et les valeurs
            // ajuste les poids
            AjusterPoids(voisinage, gagnant, senseurs, totalIterations, iterationCourante);

            double ratio = (double)iterationCourante / totalIterations;
            Pas = Pas * Math.Exp(-1 * ratio);
            Debug.WriteLine("Pas: " + Pas.ToString());

            return bestAction;
        }
        #endregion

        #region AjusterPoids (private)
        private void AjusterPoids(List<RobotNeurone> voisinage, RobotNeurone gagnant, double[] senseurs, int totalIterations, int iterationCourante)
        {
            int gamma = 8;
            double beta = 1.0 / 1024;

            for (int j = 0; j < voisinage.Count; j++)
            {
                RobotNeurone neurone = voisinage[j];

                neurone.Biais = gamma * (1.0 / NbPoints - neurone.F);
                if (neurone == gagnant)
                {
                    neurone.F = neurone.F - beta * neurone.F + beta;
                }
                else
                {
                    neurone.F = neurone.F - beta * neurone.F;
                }

                double rho;
                double ratio;

                for (int p = 0; p < NbEntrees; p++)
                {
                    // on ajuste les poids

                    // le facteur d'atténuation (diminue avec la distance au neurone gagnant)
                    ratio = (double)(neurone.X - gagnant.X) / Rayon;
                    rho = 1 - ratio * ratio;
                    neurone.Poids[p] = Pas * rho * senseurs[p] + (1 - Pas * rho) * neurone.Poids[p];

                    // on ajuste les activités
                    double decay = 3 * neurone.Activites[p] / 4;
                    neurone.Activites[p] = neurone.Activites[p] * (1 - decay);
                    ratio = (double)(neurone.X - gagnant.X) / 12;
                    rho = 1 - ratio * ratio;
                    neurone.Activites[p] = Math.Max(1, neurone.Activites[p] + rho);

                    // on ajuste les valeurs
                    if (neurone.Valeurs[p] < 0)
                    {
                        // pour éviter qu'un neurone marqué comme "pas bon" le soit éternellement
                        neurone.Valeurs[p] += neurone.Valeurs[p] * (1 - Pas / 30.0);
                    }
                    if (senseurs[p] > 0)
                    {
                        // le senseur détecte un bonbon
                        neurone.Valeurs[p] = neurone.Activites[p] + (1 - neurone.Activites[p]) * neurone.Activites[p];
                        neurone.Activites[p] = 0;
                    }
                    if (senseurs[p] < 0)
                    {
                        // le senseur détecte un mur
                        neurone.Valeurs[p] = -1 * neurone.Activites[p] + (1 - neurone.Activites[p]) * neurone.Activites[p];
                        neurone.Activites[p] = 0;
                    }

                    if (neurone.Valeurs[p] > 0)
                    {
                        neurone.Valeurs[p] = Math.Abs(neurone.Valeurs[p]) * neurone.Activites[p]
                            + (1 - Math.Abs(neurone.Valeurs[p]) * neurone.Activites[p]) * neurone.Valeurs[p];
                    }
                    if (neurone.Valeurs[p] < 0)
                    {
                        neurone.Valeurs[p] = -1 * Math.Abs(neurone.Valeurs[p]) * neurone.Activites[p]
                            + (1 + Math.Abs(neurone.Valeurs[p]) * neurone.Activites[p]) * neurone.Valeurs[p];
                    }
                } // pour chaque action 

                Helper.AfficherVecteur(neurone.Valeurs, "Valeurs");
                Helper.AfficherVecteur(neurone.Activites, "Activités");
            } // pour chaque voisin
        }
        #endregion

        #region Voisinage (protected)
        /// <summary>
        /// Obtient le voisinage du gagnant
        /// </summary>
        /// <param name="gagnant"></param>
        /// <param name="totalIteration"></param>
        /// <param name="iterationCourante"></param>
        /// <returns>
        /// Le gagnant n'est pas dans la liste retournée
        /// </returns>
        protected List<RobotNeurone> Voisinage(RobotNeurone gagnant, int totalIteration, int iterationCourante)
        {
            // on commence par calculer le rayon
            // on le fait varier exponentiellement de 32 à 6
            int min = 6;
            int max = 32;
            double beta = -1 * totalIteration / Math.Log((double)(min) / max);
            Rayon = (int)(max * Math.Exp(-1 * iterationCourante / beta));

            Debug.WriteLine("Rayon: " + Rayon.ToString() + ", Itération: " + iterationCourante.ToString());

            // on recherche les neurones présents dans le rayon
            // leurs poids seront ajustés
            List<RobotNeurone> voisinage = new List<RobotNeurone>();
            for (int x = 0; x < Line.Length; x++)
            {
                if (Math.Abs(x - gagnant.X) < Rayon)
                {
                    RobotNeurone neurone = Line[x];
                    neurone.Distance = Distance(neurone, gagnant);

                    voisinage.Add(neurone);
                }
            }

            Debug.WriteLine("Voisinage: " + voisinage.Count.ToString());
            return voisinage;
        }
        #endregion

        #region Distance (protected)
        protected override double Distance(Neurone v1, Neurone v2)
        {
            ///Distance euclidienne
            double distance = Math.Sqrt(
                ((RobotNeurone)v1).X - (((RobotNeurone)v2).X) * (((RobotNeurone)v1).X - ((RobotNeurone)v2).X)
                );
            return distance;
        }
        #endregion

        #region RechercherGagnant (private)
        private RobotNeurone RechercherGagnant(double[] senseurs)
        {
            double distanceMin = double.MaxValue;
            RobotNeurone gagnant = null;

            for (int x = 0; x < NbPoints; x++)
            {
                RobotNeurone neurone = Line[x] as RobotNeurone;

                // pour chaque neurone on calcule sa distance avec l'entrée
                double distance = 0;
                for (int i = 0; i < neurone.NbEntrees; i++)
                {
                    distance += Math.Abs(neurone.Poids[i] - senseurs[i]);
                }
                distance -= neurone.Biais;

                // est-ce le gagnant?
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    gagnant = neurone;
                }
            }

            return gagnant;
        } 
        #endregion

        #region Initialiser (protected)
        protected override void Initialiser()
        {
            Line = new RobotNeurone[NbPoints];
            Random rnd = new Random();

            for (int i = 0; i < Line.Length; i++)
            {
                RobotNeurone neurone = new RobotNeurone(NbEntrees);
                neurone.X = i;
                neurone.F = 1 / NbPoints;

                // la tendance initiale sera d'aller de l'avant
                neurone.Valeurs[0] = 0;
                neurone.Valeurs[1] = 0.5;
                neurone.Valeurs[2] = 0;

                neurone.Activites[0] = 0;
                neurone.Activites[1] = 0.5;
                neurone.Activites[2] = 0;

                //neurone.Poids[0] = rnd.NextDouble();
                //neurone.Poids[1] = rnd.NextDouble();
                //neurone.Poids[2] = rnd.NextDouble();

                Line[i] = neurone;
            }
        }
        #endregion

        /// <summary>
        /// Dernier rayon utilisé pour calculer le voisinage
        /// </summary>
        int Rayon;
    }
}
