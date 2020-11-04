using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Uno
{
    public class Joueur
    {
        private Socket MonClient, MonServeur;
        private int DrapeauSocket = 0; //1 pour serveur et 2 pour client
        private byte[] MonBuffer = new byte[256];
        private string Serveur = "Youssef";
        public string OutJson { get; private set; }

        //MonClient.Send(Encoding.Unicode.GetBytes(tMessage.Text)); btn envoyer

        #region debug

        delegate void RenvoiVersInserer(string sPartie);
        private void InsererItemThread(string sPartie)
        {
            Thread ThreadInsererItem = new Thread(new ParameterizedThreadStart(InsererItem));
            ThreadInsererItem.Start(sPartie);
        }

        private void InsererItem(object oPartie)
        {
            OutJson = (string)oPartie; //Renvoyer outJson à ficuno et changer les variables
        }

        #endregion

        #region methode

        public void EnvoyerSocket(string json)
        {
            MonClient.Send(Encoding.Unicode.GetBytes(json));
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
            MonServeur.Bind(new IPEndPoint(IPServeur, 8000));
            MonServeur.Listen(1);
            MonServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), MonServeur);
        }

        public void SurDemandeConnexion(IAsyncResult iar)
        {
            if (DrapeauSocket == 1)
            {
                Socket tmp = (Socket)iar.AsyncState;
                MonClient = tmp.EndAccept(iar);
                InitialiserReception(MonClient);
            }
        }

        public void InitialiserReception(Socket soc)
        {
            soc.BeginReceive(MonBuffer, 0, MonBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
            Array.Clear(MonBuffer, 0, MonBuffer.Length);
        }

        public void Reception(IAsyncResult iar)
        {
            if (DrapeauSocket > 0)
            {
                Socket tmp = (Socket)iar.AsyncState;
                int recu = tmp.EndReceive(iar);

                if (recu > 0)
                {
                    InsererItemThread(Encoding.Unicode.GetString(MonBuffer));
                    //lbEchanges.Items.Insert(0, Encoding.Unicode.GetString(MonBuffer));
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
                MessageBox.Show("Serveur inaccessible");
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
                MessageBox.Show("Client connecté = pas de déconnexion");

                return false;
            }
        }


        #endregion

        public Joueur(bool choix)
        {
            if (choix)
            {
                try
                {
                    Ecouter();
                }
                catch{ Connecter();}
            }
            else
            {
                Connecter();
            }

        }

    }

}
