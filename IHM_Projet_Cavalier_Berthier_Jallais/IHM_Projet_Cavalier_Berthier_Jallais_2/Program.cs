using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHM_Projet_Cavalier_Berthier_Jallais_2;


namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormeMenu_Accueil());
            //Application.Run(new FormeMenu_Regles());

            //Application.Run(new FormeMenu_Parametre_Couleur());
            //Application.Run(new FormeMenu_Parametre_Cavalier());
            //Application.Run(new FormeCavalier_Jeu());
            //Application.Run(new FormeCavalier_Simulation());
        }
    }
}
