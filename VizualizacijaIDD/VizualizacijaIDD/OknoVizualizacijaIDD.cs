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

        private int? oznacenoVozlisce = null;
        private Brush barvaOznacenega = Brushes.Violet;

        int trenutniKorak = 0;

        // podatki za postavitev drevesa
        private readonly Dictionary<Vozlisce, int> sirine = new Dictionary<Vozlisce, int>(); // hrani širine poddreves
        private readonly Dictionary<Vozlisce, PointF> pozicije = new Dictionary<Vozlisce, PointF>(); // hrani pozicije vozlišč (X,Y) v pikslih
        private const int polmerVozlisca = 15;
        private const int notranjiRob = 20; // rob risalne površine
        private int navpicniRazmik = 70; // navpični razmik nivojev (glede na višino)

        /// <summary>
        /// Konstruktor okna.
        /// </summary>
        public OknoVizualizacijaIDD()
        {
            InitializeComponent();

            // ustvari osnovno drevo
            osnovnoDrevo = new IDD();

            // ustvari IDDKoraki na podlagi osnovnega drevesa
            drevo = new IDDKoraki(osnovnoDrevo);

            // ob spremembi velikosti panela preračunamo postavitev
            pnlPrikaz.SizeChanged += (s, e) =>
            {
                LayoutTree();
                pnlPrikaz.Invalidate();
            };
        }

        /// <summary>
        /// Metoda, ki se požene ob kliku gumba USTVARI.
        /// </summary>
        private void btnUstvari_Click(object sender, EventArgs e)
        {
            if (drevo.JePrazno())
            {
                string input = tbxUstvari.Text;

                int[] elementi = input
                    .Split(',')
                    .Select(s => s.Trim())
                    .Select(int.Parse)
                    .ToArray();

                drevo.SestaviIzTabele(elementi);

                // preračun postavitve in osvežitev okna
                LayoutTree();
                trenutniKorak = 0;
                PrikaziKorak();
                pnlPrikaz.Invalidate();
            }
            tbxUstvari.Clear();
        }

        /// <summary>
        /// Metoda, ki se požene ob kliku gumba DODAJ.
        /// </summary>
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxDodaj.Text, out int v))
            {
                drevo.VstaviZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;

                LayoutTree();
                trenutniKorak = 0;
                PrikaziKorak();
                pnlPrikaz.Invalidate();

                tbxDodaj.Clear();
            }
        }

        /// <summary>
        /// Metoda, ki se požene ob kliku gumba ODSTRANI.
        /// </summary>
        private void btnOdstrani_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdstrani.Text, out int v))
            {
                drevo.BrisiZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;

                LayoutTree();
                trenutniKorak = 0;
                PrikaziKorak();
                pnlPrikaz.Invalidate();

                tbxOdstrani.Clear();
            }
        }

        /// <summary>
        /// Metoda, ki se požene ob kliku gumba IŠČI.
        /// </summary>
        private void btnIsci_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxIsci.Text, out int v))
            {
                drevo.IskanjeZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;

                // LayoutTree ni nujen pri iskanju, a ohranja konsistentnost
                LayoutTree();
                trenutniKorak = 0;
                PrikaziKorak();
                pnlPrikaz.Invalidate();

                tbxIsci.Clear();
            }
        }

        /// <summary>
        /// Metoda, ki se požene ob kliku gumba PONASTAVI.
        /// </summary>
        private void btnPonastavi_Click(object sender, EventArgs e)
        {
            osnovnoDrevo = new IDD();
            drevo = new IDDKoraki(osnovnoDrevo);
            drevo.koraki = new List<Korak>();

            oznacenoVozlisce = null;
            barvaOznacenega = Brushes.Violet;

            trenutniKorak = 0;

            LayoutTree();
            pnlPrikaz.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrikaziKorak()
        {
            if (drevo.koraki == null || drevo.koraki.Count == 0)
                return;

            if (trenutniKorak < drevo.koraki.Count)
            {
                lblRazlaga.Text = drevo.koraki[trenutniKorak].ToString();
                pnlPrikaz.Invalidate();
            }
        }

        /// <summary>
        /// Metoda za risanje, ki uporablja vnaprej izračunane pozicije.
        /// </summary>
        private void pnlPrikaz_Paint(object sender, PaintEventArgs e)
        {
            var root = drevo.DobiKoren();
            if (root == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (pozicije.Count == 0) LayoutTree(); // za vsak slučaj

            DrawEdges(e.Graphics, root);
            DrawNodes(e.Graphics, root);
        }


        /// <summary>
        /// Metoda vrača "širino" poddrevesa vozlišča v.
        /// </summary>
        private int ComputeWidths(Vozlisce v)
        {
            if (v == null) return 0;
            int sirinaLevo = ComputeWidths(v.Levo);
            int sirinaDesno = ComputeWidths(v.Desno);
            int sirinaPoddrevesa = Math.Max(1, sirinaLevo + sirinaDesno); // min 1, da list dobi svoj "prostor" in preprečimo deljenje z 0
            sirine[v] = sirinaPoddrevesa;
            return sirinaPoddrevesa;
        }

        /// <summary>
        /// Metoda vrne globino poddrevesa vozlišča v (št. nivojev).
        /// </summary>
        private int Depth(Vozlisce v)
        {
            if (v == null) return 0;
            return 1 + Math.Max(Depth(v.Levo), Depth(v.Desno)); // + 1 zaradi trenutnega vozlišča
        }

        /// <summary>
        /// Metoda razporedi vozlišča drevesa po oknu tako, da se ne prekrivajo. 
        /// Ne vrača nič, ampak le polni slovar "pozicije" s PointF(x,y) za vsako vozlišče.
        /// </summary>
        private void AssignPositions(Vozlisce v, float xLevo, float xDesno, float y)
        {
            if (v == null) return;

            float xSredina = (xLevo + xDesno) / 2f;
            pozicije[v] = new PointF(xSredina, y);

            int sirinaLevo = v.Levo != null ? sirine[v.Levo] : 0;
            int sirinaDesno = v.Desno != null ? sirine[v.Desno] : 0;
            int sirinaSkupaj = Math.Max(1, sirinaLevo + sirinaDesno);

            float razpon = (xDesno - xLevo);
            float razponLevo = sirinaLevo > 0 ? razpon * (sirinaLevo / (float)sirinaSkupaj) : 0f;
            float razponDesno = sirinaDesno > 0 ? razpon * (sirinaDesno / (float)sirinaSkupaj) : 0f;

            // pri enem otroku malo zamaknemo levo oz. desno, da otrok ni točno pod staršem
            const float minOdmik = 10f; // najmanjši odmik v px
            const float maxOdmik = 24f; // največji odmik v px

            if (v.Levo != null && v.Desno == null)
            {
                float odmik = Math.Max(minOdmik, Math.Min(maxOdmik, razponLevo * 0.15f));
                float novoLevo = xLevo;
                float novoDesno = xLevo + razponLevo - 2 * odmik; // sredino potisnemo levo za 'odmik'

                // če bi segment postal preozek, ostanemo pri originalu
                if (novoDesno - novoLevo < 2 * polmerVozlisca + 6)
                {
                    novoLevo = xLevo;
                    novoDesno = xLevo + razponLevo;
                }

                AssignPositions(v.Levo, novoLevo, novoDesno, y + navpicniRazmik);
                return;
            }
            else if (v.Desno != null && v.Levo == null)
            {
                float odmik = Math.Max(minOdmik, Math.Min(maxOdmik, razponDesno * 0.15f));
                float novoLevo = (xDesno - razponDesno) + 2 * odmik; // sredino potisnemo desno za 'odmik'
                float novoDesno = xDesno;

                if (novoDesno - novoLevo < 2 * polmerVozlisca + 6)
                {
                    novoLevo = xDesno - razponDesno;
                    novoDesno = xDesno;
                }

                AssignPositions(v.Desno, novoLevo, novoDesno, y + navpicniRazmik);
                return;
            }

            // oba otroka ali noben - brez sprememb
            if (v.Levo != null)
                AssignPositions(v.Levo, xLevo, xLevo + razponLevo, y + navpicniRazmik);

            if (v.Desno != null)
                AssignPositions(v.Desno, xDesno - razponDesno, xDesno, y + navpicniRazmik);
        }


        /// <summary>
        /// Izračuna postavitev drevesa glede na velikost panela.
        /// Počisti slovarja 'sirine' in 'pozicije', prilagodi razmike in dodeli (x,y) vsem vozliščem.
        /// </summary>
        private void LayoutTree()
        {
            // počistimo prejšnje izračune postavitve
            sirine.Clear();
            pozicije.Clear();

            var koren = drevo.DobiKoren();
            // če drevo ne obstaja ali je panel premajhen, končamo
            if (koren == null || pnlPrikaz.Width <= 2 * (notranjiRob + polmerVozlisca) || pnlPrikaz.Height <= 2 * (notranjiRob + polmerVozlisca))
                return;

            // vodoravni razpon v pikslih
            float levo = notranjiRob + polmerVozlisca;
            float desno = pnlPrikaz.Width - notranjiRob - polmerVozlisca;

            // globina drevesa (št. nivojev) in navpični razmik po višini panela
            int globina = Math.Max(1, Depth(koren));
            navpicniRazmik = Math.Max(60, (pnlPrikaz.Height - 2 * (notranjiRob + polmerVozlisca)) / globina);

            // širine poddreves
            ComputeWidths(koren);

            // dodelimo pozicije oz. (x,y) koordinate vsem vozliščem
            AssignPositions(koren, levo, desno, notranjiRob + polmerVozlisca);
        }

        /// <summary>
        /// Metoda nariše povezave od vozlišča v do njegovih sinov.
        /// </summary>
        private void DrawEdges(Graphics g, Vozlisce v)
        {
            if (v == null) return;

            var poz = pozicije[v]; // koordinate trenutnega vozlišča

            if (v.Levo != null)
            {
                var pozLevo = pozicije[v.Levo]; // koordinate levega sina
                g.DrawLine(Pens.Black, poz, pozLevo); // nariše črto od očeta do levega sina
                DrawEdges(g, v.Levo);
            }
            if (v.Desno != null)
            {
                var pozDesno = pozicije[v.Desno];
                g.DrawLine(Pens.Black, poz, pozDesno);
                DrawEdges(g, v.Desno);
            }
        }

        /// <summary>
        /// Metoda na okno nariše vozlišče v (krog + podatek) in rekurzivno nadaljuje po poddrevesih.
        /// </summary>
        private void DrawNodes(Graphics g, Vozlisce v)
        {
            if (v == null) return;

            var poz = pozicije[v];

            Brush barvaVozlisca = Brushes.LightBlue;
            Pen robVozlisca = Pens.Black;

            if (oznacenoVozlisce.HasValue && v.Podatek == oznacenoVozlisce.Value) // če je "označeno" vozlišče, spremenimo barvo in rob 
            {
                barvaVozlisca = barvaOznacenega;
                robVozlisca = new Pen(Color.Black, 2f);
            }

            g.FillEllipse(barvaVozlisca, poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca);
            g.DrawEllipse(robVozlisca, poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca);

            using (var font = new Font("Segoe UI", 10))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }; // centriranje besedila
                g.DrawString(v.Podatek.ToString(), font, Brushes.Black,
                    new RectangleF(poz.X - polmerVozlisca, poz.Y - polmerVozlisca, 2 * polmerVozlisca, 2 * polmerVozlisca), sf);
            }

            DrawNodes(g, v.Levo);
            DrawNodes(g, v.Desno);
        }

    }
}
