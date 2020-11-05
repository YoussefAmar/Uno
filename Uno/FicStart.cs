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
    public partial class FicStart : Form
    {
        public bool Choix { get;  set; }
        public bool BtnClicked { get; private set; } = false;

        public FicStart()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Choix = false;
            BtnClicked = true;

            Lancement_jeu(Choix);

        }

        private void btnServeur_Click(object sender, EventArgs e)
        {
            Choix = true;
            BtnClicked = true;

            Lancement_jeu(Choix);

        }

        public void Lancement_jeu(bool choix)
        {
            this.Hide();

            FicUno f = new FicUno(choix);

            f.ShowDialog();

            this.Close();

        }
    }
}
