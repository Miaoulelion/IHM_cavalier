using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    class Cavalier
    {
        //deplacement
        private int[] depi;
        private int[] depj;

        //case choisie pour deplacement
        private int posLig;
        private int posCol;

        //image
        private Image img;

        public Cavalier(monBouton b, int cavalierSwitch)
        {

            posLig = b.getNumLigne();
            posCol = b.getNumCol();

            switch (cavalierSwitch)
            {
                case 1:
                    img = Image.FromFile("images/cheval.png");
                    depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
                    depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
                    break;
                case 2:
                    img = Image.FromFile("images/cheval2.png");
                    depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
                    depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
                    break;
                case 3:
                    img = Image.FromFile("images/cheval3.png");
                    depi = new int[] { 1, 0, 1, -1, 0, -1, 1, -1 };
                    depj = new int[] { 0, 1, 1, 0, -1, -1, -1, 1 };
                    break;
                default:
                    img = Image.FromFile("images/cheval.png");
                    depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
                    depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
                    break;
            }
        }

        public int[] GetDepi()
        {
            return depi;
        }
        public int[] GetDepj()
        {
            return depj;
        }

        public int GetPosLig()
        {
            return posLig;
        }
        public int GetPosCol()
        {
            return posCol;
        }

        public Image GetImg()
        {
            return img;
        }

        public void Deplacer(int ligne, int colonne)
        {
            if (IsAtteignable(ligne, colonne))
            {
                this.posLig = ligne;
                this.posCol = colonne;
            }
        }

        public void DeplacerF(int ligne, int colonne)
        {
                this.posLig = ligne;
                this.posCol = colonne;
        }

        /**
         * IsAtteignable
         * permet de savoir si une case donnée (ligne, colonne) est atteingnable par AU MOINS UN deplacement du cavalier
         * le cavalier est considére en case [numLigneJouee,numColJouee])
         */
        public bool IsAtteignable(int ligne, int colonne)
        {

            for (int i = 0; i < depi.GetLength(0); ++i)
            {
                if (IsJoignable(ligne, colonne, depi[i], depj[i]))
                {
                    return true;
                }
            }
            return false;

        }

        /**
         * IsJoignable
         * permet de savoir si une case donnée (ligne, colonne) est atteingnable par UN SEUL deplacement du cavalier donné (depi,depj)
         * le cavalier est considére en case [numLigneJouee,numColJouee])
         */
        private bool IsJoignable(int ligne, int colonne, int depi1, int depj1)
        {
            //verif ligne puis colonne
            return (((posLig + depi1) == ligne) && ((posCol + depj1) == colonne));
        }

    }
}
