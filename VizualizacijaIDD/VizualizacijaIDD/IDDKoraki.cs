using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VizualizacijaIDD
{
    internal class IDDKoraki
    {
        private readonly IDD drevo;
        public List<Korak> koraki;

        public IDDKoraki(IDD drevo)
        {
            this.drevo = drevo;
        }

        public Vozlisce Koren => drevo.koren;  // preusmeri dostop do korena


        // Metode skrbijo za ustrezen seznam korakov, ki so potrebni za izvajanje tovrstnih metod (iskanje, brisanje, vstavljanje)

        public List<Korak> SestaviIzTabele(int[] tabela)
        {
            koraki = new List<Korak>(); // resetiramo seznam korakov

            foreach (int x in tabela)
            {
                var korakiVstavitve = VstaviZKoraki(x); // vstavi in dobi korake
                koraki.AddRange(korakiVstavitve);       // jih dodaj v glavni seznam
            }

            return koraki;
        }
        public List<Korak> VstaviZKoraki(int podatek)
        {
            var koraki = new List<Korak>();
            drevo.koren = HelperVstaviZKoraki(drevo.koren, podatek, koraki);
            this.koraki = koraki;
            return koraki;
        }

        private Vozlisce HelperVstaviZKoraki(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            if (vozlisce == null)
            {
                koraki.Add(new Korak { TrenutniPodatek = null, Akcija = "vstavi" });
                return new Vozlisce(podatek);
            }

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek < vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi levo" });
                vozlisce.Levo = HelperVstaviZKoraki(vozlisce.Levo, podatek, koraki);
            }
            else if (podatek > vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi desno" });
                vozlisce.Desno = HelperVstaviZKoraki(vozlisce.Desno, podatek, koraki);
            }
            else
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "napaka - podatek že obstaja" });
            }

            return vozlisce;
        }


        public List<Korak> IskanjeZKoraki(int podatek)
        {
            var koraki = new List<Korak>();
            bool najdeno = HelperIskanjeZKoraki(drevo.koren, podatek, koraki);
            if (!najdeno)
                koraki.Add(new Korak { TrenutniPodatek = null, Akcija = "ni najdeno" });
            this.koraki = koraki;
            return koraki;
        }

        private bool HelperIskanjeZKoraki(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            if (vozlisce == null)
                return false;

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek == vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "najdeno" });
                return true;
            }

            if (podatek < vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi levo" });
                return HelperIskanjeZKoraki(vozlisce.Levo, podatek, koraki);
            }
            else
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi desno" });
                return HelperIskanjeZKoraki(vozlisce.Desno, podatek, koraki);
            }
        }


        public List<Korak> BrisiZKoraki(int podatek)
        {
            var koraki = new List<Korak>();
            drevo.koren = HelperBrisiZKoraku(drevo.koren, podatek, koraki);
            this.koraki = koraki;
            return koraki;
        }

        private Vozlisce HelperBrisiZKoraku(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            if (vozlisce == null)
            {
                koraki.Add(new Korak { TrenutniPodatek = null, Akcija = "ni za brisat (null)" });
                return null;
            }

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek < vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi levo" });
                vozlisce.Levo = HelperBrisiZKoraku(vozlisce.Levo, podatek, koraki);
            }
            else if (podatek > vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi desno" });
                vozlisce.Desno = HelperBrisiZKoraku(vozlisce.Desno, podatek, koraki);
            }
            else
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "brisanje vozlišča" });

                if (vozlisce.Levo == null && vozlisce.Desno == null)
                {
                    koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "vozlišče brez otrok – izbriši" });
                    return null;
                }

                if (vozlisce.Levo == null)
                {
                    koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "ima samo desnega otroka" });
                    return vozlisce.Desno;
                }

                if (vozlisce.Desno == null)
                {
                    koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "ima samo levega otroka" });
                    return vozlisce.Levo;
                }

                Vozlisce min = Najmanjsi(vozlisce.Desno);
                koraki.Add(new Korak { TrenutniPodatek = min.Podatek, Akcija = "najdi naslednika (najmanjši v desnem)" });

                vozlisce.Podatek = min.Podatek;
                vozlisce.Desno = HelperBrisiZKoraku(vozlisce.Desno, min.Podatek, koraki);
            }

            return vozlisce;
        }

        public bool JePrazno()
        {
            return drevo.koren == null;
        }

        private Vozlisce Najmanjsi(Vozlisce vozlisce)
        {
            while (vozlisce.Levo != null)
            {
                vozlisce = vozlisce.Levo;
            }
            return vozlisce;
        }
    }
}
