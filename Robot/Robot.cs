#region using
using System;
using System.Collections.Generic;
using System.Drawing;
#endregion

namespace Robot
{
    class Robot
    {
        #region Constructeur
        /// <summary>
        /// 
        /// </summary>
        /// <param name="largeur"></param>
        public Robot(int largeur)
        {
            Largeur = largeur;
            Visites = new List<Point>();
            Senseur = new Senseur(largeur);
        }
        #endregion

        /// <summary>
        /// Liste des endroits visités par le robot
        /// </summary>
        public List<Point> Visites { get; set; }
        /// <summary>
        /// Rayon de la carte, en pratique la demi largeur d'une arête de la pièce
        /// </summary>
        public int Largeur { get; set; }
        // position du robot dans la pièce
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// Direction dans laquelle regarde le robot
        /// </summary>
        public Direction Direction { get; set; }
        public Senseur Senseur { get; set; }
    }

    public enum Action
    {
        Gauche = 0,
        Devant = 1,
        Droit = 2
    }
}
