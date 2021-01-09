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
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Newtonsoft.Json;

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
        private int nbCarte1 = -1;
        private int nbCarte2;
        //Variable comptant le nombre de carte en main à chaque tours
        private FileStream fSave; //fichier permettant la sauvegarde et le chargement
        private Partie PartieSave; //classe gardant en memoire toutes les variables de la partie 
        private bool verifuno = false; //Vérifie si le joueur a dit Uno lorsqu'il a sa dernière carte en main
        private bool typeCon; //Boolean client ou serveur selon le choix du joueur
        private Socket MonClient, MonServeur; //Socket de communication
        private int DrapeauSocket = 0; //1 pour serveur et 2 pour client
        private byte[] MonBuffer = new byte[4160]; //Buffer de donnees
        private string Serveur = "Youssef"; //Nom du serveur
        private string OutJson; //Donnees envoyer

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

            typeCon = choix;

            if (typeCon)
            {
                lbSocket.Text = "Vous êtes le serveur";

                Ecouter();

            }
            else
            {
                lbSocket.Text = "Vous êtes le client";

                Connecter();
            }

        }

        private void FicUno_Load(object sender, EventArgs e)
        {
            Start();
        }

        public Task Start() //Creation du jeu 
        {
            tourJ1 = true;

            if (!typeCon)
                Pile = null; //Place la pile à null si client

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

            pbJeu.Invalidate();

            return Task.CompletedTask;

        }

        public void Actif(bool actif) //Active ou disactive les boutons selon le tour du joueur
        {
           btnLoad.Enabled = btnPiocher.Enabled = btnSauver.Enabled = btnUno.Enabled = actif;
           lblUno.Visible = btnLoad.Visible = btnPiocher.Visible = btnSauver.Visible = btnUno.Visible = actif;
        }

        private void pbPile_Paint(object sender, PaintEventArgs e)
        {
            Actif(false);

            Pile?.Dessine(e);

            nbCarte2 = 0;
            nbCarte1 = 0;

            for (int i = 0; i < start; i++)
            {

                if (J1[i] != null && tourJ1 && typeCon)
                {
                    J1[i].Dessine(e);
                    Actif(true);
                }

                if (J2[i] != null && !tourJ1 && !typeCon)
                {
                    J2[i].Dessine(e);
                    Actif(true);
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
                if (ys > y1 && ys < (y1 + 130) && tourJ1 == true && typeCon)
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
                                MessageBox.Show("Victoire du serveur", "Victoire");

                                EnvoyerJson();

                                Deconnecter();

                                Close();

                                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du joueur 1 et on ferme l'application
                            }

                            if (J1[i] == null)
                            {// Si la carte a été jouée on passe au tour suivant et on prend en compte son effet

                                Uno(tourJ1);

                                ChangementTour();

                                Effet(Pile.ValeurEffet(Pile));

                                EnvoyerJson();

                            }
                        }

                    }
                }

                if (ys > y2 && ys < (y2 + 130) && !tourJ1 == true && !typeCon)
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
                                MessageBox.Show("Victoire du client", "Victoire");

                                EnvoyerJson();

                                Deconnecter();

                                Close();

                                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du joueur 2 et on ferme l'application
                            }

                            if (J2[i] == null)
                            {
                                Uno(tourJ1);

                                ChangementTour();

                                Effet(Pile.ValeurEffet(Pile));

                                EnvoyerJson();
                            }
                        }

                    }
                }

            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûre ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                if(Deconnecter()) //Si il n'y a pas de client connecté, le serveur peut se déco
                    Close();
            }
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

        private void bUno_Click(object sender, EventArgs e)
        {
            verifuno = true;
        }

        #endregion

        #region Methodes console

        public void ChangementTour()
        {
            tourJ1 = !tourJ1;
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
            ChangementTour();

            EnvoyerJson();
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
                case 11 : ChangementTour(); break;//Effet de la carte "passe ton tour"
                case 12 : Pioche(4); Pile.ChangeCouleur(Pile); break; //Effet de la carte +4
                case 13 : Pile.ChangeCouleur(Pile); break; //Effet de la carte changement de couleur
            }

            pbJeu.Invalidate();
        }

        #endregion

        #region Socket

        #region debug

        delegate void RenvoiVersInserer(string sPartie);
        private void InsererItemThread(string sPartie)
        {
            Thread ThreadInsererItem = new Thread(new ParameterizedThreadStart(InsererItem));
            ThreadInsererItem.Start(sPartie);
        }

        private void InsererItem(object oPartie)
        {
            if (pbJeu.InvokeRequired)
            {
                RenvoiVersInserer f = new RenvoiVersInserer(InsererItem);
                Invoke(f, new object[] { (string)oPartie });
            }
            else
            {
                OutJson = (string)oPartie;

                RecevoirJson(OutJson);
            }

        }

        #endregion

        public void RecevoirJson(string InJson)
        {
            string[] Jeu = InJson.Split('#');

                Pile = JsonConvert.DeserializeObject<Carte>(Jeu[0]);

                J1 = JsonConvert.DeserializeObject<Carte[]>(Jeu[1]);

                J2 = JsonConvert.DeserializeObject<Carte[]>(Jeu[2]);

                TourJ1 = Convert.ToBoolean(Jeu[3]);

                ExcesJ1 = JsonConvert.DeserializeObject<List<Carte>>(Jeu[4]);

                ExcesJ2 = JsonConvert.DeserializeObject<List<Carte>>(Jeu[5]);


                pbJeu.Invalidate();

                if (Pile.Gagner(J1) == true)
                {
                    DialogResult r = MessageBox.Show("Victoire du serveur", "Victoire",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                    Deconnecter();

                    Close();

                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du serveur et on ferme l'application
                }

                if (Pile.Gagner(J2) == true)
                {
                    DialogResult r = MessageBox.Show("Victoire du client", "Victoire",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                     Deconnecter();

                     Close();

                //Si toutes les cartes sont nuls on ouvre une messagebox pour annoncer la victoire du client et on ferme l'application
                }
        }

        public void EnvoyerJson()
        {
            string jPile = JsonConvert.SerializeObject(Pile);
            string jJ1 = JsonConvert.SerializeObject(J1);
            string jJ2 = JsonConvert.SerializeObject(J2);
            string jExcesJ1 = JsonConvert.SerializeObject(ExcesJ1);
            string jExcesJ2 = JsonConvert.SerializeObject(ExcesJ2);

            string json = jPile + "#" + jJ1 + "#" + jJ2 + "#" + TourJ1 + "#" + jExcesJ1 + "#" + jExcesJ2;

            EnvoyerSocket(json);

        }

        public void EnvoyerSocket(string json)
        {
            if (MonClient == null)
            {
                DialogResult r = MessageBox.Show("Client ou serveur inacessible", "Erreur de connexion",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                Deconnecter();

                Environment.Exit(0);
            }

            else
            {
                MonClient.Send(Encoding.Unicode.GetBytes(json));
            }

        }

        private IPAddress AdresseValide(string nPC)
        {

            IPAddress ipReponse = null;

            if (nPC.Length > 0)
            {

                IPAddress[] IPServeur = Dns.GetHostEntry(nPC).AddressList;

                for (int i = 0; i < IPServeur.Length; i++)
                {
                    Ping pingServeur = new Ping();
                    PingReply pingReponse = pingServeur.Send(IPServeur[i]);

                    if (pingReponse.Status == IPStatus.Success)
                    {
                        if (IPServeur[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipReponse = IPServeur[i];
                            break;
                        }
                    }

                }

            }

            return ipReponse;

        }

        public void Connecter()
        {
            DrapeauSocket = 2;
            MonClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MonClient.Blocking = false;
            IPAddress IPserveur = AdresseValide(Serveur);
            MonClient.BeginConnect(new IPEndPoint(IPserveur, 8000), new AsyncCallback(SurConnexion), MonClient);
        }

        public void Ecouter()
        {
            DrapeauSocket = 1;
            MonClient = null;
            IPAddress IPServeur = AdresseValide(Dns.GetHostName());
            MonServeur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                MonServeur.Bind(new IPEndPoint(IPServeur, 8000));
            }
            catch (SocketException) //catch la connexion multiple de serveur au port 8000
            {
                DialogResult r = MessageBox.Show("Un seul serveur peut être connecté", "Serveur déjà connecté",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                Deconnecter();

                Environment.Exit(0);
            }

            MonServeur.Listen(1);
            MonServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), MonServeur);
        }

        public void SurDemandeConnexion(IAsyncResult iar)
        {
            if (DrapeauSocket == 1)
            {
                Socket tmp = (Socket)iar.AsyncState;

                try
                {
                    MonClient = tmp.EndAccept(iar);
                }
                catch(ObjectDisposedException)
                {
                    Environment.Exit(0);
                }
                InitialiserReception(MonClient);
                Task.WaitAll(Start()); //Attend la fin de la création des cartes
                EnvoyerJson();
            }
        }

        public void InitialiserReception(Socket soc)
        {
            soc.BeginReceive(MonBuffer, 0, MonBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
            Array.Clear(MonBuffer, 0, MonBuffer.Length);
        }

        public void Reception(IAsyncResult iar)
        {
            try
            {
                if (DrapeauSocket > 0)
                {
                    Socket tmp = (Socket) iar.AsyncState;
                    int recu = tmp.EndReceive(iar);

                    if (recu > 0)
                    {
                        InsererItemThread(Encoding.Unicode.GetString(MonBuffer));

                        InitialiserReception(tmp);

                        Array.Clear(MonBuffer, 0, MonBuffer.Length);
                    }
                    else
                    {
                        tmp.Disconnect(true);
                        tmp.Close();
                        MonServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), MonServeur);
                        MonClient = null;
                    }
                }
            }
            catch
            {
                Deconnecter();

                Environment.Exit(0);
            }

        }

        public void DemandeConnexion(IAsyncResult iar)
        {
            Socket tmp = (Socket)iar.AsyncState;
            tmp.EndDisconnect(iar);
        }

        public void SurConnexion(IAsyncResult iar)
        {
            Socket tmp = (Socket)iar.AsyncState;

            if (tmp.Connected)
            {
                InitialiserReception(tmp);
            }
            else
            {
                DialogResult r = MessageBox.Show("Le serveur n'est pas connecté", "Serveur inaccessible",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0x40000);

                    Deconnecter();

                    Environment.Exit(0);

            }
        }

        public bool Deconnecter()
        {
            if (DrapeauSocket == 2)
            {
                try
                {
                    MonClient.Shutdown(SocketShutdown.Both);
                    DrapeauSocket = 0;
                    MonClient.BeginDisconnect(false, new AsyncCallback(DemandeConnexion), MonClient);

                    return true;
                }
                catch
                {
                    return true;
                }

            }
            else if (MonClient == null)
            {
                MonServeur.Close();
                DrapeauSocket = 0;

                return true;
            }
            else
            {
                MessageBox.Show("Le serveur ne peut pas se déconnecter tant qu'un client est connecté", "Client connecté", MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                return false;
            }
        }

        #endregion
    }
}
