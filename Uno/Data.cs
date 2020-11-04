using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    public class Data
    {
        private Carte pile;
        private Carte[] j1;
        private Carte[] j2;
        private List<Carte> excesJ1;
        private List<Carte> excesJ2;

        public Data(object Pile, object J1, object J2, object ExcesJ1, object ExcesJ2)
        {
            pile = (Carte)Pile;
            j1 = (Carte[])J1;
            j2 = (Carte[])J2;
            excesJ1 = (List<Carte>)ExcesJ1;
            excesJ2 = (List<Carte>)ExcesJ2;
        }

    }

}
