using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uno
{
    public partial class FicCouleur : Form
    {
        public FicCouleur()
        {
            InitializeComponent();
        }

        private Color varcolor = Color.Black; //Variable de couleur qui va être transféré au FicMain pour changer la couleur de la pile

        public Color VarColor //Get set permettant l'utilisation de la variable dans le FicMain
        {
            get { return varcolor; }

            set { varcolor = value; }
        }

        private void bRouge_Click(object sender, EventArgs e)
        {
            varcolor = Color.DarkRed; //Variable change de couleur selon le bouton cliqué
        }

        private void bBleu_Click(object sender, EventArgs e)
        {
            varcolor = Color.Blue;
        }

        private void bJaune_Click(object sender, EventArgs e)
        {
            varcolor = Color.Orange;
        }

        private void bVert_Click(object sender, EventArgs e)
        {
            varcolor = Color.Green;
        }

        //Override le contrôle du bouton de sortie afin de forcé le joueur à selectionner une option
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
    }
}
