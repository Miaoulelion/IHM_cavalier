using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    public partial class FormeCavalier_Jeu : IHM_Projet_Cavalier_Berthier_Jallais_2.FormeDamierTemplate
    {
        private Label labelTest;
        
        private Label labelVict;
        private Button boutonDepart;
        private Button boutonRecommencer;
        private Button boutonRetour;
        private Button boutonSolution;
        private int cpClic ; //compteur de coup
        private const int rtClic = 5 ; // SEUIL nombre de coup retour possible
        private int cpRtClic; //nombre de fois que l'utilisateur a appuyé sur le bouton retour (affichage dans victoire)

        private monBouton monBoutonDep; // bouton de depart
        private int depCol; //colonne de Depart
        private int depLig;
        private int jouCol; //colonne joue
        private int jouLig;

        private monBouton[] dpl; //deplacements realisés

        private bool entamer; //securise le retour coup
        private bool victoire; //permet de proposer a l utilisateur si il n a pas gagné une simulation

        private Random aleatoire; //pour choisir une case au hasard de depart


        //fenetre paramètre 
        private FormeMenu_Parametre_Couleur fmpco;
        private FormeMenu_Parametre_Cavalier fmpca;

        private Cavalier piece;
        private int choixCav;

        private Color cc; // couleur claire
        private Color cf; // couleur fonce
        private int ccc; // ChoixCombiColor

        public FormeCavalier_Jeu()
        {
            InitializeComponent();
            entamer = false;
            cpClic = 0 ;
            cpRtClic = 0;
            dpl = new monBouton[DD.GetTailleCol() * DD.GetTailleLig()]; //nbr deplacement maximum = taille de la grille

            aleatoire = new Random();
            //necessite d etre defini une seule fois pour etre sur d avoir differents nombres

            //VARIABLE A CHANGER AVEC AUTRE FORME
            //choix du cavalier
            fmpca = new FormeMenu_Parametre_Cavalier();
            choixCav = fmpca.GetCcDef();

            // choixCav = FormeMenu_Parametre_Cavalier.GetCcDef();
            // choix du damier
            fmpco = new FormeMenu_Parametre_Couleur();
            cc = fmpco.GetCcDef();
            cf = fmpco.GetCfDef();
            ccc = fmpco.GetChoixCombiColorDef();
        }

    private void FormCavalier_Solo_Load(object sender, EventArgs e)
        {
            //VARIABLE A CHANGER AVEC AUTRE FORME
            //choix du cavalier
            choixCav = 1;

            this.Text += " - MODE JEU.";

            //fenêtre
            // Size the form to be  pixels in height and width.
            int Largeur = GetTailleDamierCol() + GetLargeurPannelPX() + GetPosX(); // 533
            int Hauteur = GetTailleDamierLig() + GetPosY(); // 483
            //this.Size = new Size(Largeur, Hauteur);
            //this.Size = new Size(600, 500);
            //this.Size = new Size(800, 800);
            // Display the form in the center of the screen.
            //this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(600, 530);


            // ******************** BOUTON PANNEAU DISPOSITION **********************
            int PosIniY = GetPosY();
            int PosIniX = GetPosX();
            int TailleDamier = this.GetTailleDamierCol(); //nbr px entre le damier et les boutons du panneau (doit pourvoir laisser apparraite les labels des lignes du damier
            int TailBut = this.GetLargeurPannelPX(); //nbr pixel de la taille (hauteur et largeur) des boutons et carre du panneau
            int TailLabel_Hauteur = TailBut - PosIniY; //le label de victoire est plus petit pour permettre de se limite a la taille du damier

            // ******************** BOUTON PANNEAU ACTION**********************
            //Choix aléatoire de la coordonnée de départ
            boutonDepart = new Button();
            boutonDepart.Location = new Point(TailleDamier + PosIniX, PosIniY);
            boutonDepart.Size = new Size(TailBut, TailBut);
            boutonDepart.Text = "Choisir aléatoirement la case départ";
            boutonDepart.Click += new System.EventHandler(this.boutonDepart_Click);
            this.Controls.Add(boutonDepart);

            //Bouton pour recommencer la partie
            boutonRecommencer = new Button();
            boutonRecommencer.Location = new Point(TailleDamier+ PosIniX, PosIniY + TailBut*1);
            boutonRecommencer.Size = new Size(TailBut, TailBut);
            boutonRecommencer.Text = "Recommencer la partie";
            boutonRecommencer.Click += new System.EventHandler(this.boutonRecommencer_Click);
            boutonRecommencer.Visible = false;
            this.Controls.Add(boutonRecommencer);

            //Bouton pour retourner de rtClic coups
            boutonRetour = new Button();
            boutonRetour.Location = new Point(TailleDamier + PosIniX, PosIniY + TailBut * 2);
            boutonRetour.Size = new Size(TailBut, TailBut);
            boutonRetour.Text = "Retour de " + rtClic + "coups";
            boutonRetour.Click += new System.EventHandler(this.boutonRetour_Click);
            boutonRetour.Visible = false;
            this.Controls.Add(boutonRetour);

            //Label pour afficher defaite ou victoire
            labelVict = new Label();
            labelVict.Location = new Point(TailleDamier + PosIniX - 10, PosIniY + TailBut * 3); // -10 pour centrer
            labelVict.Size = new Size(TailBut+20, TailLabel_Hauteur); // + 20 pour affichre nbr de case completes
            labelVict.TextAlign = ContentAlignment.MiddleCenter;
            labelVict.Text = "";
            labelVict.Visible = false;
            this.Controls.Add(labelVict);


            //Bouton pour afficher la solution
            boutonSolution = new Button();
            boutonSolution.Location = new Point(TailleDamier + PosIniX, PosIniY + TailBut * 3 + TailLabel_Hauteur * 1);
            boutonSolution.Size = new Size(TailBut, TailLabel_Hauteur);
            boutonSolution.Text = "Souhaitez-vous\nvoir la solution ?";
            boutonSolution.TextAlign = ContentAlignment.MiddleCenter;
            boutonSolution.Click += new System.EventHandler(this.boutonSolution_Click);
            boutonSolution.Visible = false;
            this.Controls.Add(boutonSolution);

            // ******************** BOUTON GRILLE ACTION ALL **********************
            InitialiserBouton();

        }

        private void boutonSolution_Click(object sender, EventArgs e)
        {
            FormeCavalier_Simulation fcs = new FormeCavalier_Simulation(depLig, depCol, fmpca.GetCcDef(), ccc);
            //fcs.Set
            fcs.ShowDialog();
        }

        // ******************** BOUTON GRILLE **********************

        // ******************** BOUTON GRILLE ACTION ALL **********************
        private void InitialiserBouton()
        {
            for (int ligne = 0; ligne < DD.GetGrille().GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < DD.GetGrille().GetLength(1); ++colonne)
                {
                    if (ligne >= DD.GetTailleMarge() && ligne < (DD.GetTailleMarge() + DD.GetTailleLig()))
                    {
                        if (colonne >= DD.GetTailleMarge() && colonne < (DD.GetTailleMarge() + DD.GetTailleCol()))
                        {
                            InitialiserBouton(DD.GetMonBouton(ligne, colonne));
                        }
                    }
                }
            }
        }

        private void ReduireActionBouton()
        {
            for (int ligne = 0; ligne < DD.GetGrille().GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < DD.GetGrille().GetLength(1); ++colonne)
                {
                    if (ligne >= DD.GetTailleMarge() && ligne < (DD.GetTailleMarge() + DD.GetTailleLig()))
                    {
                        if (colonne >= DD.GetTailleMarge() && colonne < (DD.GetTailleMarge() + DD.GetTailleCol()))
                        {
                            ReduireActionBouton(DD.GetMonBouton(ligne, colonne));
                        }
                    }
                }
            }
        }

        private void SuppActionBouton()
        {
            for (int ligne = 0; ligne < DD.GetGrille().GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < DD.GetGrille().GetLength(1); ++colonne)
                {
                    if (ligne >= DD.GetTailleMarge() && ligne < (DD.GetTailleMarge() + DD.GetTailleLig()))
                    {
                        if (colonne >= DD.GetTailleMarge() && colonne < (DD.GetTailleMarge() + DD.GetTailleCol()))
                        {
                            SuppActionBouton(DD.GetMonBouton(ligne, colonne));
                        }
                    }
                }
            }
        }

        // ******************** BOUTON GRILLE ACTION **********************
        private void InitialiserBouton(monBouton b)
        {
            b.Click += new System.EventHandler(this.Buttons_Click); //Associe le bouton b à l'évenement "buttons_click" définit ci-dessous
            //b.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            //b.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
        }
        private void ReduireActionBouton(monBouton b)
        {
            //b.MouseEnter -= new System.EventHandler(this.Buttons_MouseEnter);
            //b.MouseLeave -= new System.EventHandler(this.Buttons_MouseLeave);
        }
        private void SuppActionBouton(monBouton b)
        {
            b.Click -= new System.EventHandler(this.Buttons_Click);
            //b.MouseEnter -= new System.EventHandler(this.Buttons_MouseEnter);
            //b.MouseLeave -= new System.EventHandler(this.Buttons_MouseLeave);
        }


        // ******************** BOUTON PANNEAU RECOMMENCER **********************
        private void boutonRecommencer_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                boutonDepart.Visible = true;
                boutonRetour.Visible = false;
                labelVict.Visible = false;
                victoire = false;
                boutonSolution.Visible = false;

                cpClic = 0;
                cpRtClic = 0;
                entamer = false;
                DD.ReinitialiserDamier();
                InitialiserBouton();

                //reinitialiser bouton de depart
                depCol = -1;
                depLig = -1;
                monBoutonDep = null;

                boutonRecommencer.Visible = false;

            }
        }


        // ******************** BOUTON PANNEAU DEPART **********************
        private void boutonDepart_Click(object sender, EventArgs e)
        {
            if (sender is Button && (cpClic == 0))
            {
                boutonDepart.Visible = false;
                boutonRecommencer.Visible = true;
                labelVict.Visible = false;
                victoire = false;
                boutonSolution.Visible = false;

                entamer = true;

                depLig = aleatoire.Next(GetNbrCaseCoteDamier()) + GetNbrCaseMarge() ;
                depCol = aleatoire.Next(GetNbrCaseCoteDamier()) + GetNbrCaseMarge() ;
                
                monBouton bdest = DD.GetMonBouton(depLig, depCol);
                monBoutonDep = bdest;
                PremierDeplacerPiece(bdest);
            }
        }

        // ******************** BOUTON PANNEAU RETOUR **********************
        private void boutonRetour_Click(object sender, EventArgs e)
        {
            if (cpClic > rtClic)
            {
                RetourDpl(rtClic);
                ++cpRtClic;
                //labelTest.Text = "CPT=" + cpClic;
                if (cpClic <= rtClic)
                {
                    boutonRetour.Visible = false;
                }
                labelVict.Visible = false;
                victoire = false;
                boutonSolution.Visible = false;

            }

        }

        // ******************** MOUVEMENT **********************

        // ******************** MOUVEMENT RETOUR **********************
        
        public void RetourDpl (int retourNbr)
        {
            if (rtClic < cpClic)
            {
                monBouton btmp;

                //effacement
                for (int i = cpClic-1; i >= (cpClic-retourNbr); --i)
                {
                    btmp = dpl[i];
                    btmp.Reinitialiser();
                    CibleSup(btmp, piece);

                    DD.OuvrirDispo(btmp.getNumLigne(), btmp.getNumCol());
                    dpl[i] = null;
                }

                //ajouter detail initiaux
                cpClic -= retourNbr;
                btmp = dpl[cpClic - 1];
                btmp.Colorer();

                btmp.ImageAj(piece.GetImg());
                CibleAj(btmp, this.piece);

                piece.DeplacerF(btmp.getNumLigne(), btmp.getNumCol());
                jouLig = btmp.getNumLigne();
                jouCol = btmp.getNumCol();

                DD.FermerDispo(btmp.getNumLigne(), btmp.getNumCol());
            }
        }

        // ******************** MOUVEMENT POSSIBLE **********************
        /*
        private void Buttons_MouseEnter(object sender, EventArgs e)
        {
            if (sender is monBouton)
            {
                monBouton b = sender as monBouton; // cast necessaire
                for (int i = 0; i < piece.GetDepi().GetLength(0); ++i)
                {
                    DD.GetMonBouton(b.getNumLigne() + piece.GetDepi()[i], b.getNumCol() + piece.GetDepi()[i]).Text = "X";
                }
            }

        }

        private void Buttons_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                monBouton b = sender as monBouton;
                for (int i = 0; i < piece.GetDepi().GetLength(0); ++i)
                {
                    DD.GetMonBouton(b.getNumLigne() + piece.GetDepi()[i], b.getNumCol() + piece.GetDepi()[i]).Text = "";
                }
            }
        }
        */

        // ******************** AFFICHER CIBLE POSSIBLE **********************
        private void CibleAj(monBouton b, Cavalier p)
        {
            for (int i = 0; i < p.GetDepi().GetLength(0); ++i)
            {
                DD.SetButtonCible(b, piece.GetDepi()[i], piece.GetDepj()[i]);
            }
        }

        private void CibleSup(monBouton b, Cavalier p)
        {
            for (int i = 0; i < p.GetDepi().GetLength(0); ++i)
            {
                DD.UnSetButtonCible(b, piece.GetDepi()[i], piece.GetDepj()[i]);
            }
        }

        // ******************** MOUVEMENT JOUER **********************

        private void DeplacerPiece(monBouton bori, monBouton bdest)
        {
            if ( (piece.IsAtteignable(bdest.getNumLigne(), bdest.getNumCol())) )
            {
                bori.ImageSupp();
                bori.Assombrir();
                CibleSup(bori, piece);

                piece.Deplacer(bdest.getNumLigne(), bdest.getNumCol());
                
                bdest.ImageAj(piece.GetImg());
                DD.FermerDispo(bdest.getNumLigne(), bdest.getNumCol());

                jouLig = bdest.getNumLigne();
                jouCol = bdest.getNumCol();

                dpl[cpClic] = bdest;
                ++cpClic;

                CibleAj(bdest, this.piece);
            }

        }

        private void PremierDeplacerPiece(monBouton bdest)
        {
            if (DD.IsDispo(bdest.getNumLigne(), bdest.getNumCol()))
            {
                depLig = bdest.getNumLigne();
                depCol = bdest.getNumCol();

                this.piece = new Cavalier(bdest, choixCav);

                bdest.ImageAj(piece.GetImg());

                DD.FermerDispo(depLig, depCol);

                jouLig = depLig;
                jouCol = depCol;

                dpl[cpClic] = bdest;
                ++cpClic;

                CibleAj(bdest, this.piece);
            }
        }

        private void LabelVict(monBouton b,Cavalier p)
        {
            labelVict.Text = "";
            if (IsRestCibleDispo(b, p))
            {
                labelVict.Text = "";
            }
            else
            {
                if (DD.IsToutOccupe())
                {
                    labelVict.Text = "VICTOIRE !";
                    victoire = true;
                }
                else
                {
                    labelVict.Text = "DEFAITE...";
                    victoire = false ;
                }
                switch (cpRtClic)
                {
                    case 0:
                        labelVict.Text += "\n(avec aucun retour)";
                        break;
                    case 1:
                        labelVict.Text += "\n(avec un seul retour)";
                        break;
                    default:
                        labelVict.Text += "\n(avec " + cpRtClic + " retours)";
                        break;
                }
                labelVict.Text += "\nScore = " + cpClic + " pas.";
                labelVict.Visible = true;
                if (!victoire)
                {
                    boutonSolution.Visible = true;
                }
            }
        }

        private bool IsRestCibleDispo(monBouton bori, Cavalier p)
        {
            for (int i = 0; i < p.GetDepi().GetLength(0); ++i)
            {
                if (DD.IsDispoCible(bori, p.GetDepi()[i], p.GetDepj()[i])) 
                {
                    return true;
                }
            }
            return false;
        }

        private void Buttons_Click(object sender, EventArgs e) //Evenement "cliquer sur bouton"
        {
            //Les victoires, les défaites, sont liées à l'événement "click" de boutons.
            //Ainsi les conditions de victoire/défaite devront être ici.

            if (sender is monBouton)
            {
                monBouton bdest ;
                bdest = sender as monBouton;

                if (DD.IsDispo(bdest.getNumLigne(), bdest.getNumCol()))
                {
                    if (cpClic == 0 && !entamer)
                    {
                        boutonDepart.Visible = false;
                        boutonRecommencer.Visible = true;
                        monBoutonDep = bdest;
                        PremierDeplacerPiece(bdest);
                        entamer = true;
                    }
                    else
                    {
                        if (piece.IsAtteignable(bdest.getNumLigne(), bdest.getNumCol()) ) 
                        {
                            //deplacement piece
                            monBouton bori = DD.GetMonBouton(jouLig, jouCol);
                            DeplacerPiece(bori, bdest);

                            //boutonRetour
                            if ( cpClic > rtClic)
                            {
                                boutonRetour.Visible = true;
                            }
                            else
                            {
                                boutonRetour.Visible = false;
                            }

                            //labelVictoire
                            LabelVict(bdest, piece);
                        }
                    }
                    // labelTest.Text = "CPT=" + cpClic;
                }
            }
        }

        // ******************** GETTEURS**********************
        public int getDepCoL()
        {
            return depCol;

        }
        public int getDepLig()
        {
            return depLig;
        }
        /*
        public monBouton GetMonBoutonDep()
        {
            return monBoutonDep;
        }
        */
        public bool getVictoire()
        {
            return victoire;
        }

        // ************************* PARAMETRES *********************************

        private void ColorerMesBoutons()
        {
            for (int i = 0; i < DD.GetGrille().GetLength(0); i++)
                for (int j = 0; j < DD.GetGrille().GetLength(1); j++)
                {
                    DD.GetMonBouton(i, j).SetColor(cf, cc);
                    DD.GetMonBouton(i, j).Colorer();
                }
        }

        private void choixDuDestrierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cpClic != 0)
            {
                DialogResult reponse = MessageBox.Show(
               "Lorsque la partie est lancée,\nLa modification des paramètres est impossible.",
               "Modifications impossibles",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button3,
               MessageBoxOptions.RightAlign);
            }
            else
            {
                fmpca.ShowDialog();
                if (fmpca.GetConfirmation())
                {
                    choixCav = fmpca.GetCcDef();
                }
            }
        }

        private void couleursDuDamierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cpClic != 0)
            {
                DialogResult reponse = MessageBox.Show(
               "Lorsque la partie est lancée,\nLa modification des paramètres est impossible.",
               "Modifications impossibles",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button3,
               MessageBoxOptions.RightAlign);
            }
            else
            {
                fmpco.ShowDialog();
                if (fmpco.GetConfirmation())
                {
                    cc = fmpco.GetCcDef();
                    cf = fmpco.GetCfDef();
                    ccc = fmpco.GetChoixCombiColorDef();
                    ColorerMesBoutons();
                }
            }

        }
    }
}