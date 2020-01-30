using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    public partial class FormeDamierTemplate : Form
    {
        private DamierDispo dD;
        private int nbrCaseCoteDamier; //nbr ligne et colonne de notre damier
        private int nbrCaseMarge; //taille de la marge par bord
        private int tailleBoutonPX; // taille d un cote du bouton
        private int largeurPannelPX;
        private FormeMenu_Parametre_Cavalier fmpca ;
        private FormeMenu_Parametre_Couleur fmpco;


        //definition de POSITION du damier
        private int posX;
        private int posY;

        //Parametres de personnnalisation
        //cavalier
        private int cavChoix;
        //couleur damier
        private Color colorF;//fonce
        private Color colorC;//clair

        internal DamierDispo DD { get => dD; }

        public FormeDamierTemplate()
        {
            InitializeComponent();

             //Parametres de personnnalisation PAR DEFAULTS
             //cavalier
            cavChoix = 1;
            //couleur damier
            colorF = Color.SaddleBrown ;//fonce
            colorC = Color.Beige;//clair

            //damier
            this.nbrCaseCoteDamier = 8;
            this.tailleBoutonPX = 50;
            this.nbrCaseMarge = 2;

            //definition de POSITION du damier
            this.posX = 0;
            this.posY = 50;
            dD = new DamierDispo(this.nbrCaseCoteDamier, this.tailleBoutonPX, this.nbrCaseMarge, this.posX, this.posY, this.colorC, this.colorF);
            this.largeurPannelPX = 100; //100 pour les boutons du panneau

            //fenêtre
            // Size the form to be  pixels in height and width.
            int Largeur = GetTailleDamierCol() + largeurPannelPX + posX; // 533
            int Hauteur = GetTailleDamierLig() + posY ; // 483
            //this.Size = new Size(Largeur, Hauteur);
            //this.Size = new Size(600, 500);
            this.Size = new Size(1200, 1500);
            // Display the form in the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void FormeDamierTemplate_Load(object sender, EventArgs e)
        {
            foreach (monBouton b in DD.GetGrille())
            {
                this.Controls.Add(b);
            }
            foreach (Label l in DD.GetLabels())
            {
                this.Controls.Add(l);
            }

            this.Text = "Jeu du Cavalier - BERTHIER Nicolas et JALLAIS Adrien";
        }

        public int GetNbrCaseCoteDamier()
        {
            return nbrCaseCoteDamier;
        }
        public int GetNbrCaseMarge()
        {
            return nbrCaseMarge;
        }
        public int GetTailleBoutonPX()
        {
            return tailleBoutonPX;
        }
        public int GetLargeurPannelPX()
        {
            return largeurPannelPX;
        }

        public int GetTailleDamierCol() {
            return (DD.GetTailleCol() * (DD.GetTailleBouton() + 1) + DD.GetTailleLabel() );
        }
        public int GetTailleDamierLig()
        {
            return (DD.GetTailleLig() * (DD.GetTailleBouton() + 1) + DD.GetTailleLabel());
        }

        private void couleursDuDamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmpco = new FormeMenu_Parametre_Couleur();
            fmpco.ShowDialog();
            colorF = fmpco.GetCcDef();
            colorC = fmpco.GetCfDef();
        }

        private void choixDeLaMontureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmpca = new FormeMenu_Parametre_Cavalier();
            fmpca.ShowDialog();
            cavChoix = fmpca.GetCcDef();
        }

        public int GetCavChoix()
        {
            return cavChoix;
        }

        public int GetPosX()
        {
            return posX;
        }
        public int GetPosY()
        {
            return posY;
        }

    }
}
