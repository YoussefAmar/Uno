namespace Uno
{
    partial class FicStart
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
            this.btnClient = new System.Windows.Forms.Button();
            this.lblOu = new System.Windows.Forms.Label();
            this.btnServeur = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClient
            // 
            this.btnClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClient.Location = new System.Drawing.Point(204, 67);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(189, 75);
            this.btnClient.TabIndex = 0;
            this.btnClient.Text = "Client";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // lblOu
            // 
            this.lblOu.AutoSize = true;
            this.lblOu.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOu.Location = new System.Drawing.Point(250, 222);
            this.lblOu.Name = "lblOu";
            this.lblOu.Size = new System.Drawing.Size(88, 55);
            this.lblOu.TabIndex = 1;
            this.lblOu.Text = "Ou";
            // 
            // btnServeur
            // 
            this.btnServeur.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServeur.Location = new System.Drawing.Point(204, 378);
            this.btnServeur.Name = "btnServeur";
            this.btnServeur.Size = new System.Drawing.Size(189, 75);
            this.btnServeur.TabIndex = 2;
            this.btnServeur.Text = "Serveur";
            this.btnServeur.UseVisualStyleBackColor = true;
            this.btnServeur.Click += new System.EventHandler(this.btnServeur_Click);
            // 
            // FicStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(614, 536);
            this.Controls.Add(this.btnServeur);
            this.Controls.Add(this.lblOu);
            this.Controls.Add(this.btnClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FicStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Début du jeu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FicStart_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Label lblOu;
        private System.Windows.Forms.Button btnServeur;
    }
}