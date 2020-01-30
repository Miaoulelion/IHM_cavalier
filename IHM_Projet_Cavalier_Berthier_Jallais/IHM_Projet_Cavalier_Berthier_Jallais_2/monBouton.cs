using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    class monBouton : Button
    {
        private int numCol;
        private int numLigne;
        private Color cc;
        private Color cf;
        public monBouton(int numLigne, int numCol)
        {
            this.numCol = numCol;
            this.numLigne = numLigne;
            cf = Color.SaddleBrown;
            cc = Color.Beige;
            // cc = FormeMenu_Parametre_Couleur.GetCcDef();
            // cf = FormeMenu_Parametre_Couleur.GetCfDef();
        }

        public monBouton(int numLigne, int numCol, Color colorChoixCF, Color colorChoixCC)
        {
            this.numCol = numCol;
            this.numLigne = numLigne;
            cf = colorChoixCF;
            cc = colorChoixCC;
            // cc = FormeMenu_Parametre_Couleur.GetCcDef();
            // cf = FormeMenu_Parametre_Couleur.GetCfDef();
        }
        public int getNumCol()
        {
            return this.numCol;
        }
        public int getNumLigne()
        {
            return this.numLigne;
        }

        public void Reinitialiser()
        {
            this.ImageSupp();
            this.Text = "";
            this.Colorer() ;
        }

        // ***************** IMAGE ***********************
        public void ImageAj(Image img)
        {
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void ImageSupp()
        {
            this.BackgroundImage = null;
        }


        // ***************** COULEUR *********************
        public void Assombrir()
        {

            int ligne = this.getNumLigne();
            int colonne = this.getNumCol();
            if (ligne % 2 == 1)
            {
                if (colonne % 2 == 1)
                    this.BackColor = Color.FromArgb(189, 193, 197);
                else
                    this.BackColor = Color.FromArgb(140, 140, 140);
            }
            else
            {
                if (colonne % 2 == 0)
                    this.BackColor = Color.FromArgb(189, 193, 197);
                else
                    this.BackColor = Color.FromArgb(140, 140, 140);
            }
            // b.BackColor = Color.Beige;
            //rgb(255, 245, 245)
            //#FFF5F5DC
            // b.BackColor = Color.SaddleBrown;
            //rgb(255, 139, 69)
            //#FF8B4513
        }

        public void Colorer()
        {
            int ligne = this.getNumLigne();
            int colonne = this.getNumCol();
            if (ligne % 2 == 1)
            {
                if (colonne % 2 == 1)
                    this.BackColor = cc;
                else
                    this.BackColor = cf;
            }
            else
            {
                if (colonne % 2 == 0)
                    this.BackColor = cc;
                else
                    this.BackColor = cf;
            }
        }

        public void SetColor(Color colorChoixCF, Color colorChoixCC)
        {
            cf = colorChoixCF ;
            cc = colorChoixCC;
        }

    }
}
