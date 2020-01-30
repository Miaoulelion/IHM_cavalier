using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    class Damier
    {
        // taille nette de notre damiers
        // taille réelle en opposition avec une taille brute (comprenant les marges de deplacement du cavalier)
        private int tailleLig; //nbr ligne de notre damier
        private int tailleCol; //nbr colonne de notre damier
        private int tailleMarge; //taille de la marge par bord
        private int tailleBouton; // taille d un cote du bouton
        private int tailleLabel; //taille du cadre accorde aux labels
        private monBouton[,] grille; //déclaration grille dans les attributs de la class Form1
        private Label[] labels; //table des lables à ajouter

        //paramètres du damier
        private Color colorChoixCF; //couleur foncee
        private Color colorChoixCC; //couleur claire

        //definition de POSITION du damier
        private int posX;
        private int posY;

        public Damier(int nbrCaseCoteDamier, int tailleBoutonPX, int nbrCaseMarge, int posX, int posY,  Color colorChoixCF, Color colorChoixCC)
        {
            //definition de la COULEUR du damier
            this.colorChoixCF = colorChoixCF;
            this.colorChoixCC = colorChoixCC;
            {
                // definition de TAILLE du damier
                tailleLig = nbrCaseCoteDamier; //8 nbr de ligne
                tailleCol = nbrCaseCoteDamier; //8 nbr de colonnes
                tailleBouton = tailleBoutonPX;  //bouton de 50 px
                tailleLabel = tailleBoutonPX / 2;
                tailleMarge = nbrCaseMarge; //2 bouton de marge par cote

                //definition de POSITION du damier
                this.posY = posY;
                this.posX = posX;

                //Definition de la taille de la grille contenant bouton
                grille = new monBouton[tailleLig + (tailleMarge * 2), tailleCol + (tailleMarge * 2)];

                //Definition de la taille de la table labels contenant les labels
                labels = new Label[tailleLig + tailleCol];
                int il = 0; //indice pour inserrer un label

                //Création du damier. On remplit de boutons la grille de boutons.
                for (int ligne = 0; ligne < grille.GetLength(0); ++ligne)
                {
                    for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
                    {
                        // ********** BOUTON ************

                        monBouton b = new monBouton(ligne, colonne, colorChoixCF, colorChoixCC);  //j'enregistre un numéro de colonne et de ligne pour chacun de mes boutons

                        if (ligne >= tailleMarge && ligne < (tailleMarge + tailleLig))

                        {
                            if (colonne >= tailleMarge && colonne < (tailleMarge + tailleCol))
                            {
                                b.Size = new Size(tailleBouton, tailleBouton);  //Je fais des boutons carrés
                                b.Visible = true;
                                b.Location = new Point(1 + (colonne - tailleMarge) * tailleBouton + posX, 1 + (ligne - tailleMarge) * tailleBouton + posY); //1 pour avoir un peu d'espace

                                // ******** Background couleur des cellule *******
                                b.Colorer();
                            }
                            else
                            {
                                b.Size = new Size(0, 0); //si le bouton est en dehors du damier
                                b.Visible = false;
                                //b.Text = "dehors";
                            }
                        }
                        else
                        {
                            b.Size = new Size(0, 0); //si le bouton est en dehors du damier
                            b.Visible = false;
                            //b.Text = "dehors";
                        }

                        //b.Text = "l" + b.getNumLigne() + 'c' + b.getNumCol(); //identification des boutons
                        grille[ligne, colonne] = b; //On remplit la grille de boutons
                    }
                }

                // ********** LABEL ************
                for (int ligne = 0; ligne < grille.GetLength(0); ++ligne)
                {
                    if (ligne >= tailleMarge && ligne < (tailleMarge + tailleLig))
                    {
                        //ligne
                        Label labelLigne = new Label();
                        labelLigne.Location = new Point(tailleCol * tailleBouton + 1 + posX, 20 + tailleBouton * (ligne - tailleMarge) + posY);
                        labelLigne.Size = new Size(tailleLabel, tailleLabel);
                        labelLigne.Text = "" + (ligne - tailleMarge + 1);
                        //ajout dans la table labels
                        labels[il] = labelLigne;
                        ++il;
                    }
                }
                for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
                {
                    if (colonne >= tailleMarge && colonne < (tailleMarge + tailleCol))
                    {
                        //colonne
                        Label labelColonne = new Label();
                        labelColonne.Location = new Point(20 + tailleBouton * (colonne - tailleMarge) + posX, tailleLig * tailleBouton + 1 + posY);
                        labelColonne.Size = new Size(tailleLabel, tailleLabel); //ATTENTION les labels peuvent ne pas apparaitre à cause de leur taille par défaut et des distances mises entre ceux-ci
                        labelColonne.Text = "" + (colonne - tailleMarge + 1);
                        //ajout dans la table labels
                        labels[il] = labelColonne;
                        ++il;
                    }
                }
            }
        }


        public Damier(int nbrCaseCoteDamier, int tailleBoutonPX, int nbrCaseMarge, int posX, int posY)
        {
            // definition de TAILLE du damier
            tailleLig = nbrCaseCoteDamier; //8 nbr de ligne
            tailleCol = nbrCaseCoteDamier; //8 nbr de colonnes
            tailleBouton = tailleBoutonPX;  //bouton de 50 px
            tailleLabel = tailleBoutonPX /2;
            tailleMarge = nbrCaseMarge; //2 bouton de marge par cote

            //definition de POSITION du damier
            this.posY = posY;
            this.posX = posX;

            //Definition de la taille de la grille contenant bouton
            grille = new monBouton[tailleLig + (tailleMarge * 2), tailleCol + (tailleMarge * 2)];

            //Definition de la taille de la table labels contenant les labels
            labels = new Label[tailleLig+tailleCol];
            int il = 0; //indice pour inserrer un label

            //Création du damier. On remplit de boutons la grille de boutons.
            for (int ligne = 0; ligne < grille.GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
                {
                    // ********** BOUTON ************
                    monBouton b = new monBouton(ligne, colonne);  //j'enregistre un numéro de colonne et de ligne pour chacun de mes boutons

                    if (ligne >= tailleMarge && ligne < (tailleMarge + tailleLig))

                    {
                        if (colonne >= tailleMarge && colonne < (tailleMarge + tailleCol))
                        {
                            b.Size = new Size(tailleBouton, tailleBouton);  //Je fais des boutons carrés
                            b.Visible = true;
                            b.Location = new Point(1 + (colonne - tailleMarge) * tailleBouton + posX, 1 + (ligne - tailleMarge) * tailleBouton + posY); //1 pour avoir un peu d'espace

                            // ******** Background couleur des cellule *******
                            b.Colorer();
                        }
                        else
                        {
                            b.Size = new Size(0, 0); //si le bouton est en dehors du damier
                            b.Visible = false;
                            //b.Text = "dehors";
                        }
                    }
                    else
                    {
                        b.Size = new Size(0, 0); //si le bouton est en dehors du damier
                        b.Visible = false;
                        //b.Text = "dehors";
                    }

                    //b.Text = "l" + b.getNumLigne() + 'c' + b.getNumCol(); //identification des boutons
                    grille[ligne, colonne] = b; //On remplit la grille de boutons
                }
            }

            // ********** LABEL ************
            for (int ligne = 0; ligne < grille.GetLength(0); ++ligne)
            {
                if (ligne >= tailleMarge && ligne < (tailleMarge + tailleLig))
                {
                    //ligne
                    Label labelLigne = new Label();
                    labelLigne.Location = new Point(tailleCol * tailleBouton + 1 + posX, 20 + tailleBouton * (ligne - tailleMarge) + posY);
                    labelLigne.Size = new Size(tailleLabel, tailleLabel);
                    labelLigne.Text = "" + (ligne - tailleMarge + 1);
                    //ajout dans la table labels
                    labels[il] = labelLigne;
                    ++il;
                }
            }
            for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
            {
                if (colonne >= tailleMarge && colonne < (tailleMarge + tailleCol))
                {
                    //colonne
                    Label labelColonne = new Label();
                    labelColonne.Location = new Point(20 + tailleBouton * (colonne - tailleMarge) + posX, tailleLig * tailleBouton + 1 + posY);
                    labelColonne.Size = new Size(tailleLabel, tailleLabel); //ATTENTION les labels peuvent ne pas apparaitre à cause de leur taille par défaut et des distances mises entre ceux-ci
                    labelColonne.Text = "" + (colonne - tailleMarge +1);
                    //ajout dans la table labels
                    labels[il] = labelColonne;
                    ++il;
                }
            }

        }


        public int GetTailleLig()
        {
            return tailleLig;
        }
        public int GetTailleCol()
        {
            return tailleCol;
        }
        public int GetTailleMarge()
        {
            return tailleMarge;
        }
        public int GetTailleBouton()
        {
            return tailleBouton;
        }
        public int GetTailleLabel()
        {
            return tailleLabel;
        }

        public monBouton[,] GetGrille()
        {
            return grille;
        }

        public monBouton GetMonBouton(int ligne, int colonne)
        {
            if ((ligne < grille.GetLength(0)) && (colonne < grille.GetLength(1)) && (ligne >= 0) && (colonne >= 0))
            {
                return grille[ligne, colonne];
            }
            else
                return null;
        }

        public Label[] GetLabels()
        {
            return labels;
        }

        internal void SetButtonCible(monBouton bDepart, int depi, int depj)
        {
            grille[bDepart.getNumLigne() + depi, bDepart.getNumCol() + depj].Text = "X";
        }
        internal void UnSetButtonCible(monBouton bDepart, int depi, int depj)
        {
            grille[bDepart.getNumLigne() + depi, bDepart.getNumCol() + depj].Text = "";
        }

        public void reinitialiserGrille()
        {
            monBouton btmp;
            for (int ligne = 0; ligne < grille.GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
                {
                    if (ligne >= GetTailleMarge() && ligne < (GetTailleMarge() + GetTailleLig()))
                    {
                        if (colonne >= GetTailleMarge() && colonne < (GetTailleMarge() + GetTailleCol()))
                        {
                            btmp = grille[ligne, colonne];
                            btmp.Enabled = true;
                            btmp.BackgroundImage = null;
                            btmp.Colorer();
                            btmp.Text = "";
                        }
                    }
                }
            }
        }

        public void MonBouton_ImageAj(int ligne, int colonne, Image img)
        {
            monBouton btmp = GetMonBouton(ligne, colonne);
            btmp.ImageAj(img);
        }

        public void MonBouton_ImageSupp(int ligne, int colonne)
        {
            monBouton btmp = GetMonBouton(ligne, colonne);
            btmp.ImageSupp();
        }

        public void MonBouton_Assombrir(int ligne, int colonne)
        {
            monBouton btmp = GetMonBouton(ligne, colonne);
            btmp.Assombrir();
        }
    }
}
