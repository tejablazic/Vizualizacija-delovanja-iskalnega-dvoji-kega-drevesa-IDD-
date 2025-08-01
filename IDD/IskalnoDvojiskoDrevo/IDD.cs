using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IskalnoDvojiskoDrevo
{
    internal class IDD
    {
        internal Vozlisce koren { get; set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        public IDD()
        {
            this.koren = null;
        }
        /// <summary>
        /// konstruktor za IDD ustvarjen s tabelo
        /// </summary>
        /// <param name="tabela"></param>
        public IDD(int[] tabela)
        {
            this.koren = null;
            SestaviIzTabele(tabela);
        }

        /// <summary>
        /// vsak element v tabeli postopoma dodamo v IDD
        /// Namen ni ustvariti najnižje možno drevo, le postopoma graditi IDD
        /// </summary>
        public void SestaviIzTabele(int[] tabela)
        {
            foreach(int x in tabela)
            {
                Vstavi(x);
            }
        }

        /// <summary>
        /// metoda za vstavljanje v IDD, ki se sklicuje na rekurzivno metodo
        /// </summary>
        public void Vstavi(int podatek)
        {
            this.koren = HelperVstavi(this.koren, podatek);
        }

        public Vozlisce HelperVstavi(Vozlisce vozlisce, int podatek)
        {
            if(vozlisce == null) // prazno drevo
            {
                return new Vozlisce(podatek);
            }
            if(podatek < vozlisce.Podatek) // gremo v levo poddrevo
            {
                vozlisce.Levo = HelperVstavi(vozlisce.Levo, podatek);
            }
            else if(podatek > vozlisce.Podatek) // desno poddrevo
            {
                vozlisce.Desno = HelperVstavi(vozlisce.Desno, podatek);
            }

            return vozlisce;
            
        }

        /// <summary>
        /// metoda vrne true, če element obstaja v drevesu in false Sicer
        /// </summary>
        public bool Iskanje(int podatek)
        {
            return HelperIskanje(this.koren, podatek);
        }

        public bool HelperIskanje(Vozlisce vozlisce, int podatek)
        {
            if(vozlisce == null) // v drevesu ni podatka
            {
                return false;
            }
            if(podatek == vozlisce.Podatek) // nasli smo ga
            {
                return true;
            }
            if(podatek < vozlisce.Podatek) // iskanje v levem poddrevesu
            {
                return HelperIskanje(vozlisce.Levo, podatek);
            }
            else
            {
                return HelperIskanje(vozlisce.Desno, podatek); // iskanje v densme poddr.
            }
        }
        /// <summary>
        /// metoda za brisanje elementa v IDD
        /// Ločimo tri možnosti: vozlišče ki ga izbrišemo ima samo levega/desnega sina, nima sinov, ima oba sinova
        /// </summary>
        public void Brisi(int podatek)
        {
            this.koren = HelperBrisi(koren, podatek);
        }

        public Vozlisce HelperBrisi(Vozlisce vozlisce, int podatek)
        {
            if (vozlisce == null) return null; // prazno

            if (podatek < vozlisce.Podatek) // premik v levo poddrevo
            {
                vozlisce.Levo = HelperBrisi(vozlisce.Levo, podatek);
            }
            else if (podatek > vozlisce.Podatek) // desmo poddrevo
            {
                vozlisce.Desno = HelperBrisi(vozlisce.Desno, podatek);
            }
            else // iskan podatek za brisanje
            {
                // brez otrok
                if (vozlisce.Levo == null && vozlisce.Desno == null)
                    return null;

                // en otrok
                if (vozlisce.Levo == null)
                    return vozlisce.Desno;
                if (vozlisce.Desno == null)
                    return vozlisce.Levo;

                // dva otroka (menjava z najmanjsim sinom)
                Vozlisce min = Najmanjsi(vozlisce.Desno);
                vozlisce.Podatek = min.Podatek;
                vozlisce.Desno = HelperBrisi(vozlisce.Desno, min.Podatek);
            }
            return vozlisce;
        }

        /// <summary>
        /// Poisce najmanjsi podatek v IDD (iskanje v levem poddrevesu)
        /// </summary>
        private Vozlisce Najmanjsi(Vozlisce vozlisce)
        {
            while(vozlisce.Levo != null)
            {
                vozlisce = vozlisce.Levo;
            }
            return vozlisce;
        }

        /// <summary>
        /// Izpis drevesa
        /// </summary>
        public override string ToString()
        {
            return VozlisceToString(koren, 0);
        }

        private string VozlisceToString(Vozlisce vozlisce, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 4);

            if (vozlisce == null)
                return indent + "null";

            string rezultat = indent + $"IDD({vozlisce.Podatek},\n";

            // levo
            rezultat += indent + "    levo(";
            if (vozlisce.Levo == null)
            {
                rezultat += "null";
            }
            else
            {
                rezultat += VozlisceToString(vozlisce.Levo, indentLevel + 2).TrimStart();
            }
            rezultat += "),\n";

            // desno
            rezultat += indent + "    desno(";
            if (vozlisce.Desno == null)
            {
                rezultat += "null";
            }
            else
            {
                rezultat += VozlisceToString(vozlisce.Desno, indentLevel + 2).TrimStart();
            }
            rezultat += ")\n";

            rezultat += indent + ")";

            return rezultat;
        }





    }
}
