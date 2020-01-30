namespace IHM_Projet_Cavalier_Berthier_Jallais_2
{
    partial class FormeCavalier_Jeu
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CouleursDuDamierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choixDuDestrierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paramètresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(851, 33);
            this.menuStrip1.TabIndex = 160;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // paramètresToolStripMenuItem
            // 
            this.paramètresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CouleursDuDamierToolStripMenuItem,
            this.choixDuDestrierToolStripMenuItem});
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.paramètresToolStripMenuItem.Text = "Paramètres";
            // 
            // CouleursDuDamierToolStripMenuItem
            // 
            this.CouleursDuDamierToolStripMenuItem.Name = "CouleursDuDamierToolStripMenuItem";
            this.CouleursDuDamierToolStripMenuItem.Size = new System.Drawing.Size(271, 34);
            this.CouleursDuDamierToolStripMenuItem.Text = "Couleurs du Damier";
            this.CouleursDuDamierToolStripMenuItem.Click += new System.EventHandler(this.couleursDuDamierToolStripMenuItem_Click);
            // 
            // choixDuDestrierToolStripMenuItem
            // 
            this.choixDuDestrierToolStripMenuItem.Name = "choixDuDestrierToolStripMenuItem";
            this.choixDuDestrierToolStripMenuItem.Size = new System.Drawing.Size(271, 34);
            this.choixDuDestrierToolStripMenuItem.Text = "Choix du Destrier";
            this.choixDuDestrierToolStripMenuItem.Click += new System.EventHandler(this.choixDuDestrierToolStripMenuItem_Click);
            // 
            // FormeCavalier_Jeu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(851, 655);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormeCavalier_Jeu";
            this.Text = "Projet IHM - Jeu du Cavalier - BERTHIER Nicolas et JALLAIS Adrien";
            this.Load += new System.EventHandler(this.FormCavalier_Solo_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CouleursDuDamierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choixDuDestrierToolStripMenuItem;
    }
}
