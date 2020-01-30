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
    public partial class FormeMenu_Parametre_Couleur : Form
    {
        private Color cc; //couleur claire
        private Color cf; //couleur fonce
        private int choixCombiColor;

        private Color ccDef; //couleur claire apres validation
        private Color cfDef; //couleur fonce apres validation
        private int choixCombiColorDef;

        private bool confirmation;

        public FormeMenu_Parametre_Couleur()
        {
            InitializeComponent();
            cc = Color.Beige; // plus claire
            cf = Color.SaddleBrown; //plus fonce
            ccDef = Color.Beige; // plus claire
            cfDef = Color.SaddleBrown; //plus fonce
            choixCombiColor = 1;
            choixCombiColorDef = 1;
            radioButton1.Checked = true;
        }

        public FormeMenu_Parametre_Couleur(Color choixCouleurCF, Color choixCouleurCC)
        {
            InitializeComponent();
            cc = choixCouleurCC; // plus claire
            cf = choixCouleurCF; //plus fonce
            ccDef = choixCouleurCC; // plus claire
            cfDef = choixCouleurCF; //plus fonce

            radioButton1.Checked = true;
        }

        public FormeMenu_Parametre_Couleur(int choixCombiColor)
        {
            InitializeComponent();
            this.choixCombiColorDef = choixCombiColor;
            switch (choixCombiColor)
            {
                case 1:
                    cc = Color.Beige; // plus claire
                    cf = Color.SaddleBrown; //plus fonce
                    ccDef = Color.Beige; // plus claire
                    cfDef = Color.SaddleBrown; //plus fonce
                    radioButton1.Checked = true;
                    break;
                case 2:
                    cc = Color.FromArgb(233, 163, 201); //plus claire
                    cf = Color.FromArgb(161, 215, 106); //plus fonce
                    ccDef = Color.FromArgb(233, 163, 201); //plus claire
                    cfDef = Color.FromArgb(161, 215, 106); //plus fonce
                    radioButton2.Checked = true;
                    break;
                case 3:
                    cc = Color.FromArgb(241, 163, 64); // plus claire
                    cf = Color.FromArgb(153, 142, 195); //plus fonce
                    ccDef = Color.FromArgb(241, 163, 64); // plus claire
                    cfDef = Color.FromArgb(153, 142, 195); //plus fonce
                    radioButton3.Checked = true;
                    break;
                case 4:
                    cc = Color.FromArgb(239, 138, 98); // plus claire
                    cf = Color.FromArgb(103, 169, 207); //plus fonce
                    ccDef = Color.FromArgb(239, 138, 98); // plus claire
                    cfDef = Color.FromArgb(103, 169, 207); //plus fonce
                    radioButton4.Checked = true;
                    break;
                case 5:
                    cc = Color.FromArgb(216, 179, 101); // plus claire
                    cf = Color.FromArgb(90, 180, 172); //plus fonce
                    ccDef = Color.FromArgb(216, 179, 101); // plus claire
                    cfDef = Color.FromArgb(90, 180, 172); //plus fonce
                    radioButton5.Checked = true;
                    break;
                default:
                    cc = Color.Beige; // plus claire
                    cf = Color.SaddleBrown; //plus fonce
                    ccDef = Color.Beige; // plus claire
                    cfDef = Color.SaddleBrown; //plus fonce
                    radioButton1.Checked = true;
                    choixCombiColorDef = 1;
                    break;
            }
        }

        private void FormeMenu_Parametre_Couleur_Load(object sender, EventArgs e)
        {
            this.Text = "Paramètres - Damier de fromage.";

            groupBox1.Text = "Fromages AOP";

            radioButton1.Text = "Abondance";
            pictureBox1.BackColor = Color.Beige; // plus claire
            pictureBox2.BackColor = Color.SaddleBrown; //plus fonce

            radioButton2.Text = "Chaource";
            pictureBox3.BackColor = Color.FromArgb(233, 163, 201); //plus claire
            pictureBox4.BackColor = Color.FromArgb(161, 215, 106); //plus fonce

            radioButton3.Text = "Laguiole";
            pictureBox5.BackColor = Color.FromArgb(241, 163, 64); // plus claire
            pictureBox6.BackColor = Color.FromArgb(153, 142, 195); //plus fonce

            radioButton4.Text = "Chavignol";
            pictureBox7.BackColor = Color.FromArgb(239, 138, 98); // plus claire
            pictureBox8.BackColor = Color.FromArgb(103, 169, 207); //plus fonce

            radioButton5.Text = "Valencay";
            pictureBox9.BackColor = Color.FromArgb(216, 179, 101); // plus claire
            pictureBox10.BackColor = Color.FromArgb(90, 180, 172); //plus fonce
        }

        public Color GetCfDef()
        {
            return cfDef;
        }
        public Color GetCcDef()
        {
            return ccDef;
        }
        public int GetChoixCombiColorDef()
        {
            return choixCombiColorDef;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton5.Text = "Abondance";
            cc = Color.Beige; // plus claire
            cf = Color.SaddleBrown; //plus fonce
            choixCombiColor = 1;
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton2.Text = "Chaource";
            cc = Color.FromArgb(233, 163, 201); //plus claire
            cf = Color.FromArgb(161, 215, 106); //plus fonce
            choixCombiColor = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton3.Text = "Laguiole";
            cc = Color.FromArgb(241, 163, 64); // plus claire
            cf = Color.FromArgb(153, 142, 195); //plus fonce
            choixCombiColor = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton4.Text = "Chavignol";
            cc = Color.FromArgb(239, 138, 98); // plus claire
            cf = Color.FromArgb(103, 169, 207); //plus fonce
            choixCombiColor = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton1.Text = "Valencay";
            cc = Color.FromArgb(216, 179, 101); // plus claire
            cf = Color.FromArgb(90, 180, 172); //plus fonce
            choixCombiColor = 5;
        }

        private void bValider_Click(object sender, EventArgs e)
        {
            ccDef = cc;
            cfDef = cf;
            choixCombiColorDef = choixCombiColor;
            confirmation = true;
            this.Close();
        }

        private void bRetour_Click(object sender, EventArgs e)
        {
            confirmation = false;
            this.Close();
        }

        public bool GetConfirmation() { 
            return confirmation;
        }


    }
}
