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
using System.IO;
using System.Media;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WMPLib;
using System.Diagnostics;

namespace Uno
{
    public partial class FicUno : Form
    {
        #region Membres
    
        private Carte Pile = new Carte(590, 270);
        private Carte[] J1 = new Carte[10];
        private Carte[] J2 = new Carte[10];
        private List<Carte> ExcesJ1 = new List<Carte>();
        private List<Carte> ExcesJ2 = new List<Carte>();
        //Liste des cartes en trop à garder en mémoire
        private int[] x = new int[10];
        //Position des cartes en x des deux joueurs
        private int y1 = 500;
        private int y2 = 100;
        //Position des cartes en y des deux joueurs
        private bool tourJ1;
        //Boolen permetant au joueur 1 de cliquer lors de son tours
        private int start = 10;
        //Nombre de carte en main maximum durant la partie (10 maximum)
        private int nbCarte1;
        private int nbCarte2;
        //Variable comptant le nombre de carte en main à chaque tours
        private FileStream fSave; //fichier permettant la sauvegarde et le chargement
        private Partie PartieSave; //classe gardant en memoire toutes les variables de la partie 
        private WindowsMediaPlayer player = new WindowsMediaPlayer(); //lecteur de musique 
        private bool son; //boleen contrôlant si la musique est jouer ou non
        private bool verifuno = false; //Vérifie si le joueur a dit Uno lorsqu'il a sa dernière carte en main
        private bool client;

        #endregion

        #region Accesseur
        internal Carte pile { get => Pile; set => Pile = value; }
        internal Carte[] j1 { get => J1; set => J1 = value; }
        internal Carte[] j2 { get => J2; set => J2 = value; }
        public bool TourJ1 { get => tourJ1; set => tourJ1 = value; }
        #endregion

        #region Methodes windows form
        public FicUno(bool choix)
        {
            InitializeComponent();
            ClientSize = new Size(1280, 720);
            PartieSave = new Partie(this);
            player.URL = "tetris.mp3";
            player.settings.setMode("loop", true);

            client = choix;

        }

        private void FicUno_Load(object sender, EventArgs e)
        {
            tourJ1 = true;

            son = true;
            btnMusic.Text = " | | "; //initialise les élements relié à la musique

            nbCarte2 = 7;

            for (int i = 0; i < start; i++)
            {

                if (i == 0)
                {
                    x[i] = 5;

                    //Position de départ de la 1er carte
                }

                else

                    x[i] = x[i - 1] + 130;

                //Incrémentation de la position des cartes

                if (i < 7)
                {
                    J1[i] = new Carte(x[i], y1);

                    J2[i] = new Carte(x[i], y2);
                }
                //Initialisation des 7 cartes de départs

            }
        }

        private void pbPile_Paint(object sender, PaintEventArgs e)
        {
            nbCarte2 = 0;
            nbCarte1 = 0;

            Pile.Dessine(e);

            for (int i = 0; i < start; i++)
            {

                if (J1[i] != null && tourJ1)
                    {
                        J1[i].Dessine(e);
                    }

                if (J2[i] != null && !tourJ1)
                    {
                        J2[i].Dessine(e);
                    }
                //Dessine les cartes si elle on des attributs associés

                if (J1[i] != null)
                {
                    nbCarte1++;
                }

                if (J2[i] != null)
                {
                    nbCarte2++;
                }
                //Calcul du nombre total de carte en main
            }

            lbNbCarte2.Text = "Cartes : " + nbCarte2.ToString();
            lbNbCarte1.Text = "Cartes : " + nbCarte1.ToString();
            //Affichage du nombre total de carte en main

            Invalidate();
            //Raffraichissement de l'ecran
        }

        private void pbJeu_MouseClick(object sender, MouseEventArgs e)
        {
            int xs = 0;
            int ys = 0;

            xs = e.X;
            ys = e.Y;

            for (int i = 0; i < 10; i++)
            {
                if (ys > y1 && ys < (y1 + 130) && tourJ1 == true)
                {
                    if (xs > x[i] && xs < (x[i] + 100) && x[i] != 0)
                    //Recherche de la position de la carte si le joueur 1 la joue
                    {
                        if (J1[i] != null)
                        {
                            
                            J1[i] = J1[i].Compare(Pile);

                            if (J1[i] == null) //Si la carte a été jouée
                            {

                                for (int j = 0; j < start; j++) //On compare toutes les cartes a celles jouée pour trouver les même et les joué aussi
                                {
                                    if(J1[j] != null)

                                    J1[j] = J1[j].HardCompare(Pile);
                                }
                            }

                            pbJeu.Invalidate();

                            //Compare la carte jouer et update l'ecran de jeu

                            if (Pile.Gagner(J1) == true)
                            {
                                MessageBox.Show("Victoire du Joueur 1", "Victoire");
                                Close();

                                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du joueur 1 et on ferme l'application
                            }

                            if (J1[i] == null)
                            {// Si la carte a été jouée on passe au tour suivant et on prend en compte son effet

                                Uno(tourJ1);

                                tourJ1 = ChangementTour(tourJ1);

                                Effet(Pile.ValeurEffet(Pile));

                            }
                        }

                    }
                }

                if (ys > y2 && ys < (y2 + 130) && !tourJ1 == true)
                {
                    if (xs > x[i] && xs < (x[i] + 100) && x[i] != 0)

                    //Recherche de la position de la carte si le joueur 2 là joue
                    {
                        if (J2[i] != null)
                        {
                            J2[i] = J2[i].Compare(Pile);

                            if (J2[i] == null) //Si la carte a été jouée
                            {

                                for (int j = 0; j < start; j++) //On compare toutes les cartes a celles jouée pour trouver les même et les joué aussi
                                {
                                    if (J2[j] != null)

                                        J2[j] = J2[j].HardCompare(Pile);
                                }
                            }

                            pbJeu.Invalidate();

                            if (Pile.Gagner(J2) == true)
                            {
                                MessageBox.Show("Victoire du Joueur 2", "Victoire");
                                Close();

                                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du joueur 2 et on ferme l'application
                            }

                            if (J2[i] == null)
                            {
                                Uno(tourJ1);

                                tourJ1 = ChangementTour(tourJ1);

                                Effet(Pile.ValeurEffet(Pile));
                            }
                        }

                    }
                }

            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûre ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        private void btnSauver_Click(object sender, EventArgs e) //Sauvegarde du fichier
        {

            IFormatter format = new BinaryFormatter(); //Choix du format qui est en binaire ici

            fSave = new FileStream(@"Uno.sav", FileMode.Create); //creation du fichier Uno.sav

            format.Serialize(fSave, PartieSave); //Ajout des infos de la partie dans le fichier

            string filePath = System.IO.Path.GetFullPath("Uno.sav"); //chemin du fichier

            fSave.Close(); //arrêt des operation sur le fichier

            MessageBox.Show("Fichier sauvegardé dans :  " + filePath); //Affichage du chemin a l'utilisateur

        }

        private void btnLoad_Click(object sender, EventArgs e) //Chargement du fichier
        {
            try
            {
                fSave = new FileStream(@"Uno.sav", FileMode.Open); //Ouverture du fichier

                IFormatter format = new BinaryFormatter(); //format binaire

                PartieSave = (Partie)format.Deserialize(fSave); //on donne les infos de la partie à la variable partieSave

                fSave.Close();

                MessageBox.Show("Fichier trouvé, lancement de la partie chargée", "Chargement de la partie");

                pbJeu.Invalidate(); //On rafraichit l'ecran
            }

            catch { MessageBox.Show("Aucun fichier sauvegardé", "Veuillez sauver une partie"); }
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            son = !son; //changement de la variable au click

            if (son)
            {
                player.controls.play();//joue si true

                btnMusic.Text = " | | ";
            }
            else
            {
                player.controls.pause();//pause si false

                btnMusic.Text = " ► ";
            }

        }

        private void bUno_Click(object sender, EventArgs e)
        {
            verifuno = true;
        }

        #endregion

        #region Methodes console

        public bool ChangementTour(bool tourJoueur)
        {

            tourJoueur = !tourJoueur;

            return tourJoueur;
        }

        private void Uno(bool tourJoueur)
        {
            if (tourJoueur && nbCarte1 == 2) //Condition pour le uno J1
            {

                if (!verifuno)
                {
                    Pioche(4);
                }

                verifuno = false;
            }

            if (!tourJoueur && nbCarte2 == 2) //Condition pour le uno J2
            {

                if (!verifuno)
                {
                    Pioche(4);
                }

                verifuno = false;
            }
        }

        private void btnPiocher_Click(object sender, EventArgs e)
        {   //On pioche
            Pioche(1);
            //On passe le tour
            tourJ1 = ChangementTour(tourJ1);
        }

        public void Pioche(int j)
        {
            int a = j;
            do
            {
                int i = 0; //Variable utilisé dans la recherche de carte
                bool flag = true; //Permet de savoir si on a trouvé un emplacement vide

                if (tourJ1)
                {
                    while (i < 10 && flag == true)
                    {// Va rechercher une place vide jusqu'a que toutes les places soit parcourues ou que l'on en trouve une vide
                        if (J1[i] == null)
                        { // Si une place vide a été trouvé, on y ajoute une carte et on rafraichi l'écran
                            J1[i] = new Carte(x[i], y1);

                            if(ExcesJ1.Count != 0)
                            { //Remplacer par la carte de la liste

                                var tmpCarte = ExcesJ1.First(); //Placer l'exces dans un tmp

                                tmpCarte.Remplacer(J1[i]); //Remplacer la carte piocher

                                ExcesJ1.RemoveAt(0); //Supprime 1er element

                            }

                            pbJeu.Invalidate();
                            flag = false;
                        }
                        i++;
                    }

                    if (flag) //Emplacement vide non trouver
                    {//Si la carte est obtenue via une pioche normal on afficher un changement de carte dans le cas où on a 10 cartes en main
                        if (a == 1)
                        {
                            MessageBox.Show("Nombre de carte maximum atteint, échange de la dernière carte", "10 cartes maximums");
                            J1[i-1] = J1[i-1].Compare(J1[i-1]);
                            J1[i-1] = new Carte(x[i-1], y1);
                            pbJeu.Invalidate();
                        }

                        else //Si la carte est obtenue via un effet +2 ou +4
                        {
                            var CarteExces = new Carte(0, 0);
                            ExcesJ1.Add(CarteExces);
                          
                        }
                    }
                }

                if (!tourJ1)
                {
                    while (i < 10 && flag == true)
                    {
                        if (J2[i] == null)
                        {
                            J2[i] = new Carte(x[i], y2);

                            if (ExcesJ2.Count != 0)
                            { //Remplacer par la carte de la liste

                                var tmpCarte = ExcesJ2.First(); //Placer l'exces dans un tmp

                                tmpCarte.Remplacer(J2[i]); //Remplacer la carte piocher

                                ExcesJ2.RemoveAt(0); //Supprime 1er element

                            }

                            pbJeu.Invalidate();
                            flag = false;
                        }

                        i++;
                    }

                    if (flag)
                    {
                        if (a == 1)
                        {
                            MessageBox.Show("Nombre de carte maximum atteint, échange de la dernière carte", "10 cartes maximums");
                            J2[i - 1] = J2[i - 1].Compare(J2[i - 1]);
                            J2[i - 1] = new Carte(x[i - 1], y2);
                            pbJeu.Invalidate();
                        }

                        else //Si la carte est obtenue via un effet +2 ou +4
                        {
                            var CarteExces = new Carte(0, 0);
                            ExcesJ2.Add(CarteExces);

                        }
                    }
                }

                j--; // Nombre de carte a piocher est decrementer jusqu'a atteindre 0

            }
            while (j != 0);
        }

        public void Effet(int e)
        {
            switch(e)
            {
                case 10 : Pioche(2); break; //Effet de la carte +2
                case 11 : tourJ1 = ChangementTour(tourJ1); break;//Effet de la carte "passe ton tour"
                case 12 : Pioche(4); Pile.ChangeCouleur(Pile); break; //Effet de la carte +4
                case 13 : Pile.ChangeCouleur(Pile); break; //Effet de la carte changement de couleur
            }

            pbJeu.Invalidate();
        }

        #endregion

       
    }
}
