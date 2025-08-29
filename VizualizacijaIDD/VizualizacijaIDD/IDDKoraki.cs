using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VizualizacijaIDD
{
    internal class IDDKoraki
    {
        private readonly IDD drevo;      // dejansko drevo
        public List<Korak> koraki;       // seznam korakov za animacijo

        public IDDKoraki(IDD drevo)
        {
            this.drevo = drevo;
            this.koraki = new List<Korak>();
        }

        public Vozlisce DobiKoren()
        {
            return drevo.koren;
        }

        public bool JePrazno()
        {
            return drevo.koren == null;
        }

        public void SestaviIzTabele(int[] podatki)
        {
            drevo.SestaviIzTabele(podatki); // iz razreda IDD
        }

        /// <summary>
        /// Priprava korakov za vstavljanje
        /// </summary>
        public List<Korak> PripraviVstavljanje(int podatek)
        {
            var k = new List<Korak>();
            HelperPripraviVstavljanje(drevo.koren, podatek, k);
            koraki = k;
            return k;
        }

        private void HelperPripraviVstavljanje(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            //Dodamo ustrezne korake, pri tem ne spreminjamo drevesa
            if (vozlisce == null)
            {
                koraki.Add(new Korak { TrenutniPodatek = podatek, Akcija = "vstavi" });
                return;
            }

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek < vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi levo" });
                HelperPripraviVstavljanje(vozlisce.Levo, podatek, koraki);
            }
            else if (podatek > vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi desno" });
                HelperPripraviVstavljanje(vozlisce.Desno, podatek, koraki);
            }
            else
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "napaka - podatek že obstaja" });
            }
        }

        /// <summary>
        /// Dejansko vstavimo v drevo (med animacijo)
        /// </summary>
        public void VstaviZKoraki(int podatek)
        {
            drevo.Vstavi(podatek);
        }

        /// <summary>
        /// Priprava korakov za iskanje
        /// </summary>
        public List<Korak> IskanjeZKoraki(int podatek)
        {
            var k = new List<Korak>();
            bool najdeno = HelperIskanjeZKoraki(drevo.koren, podatek, k); // iz razreda IDD
            if (!najdeno)
                k.Add(new Korak { TrenutniPodatek = null, Akcija = "napaka - podatek ne obstaja" });
            koraki = k;
            return k;
        }

        private bool HelperIskanjeZKoraki(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            if (vozlisce == null)
                return false;

            //Ustrezni koraki

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek == vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "najdeno" });
                return true;
            }
            else if (podatek < vozlisce.Podatek)
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

        /// <summary>
        /// Priprava korakov za brisanje vozlišča
        /// </summary>
        public List<Korak> PripraviBrisanje(int podatek)
        {
            var k = new List<Korak>();
            HelperPripraviBrisanje(drevo.koren, podatek, k);
            koraki = k;
            return k;
        }

        private void HelperPripraviBrisanje(Vozlisce vozlisce, int podatek, List<Korak> koraki)
        {
            //Ustrezni koraki
            if (vozlisce == null)
            {
                koraki.Add(new Korak { TrenutniPodatek = null, Akcija = "napaka - podatek ne obstaja" });
                return;
            }

            koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "primerjaj" });

            if (podatek < vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi levo" });
                HelperPripraviBrisanje(vozlisce.Levo, podatek, koraki);
            }
            else if (podatek > vozlisce.Podatek)
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "pojdi desno" });
                HelperPripraviBrisanje(vozlisce.Desno, podatek, koraki);
            }
            else
            {
                koraki.Add(new Korak { TrenutniPodatek = vozlisce.Podatek, Akcija = "brisanje vozlišča" });
            }
        }

        /// <summary>
        /// Dejansko brisanje elementa v drevesu
        /// </summary>
        public void DejanskoBrisi(int podatek)
        {
            drevo.Brisi(podatek);
        }


    }
}
