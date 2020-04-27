namespace Uno
{
    partial class FicCouleur
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bBleu = new System.Windows.Forms.Button();
            this.bRouge = new System.Windows.Forms.Button();
            this.bJaune = new System.Windows.Forms.Button();
            this.bVert = new System.Windows.Forms.Button();
            this.lbCouleur = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bBleu
            // 
            this.bBleu.BackColor = System.Drawing.Color.Blue;
            this.bBleu.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bBleu.ForeColor = System.Drawing.Color.Black;
            this.bBleu.Location = new System.Drawing.Point(397, 125);
            this.bBleu.Name = "bBleu";
            this.bBleu.Size = new System.Drawing.Size(133, 81);
            this.bBleu.TabIndex = 0;
            this.bBleu.UseVisualStyleBackColor = false;
            this.bBleu.Click += new System.EventHandler(this.bBleu_Click);
            // 
            // bRouge
            // 
            this.bRouge.BackColor = System.Drawing.Color.DarkRed;
            this.bRouge.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bRouge.ForeColor = System.Drawing.Color.Black;
            this.bRouge.Location = new System.Drawing.Point(259, 125);
            this.bRouge.Name = "bRouge";
            this.bRouge.Size = new System.Drawing.Size(133, 81);
            this.bRouge.TabIndex = 1;
            this.bRouge.UseVisualStyleBackColor = false;
            this.bRouge.Click += new System.EventHandler(this.bRouge_Click);
            // 
            // bJaune
            // 
            this.bJaune.BackColor = System.Drawing.Color.Orange;
            this.bJaune.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bJaune.Location = new System.Drawing.Point(397, 212);
            this.bJaune.Name = "bJaune";
            this.bJaune.Size = new System.Drawing.Size(133, 80);
            this.bJaune.TabIndex = 2;
            this.bJaune.UseVisualStyleBackColor = false;
            this.bJaune.Click += new System.EventHandler(this.bJaune_Click);
            // 
            // bVert
            // 
            this.bVert.BackColor = System.Drawing.Color.Green;
            this.bVert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bVert.Location = new System.Drawing.Point(259, 212);
            this.bVert.Name = "bVert";
            this.bVert.Size = new System.Drawing.Size(133, 80);
            this.bVert.TabIndex = 3;
            this.bVert.UseVisualStyleBackColor = false;
            this.bVert.Click += new System.EventHandler(this.bVert_Click);
            // 
            // lbCouleur
            // 
            this.lbCouleur.AutoSize = true;
            this.lbCouleur.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouleur.Location = new System.Drawing.Point(188, 51);
            this.lbCouleur.Name = "lbCouleur";
            this.lbCouleur.Size = new System.Drawing.Size(426, 46);
            this.lbCouleur.TabIndex = 4;
            this.lbCouleur.Text = "Choix de la nouvelle couleur";
            // 
            // FicCouleur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(795, 443);
            this.Controls.Add(this.lbCouleur);
            this.Controls.Add(this.bVert);
            this.Controls.Add(this.bJaune);
            this.Controls.Add(this.bRouge);
            this.Controls.Add(this.bBleu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FicCouleur";
            this.Text = "FicCouleur";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bBleu;
        private System.Windows.Forms.Button bRouge;
        private System.Windows.Forms.Button bJaune;
        private System.Windows.Forms.Button bVert;
        private System.Windows.Forms.Label lbCouleur;
    }
}