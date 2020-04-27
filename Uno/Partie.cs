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
    class Partie : ISerializable
    {
       private static FicUno fenetrePrincipale;

        public static FicUno FenetrePrincipale { get => fenetrePrincipale; set => fenetrePrincipale = value; } 
        //static et en get set pour pouvoir transferer les variables au main lors du chargement de la partie

        public Partie(FicUno uno)
        {
            if(fenetrePrincipale == null)
                fenetrePrincipale = uno; //on initialise la fenetre une fois
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //Obtient les variables demandees et les ajoutes au fichiers lors de la serialisation
            info.AddValue("Pile", fenetrePrincipale.pile, typeof(Carte));
            info.AddValue("J1Tour",fenetrePrincipale.TourJ1, typeof(bool));
            info.AddValue("J1", fenetrePrincipale.j1, typeof(Carte[]));
            info.AddValue("J2", fenetrePrincipale.j2, typeof(Carte[]));
        }

        public Partie(SerializationInfo info, StreamingContext context)
        {
            //Va rechercher les valeurs demandees dans le fichier serialisé
            fenetrePrincipale.pile = (Carte)info.GetValue("Pile", typeof(Carte));
            fenetrePrincipale.TourJ1 = (bool)info.GetValue("J1Tour", typeof(bool));
            fenetrePrincipale.j1 = (Carte[])info.GetValue("J1", typeof(Carte[]));
            fenetrePrincipale.j2 = (Carte[])info.GetValue("J2", typeof(Carte[]));
        }

    }
}

