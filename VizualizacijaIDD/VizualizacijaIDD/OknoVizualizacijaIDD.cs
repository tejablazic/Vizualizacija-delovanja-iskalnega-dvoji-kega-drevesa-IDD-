using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VizualizacijaIDD
{
    public partial class OknoVizualizacijaIDD : Form
    {
        IDD drevo = new IDD();
        int trenutniKorak = 0; // indeks trenutne vrstice v seznamu Razlaga // SE ŠE NE POVEČUJE
        // Timer casovnik = new Timer();

        public OknoVizualizacijaIDD()
        {
            InitializeComponent();
        }

        private void btnUstvari_Click(object sender, EventArgs e)
        {
            // MANJKA
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxDodaj.Text, out int v))
            {
                drevo.Vstavi(v);
                trenutniKorak = 0;
                PrikaziKorak(); // prikažemo prvi korak
            }
        }

        private void btnOdstrani_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdstrani.Text, out int v))
            {
                drevo.Odstrani(v);
                trenutniKorak = 0;
                PrikaziKorak();
            }
        }

        private void btnIsci_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxIsci.Text, out int v))
            {
                drevo.Isci(v);
                trenutniKorak = 0;
                PrikaziKorak();
            }
        }

        private void PrikaziKorak()
        {
            if (drevo.Razlaga.Count == 0)
                return;

            if (trenutniKorak < drevo.Razlaga.Count)
            {
                lblRazlaga.Text = drevo.Razlaga[trenutniKorak];
                pnlPrikaz.Invalidate();
            }
        }

        private void pnlPrikaz_Paint(object sender, PaintEventArgs e)
        {
            if (drevo.Koren != null)
            {
                NarisiDrevo(e.Graphics, drevo.Koren, pnlPrikaz.Width / 2, 30, pnlPrikaz.Width / 4);
            }
        }

        public void NarisiDrevo(Graphics g, Vozlisce v, int x, int y, int dx)
        {
            if (v == null) 
                return;

            // Povezave
            if (v.Levo != null)
                g.DrawLine(Pens.Black, x, y, x - dx, y + 50);
            if (v.Desno != null)
                g.DrawLine(Pens.Black, x, y, x + dx, y + 50);

            // Vozlišče
            g.FillEllipse(Brushes.LightBlue, x - 15, y - 15, 30, 30);
            g.DrawEllipse(Pens.Black, x - 15, y - 15, 30, 30);
            g.DrawString(v.Vrednost.ToString(), new Font("Arial", 10), Brushes.Black, x - 10, y - 8);
            NarisiDrevo(g, v.Levo, x - dx, y + 50, dx / 2);
            NarisiDrevo(g, v.Desno, x + dx, y + 50, dx / 2);
        }

    }


    public class Vozlisce
    {
        public int Vrednost;
        public Vozlisce Levo;
        public Vozlisce Desno;
        public int X;
        public int Y;
    }

    public class IDD
    {
        public Vozlisce Koren;
        public List<string> Razlaga = new List<string>(); // vsebuje nize za razlago korakov

        /// <summary>
        /// Glavna funkcija za vstavljanje vrednosti v drevo, ki kliče pomožno funkcijo.
        /// </summary>
        public void Vstavi(int vrednost)
        {
            Razlaga.Clear();
            Koren = VstaviPomozna(Koren, vrednost);
        }

        /// <summary>
        /// Vstavi vrednost na ustrezno mesto v drevo s pomočjo rekurzije.
        /// </summary>
        private Vozlisce VstaviPomozna(Vozlisce trenutno, int vrednost)
        {
            int trenutnaVrednost = (trenutno != null) ? trenutno.Vrednost : -1; // če trenutno ni null, vzamemo trenutno.Vrednost, sicer -1
            if (trenutno == null)
            {
                Razlaga.Add($"Dodajam vozlišče z vrednostjo {vrednost}.");
                return new Vozlisce { Vrednost = vrednost };
            }

            if (vrednost == trenutno.Vrednost)
            {
                Razlaga.Add("Vozlišče s to vrednostjo že obstaja!");
                return trenutno;
            }

            if (vrednost < trenutno.Vrednost)
                trenutno.Levo = VstaviPomozna(trenutno.Levo, vrednost);
            else
                trenutno.Desno = VstaviPomozna(trenutno.Desno, vrednost);

            return trenutno;
        }

        /// <summary>
        /// Glavna funkcija za iskanje vrednosti v drevesu, ki kliče pomožno funkcijo.
        /// </summary>
        public bool Isci(int vrednost)
        {
            Razlaga.Clear();
            return IsciPomozna(Koren, vrednost);
        }

        /// <summary>
        /// Išče vrednost v drevesu s pomočjo rekurzije.
        /// </summary>
        private bool IsciPomozna(Vozlisce trenutno, int vrednost)
        {
            if (trenutno == null)
            {
                Razlaga.Add($"Vozlišče z vrednostjo {vrednost} ni bilo najdeno.");
                return false;
            }

            Razlaga.Add($"Iščem vozlišče z vrednostjo {trenutno.Vrednost}");

            if (vrednost == trenutno.Vrednost)
            {
                Razlaga.Add($"Vozlišče z vrednostjo {vrednost} je bilo najdeno.");
                return true;
            }

            if (vrednost < trenutno.Vrednost)
            {
                Razlaga.Add($"Ker je {vrednost} < {trenutno.Vrednost}, gremo levo.");
                return IsciPomozna(trenutno.Levo, vrednost);
            }

            else
            {
                Razlaga.Add($"Ker je {vrednost} > {trenutno.Vrednost}, gremo desno.");
                return IsciPomozna(trenutno.Desno, vrednost);
            }
        }

        /// <summary>
        /// Glavna funkcija za brisanje vrednosti iz drevesa, ki kliče pomožno funkcijo.
        /// </summary>
        public void Odstrani(int vrednost)
        {
            Razlaga.Clear();
            Koren = OdstraniPomozna(Koren, vrednost);
        }

        /// <summary>
        /// Odstrani vrednost iz drevesa s pomočjo rekurzije.
        /// </summary>
        private Vozlisce OdstraniPomozna(Vozlisce trenutno, int vrednost)
        {
            if (trenutno == null)
            {
                Razlaga.Add($"Vozlišča z vrednostjo {vrednost} ne morem odstraniti, ker ne obstaja!");
                return null;
            }

            Razlaga.Add($"Brišem vozlišče z vrednostjo {trenutno.Vrednost}.");

            if (vrednost < trenutno.Vrednost)
            {
                Razlaga.Add($"Ker je {vrednost} < {trenutno.Vrednost}, gremo levo.");
                trenutno.Levo = OdstraniPomozna(trenutno.Levo, vrednost);
            }

            else if (vrednost > trenutno.Vrednost)
            {
                Razlaga.Add($"Ker je {vrednost} > {trenutno.Vrednost}, gremo desno.");
                trenutno.Desno = OdstraniPomozna(trenutno.Desno, vrednost);
            }

            else 
            {
                Razlaga.Add($"Vozlišče z vrednostjo {vrednost} je najdeno. Lahko ga izbrišemo.");

                // vozlišče brez sinov
                if (trenutno.Levo == null && trenutno.Desno == null)
                {
                    Razlaga.Add("Vozlišče nima sinov, zato ne rabimo nič popravljati.");
                    return null;
                }

                // vozlišče z enim sinom
                if (trenutno.Desno == null) // levi sin
                {
                    Razlaga.Add("Vozlišče ima samo levega sina. Premaknemo ga navzgor.");
                    return trenutno.Levo;
                }

                if (trenutno.Levo == null) // desni sin
                {
                    Razlaga.Add("Vozlišče ima samo desnega sina. Premaknemo ga navzgor.");
                    return trenutno.Desno;
                }

                // vozlišče z dvema sinovoma
                Vozlisce naslednik = NajmanjsiDesno(trenutno.Desno);
                Razlaga.Add($"Vozlišče ima dva sinova. Zamenjamo ga z naslednikom ({naslednik.Vrednost}).");
                trenutno.Vrednost = naslednik.Vrednost;
                trenutno.Desno = OdstraniPomozna(trenutno.Desno, naslednik.Vrednost); // zbrišemo naslednika iz desnega poddrevesa

            }
            return trenutno;
        }

        /// <summary>
        /// Poišče najmanjšo vrednost v desnem poddrevesu.
        /// </summary>
        private Vozlisce NajmanjsiDesno(Vozlisce vozlisce)
        {
            while (vozlisce.Levo != null) // gremo levo, kjer so manjši elementi
            {
                vozlisce = vozlisce.Levo;
            }
            return vozlisce;
        }

    }
}
