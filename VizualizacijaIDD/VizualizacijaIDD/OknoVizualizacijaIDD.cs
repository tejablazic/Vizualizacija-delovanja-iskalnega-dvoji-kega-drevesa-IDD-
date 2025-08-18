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
        private IDD osnovnoDrevo;
        private IDDKoraki drevo;
        private int? oznacenoVozlisce = null;
        private Brush barvaOznacenega = Brushes.Violet;

        int trenutniKorak = 0; // indeks trenutne vrstice v seznamu Razlaga // SE ŠE NE POVEČUJE
        // Timer casovnik = new Timer();

        public OknoVizualizacijaIDD()
        {
            InitializeComponent();

            // Ustvari osnovno drevo
            osnovnoDrevo = new IDD();

            // Ustvari IDDKoraki na podlagi osnovnega drevesa
            drevo = new IDDKoraki(osnovnoDrevo);
        }

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
                trenutniKorak = 0;
                PrikaziKorak(); // prikažemo prvi korak
                
            }
            tbxUstvari.Clear();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxDodaj.Text, out int v))
            {
                drevo.VstaviZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;
                trenutniKorak = 0;
                PrikaziKorak(); // prikažemo prvi korak
                tbxDodaj.Clear();
            }
        }

        private void btnOdstrani_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdstrani.Text, out int v))
            {
                drevo.BrisiZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;
                trenutniKorak = 0;
                PrikaziKorak();
                tbxOdstrani.Clear();
            }
        }

        private void btnIsci_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxIsci.Text, out int v))
            {
                drevo.IskanjeZKoraki(v);
                oznacenoVozlisce = v;
                barvaOznacenega = Brushes.Violet;
                trenutniKorak = 0;
                PrikaziKorak();
                tbxIsci.Clear();
            }
        }

        private void btnPonastavi_Click(object sender, EventArgs e)
        {
            osnovnoDrevo = new IDD();
            drevo = new IDDKoraki(osnovnoDrevo);
            drevo.koraki = new List<Korak>();

            oznacenoVozlisce = null;
            barvaOznacenega = Brushes.Violet;

            trenutniKorak = 0;
            pnlPrikaz.Invalidate();
        }

        private void PrikaziKorak()
        {
            if (drevo.koraki.Count == 0)
                return;

            if (trenutniKorak < drevo.koraki.Count)
            {
                lblRazlaga.Text = drevo.koraki[trenutniKorak].ToString();
                pnlPrikaz.Invalidate();
            }
        }

        private void pnlPrikaz_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (drevo.DobiKoren() != null)
            {
                // NarisiDrevo(e.Graphics, drevo.Koren, pnlPrikaz.Width / 2, 30, pnlPrikaz.Width / 4);
                int offset = pnlPrikaz.Width / 10; // približno 1/10 širine panela
                NarisiDrevo(e.Graphics, drevo.Koren, (pnlPrikaz.Width / 2) + offset, 30, pnlPrikaz.Width / 4);
            }
            */

            if (drevo.DobiKoren() == null) return;
    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

    int margin = 30;
    int offset = pnlPrikaz.Width / 10;    // “malo desno”
    int center = (pnlPrikaz.Width / 2) + offset;

    // kolikšen dx še dopušča, da center + 2*dx ostane v panelu
    int maxDxRight = (pnlPrikaz.Width - margin - center) / 2;
    int dx = Math.Min(pnlPrikaz.Width / 4, Math.Max(40, maxDxRight));  // ne manj kot 40

    NarisiDrevo(e.Graphics, drevo.Koren, center, 30, dx);
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

            // Barva vozlišč
            Brush barvaVozlisc = Brushes.LightBlue;
            Pen rob = Pens.Black;

            if (oznacenoVozlisce.HasValue && v.Podatek == oznacenoVozlisce.Value)
            {
                barvaVozlisc = barvaOznacenega;
                rob = new Pen(Color.Black, 2f);
            }

            // Vozlišče
            g.FillEllipse(barvaVozlisc, x - 15, y - 15, 30, 30);
            g.DrawEllipse(rob, x - 15, y - 15, 30, 30);

            // Centriran zapis v sredini vozlišča
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using (var font = new Font("Arial", 10))
            {
                g.DrawString(v.Podatek.ToString(), font, Brushes.Black,
                             new RectangleF(x - 15, y - 15, 30, 30), sf);
            }

            //g.DrawString(v.Podatek.ToString(), new Font("Arial", 10), Brushes.Black, x - 10, y - 8);
            
            // Rekurzivni klic
            NarisiDrevo(g, v.Levo, x - dx, y + 50, dx / 2);
            NarisiDrevo(g, v.Desno, x + dx, y + 50, dx / 2);
        }

        
    }

    
}
