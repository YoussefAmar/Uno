using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.Serialization;


namespace Uno
{
    [Serializable]
    class Carte : ISerializable
    {
        #region Membres
        private Random rngValeur = new Random(); 
        private Random rngCouleur = new Random(); // Générateur de valeur aléatoire pour obtenir une couleur et valeur pour la carte
        private string NomValeur; //Valeur de la carte en string
        private int choixCouleur; 
        private int Valeur;
        private Color Couleur; // Variable de stockage de la couleur et la valeur
        private Rectangle r; // Rectangle formant la carte
        private Brush bCouleur,bEllipse; // Stockage du design couleur de l'ellipse/rectangle
        private Color cEllipse; //Couleur de l'ellipse
        private int xc;
        private int yc;

        #endregion

        #region Constructeur
        public Carte(int x, int y)
        {
            this.xc = x;
            this.yc = y;

            Thread.Sleep(200); //Permet d'obtenir une différente valeur aléatoire pour chaque carte

            Valeur = rngValeur.Next(1400);

            Valeur = Valeur % 14;

            switch (Valeur)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: NomValeur = Valeur.ToString(); break;

                case 10: NomValeur = "+2"; break;

                case 11: NomValeur = "P"; break;

                case 12: NomValeur = "+4"; break;

                case 13: NomValeur = "C"; break;
                
                    //Génére la valeur de la carte
            }

            if (Valeur >= 0 && Valeur < 12)
            {

                choixCouleur = rngCouleur.Next(500);

                choixCouleur = choixCouleur % 4;


                switch (choixCouleur)
                {

                    case 0: Couleur = Color.Green; break;

                    case 1: Couleur = Color.Blue; break;

                    case 2: Couleur = Color.Orange; break;

                    case 3: Couleur = Color.DarkRed; break;

                }
            }

            else if (Valeur == 12 || Valeur == 13)
            {
                Couleur = Color.Black;
            }

            //Génére la couleur de la carte

            r = new Rectangle(this.xc,this.yc, 100, 130);

            //Génére le rectangle de la carte
        }

        public Carte(SerializationInfo info, StreamingContext context)
        {
            this.Valeur = (int)info.GetValue("Valeur", typeof(int));
            this.Couleur = (Color)info.GetValue("Couleur", typeof(Color));
            this.NomValeur = (string)info.GetValue("Texte", typeof(string));
            this.xc = (int)info.GetValue("xc", typeof(int));
            this.yc = (int)info.GetValue("yc", typeof(int));

            r = new Rectangle(this.xc,this.yc, 100, 130);
        } //Deserialization des données de la carte

        #endregion

        #region Methodes

        public void Dessine(PaintEventArgs g)
        {
            g.Graphics.DrawRectangle(new Pen(Color.Black), this.r);
            this.bCouleur = new SolidBrush(this.Couleur);
            this.bEllipse = new SolidBrush(Color.White);
            this.cEllipse = Color.White;

            g.Graphics.FillRectangle(this.bCouleur,this.r);
            g.Graphics.DrawEllipse(new Pen(cEllipse), this.r.X + this.r.Width / 8, this.r.Y + this.r.Height / 6, 75, 90);
            g.Graphics.FillEllipse(bEllipse, this.r.X + this.r.Width / 8, this.r.Y + this.r.Height / 6, 75, 90);
            g.Graphics.DrawString(this.NomValeur, new Font("Arial", 30, FontStyle.Bold), Brushes.Black, 30 + this.r.X, 45 + this.r.Y);

            //Dessine la carte
        }

        public Carte Compare(Carte Pile)
        {
            if (this.Valeur == Pile.Valeur)
            {
                Pile.Couleur = this.Couleur;
                Pile.bCouleur = new SolidBrush(this.Couleur);

                return null;
            }

            else if (Pile.Couleur == this.Couleur)
            {
                Pile.Valeur = this.Valeur;
                Pile.NomValeur = this.NomValeur;

                return null;
            }

            else if (Pile.Couleur == Color.Black || this.Couleur == Color.Black)
            {
                Pile.Valeur = this.Valeur;
                Pile.NomValeur = this.NomValeur;
                Pile.Couleur = this.Couleur;
                Pile.bCouleur = new SolidBrush(this.Couleur);

                return null;

            }
            //La pile devient la carte jouee
            else
            {
                MessageBox.Show("La carte jouée doit avoir le même chiffre ou la même couleur que la carte de pile", "Carte non jouable");
                return this;
            }

            //Renvoie null pour supprimer la carte jouée si elle est correct ou renvoie la carte si elle n'est pas correct
        }

        public bool Gagner(Carte[] Joueur)
        {
            bool flag = true;

            for (int i = 0; i < 10; i++)
            {
                if (Joueur[i] != null)
                {
                    flag = false;
                }
            }

            return flag;
        }

        public int ValeurEffet(Carte Pile)
        {
            return (Pile.Valeur); // Renvoi la valeur de la carte
        }

        public void ChangeCouleur(Carte Pile)
        {
            FicCouleur f = new FicCouleur(); //Appel du FicCouleur

            if(f.ShowDialog() == DialogResult.OK) //Si une option a été selectionner on va changer la couleur de la pile puis fermer FicCouleur
            {
                Pile.Couleur = f.VarColor;
                f.Close();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) //Serialization des données de la carte
        {
            info.AddValue("Valeur",this.Valeur, typeof(int));
            info.AddValue("Couleur", this.Couleur, typeof(Color));
            info.AddValue("Texte", this.NomValeur, typeof(string));
            info.AddValue("xc", this.xc, typeof(int));
            info.AddValue("yc", this.yc, typeof(int));
        }

        #endregion
    }
}
