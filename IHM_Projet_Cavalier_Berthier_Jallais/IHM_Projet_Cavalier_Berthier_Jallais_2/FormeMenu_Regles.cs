using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    public partial class FormeMenu_Regles : Form
    {

        private FileStream source;
        private string filepath;
        /*
        private System.Windows.Controls.WebBrowser web;
        private Assembly assembly;
        private Stream reader;
        private Uri uri;
        */
        public FormeMenu_Regles()
        {
            InitializeComponent();
        }

        private void FormeMenu_Regles_Load(object sender, EventArgs e)
        {
           this.Text = "Jeu du Cavalier - Règles.";

            filepath = "regles/regles.html";
            source = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            webBrowser1.DocumentStream = source;
            
            //web.Document() = source ;
            //web.DocumentText = html;
            //web.Navigating(source) ;

            //assembly = Assembly.GetExecutingAssembly();
            //reader = new StreamReader(assembly.GetManifestResourceStream(filepath));
            //web.Document = reader.Read ;

            //uri = new Uri(filepath);
            //SHDocVw.WebBrowser.Navigate(filepath);
            //SHDocVw.WebBrowser.Navigate(uri);
            //webBrowser.Navigate(uri);

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
