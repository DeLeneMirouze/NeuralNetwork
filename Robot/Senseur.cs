#region using
using System;
#endregion

namespace Robot
{
    /// <summary>
    /// Capteurs du robot
    /// </summary>
    public sealed class Senseur
    {
        #region Constructeur
        public Senseur(int largeur)
        {
            Largeur = largeur;
        } 
        #endregion

        #region LireSenseurs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece">Plan de la pièce</param>
        /// <param name="direction">Direction du robot</param>
        /// <param name="x">Position x du robot</param>
        /// <param name="y">Position y du robot</param>
        /// <returns></returns>
        public double[] LireSenseurs(int[][] piece, Direction direction, int x, int y)
        {
            // on a 3 senseurs
            // le codage sera:
            // (senseur gauche, senseur devant, senseur droit)
            // droite/gauche c'est par rapport au robot
            double[] s = new double[3];


            // important: la pièce est torique dans les deux dimensions
            // il faut en tenir compte bien sûr!
            switch (direction)
            {
                case Direction.Nord:
                    s[0] = CaptureOuest(piece, x, y);
                    s[1] = CaptureNord(piece, x, y);
                    s[2] = CaptureEst(piece, x, y);
                    break;
                case Direction.Sud:
                    s[0] = CaptureEst(piece, x, y);
                    s[1] = CaptureSud(piece, x, y);
                    s[2] = CaptureOuest(piece, x, y);
                    break;
                case Direction.Est:
                    s[0] = CaptureNord(piece, x, y);
                    s[1] = CaptureEst(piece, x, y);
                    s[2] = CaptureSud(piece, x, y);
                    break;
                case Direction.Ouest:
                    s[0] = CaptureSud(piece, x, y);
                    s[1] = CaptureOuest(piece, x, y);
                    s[2] = CaptureNord(piece, x, y);
                    break;
                default:
                    break;
            }

            return s;
        }
        #endregion

        #region CaptureOuest
        /// <summary>
        /// Lecture des senseurs dans la direction ouest
        /// </summary>
        /// <param name="piece">Plan de la pièce</param>
        /// <param name="X">Position X de départ (celle du robot)</param>
        /// <param name="Y">Position Y de départ (celle du robot)</param>
        /// <returns></returns>
        private double CaptureOuest(int[][] piece, int X, int Y)
        {
            double s = 0;

            for (int x = X - 1; x >= 0; x--)
            {
                if (piece[Y][x] == 1)
                {
                    // un mur
                    s = CalculStimulus(X - x, true);
                    return s;
                }
                else if (piece[Y][x] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(X - x, false);
                    return s;
                }
            }

            // autre bord
            for (int x = Largeur - 1; x > X; x--)
            {
                if (piece[Y][x] == 1)
                {
                    // un mur
                    s = CalculStimulus(Largeur - x, true);
                    return s;
                }
                else if (piece[Y][x] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(Largeur - x, false);
                    return s;
                }
            }

            return s;
        }
        #endregion

        #region CaptureEst
        /// <summary>
        /// Lecture des senseurs dans la direction est
        /// </summary>
        /// <param name="piece">Plan de la pièce</param>
        /// <param name="X">Position X de départ (celle du robot)</param>
        /// <param name="Y">Position Y de départ (celle du robot)</param>
        /// <returns></returns>
        private double CaptureEst(int[][] piece, int X, int Y)
        {
            double s = 0;

            for (int x = X + 1; x < Largeur; x++)
            {
                if (piece[Y][x] == 1)
                {
                    // un mur!
                    s = CalculStimulus(x - X, true);
                    return s;
                }
                else if (piece[Y][x] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(x - X, false);
                    return s;
                }
            }

            // autre bord
            for (int x = 1; x < X; x++)
            {
                if (piece[Y][x] == 1)
                {
                    // un mur!
                    s = CalculStimulus(x, true);
                    return s;
                }
                else if (piece[Y][x] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(x, false);
                    return s;
                }
            }

            return s;
        }
        #endregion

        #region CaptureSud
        /// <summary>
        /// Lecture des senseurs dans la direction sud
        /// </summary>
        /// <param name="piece">Plan de la pièce</param>
        /// <param name="X">Position X de départ (celle du robot)</param>
        /// <param name="Y">Position Y de départ (celle du robot)</param>
        /// <returns></returns>
        private double CaptureSud(int[][] piece, int X, int Y)
        {
            double s = 0;

            for (int y = Y + 1; y < Largeur; y++)
            {
                if (piece[y][X] == 1)
                {
                    // un mur
                    s = CalculStimulus(y - Y, true);
                    return s;
                }
                else if (piece[y][X] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(y - Y, false);
                    return s;
                }
            }

            // autre bord
            for (int y = 1; y < Y - 1; y++)
            {
                if (piece[y][X] == 1)
                {
                    // un mur
                    s = CalculStimulus(y, true);
                    return s;
                }
                else if (piece[y][X] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(y, false);
                    return s;
                }
            }

            return s;
        }
        #endregion

        #region CaptureNord
        /// <summary>
        /// Lecture des senseurs dans la direction nord
        /// </summary>
        /// <param name="piece">Plan de la pièce</param>
        /// <param name="X">Position X de départ (celle du robot)</param>
        /// <param name="Y">Position Y de départ (celle du robot)</param>
        /// <returns></returns>
        private double CaptureNord(int[][] piece, int X, int Y)
        {
            double s = 0;

            for (int y = Y  - 1; y >= 0; y--)
            {
                if (piece[y][X] == 1)
                {
                    // un mur
                    s = CalculStimulus(Y - y, true);
                    return s;
                }
                else if (piece[y][X] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(Y - y, false);
                    return s;
                }
            }

            // autre bord
            for (int y = Largeur - 1; y > Y; y--)
            {
                if (piece[y][X] == 1)
                {
                    // un mur
                    s = CalculStimulus(Largeur - y, true);
                    return s;
                }
                else if (piece[y][X] == 2)
                {
                    // un bonbon!
                    s = CalculStimulus(Largeur - y, false);
                    return s;
                }
            }

            return s;
        }
        #endregion

        #region CalculStimulus
        public static double CalculStimulus(int d, bool mur)
        {
            d = Math.Abs(d);
            double retour = Math.Exp(-1 * (double)d / 15.0);

            return mur ? -1 * retour : retour;
        }
        #endregion

        int Largeur;
    }
}
