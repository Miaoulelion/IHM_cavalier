using IHM_Projet_Cavalier_Berthier_Jallais_2;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    public class Euler
    {
        private int[,] echec = new int[12, 12];

        private static int[] depi ;
        private static int[] depj ;

        public Euler (int[] deplacementLigne, int[] deplacementColonne)
        {
            depi = deplacementLigne;
            depj = deplacementColonne;
        }

        public Euler()
        {
            depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 }; 
            depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        }

        // ii et jj evoluent de 1 à 8 !
        public int[,] getEchec(int ii, int jj)
        {
            if (ii >= 1 && ii <= 8 && jj >= 1  && jj <= 8)
            {
                /*--------------------------------------------------------------------*/
                /*            Définitions et déclarations                             */
                /*--------------------------------------------------------------------*/

                int nb_fuite, min_fuite, lmin_fuite = 0;
                int i, j, k, l;


                /*--------------------------------------------------------------------*/
                /*                          Initialisation                            */
                /*--------------------------------------------------------------------*/

                // ii et jj evoluent de 1 à 8 !
                //Console.WriteLine("Case de départ: " + ii + "  " + jj);


                // on definit 
                // toutes les cases des marges comme ne etant PAS a faire (en mettant -1)
                // toutes les cases du damier comme etant a faire (en mettant 0)
                for (i = 0; i < 12; i++)
                    for (j = 0; j < 12; j++)
                        echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);



                /*--------------------------------------------------------------------*/
                /*                Parcours du cavalier sur l'échiquier                */
                /*--------------------------------------------------------------------*/

                i = ii + 1; j = jj + 1;
                // on definit la case de depart comme la premiere a etre faite
                // (on ne peut pas mette 0 car cela voudrait dire qu'elle reste à faire)
                echec[i, j] = 1;

                // on parcours toutes les cases restantes (2 à 64)
                // pour leur assigner leur rang dans damierNum
                for (k = 2; k <= 64; k++)
                {
                    // on evalue le nbr de fuites 
                    // pour chacuns des deplacements possibles de la piece pie
                    for (l = 0, min_fuite = 11; l < 8; l++)
                    {
                        ii = i + depi[l]; jj = j + depj[l];

                        nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                        // condition qui repere le num du deplacement 
                        // necessaire pour atteindre la case 
                        // pour laquelle le nombre de fuite est minimale
                        if (nb_fuite < min_fuite)
                        {
                            min_fuite = nb_fuite;  //nombre de fuite minimum
                            lmin_fuite = l; //indice du deplacement pour fuite minimal
                        }
                    }

                    // cas where IMPASSE
                    // il n y a pas de fuite
                    // alors que le damierNum n est PAS termine
                    if (min_fuite == 9 & k != 64)
                    {
                        //Console.WriteLine("***   IMPASSE   ***");
                        break;
                    }
                    i += depi[lmin_fuite]; j += depj[lmin_fuite];
                    echec[i, j] = k;
                }

                /*
                for (i = 2; i < 10; i++)
                {
                    for (j = 2; j < 10; j++)
                    {
                        Console.Write(echec[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.ReadKey();
                */

            }

            return echec;
        }

        /**********************************************************************/
        /*     Fonction de recherche du nombre de cases de fuite possibles    */
        /**********************************************************************/

        private int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }

    }

    //V1
    /*
{
    int nb_fuite, min_fuite, lmin_fuite = 0;
    int i, j, k, l, destL, destC;
    int nbrCases = DD.GetTailleCol() * DD.GetTailleLig();

    InitialisationDamierNum();
    InitialisaitonIAChoix();

    //ii = depLig;
    //jj = depCol;
    //i = depCol + 2; j = depLig + 2; //Par rapport à l'algo du prof, il y a une séparation de 2

    // on definit la case de depart comme la premiere a etre faite
    // (on ne peut pas mette 0 car cela voudrait dire qu'elle reste à faire)
    destL = b.getNumLigne();
    destC = b.getNumCol();
    i = destL;
    j = destC;
    damierNum[i, j] = 1;

    //faire la liste des coordonnées à jouer
    IAchoixLigne[0] = i;
    IAchoixColonne[0] = j;
    IAchoix[0] = b ;
    Console.WriteLine("***" + 1 + "eme bouton =  *** ligne =" + (i-1) + "colonne=" + (j-1));

    // on parcours toutes les cases restantes (2 à 64)
    // pour leur assigner leur rang dans damierNum
    for (k = 2; k <= nbrCases; k++)
    {
        // on evalue le nbr de fuites 
        // pour chacuns des deplacements possibles de la piece pie
        // qui est en case dest  [destL,destC]
        for (l = 0, min_fuite = 11; l < piece.GetDepi().Length ; l++)
        {
            destL = i + piece.GetDepi()[l];
            destC = j + piece.GetDepj()[l];

            nb_fuite = ((damierNum[destL, destC] != 0) ? 10 : fuite(destL,destC));

            // condition qui repere le num du deplacement 
            // necessaire pour atteindre la case 
            // pour laquelle le nombre de fuite est minimale
            if (nb_fuite < min_fuite)
            {
                min_fuite = nb_fuite; //nombre de fuite minimum
                lmin_fuite = l; //indice du deplacement pour fuite minimal
            }
        }

        // cas where IMPASSE
        // il n y a pas de fuite
        // alors que le damierNum n est PAS termine
        if (min_fuite == 9 & k != nbrCases)
        {
            DialogResult reponseIMPASSE = MessageBox.Show("Probleme au " + k + "eme coup.", "IMPASSE",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error,
                                                    MessageBoxDefaultButton.Button3,
                                                    MessageBoxOptions.RightAlign);
            break;
        }

        // deplacement de la piece 
        // vers la case pour laquelle le nombre de fuite est minimale
        i += piece.GetDepi()[lmin_fuite]; 
        j += piece.GetDepj()[lmin_fuite];
        damierNum[i, j] = k;
        Console.WriteLine("***"+    k +"eme bouton =  *** ligne =" + (i-1) + "colonne=" +(j-1));

        //faire la liste des coordonnées à jouer
        IAchoixLigne[k - 1] = i;
        IAchoixColonne[k - 1] = j;
        IAchoix[k - 1] = DD.GetMonBouton(i+2, j+2);
    }
}
*/
}

