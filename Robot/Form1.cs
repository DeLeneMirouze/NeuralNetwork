#region using
using BoiteAOutils;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
#endregion

namespace Robot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // initialiser

            robot = new Robot(Largeur);
            // état initial du robot
            robot.Direction = Direction.Nord;
            robot.X = 0;
            robot.Y = Largeur - 1;
            currentT = 0;

            button3.Enabled = true;
            button2.Enabled = true;
            NbBonbons = 0;
            Liste.Text = null;

            // schéma de la pièce avec les murs représentées sous la forme d'un carré
            // En fait la pièce est torique dans ses deux dimensions
            // cela signifie que si on déborde d'un bord, on arrive sur le bord opposé
            // la direction du robot ne change évidemment pas
            piece = new int[Largeur][];
            piece[0] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            piece[1] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            piece[2] = new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 };
            piece[3] = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            piece[4] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            piece[5] = new int[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 };
            piece[6] = new int[] { 0, 0, 1, 1, 1, 0, 1, 0, 0, 0 };
            piece[7] = new int[] { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 };
            piece[8] = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            piece[9] = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };

            reseau = new KohonenMapRobot(256, 1.0, Piece.Width, Piece.Height);

            pasX = (int)(Piece.Width / Largeur);
            pasY = (int)(Piece.Height / Largeur);

            Graphics g = Piece.CreateGraphics();
            g.Clear(Color.White);

            DessinerMurs(g);
            DisposerBonbons(g, 5);
            DessinerRobot(g, robot);

            double[] senseurs = CapturerSenseurs(robot);

            Liste.Text += string.Format("Senseur: {0}/{1}/{2}", senseurs[0].ToString("0.0000;-0.0000"), senseurs[1].ToString("0.0000;-0.0000"), senseurs[2].ToString("0.0000;-0.0000")) + Environment.NewLine;
        }

        #region DessinerRobot (private)
        private void DessinerRobot(Graphics g, Robot robot)
        {
            Rectangle r = new Rectangle(robot.X * pasX, robot.Y * pasY, pasX, pasY);
            g.FillRectangle(Brushes.Green, r);
            Pen pen = new Pen(Color.Orange, 6);
            switch (robot.Direction)
            {
                case Direction.Nord:
                    g.DrawLine(pen, robot.X * pasX, robot.Y * pasY, robot.X * pasX + pasX, robot.Y * pasY);
                    break;
                case Direction.Sud:
                    g.DrawLine(pen, robot.X * pasX, robot.Y * pasY + pasY, robot.X * pasX + pasX, robot.Y * pasY + pasY);
                    break;
                case Direction.Ouest:
                    g.DrawLine(pen, robot.X * pasX, robot.Y * pasY, robot.X * pasX, robot.Y * pasY + pasY);
                    break;
                case Direction.Est:
                    g.DrawLine(pen, robot.X * pasX + pasX, robot.Y * pasY, robot.X * pasX + pasX, robot.Y * pasY + pasY);
                    break;
                default:
                    break;
            }

            Position.Text = string.Format("X={0}, Y={1}", robot.X,robot.Y);
        }
        #endregion

        #region DisposerBonbons (private)
        private void DisposerBonbons(Graphics g, int b)
        {
            Random rnd = new Random();
            bonbonRects = new Rectangle[b];
            bonbons = new Point[b];
            int trouve = 0;

            while (trouve != b)
            {
                int index = rnd.Next(Largeur);
                int y = rnd.Next(Largeur);
                int x = rnd.Next(Largeur);

                // on ne place pas de bonbon sur la case en bas à gauche
                // car c'est d'ici que démarre le robot
                if (piece[y][x] == 0 && !(x == 0 && y == Largeur - 1) && piece[y][x] != 2)
                {
                    Rectangle r = new Rectangle(x * pasX, y * pasY, pasX, pasY);
                    bonbonRects[trouve] = r;

                    Point point = new Point(x, y);
                    bonbons[trouve] = point;

                    piece[y][x] = 2;
                    trouve++;

                    string message = string.Format("({0},{1})", x, y);
                }
            }

            g.FillRectangles(Brushes.Pink, bonbonRects);
        }
        #endregion

        #region DessinerMurs (private)
        private void DessinerMurs(Graphics g)
        {
            for (int y = 0; y < piece.Length; y++)
            {
                int[] ligne = piece[y];

                for (int x = 0; x < ligne.Length; x++)
                {
                    if (ligne[x] == 1)
                    {
                        Rectangle m1 = new Rectangle(x * pasX, y * pasY, pasY, pasX);
                        g.FillRectangle(Brushes.Black, m1);
                    }
                }
            }
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// dessin de la pièce
        /// </summary>
        /// <remarks>
        /// Attention:
        /// 
        /// La première dimension est l'axe des Y
        /// La deuxième dimension est l'axe des X
        /// </remarks>
        int[][] piece;
        /// <summary>
        /// largeur d'une cellule en pixels
        /// </summary>
        int pasX;
        /// <summary>
        /// hauteur d'une cellule en pixels
        /// </summary>
        int pasY;
        /// <summary>
        /// Largeur de la pièce
        /// </summary>
        int Largeur = 10;

        // états du robot
        Robot robot;

        /// <summary>
        /// Réseau
        /// </summary>
        KohonenMapRobot reseau;
        /// <summary>
        /// Position des bonbons dans deux formats différents
        /// </summary>
        Rectangle[] bonbonRects;
        Point[] bonbons;
        /// <summary>
        /// Nb de bonbons trouvés par le robot
        /// </summary>
        int NbBonbons; 
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            // lancer la recherche sur itération

            int nbIterations = (int)NbIterations.Value;
            Graphics g = Piece.CreateGraphics();

            for (int t = 0; t < nbIterations; t++)
            {
                Passer(nbIterations, g, t);

                if (piece[robot.Y][robot.X] == 2)
                {
                    // bonbon trouvé!
                    NbBonbons++;
                    MangerBonbons(robot.X, robot.Y);
                }

                if (NbBonbons == bonbonRects.Length)
                {
                    // plus de bonbons à chercher, on arrête
                    return;
                }
            }
        }

        #region MangerBonbons (private)
        private void MangerBonbons(int x, int y)
        {
            bonbons = bonbons.Where(b => b.X != x || b.Y != y).ToArray();
            bonbonRects = new Rectangle[bonbons.Length];

            for (int i = 0; i < bonbons.Length; i++)
            {
                Point point = bonbons[i];
     
                Rectangle rect = new Rectangle(point.X * pasX, point.Y * pasY, pasX, pasY);
                bonbonRects[i] = rect;
            }
        } 
        #endregion

        #region Passer (private)
        /// <summary>
        /// Implémente le cycle d'acions à réaliser à chaque itération
        /// </summary>
        /// <param name="nbIterations"></param>
        /// <param name="g"></param>
        /// <param name="t"></param>
        private void Passer(int nbIterations, Graphics g, int t)
        {
            double[] senseurs = CapturerSenseurs(robot);
            int action = reseau.Entrainer(senseurs, t, nbIterations);
            EffectuerAction(action);

            g.Clear(Color.White);
            DessinerMurs(g);
            g.FillRectangles(Brushes.Pink, bonbonRects);
            AfficherVisites(g, robot.Visites);
            DessinerRobot(g, robot);

            senseurs = CapturerSenseurs(robot);
            Liste.Text += string.Format("* Action: {0}", (Action)action) + Environment.NewLine;
            Liste.Text += string.Format("Senseur: {0}/{1}/{2}", senseurs[0].ToString("0.0000;-0.0000"), senseurs[1].ToString("0.0000;-0.0000"), senseurs[2].ToString("0.0000;-0.0000")) + Environment.NewLine;
            Liste.Text += "Robot: " + robot.Direction.ToString() + "; " + robot.X + ";" + robot.Y + Environment.NewLine;

            Sensor.Text = string.Format("Senseur: {0}/{1}/{2}", senseurs[0].ToString("0.0000;-0.0000"), senseurs[1].ToString("0.0000;-0.0000"), senseurs[2].ToString("0.0000;-0.0000"));

            Helper.AfficherVecteur(senseurs, "Senseurs");
        } 
        #endregion

        private int currentT = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            // 1X jusqu'au max

            int nbIterations = (int)NbIterations.Value;
            Graphics g = Piece.CreateGraphics();

            Passer(nbIterations, g, currentT);

            currentT++;
            if (piece[robot.Y][robot.X] == 2)
            {
                NbBonbons++;
                MangerBonbons(robot.X, robot.Y);
            }

            if (currentT == nbIterations || NbBonbons == bonbonRects.Length)
            {
                // le max est atteint
                button3.Enabled = false;
            }
        }

        #region AfficherVisites (private)
        private void AfficherVisites(Graphics g, List<Point> visites)
        {
            foreach (Point point in visites)
            {
                int X = pasX * point.X + pasX / 2;
                int Y = pasY * point.Y + pasY / 2;
                g.FillEllipse(Brushes.Red, X, Y, 8, 8);
            }
        }
        #endregion

        #region EffectuerAction (private)
        private void EffectuerAction(int action)
        {
            if (action == 0)
            {
                // pivote à gauche
                switch (robot.Direction)
                {
                    case Direction.Nord:
                        robot.Direction = Direction.Ouest;
                        break;
                    case Direction.Sud:
                        robot.Direction = Direction.Est;
                        break;
                    case Direction.Est:
                        robot.Direction = Direction.Nord;
                        break;
                    case Direction.Ouest:
                        robot.Direction = Direction.Sud;
                        break;
                    default:
                        break;
                }
            }
            else if (action == 1)
            {
                // avance

                Point point = new Point(robot.X, robot.Y);
                robot.Visites.Add(point);

                switch (robot.Direction)
                {
                    case Direction.Nord:
                        robot.Y--;
                        break;
                    case Direction.Sud:
                        robot.Y++;
                        break;
                    case Direction.Est:
                        robot.X++;
                        break;
                    case Direction.Ouest:
                        robot.X--;
                        break;
                    default:
                        break;
                }

                // la pièce est torique dans toutes les dimensions
                if (robot.Y < 0)
                {
                    robot.Y = Largeur - 1;
                }
                else if (robot.Y == Largeur)
                {
                    robot.Y = 0;
                }
                if (robot.X < 0)
                {
                    robot.X = Largeur - 1;
                }
                else if (robot.X == Largeur)
                {
                    robot.X = 0;
                }
            }
            else
            {
                // pivote à droite

                switch (robot.Direction)
                {
                    case Direction.Nord:
                        robot.Direction = Direction.Est;
                        break;
                    case Direction.Sud:
                        robot.Direction = Direction.Ouest;
                        break;
                    case Direction.Est:
                        robot.Direction = Direction.Sud;
                        break;
                    case Direction.Ouest:
                        robot.Direction = Direction.Nord;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region CapturerSenseurs (private)
        private double[] CapturerSenseurs(Robot robot)
        {
            // http://members.ozemail.com.au/~dekker/robot.pdf

            double[] senseurs = robot.Senseur.LireSenseurs(piece, robot.Direction, robot.X, robot.Y);
            if (senseurs.Any(s => s > 0))
            {
                // on a détecté au moins un bonbon
                // dans ce cas on met à 0 les senseurs qui détectent un mur
                for (int s = 0; s < 3; s++)
                {
                    if (senseurs[s] < 0)
                    {
                        senseurs[s] = 0;
                    }
                }
            }

            return senseurs;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            double[] senseurs = CapturerSenseurs(robot);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Liste.Text = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public enum Direction
    {
        Nord,
        Sud,
        Est,
        Ouest
    }
}
