using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IskalnoDvojiskoDrevo
{
    /// <summary>
    /// Razred IDD, ki skrbi za ustrezne korake med izvajanjem posameznih metod
    /// </summary>
    internal class IDDKoraki
    {
        private readonly IDD drevo;

        public IDDKoraki(IDD drevo)
        {
            this.drevo = drevo;
        }

        // Metode skrbijo za ustrezen seznam korakov, ki so potrebni za izvajanje tovrstnih metod (iskanje, brisanje, vstavljanje)

        public List<Korak> VstaviZKoraki(int podatek)
        {
            List<Korak> koraki = new List<Korak>();
            drevo.koren = HelperVstaviZKoraki(drevo.koren, podatek, koraki);
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
            List<Korak> koraki = new List<Korak>();
            bool najdeno = HelperIskanjeZKoraki(drevo.koren, podatek, koraki);

            if (!najdeno)
                koraki.Add(new Korak { TrenutniPodatek = null, Akcija = "ni najdeno" });

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
            List<Korak> koraki = new List<Korak>();
            drevo.koren = HelperBrisiZKoraku(drevo.koren, podatek, koraki);
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
