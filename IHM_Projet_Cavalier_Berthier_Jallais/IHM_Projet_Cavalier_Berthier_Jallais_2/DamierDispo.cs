using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    class DamierDispo : Damier
    {
        private bool[,] dispo; // disponibilité des cases pour un mouvement
        // prend en compte les case de marge en false
        // afin de le prendre en compte dans les conditions de fin de partie
        public DamierDispo(int nbrCaseCoteDamier, int tailleBoutonPX, int nbrCaseMarge, int posX, int posY) 
            : base(nbrCaseCoteDamier, tailleBoutonPX, nbrCaseMarge, posX, posY )
        {
            dispo = new bool[GetGrille().GetLength(0), GetGrille().GetLength(1)];
            InitialiserDispo();
        }

        public DamierDispo(int nbrCaseCoteDamier, int tailleBoutonPX, int nbrCaseMarge, int posX, int posY, Color colorChoixCF, Color colorChoixCC) 
            : base(nbrCaseCoteDamier, tailleBoutonPX, nbrCaseMarge, posX, posY, colorChoixCF, colorChoixCC)
        {
            dispo = new bool[GetGrille().GetLength(0), GetGrille().GetLength(1)];
            InitialiserDispo();
        }

        public bool IsDispoCible(monBouton bDepart, int depi, int depj)
        {
            return IsDispo(bDepart.getNumLigne() + depi, bDepart.getNumCol() + depj);
        }

        public bool IsDispo(int ligne, int colonne)
        {
            if ((ligne < dispo.GetLength(0)) && (colonne < dispo.GetLength(1)) && (ligne >= 0) && (colonne >= 0))
            {
                return dispo[ligne, colonne];
            }
            else
                return false ;
        }

        public bool IsToutOccupe()
        {
            for (int ligne = 0; ligne < dispo.GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < dispo.GetLength(1); ++colonne)
                {
                   if (IsDispo(ligne, colonne))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void FermerDispo(int ligne, int colonne)
        {
            dispo[ligne, colonne] = false;
        }

        public void OuvrirDispo(int ligne, int colonne)
        {
            dispo[ligne, colonne] = true;
        }
        private void InitialiserDispo()
        {
            for (int ligne = 0; ligne < dispo.GetLength(0); ++ligne)
            {
                for (int colonne = 0; colonne < dispo.GetLength(1); ++colonne)
                {
                    if (ligne >= GetTailleMarge() && ligne < (GetTailleMarge() + GetTailleLig()))
                    {
                        if (colonne >= GetTailleMarge() && colonne < (GetTailleMarge() + GetTailleCol()))
                        {
                            dispo[ligne, colonne] = true;
                        }
                        else
                            dispo[ligne, colonne] = false;

                    }
                    else
                        dispo[ligne, colonne] = false;
                }
            }
        }

        public void ReinitialiserDamier()
        {
            reinitialiserGrille();
            InitialiserDispo();
        }
    }
}
