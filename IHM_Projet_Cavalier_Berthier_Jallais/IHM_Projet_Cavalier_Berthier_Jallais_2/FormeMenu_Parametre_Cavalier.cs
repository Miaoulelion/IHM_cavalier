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
    public partial class FormeMenu_Parametre_Cavalier : Form
    {
        private int cc; //choix cavalier
        private int ccDef; // choix cavalier apres validation
        private bool confirmation;
        public FormeMenu_Parametre_Cavalier()
        {
            InitializeComponent();
            
            cc = 1;
            ccDef = 1;

            radioButton1.Checked = true;
        }

        public FormeMenu_Parametre_Cavalier(int choixCav)
        {
            InitializeComponent();

            if (choixCav >=1 && choixCav <= 3)
            {
                cc = choixCav;
                ccDef = choixCav;

                switch (choixCav)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        break;
                    case 3:
                        radioButton3.Checked = true;
                        break;
                    default:
                        radioButton1.Checked = true;
                        break;
                }

            }
        }

        private void FormeMenu_Parametre_Cavalier_Load(object sender, EventArgs e)
        {
            this.Text = "Paramètres - Couleurs du destrier.";

            groupBox1.Text = "Destriers";

            radioButton1.Text = "Perceval\n( mode normal )";
            radioButton2.Text = "Lancelot\n( mode normal )";
            radioButton3.Text = "Mickey\n( mode triche )";

            pictureBox1.BackgroundImage = Image.FromFile("images/cheval.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox2.BackgroundImage = Image.FromFile("images/cheval2.png");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox3.BackgroundImage = Image.FromFile("images/cheval3.png");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;


        }

        private void bValider_Click(object sender, EventArgs e)
        {
            ccDef = cc;
            confirmation = true;
            this.Close();
        }

        private void bRetour_Click(object sender, EventArgs e)
        {
            confirmation = false;
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cc = 3;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cc = 2;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cc = 1;
        }

        public int GetCcDef()
        {
            return ccDef;
        }

        public bool GetConfirmation()
        {
            return confirmation;
        }
    }
}
