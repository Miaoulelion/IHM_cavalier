using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHM_Projet_Cavalier_Berthier_Jallais_2;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    public partial class FormeCavalier_Simulation : IHM_Projet_Cavalier_Berthier_Jallais_2.FormeDamierTemplate
    {

        private Button boutonDepart;
        private Button boutonPause;
        private Button boutonReprise;
        private Button boutonRecommencer;

        private Label label1; //Label "Temps de Pause\npar Enjambées"
        //private Label labelDuree; //labelDuree.Text = "(en\nsecondes)" ;
        //private Label labelPas; //labelPas.Text = "(en\npas)"

        //private Label label2;

        private ListBox ListChoixDuree;
        private int ChoixDuree;
        private ListBox ListNbrPas;
        private int NbrPas;

        private int depCol;//première colonne jouée (aléatoirement ou non)
        private int depLig; //idem pour ligne (aléatoirement ou non)

        private bool simulationLancee; // la simulation est elle lance ?
        private bool stopSimulation; // si vrai empeche de lancer la simulation ( pour eviter de relancer un deuxieme cheval)
        private bool pause;
        private int rang; //securise la pause et la reprise de la simulation
        private int cpClic; //nbr de tours (jeux) passés

        private int[] IAchoixLigne; // stocke les mouvements à faire par l IA
        private int[] IAchoixColonne;
        private monBouton[] IAchoix;

        private int[,] damierNum;

        private Random aleatoire; //pour choisir une case au hasard de depart

        //fenetre paramètre 
        private FormeMenu_Parametre_Couleur fmpco;
        private FormeMenu_Parametre_Cavalier fmpca;

        private Cavalier piece;
        private int choixCav;

        private Color cc; // couleur claire
        private Color cf; // couleur fonce

        private bool demo;

        // ******************** CONSTRUCTEUR **********************
        public FormeCavalier_Simulation()
        {
            InitializeComponent();
            aleatoire = new Random();
            //constante A NE PAS CHANGER
            cpClic = 0;
            rang = 0;
            pause = false;
            stopSimulation = false;
            simulationLancee = false;
            demo = false;

            //VARIABLE A CHANGER AVEC AUTRE FORME
            //choix du cavalier
            fmpca = new FormeMenu_Parametre_Cavalier();
            choixCav = fmpca.GetCcDef();

            // choixCav = FormeMenu_Parametre_Cavalier.GetCcDef();
            // choix du damier
            fmpco = new FormeMenu_Parametre_Couleur();
            cc = fmpco.GetCcDef();
            cf = fmpco.GetCfDef();

            //obliger user a choisir nbrpas et duree
            NbrPas = (5);
            ChoixDuree = (2);
        }

        public FormeCavalier_Simulation(int depLig, int depCol, int choixCav, int choixCombiColor)
        {
            InitializeComponent();
            FormeCavalier_Simulation_Load(null,null);
            aleatoire = new Random();
            //constante A NE PAS CHANGER
            cpClic = 0;
            rang = 0;
            pause = false;
            stopSimulation = false;
            simulationLancee = false;
            demo = true;

            //VARIABLE A CHANGER AVEC AUTRE FORME
            //choix du cavalier
            fmpca = new FormeMenu_Parametre_Cavalier(choixCav);
            this.choixCav = fmpca.GetCcDef();

            // choixCav = FormeMenu_Parametre_Cavalier.GetCcDef();
            // choix du damier
            fmpco = new FormeMenu_Parametre_Couleur(choixCombiColor);
            cc = fmpco.GetCcDef();
            cf = fmpco.GetCfDef();
            ColorerMesBoutons();

            //obliger user a choisir nbrpas et duree
            NbrPas = (5);
            ChoixDuree = (2);

            monBouton b = DD.GetMonBouton(depLig, depCol);
            Buttons_Click(b,null);
        }

        // ******************** CHARGEMENT  **********************
        private void FormeCavalier_Simulation_Load(object sender, EventArgs e)
        {
            this.Text += " - MODE SIMULATION.";

            // ******************** BOUTON PANNEAU DISPOSITION **********************

            //fenêtre
            // Size the form to be  pixels in height and width.
            int Largeur = GetTailleDamierCol() + GetLargeurPannelPX() + GetPosX(); // 533
            int Hauteur = GetTailleDamierLig() + GetPosY(); // 483
            //this.Size = new Size(Largeur, Hauteur);
            this.Size = new Size(600, 530);


            //this.Size = new Size(600, 500);
            //this.Size = new Size(800, 800);
            // Display the form in the center of the screen.
            //this.StartPosition = FormStartPosition.CenterScreen;

            // ******************** BOUTON PANNEAU DISPOSITION **********************
            int PosIniY = GetPosY();
            int PosIniX = GetPosX();
            int TailleDamier = this.GetTailleDamierCol(); //nbr px entre le damier et les boutons du panneau (doit pourvoir laisser apparraite les labels des lignes du damier
            int TailBut = this.GetLargeurPannelPX(); //nbr pixel de la taille (hauteur et largeur) des boutons et carre du panneau
            int TailLabel_Hauteur = TailBut - PosIniY; //le label de victoire est plus petit pour permettre de se limite a la taille du damier

            //Création des boutons du jeu

            //Bouton de pause
            boutonPause = new Button();
            boutonPause.Location = new Point(8 * 50 + 50 + PosIniX, 0 + PosIniY);
            boutonPause.Size = new Size(100, 25);
            boutonPause.Text = "Pause";
            //boutonPause.Visible = false;
            this.Controls.Add(boutonPause);
            boutonPause.Click += new System.EventHandler(this.boutonPause_Click);

            //Bouton de reprise
            boutonReprise = new Button();
            boutonReprise.Location = new Point(8 * 50 + 50 + PosIniX, 0 + PosIniY);
            boutonReprise.Size = new Size(100, 25);
            boutonReprise.Text = "Reprise";
            this.Controls.Add(boutonReprise);
            boutonReprise.Click += new System.EventHandler(this.boutonReprise_Click);
            //boutonReprise.Visible = false;


            //Bouton Choix aléatoire de la coordonnée de départ
            boutonDepart = new Button();
            boutonDepart.Location = new Point(8 * 50 + 50 + PosIniX, 25 + PosIniY);
            boutonDepart.Size = new Size(100, 100);
            boutonDepart.Text = "Choisir aléatoirement la case départ";
            this.Controls.Add(boutonDepart);
            boutonDepart.Click += new System.EventHandler(this.boutonDepart_Click);


            //Bouton pour réinitialiser la partie
            boutonRecommencer = new Button();
            boutonRecommencer.Location = new Point(8 * 50 + 50 + PosIniX, 125 + PosIniY);
            boutonRecommencer.Size = new Size(100, 100);
            boutonRecommencer.Text = "Reinitialiser la simulation";
            //boutonRecommencer.Visible = false;
            this.Controls.Add(boutonRecommencer);
            boutonRecommencer.Click += new System.EventHandler(this.boutonReinitialiser_Click);

            //Label "Temps de Pause\npar Enjambées"
            label1 = new Label();
            label1.Location = new Point(8 * 50 + 40 + PosIniX, 245 + PosIniY);
            label1.Size = new Size(125,30); 
            label1.Text = "Temps de \n Pause / Enjambées";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(label1);

            //Listbox, choix de la durée des pauses
            ListChoixDuree = new ListBox();
            ListChoixDuree.Location = new Point(8 * 50 + 50 +PosIniX, 280+ PosIniY); // 260 = (255 + 5)
            ListChoixDuree.Size = new Size(50, 60);
            this.Controls.Add(ListChoixDuree);
            ListChoixDuree.Items.Add(0);
            ListChoixDuree.Items.Add(2);
            ListChoixDuree.Items.Add(5);
            ListChoixDuree.Click += new System.EventHandler(this.ListChoixDuree_Click);

            /*
            //Label "(en\nsec)" ;
            labelDuree = new Label();
            labelDuree.Location = new Point(8 * 50 +  PosIniX, 260+50 + PosIniY);
            labelDuree.Size = new Size(30, 30);
            labelDuree.Text = "en\nsec" ;
            labelDuree.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(labelDuree);
            */
            //Listbox, choix du nombre de pauses
            ListNbrPas = new ListBox();
            ListNbrPas.Location = new Point(8 * 50 + 110 + PosIniX, 280+ PosIniY); // 625 = (320+5)
            ListNbrPas.Size = new Size(50, 60);
            this.Controls.Add(ListNbrPas);
            ListNbrPas.Items.Add(1);
            ListNbrPas.Items.Add(5);
            ListNbrPas.Items.Add(64);
            ListNbrPas.Click += new System.EventHandler(this.ListNbrePauses_Click);
            /*
            //Label  "(en\npas)"
            labelPas = new Label();
            labelPas.Location = new Point(8 * 50 +  PosIniX, 325+50 + PosIniY);
            labelPas.Size = new Size(30, 30);
            labelPas.Text = "en\npas";
            labelPas.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(labelPas);
            */

            // ******************** BOUTON GRILLE ACTION ALL **********************
            InitialiserBouton();
            AddMesBoutons();

            // ******************** SIMULATION AFFICHAGE **********************
            if (demo)
            {
                boutonPause.Visible = true;
                boutonReprise.Visible = false;

                label1.Visible = false;
                ListChoixDuree.Visible = false;
                //labelDuree.Visible = false;
                ListNbrPas.Visible = false;
                //labelPas.Visible = false;
                boutonDepart.Visible = false;
                boutonRecommencer.Visible = false;
                
            }
            else
            {
                boutonPause.Visible = false;  // dif
                boutonReprise.Visible = false;

                label1.Visible = true;  // dif
                ListChoixDuree.Visible = true;  // dif
                //labelDuree.Visible = true; // dif
                ListNbrPas.Visible = true;  // dif
                //labelPas.Visible = true;  // dif

                boutonDepart.Visible = true; // dif
                boutonRecommencer.Visible = false;
            }
        }

        private void AddMesBoutons()
        {
            for (int i = 0; i < DD.GetGrille().GetLength(0); i++)
                for (int j = 0; j < DD.GetGrille().GetLength(1); j++)
                {
                    this.Controls.Add(DD.GetMonBouton(i, j));
                }
        }

        // ******************** BOUTON GRILLE ACTION **********************
        //all bouton
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
        // 1 bouton
        private void InitialiserBouton(monBouton b)
        {
            b.Click += new System.EventHandler(this.Buttons_Click); //Associe le bouton b à l'évenement "buttons_click" définit ci-dessous
            //b.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            //b.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
        }

        private void Buttons_Click(object sender, EventArgs e)
        {
            if (sender is monBouton && (cpClic == 0) && !pause)
            {
                if (NbrPas == (-1) || ChoixDuree == (-1))
                {
                    DialogResult reponse = MessageBox.Show(
                       "La sélection du nombre de pas et de la durée de la pause est incorrecte." +
                       "Veuillez recommencer la partie avec des sélections correctes.",
                       "Erreur de saisie",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button3,
                       MessageBoxOptions.RightAlign);
                }
                else
                {
                    monBouton b = sender as monBouton;

                    ++cpClic;

                    // b.Enabled = false;
                    depLig = b.getNumLigne();
                    depCol = b.getNumCol();
                    if (demo)
                    {
                        boutonPause.Visible = false;

                        boutonDepart.Visible = false;
                        boutonRecommencer.Visible = false;

                        label1.Visible = false;
                        ListChoixDuree.Visible = false;
                        ListNbrPas.Visible = false;
                    }
                    else
                    {
                        boutonPause.Visible = true;

                        boutonDepart.Visible = false;
                        boutonRecommencer.Visible = false;

                        label1.Visible = false;
                        ListChoixDuree.Visible = false;
                        ListNbrPas.Visible = false;
                    }


                    piece = new Cavalier(b, choixCav);
                    IAchoixRemplissage(b);
                    LancerSimulation();
                }

            }
        }

        // ******************** PANNEAU **********************

        // ******************** LABEL PANNEAU **********************

        private void ListNbrePauses_Click(object sender, EventArgs e)
        {
            this.NbrPas = Convert.ToInt32(ListNbrPas.Text);
        }

        private void ListChoixDuree_Click(object sender, EventArgs e)
        {
            this.ChoixDuree = Convert.ToInt32(ListChoixDuree.Text);
        }
        // ******************** BOUTON PANNEAU **********************

        // ******************** BOUTON PANNEAU RECOMMENCER **********************
        private void boutonReinitialiser_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                if (!simulationLancee)
                {
                    stopSimulation = true;
                    pause = false;

                    boutonDepart.Visible = true;
                    boutonRecommencer.Visible = false;

                    label1.Visible = true ;
                    ListChoixDuree.Visible = true ;
                    ListNbrPas.Visible = true ;

                    boutonPause.Visible = false;
                    boutonReprise.Visible = false;

                    cpClic = 0;
                    rang = 0;
                    DD.ReinitialiserDamier();
                }
                else
                {
                    stopSimulation = true;
                    pause = false;

                    boutonDepart.Visible = true;
                    boutonRecommencer.Visible = false;

                    label1.Visible = true;
                    ListChoixDuree.Visible = true;
                    ListNbrPas.Visible = true;

                    boutonPause.Visible = false;
                    boutonReprise.Visible = false;

                    cpClic = 0;
                    rang = 0;
                    DD.ReinitialiserDamier();
                }
            }
        }

        private void boutonPause_Click(object sender, EventArgs e)
        {
            pause = true;
            boutonPause.Visible = false;
            boutonReprise.Visible = true;

            label1.Visible = true ;

            boutonDepart.Visible = false;
            boutonRecommencer.Visible = true;
            ListChoixDuree.Visible = true ;
            ListNbrPas.Visible = true;
        }
        private void boutonReprise_Click(object sender, EventArgs e)
        {
            pause = false;
            boutonReprise.Visible = false;
            boutonPause.Visible = true;

            label1.Visible = false ;

            boutonDepart.Visible = false;
            boutonRecommencer.Visible = false;
            ListChoixDuree.Visible = false;
            ListNbrPas.Visible = false;
            LancerSimulation();
        }

        // ******************** BOUTON PANNEAU DEPART **********************
        private void boutonDepart_Click(object sender, EventArgs e)
        {
            if (sender is Button && (cpClic == 0) && !pause)
            {

                ////// ESSAI NICO ADRI
                cpClic = 0;
                rang = 0;
                pause = false;
                stopSimulation = false;
                simulationLancee = false;
                //////
                ///
                ++cpClic;
                boutonPause.Visible = true;

                boutonDepart.Visible = false;
                boutonRecommencer.Visible = false;

                label1.Visible = false;
                ListChoixDuree.Visible = false;
                ListNbrPas.Visible = false;
 
                depLig = aleatoire.Next(GetNbrCaseCoteDamier()) + GetNbrCaseMarge();
                depCol = aleatoire.Next(GetNbrCaseCoteDamier()) + GetNbrCaseMarge();

                monBouton bdest = DD.GetMonBouton(depLig, depCol);
                piece = new Cavalier(bdest, choixCav);
                IAchoixRemplissage(bdest);
                LancerSimulation() ;

            }
        }


        // ******************** SIMULATION **********************
        private async void LancerSimulation()
        {
            bool respectTpsAttente ;
            int delai;
            simulationLancee = true ;
            try
            {
                for (int i = rang; i < 64 && (!pause) && (!stopSimulation) ; ++i)
                {
                    if (stopSimulation)
                    {
                        i = 65 ;
                        break;
                    }
                    if (!pause)
                    {
                        if (stopSimulation)
                        {
                            i = 65;
                            break;
                        }
                        if (i == 0)
                        {
                            PremierDeplacerPiece(IAchoix[i]);
                            //MajBoutons();
                            ++rang;
                        }
                        if (i < 64 && i > 0 )
                        {
                            if (stopSimulation)
                            {
                                i = 65;
                                break;
                            }
                            else
                            {
                                respectTpsAttente = false;
                                if (NbrPas == 0)
                                {
                                    //ADRIEN
                                    //delai = 1;
                                    //await Task.Delay(delai);

                                    if (stopSimulation)
                                    {
                                        i = 65;
                                        break;
                                    }
                                    else
                                    {
                                        if (!pause)
                                        {
                                            DeplacerPiece(IAchoix[i - 1], IAchoix[i]);
                                            IAchoix[i - 1].Text = "" + i;
                                            //MajBoutons();
                                            ++rang;
                                        }
                                    }
                                }
                                if (NbrPas > 0)
                                {
                                    //ADRIEN
                                    //delai = 1;
                                    //await Task.Delay(delai);
                                    //NICOLAS sachant 
                                    //LancerSimulation(int tempsAttente, int nbrPas)
                                    //LancerSimulation(this.ChoixDuree, this.NbrPas);

                                    if (stopSimulation)
                                    {
                                        i = 65;
                                        break;
                                    }
                                    if (i % NbrPas == 0 && i != 0 && i < 64)
                                    {
                                        await Task.Delay(ChoixDuree * 1000);
                                    }

                                    if (stopSimulation)
                                    {
                                        i = 65;
                                        break;
                                    }
                                    else
                                    {
                                        respectTpsAttente = true;
                                        if (respectTpsAttente && !pause)
                                        {
                                            if (stopSimulation)
                                            {
                                                i = 65;
                                                break;
                                            }
                                            else
                                            {
                                                DeplacerPiece(IAchoix[i - 1], IAchoix[i]);
                                                IAchoix[i - 1].Text = "" + i;
                                                //MajBoutons();
                                                ++rang;
                                                if (i == 63)
                                                {
                                                    boutonPause.Visible = false;  // dif
                                                    boutonReprise.Visible = false;

                                                    boutonRecommencer.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (pause)
                    {
                        break ;
                    }
                }
            }
            catch (Exception)
            {
                DialogResult reponse = MessageBox.Show(
                "La sélection du nombre de pas et de la durée de la pause est incorrecte." +
                "Veuillez recommencer la partie avec des sélections correctes.",
                "Erreur de saisie",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button3,
                MessageBoxOptions.RightAlign);
            }
            simulationLancee = false;


        }

        // ************************* DEPLACEMENT PIECE **************************

        private void DeplacerPiece(monBouton bori, monBouton bdest)
        {
            if ((piece.IsAtteignable(bdest.getNumLigne(), bdest.getNumCol())))
            {
                bori.ImageSupp();
                bori.Assombrir();
                //CibleSup(bori, piece);

                piece.Deplacer(bdest.getNumLigne(), bdest.getNumCol());
                bdest.ImageAj(piece.GetImg());
                DD.FermerDispo(bdest.getNumLigne(), bdest.getNumCol());



                //CibleAj(bdest, piece);
            }

        }

        private void PremierDeplacerPiece(monBouton bdest)
        {
            if (DD.IsDispo(bdest.getNumLigne(), bdest.getNumCol()))
            {
                depLig = bdest.getNumLigne();
                depCol = bdest.getNumCol();

                piece = new Cavalier(bdest, choixCav);

                bdest.ImageAj(piece.GetImg());

                DD.FermerDispo(depLig, depCol);

                //CibleAj(bdest, piece);
            }
        }

        // ************************* SIMULATION DamierNum *********************************

        private void InitialisationDamierNum()
        {
            //damierNum = new int[DD.GetGrille().GetLength(0), DD.GetGrille().GetLength(1)];
            damierNum = new int[12, 12];
        }

        private void InitialisationIAChoix()
        {
            //8*8 = 64
            IAchoixLigne = new int[DD.GetTailleLig() * DD.GetTailleCol()];
            IAchoixColonne = new int[DD.GetTailleLig() * DD.GetTailleCol()];
            IAchoix = new monBouton[DD.GetTailleLig() * DD.GetTailleCol()];
        }

        //  Parcours de la piece (cavalier) sur l'échiquier à partir d une case donnee
        private void IAchoixRemplissage(monBouton b)
        {
            //initialisation
            InitialisationIAChoix(); 
            InitialisationDamierNum();

            //simulation
            int ii = b.getNumLigne() - 1;
            int jj = b.getNumCol() - 1;
            piece = new Cavalier (b, choixCav);
            int[] depi = piece.GetDepi();
            int [] depj = piece.GetDepj();
            Euler euler = new Euler(depi,depj) ;

            damierNum = euler.getEchec(ii, jj);

            int rangBouton;
            monBouton btmp;
            for (int i = 0 ; i < damierNum.GetLength(0); ++i)
            {
                for (int j = 0; j < damierNum.GetLength(1); ++j)
                {
                    // on ne veut pas certaines des 12*12 cases ( 0 et -1)
                    if (damierNum[i, j] >= 1 )
                    {
                        //extraction
                        rangBouton = damierNum[i, j]; //rang allant de 1 à 64
                        btmp = DD.GetMonBouton(i, j);
                        //assignation
                        IAchoix[rangBouton - 1] = btmp; //(rang-1) allant de 0 à 63
                        IAchoixLigne[rangBouton - 1] = btmp.getNumLigne(); 
                        IAchoixColonne[rangBouton - 1] = btmp.getNumCol();
                    }

                }
            }
        }

        // ************************* PARAMETRES *********************************

        private void couleursDuDamierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( cpClic != 0)
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
                    ColorerMesBoutons();
                }
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

        private void ColorerMesBoutons()
        {
            for (int i = 0; i < DD.GetGrille().GetLength(0); i++)
                for (int j = 0; j < DD.GetGrille().GetLength(1); j++)
                {
                    DD.GetMonBouton(i, j).SetColor(cf,cc);
                    DD.GetMonBouton(i, j).Colorer() ;
                }
        }
    }
}
