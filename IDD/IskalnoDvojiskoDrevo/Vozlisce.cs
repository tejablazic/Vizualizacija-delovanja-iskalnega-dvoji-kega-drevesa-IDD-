using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IskalnoDvojiskoDrevo
{
    internal class Vozlisce
    {

        public int Podatek;
        public Vozlisce Levo; // levi sin (struktura drevesa)
        public Vozlisce Desno; // desni sin, sinovi so tudi IDD

        /// <summary>
        /// konstruktor
        /// </summary>
        public Vozlisce(int podatek)
        {
            Podatek = podatek;
            Levo = null;
            Desno = null;
        }

    }
}
