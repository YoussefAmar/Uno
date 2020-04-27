namespace Uno
{
    partial class FicUno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FicUno));
            this.pbJeu = new System.Windows.Forms.PictureBox();
            this.lJ1 = new System.Windows.Forms.Label();
            this.lJ2 = new System.Windows.Forms.Label();
            this.lPile = new System.Windows.Forms.Label();
            this.btnPiocher = new System.Windows.Forms.Button();
            this.lbNbCarte2 = new System.Windows.Forms.Label();
            this.lbNbCarte1 = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSauver = new System.Windows.Forms.Button();
            this.btnMusic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbJeu)).BeginInit();
            this.SuspendLayout();
            // 
            // pbJeu
            // 
            this.pbJeu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbJeu.Location = new System.Drawing.Point(0, 0);
            this.pbJeu.Name = "pbJeu";
            this.pbJeu.Size = new System.Drawing.Size(1509, 764);
            this.pbJeu.TabIndex = 0;
            this.pbJeu.TabStop = false;
            this.pbJeu.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPile_Paint);
            this.pbJeu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbJeu_MouseClick);
            // 
            // lJ1
            // 
            this.lJ1.AutoSize = true;
            this.lJ1.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lJ1.Location = new System.Drawing.Point(900, 1000);
            this.lJ1.Name = "lJ1";
            this.lJ1.Size = new System.Drawing.Size(146, 46);
            this.lJ1.TabIndex = 3;
            this.lJ1.Text = "Joueur 1";
            // 
            // lJ2
            // 
            this.lJ2.AutoSize = true;
            this.lJ2.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lJ2.Location = new System.Drawing.Point(900, 50);
            this.lJ2.Name = "lJ2";
            this.lJ2.Size = new System.Drawing.Size(146, 46);
            this.lJ2.TabIndex = 4;
            this.lJ2.Text = "Joueur 2";
            // 
            // lPile
            // 
            this.lPile.AutoSize = true;
            this.lPile.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPile.Location = new System.Drawing.Point(800, 490);
            this.lPile.Name = "lPile";
            this.lPile.Size = new System.Drawing.Size(74, 46);
            this.lPile.TabIndex = 5;
            this.lPile.Text = "Pile";
            // 
            // btnPiocher
            // 
            this.btnPiocher.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPiocher.Location = new System.Drawing.Point(1200, 490);
            this.btnPiocher.Name = "btnPiocher";
            this.btnPiocher.Size = new System.Drawing.Size(130, 60);
            this.btnPiocher.TabIndex = 6;
            this.btnPiocher.Text = "Piocher";
            this.btnPiocher.UseVisualStyleBackColor = true;
            this.btnPiocher.Click += new System.EventHandler(this.btnPiocher_Click);
            // 
            // lbNbCarte2
            // 
            this.lbNbCarte2.AutoSize = true;
            this.lbNbCarte2.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNbCarte2.Location = new System.Drawing.Point(600, 50);
            this.lbNbCarte2.Name = "lbNbCarte2";
            this.lbNbCarte2.Size = new System.Drawing.Size(0, 46);
            this.lbNbCarte2.TabIndex = 7;
            // 
            // lbNbCarte1
            // 
            this.lbNbCarte1.AutoSize = true;
            this.lbNbCarte1.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNbCarte1.Location = new System.Drawing.Point(600, 1000);
            this.lbNbCarte1.Name = "lbNbCarte1";
            this.lbNbCarte1.Size = new System.Drawing.Size(0, 46);
            this.lbNbCarte1.TabIndex = 8;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitter.Location = new System.Drawing.Point(88, 600);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(130, 60);
            this.btnQuitter.TabIndex = 9;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(88, 520);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(130, 60);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Charger";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSauver
            // 
            this.btnSauver.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSauver.Location = new System.Drawing.Point(88, 440);
            this.btnSauver.Name = "btnSauver";
            this.btnSauver.Size = new System.Drawing.Size(130, 60);
            this.btnSauver.TabIndex = 11;
            this.btnSauver.Text = "Sauver";
            this.btnSauver.UseVisualStyleBackColor = true;
            this.btnSauver.Click += new System.EventHandler(this.btnSauver_Click);
            // 
            // btnMusic
            // 
            this.btnMusic.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMusic.Location = new System.Drawing.Point(1600, 36);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(65, 60);
            this.btnMusic.TabIndex = 12;
            this.btnMusic.Text = "| |";
            this.btnMusic.UseVisualStyleBackColor = true;
            this.btnMusic.Click += new System.EventHandler(this.btnMusic_Click);
            // 
            // FicUno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1509, 764);
            this.Controls.Add(this.btnMusic);
            this.Controls.Add(this.btnSauver);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.lbNbCarte1);
            this.Controls.Add(this.lbNbCarte2);
            this.Controls.Add(this.btnPiocher);
            this.Controls.Add(this.lPile);
            this.Controls.Add(this.lJ2);
            this.Controls.Add(this.lJ1);
            this.Controls.Add(this.pbJeu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FicUno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNO";
            this.Load += new System.EventHandler(this.FicUno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbJeu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbJeu;
        private System.Windows.Forms.Label lJ1;
        private System.Windows.Forms.Label lJ2;
        private System.Windows.Forms.Label lPile;
        private System.Windows.Forms.Button btnPiocher;
        private System.Windows.Forms.Label lbNbCarte2;
        private System.Windows.Forms.Label lbNbCarte1;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSauver;
        private System.Windows.Forms.Button btnMusic;
    }
}

