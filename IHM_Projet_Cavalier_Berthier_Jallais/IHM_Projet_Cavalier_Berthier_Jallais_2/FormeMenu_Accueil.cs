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
    public partial class FormeMenu_Accueil : Form
    {
        public FormeMenu_Accueil()
        {
            InitializeComponent();
        }

        private void FormeMenu_Accueil_Load(object sender, EventArgs e)
        {
            this.Text = "Jeu du Cavalier - BERTHIER Nicolas et JALLAIS Adrien - Projet IHM.";
            this.BackColor = Color.White ;
            this.BackgroundImage = Image.FromFile("images/Accueil.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FormeCavalier_Jeu fcj = new FormeCavalier_Jeu();
            fcj.ShowDialog() ;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FormeCavalier_Simulation fcs = new FormeCavalier_Simulation();
            fcs.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FormeMenu_Regles fmr = new FormeMenu_Regles();
            fmr.ShowDialog();
        }
    }
}
