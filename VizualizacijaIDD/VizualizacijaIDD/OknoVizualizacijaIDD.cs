using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VizualizacijaIDD
{
    public partial class OknoVizualizacijaIDD : Form
    {
        private IDD osnovnoDrevo;
        private IDDKoraki drevo;

        private int trenutniKorak;
        private int? oznacenoVozlisce; // trenutno označena vozlišča, lahko je null
        private Brush barvaOznacenega;

        // podatki za postavitev drevesa
        private readonly Dictionary<Vozlisce, int> sirine = new Dictionary<Vozlisce, int>(); // hrani širine poddreves
        private readonly Dictionary<Vozlisce, PointF> pozicije = new Dictionary<Vozlisce, PointF>(); // hrani pozicije vozlišč (X,Y) v pikslih
        private const int polmerVozlisca = 15;
        private const int notranjiRob = 20; // rob risalne površine (odmik od roba panela)
        private int navpicniRazmik = 70; // navpični razmik nivojev (glede na višino)

        /// <summary>
        /// Konstruktor okna za vizualizacijo drevesa.
        /// </summary>
        public OknoVizualizacijaIDD()
        {
            InitializeComponent();

            // ustvari osnovno drevo
            osnovnoDrevo = new IDD();

            // ustvari IDDKoraki na podlagi osnovnega drevesa
            drevo = new IDDKoraki(osnovnoDrevo);

            // inicializacija timerja za animacijo
            animacijaTimer.Interval = 800; // 0.8s
            animacijaTimer.Tick += AnimacijaTimer_Tick; // ko preteče interval, se izvede metoda

            // ob spremembi velikosti panela preračunamo postavitev
            pnlPrikaz.SizeChanged += (s, e) =>
            {
                PostavitevDrevesa(); // ponovno izračuna postavitev (koordinate) vozlišč
                pnlPrikaz.Invalidate();
            };

            // Privzete vrednosti na začetku
            btnNazaj.Enabled = false;
            btnNaprej.Enabled = false;
            rb1x.Checked = true;
        }

        /// <summary>
        /// Metoda za ustvarjanje drevesa
        /// </summary>
        private void btnUstvari_Click(object sender, EventArgs e)
        {
            if (drevo.JePrazno())
            {
                string input = tbxUstvari.Text; // prebere vnos

                int[] elementi = input
                    .Split(',')
                    .Select(s => s.Trim()) // odstrani presledke okrog posameznih elementov
                    .Select(int.Parse)
                    .ToArray();

                drevo.SestaviIzTabele(elementi); // zgradi drevo iz podane tabele elementov 

                // preračun postavitve in osvežitev okna
                PostavitevDrevesa();
                trenutniKorak = 0;
                PrikaziKorak();
                pnlPrikaz.Invalidate();
            }

            tbxUstvari.Clear();
            btnUstvari.Enabled = false; // onemogoči klik gumba
        }

        /// <summary>
        /// Metoda za dodajanje novega vozlišča. Izvede se ob kliku na gumb "Dodaj".
        /// </summary>
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxDodaj.Text, out int v))
            {
                drevo.koraki = drevo.PripraviVstavljanje(v);
                trenutniKorak = 0;
                ZacniAnimacijo();
                tbxDodaj.Clear();
                btnUstvari.Enabled = false;
            }

        }

        /// <summary>
        /// Metoda za odstranitev vozlišča. Izvede se ob kliku na gumb "Odstrani".
        /// </summary>
        private void btnOdstrani_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdstrani.Text, out int v))
            {
                drevo.koraki = drevo.PripraviBrisanje(v);
                trenutniKorak = 0;
                ZacniAnimacijo();
                tbxOdstrani.Clear();
            }
        }

        /// <summary>
        /// Metoda ki obarva iskano vozlišče. Izvede se ob kliku na gumb "Išči".
        /// </summary>
        private void btnIsci_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxIsci.Text, out int v))
            {
                drevo.koraki = drevo.IskanjeZKoraki(v);
                trenutniKorak = 0;
                ZacniAnimacijo();
                tbxIsci.Clear();
            }
        }

        /// <summary>
        /// Metoda resetira drevo. Izvede se ob kliku na gumb "Ponastavi".
        /// </summary>
        private void btnPonastavi_Click(object sender, EventArgs e)
        {
            osnovnoDrevo = new IDD(); // ustvari novo prazno osnovno drevo
            drevo = new IDDKoraki(osnovnoDrevo); // ustvari nov model korakov na podlagi praznega drevesa
            drevo.koraki = new List<Korak>(); // inicializira prazen seznam korakov

            oznacenoVozlisce = null;
            barvaOznacenega = Brushes.Violet;

            trenutniKorak = 0;

            PostavitevDrevesa(); // ponovno izračuna postavitev vozlišč
            pnlPrikaz.Invalidate();
            btnUstvari.Enabled = true; // omogoči klik na gumb "Ustvari" (spet lahko naredimo novo drevo)
            lblRazlaga.Text = "";
        }

        /// <summary>
        /// Metoda prikaže trenutno razlago koraka in osveži prikaz.
        /// </summary>
        private void PrikaziKorak()
        {
            if (drevo.koraki == null || drevo.koraki.Count == 0)
                return;

            if (trenutniKorak < drevo.koraki.Count)
            {
                lblRazlaga.Text = drevo.koraki[trenutniKorak].ToString(); // v "lblRazlaga" izpiše besedilo trenutnega koraka
                pnlPrikaz.Invalidate();
            }
        }

        /// <summary>
        /// Metoda za risanje, ki uporablja vnaprej izračunane pozicije.
        /// </summary>
        private void pnlPrikaz_Paint(object sender, PaintEventArgs e)
        {
            var koren = drevo.DobiKoren(); // koren drevesa
            if (koren == null) 
                return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // omogoči gladko risanje

            if (pozicije.Count == 0) 
                PostavitevDrevesa(); // če pozicije še niso izračunane, jih izračuna

            NarisiPovezave(e.Graphics, koren); // nariše povezave
            NarisiVozlisca(e.Graphics, koren); // nariše vozlišča
        }


        /// <summary>
        /// Metoda rekurzivno izračuna širino poddrevesa vozlišča v.
        /// </summary>
        private int IzracunajSirine(Vozlisce v)
        {
            if (v == null) 
                return 0;

            int sirinaLevo = IzracunajSirine(v.Levo);
            int sirinaDesno = IzracunajSirine(v.Desno);
            int sirinaPoddrevesa = Math.Max(1, sirinaLevo + sirinaDesno); // min 1, da list dobi svoj "prostor" in preprečimo deljenje z 0
            sirine[v] = sirinaPoddrevesa; // shrani širino vozlišča v slovar
            return sirinaPoddrevesa;
        }

        /// <summary>
        /// Metoda rekurzivno izračuna globino poddrevesa vozlišča v (št. nivojev).
        /// </summary>
        private int Globina(Vozlisce v)
        {
            if (v == null) 
                return 0;

            return 1 + Math.Max(Globina(v.Levo), Globina(v.Desno)); // + 1 zaradi trenutnega vozlišča
        }

        /// <summary>
        /// Metoda razporedi vozlišča drevesa po oknu tako, da se ne prekrivajo. 
        /// Ne vrača nič, ampak le polni slovar "pozicije" s PointF(x,y) za vsako vozlišče.
        /// </summary>
        private void RazporediVozlisca(Vozlisce v, float xLevo, float xDesno, float y)
        {
            if (v == null) 
                return;

            float xSredina = (xLevo + xDesno) / 2f; // sredina razpona (x koordinata)
            pozicije[v] = new PointF(xSredina, y); // shrani položaj trenutnega vozlišča

            int sirinaLevo = v.Levo != null ? sirine[v.Levo] : 0; // širina levega poddrevesa (0, če ga ni)
            int sirinaDesno = v.Desno != null ? sirine[v.Desno] : 0;
            int sirinaSkupaj = Math.Max(1, sirinaLevo + sirinaDesno); // vsaj 1, da se izognemo deljenju z 0

            float razpon = (xDesno - xLevo); // celotni razpon za razporeditev otrok
            float razponLevo = sirinaLevo > 0 ? razpon * (sirinaLevo / (float)sirinaSkupaj) : 0f; // delež razpona za levo poddrevo
            float razponDesno = sirinaDesno > 0 ? razpon * (sirinaDesno / (float)sirinaSkupaj) : 0f;

            // pri enem otroku malo zamaknemo levo oz. desno, da otrok ni točno pod staršem
            const float minOdmik = 10f; // najmanjši odmik v px
            const float maxOdmik = 24f; // največji odmik v px

            if (v.Levo != null && v.Desno == null) // samo levi sin
            {
                float odmik = Math.Max(minOdmik, Math.Min(maxOdmik, razponLevo * 0.15f)); // izračunaj odmik (z omejitvijo med min in max)
                float novoLevo = xLevo;
                float novoDesno = xLevo + razponLevo - 2 * odmik; // premakne mejo desno, da otrok ni direktno pod staršem

                // če bi bil razpon preozek, ostanemo pri prvotnem razponu
                if (novoDesno - novoLevo < 2 * polmerVozlisca + 6)
                {
                    novoLevo = xLevo;
                    novoDesno = xLevo + razponLevo;
                }

                RazporediVozlisca(v.Levo, novoLevo, novoDesno, y + navpicniRazmik); // rekurzivno obdela levo poddrevo
                return;
            }

            else if (v.Desno != null && v.Levo == null) // samo desni sin
            {
                float odmik = Math.Max(minOdmik, Math.Min(maxOdmik, razponDesno * 0.15f));
                float novoLevo = (xDesno - razponDesno) + 2 * odmik; // premakne mejo levo, da otrok ni direktno pod staršem
                float novoDesno = xDesno;

                if (novoDesno - novoLevo < 2 * polmerVozlisca + 6)
                {
                    novoLevo = xDesno - razponDesno;
                    novoDesno = xDesno;
                }

                RazporediVozlisca(v.Desno, novoLevo, novoDesno, y + navpicniRazmik);
                return;
            }

            // če ima vozlišče oba otroka ali nobenega, ne premikamo
            if (v.Levo != null)
                RazporediVozlisca(v.Levo, xLevo, xLevo + razponLevo, y + navpicniRazmik); // rekurzivno obdela levo poddrevo

            if (v.Desno != null)
                RazporediVozlisca(v.Desno, xDesno - razponDesno, xDesno, y + navpicniRazmik); // rekurzivno obdela desno poddrevo
        }

        /// <summary>
        /// Izračuna postavitev drevesa glede na velikost panela.
        /// Počisti slovarja "sirine" in "pozicije2, prilagodi razmike in dodeli (x,y) vsem vozliščem.
        /// </summary>
        private void PostavitevDrevesa()
        {
            // počistimo prejšnje izračune postavitve
            sirine.Clear();
            pozicije.Clear();

            var koren = drevo.DobiKoren();

            // če drevo ne obstaja ali je panel premajhen, končamo
            if (koren == null || pnlPrikaz.Width <= 2 * (notranjiRob + polmerVozlisca) || pnlPrikaz.Height <= 2 * (notranjiRob + polmerVozlisca))
                return;

            // vodoravni razpon v pikslih
            float levo = notranjiRob + polmerVozlisca; // leva meja območja risanja
            float desno = pnlPrikaz.Width - notranjiRob - polmerVozlisca; // desna meja območja risanja

            // globina drevesa (št. nivojev) in navpični razmik po višini panela
            int globina = Math.Max(1, Globina(koren));
            navpicniRazmik = Math.Max(60, (pnlPrikaz.Height - 2 * (notranjiRob + polmerVozlisca)) / globina); // navpični razmik med nivoji (najmanj 60 px)

            // širine poddreves
            IzracunajSirine(koren);

            // dodelimo pozicije oz. (x,y) koordinate vsem vozliščem
            RazporediVozlisca(koren, levo, desno, notranjiRob + polmerVozlisca); // razporedi vsa vozlišča po panelu
        }

        /// <summary>
        /// Metoda nariše povezave od vozlišča v do njegovih sinov.
        /// </summary>
        private void NarisiPovezave(Graphics g, Vozlisce v)
        {
            if (v == null || !pozicije.ContainsKey(v)) // če vozlišče ne obstaja ali nima izračunane pozicije, končamo
                return;

            var poz = pozicije[v]; // (x,y) položaj trenutnega vozlišča

            if (v.Levo != null) // ima levega sina
            {
                if (pozicije.ContainsKey(v.Levo)) // imamo izračunano pozicijo levega
                {
                    var pozLevo = pozicije[v.Levo]; // (x,y) položaj levega sina
                    g.DrawLine(Pens.Black, poz, pozLevo);
                    NarisiPovezave(g, v.Levo); // rekurzivno nariše povezave naprej po levem poddrevesu
                }
            }
            if (v.Desno != null)
            {
                if (pozicije.ContainsKey(v.Desno))
                {
                    var pozDesno = pozicije[v.Desno];
                    g.DrawLine(Pens.Black, poz, pozDesno);
                    NarisiPovezave(g, v.Desno);
                }
            }
        }

        /// <summary>
        /// Metoda na okno nariše vozlišče v (krog + podatek) in rekurzivno nadaljuje po poddrevesih.
        /// </summary>
        private void NarisiVozlisca(Graphics g, Vozlisce v)
        {
            if (v == null) 
                return;

            var poz = pozicije[v]; // (x,y) položaj trenutnega vozlišča

            Brush barvaVozlisca = Brushes.LightBlue;
            Pen robVozlisca = Pens.Black;

            if (oznacenoVozlisce.HasValue && v.Podatek == oznacenoVozlisce.Value) // če je "označeno" vozlišče, spremenimo barvo in rob 
            {
                barvaVozlisca = barvaOznacenega;
                robVozlisca = new Pen(Color.Black, 2f); // 2f določa debelino roba
            }

            g.FillEllipse(barvaVozlisca, poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca); // FillEllipse(Brush brush, float x, float y, float width, float height)
            g.DrawEllipse(robVozlisca, poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca); // DrawEllipse(Pen pen, float x, float y, float width, float height)

            using (var font = new Font("Arial", 10))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }; // objekt, ki določa centriranje besedila (horizontalna poravnava, navpična poravnava)
                g.DrawString(v.Podatek.ToString(), font, Brushes.Black, new RectangleF(poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca), sf); // DrawString(string besedilo, Font pisava, Brush barva, RectangleF pravokotnik, StringFormat poravnava)
            }

            NarisiVozlisca(g, v.Levo);
            NarisiVozlisca(g, v.Desno);
        }

        /// <summary>
        /// Metoda začne animacijo drevesa oziroma pripravi ročni način prikaza.
        /// </summary>
        private void ZacniAnimacijo()
        {
            PostavitevDrevesa();
            trenutniKorak = 0;
            oznacenoVozlisce = null;
            barvaOznacenega = Brushes.Violet;

            // ročni način
            if (rbRocno.Checked)
            {
                animacijaTimer.Stop(); // ne zaženemo timerja
                btnNaprej.Enabled = true; // omogočimo gumba Nazaj/Naprej
                btnNazaj.Enabled = true;
                pnlPrikaz.Invalidate();
                return; // brez animacije
            }

            // animacija
            btnNaprej.Enabled = false; // onemogočimo gumba Nazaj/Naprej
            btnNazaj.Enabled = false;
            animacijaTimer.Start(); // poženemo timer
        }

        /// <summary>
        /// Metoda izvede en korak animacije drevesa ob vsakem sproženem dogodku časovnika.
        /// </summary>
        private void AnimacijaTimer_Tick(object sender, EventArgs e)
        {
            if (drevo.koraki == null || trenutniKorak >= drevo.koraki.Count)
            {
                animacijaTimer.Stop();
                return;
            }

            var korak = drevo.koraki[trenutniKorak];

            // izvedi dejanski poseg, če je vstavljanje ali brisanje
            if (korak.Akcija == "vstavi")
            {
                // tu dejansko vstavi element v drevo
                drevo.VstaviZKoraki(korak.TrenutniPodatek.Value); // zdaj se drevo spremeni
            }
            else if (korak.Akcija == "brisanje vozlišča")
            {
                // tu dejansko odstrani element
                drevo.DejanskoBrisi(korak.TrenutniPodatek.Value);
            }

            // označi vozlišče za vizualizacijo
            oznacenoVozlisce = korak.TrenutniPodatek;

            // barva glede na akcijo
            switch (korak.Akcija)
            {
                case "primerjaj": barvaOznacenega = Brushes.Orange; break;
                case "pojdi levo":
                case "pojdi desno": barvaOznacenega = Brushes.LightGreen; break;
                case "najdeno": barvaOznacenega = Brushes.LightCoral; break;
                case "vstavi": barvaOznacenega = Brushes.Violet; break;
                case "brisanje vozlišča": barvaOznacenega = Brushes.Red; break;
                default: barvaOznacenega = Brushes.Gray; break;
            }

            lblRazlaga.Text = korak.Akcija + (korak.TrenutniPodatek != null ? $" ({korak.TrenutniPodatek})" : "");

            PostavitevDrevesa(); // osveži pozicije po vsakem posegu
            pnlPrikaz.Invalidate(); // ponovno nariši

            trenutniKorak++;
        }


        // metode za spreminjanje hitrosti animacije

        /// <summary>
        /// Metoda, ki se izvede ob označenem gumbu za hitrost 0.5x.
        /// Interval časovnika nastavi na 2000 ms.
        /// </summary>
        private void rb0_5x_CheckedChanged(object sender, EventArgs e) { 
            if (((RadioButton)sender).Checked) { 
                animacijaTimer.Interval = 2000;
                btnNaprej.Enabled = false; // onemogoči gumba Nazaj/Naprej
                btnNazaj.Enabled = false;
            } 
        }

        /// <summary>
        /// Metoda, ki se izvede ob označenem gumbu za hitrost 1x.
        /// Interval časovnika nastavi na 1000 ms.
        /// </summary>
        private void rb1x_CheckedChanged(object sender, EventArgs e) { 
            if (((RadioButton)sender).Checked) { 
                animacijaTimer.Interval = 1000;
                btnNaprej.Enabled = false; // onemogoči gumba Nazaj/Naprej
                btnNazaj.Enabled = false;
            } 
        }

        /// <summary>
        /// Metoda, ki se izvede ob označenem gumbu za hitrost 1.5x.
        /// Interval časovnika nastavi na 500 ms.
        /// </summary>
        private void rb1_5x_CheckedChanged(object sender, EventArgs e) { 
            if (((RadioButton)sender).Checked) { 
                animacijaTimer.Interval = 500;
                btnNaprej.Enabled = false; // onemogoči gumba Nazaj/Naprej
                btnNazaj.Enabled = false;
            } 
        }

        /// <summary>
        /// Metoda, ki se izvede ob označenem gumbu za ročno premikanje.
        /// </summary>
        private void rbRocno_CheckedChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked)
            {
                animacijaTimer.Stop(); // takoj ustavi animacijo
                btnNaprej.Enabled = true; // omogoči gumba Nazaj/Naprej
                btnNazaj.Enabled = true;
            }
        }

        /// <summary>
        /// Metoda, ki se izvede ob kliku na gumb "Nazaj".
        /// </summary>
        private void btnNazaj_Click(object sender, EventArgs e)
        {
            if (drevo.koraki == null || drevo.koraki.Count == 0) // če ni korakov, ne naredimo nič
                return;

            if (trenutniKorak <= 0) // če smo na začetku, ne gremo nazaj
                return;

            trenutniKorak = Math.Max(0, trenutniKorak - 2); // premaknemo se za dve mesti nazaj, ker "AnimacijaTimer_Tick" poveča "trenutniKorak" za 1
            AnimacijaTimer_Tick(sender, e); // izvedemo animacijo za novi trenutni korak
        }

        /// <summary>
        /// Metoda, ki se izvede ob kliku na gumb "Naprej".
        /// </summary>
        private void btnNaprej_Click(object sender, EventArgs e)
        {
            AnimacijaTimer_Tick(sender, e); // izvedemo naslednji korak animacije
        }
    }
}
