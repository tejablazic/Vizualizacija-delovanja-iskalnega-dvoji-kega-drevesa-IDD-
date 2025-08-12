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
            // MANJKA
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxDodaj.Text, out int v))
            {
                drevo.VstaviZKoraki(v);
                trenutniKorak = 0;
                PrikaziKorak(); // prikažemo prvi korak
            }
        }

        private void btnOdstrani_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdstrani.Text, out int v))
            {
                drevo.BrisiZKoraki(v);
                trenutniKorak = 0;
                PrikaziKorak();
            }
        }

        private void btnIsci_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxIsci.Text, out int v))
            {
                drevo.IskanjeZKoraki(v);
                trenutniKorak = 0;
                PrikaziKorak();
            }
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
            if (drevo.DobiKoren() != null)
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
            g.DrawString(v.Podatek.ToString(), new Font("Arial", 10), Brushes.Black, x - 10, y - 8);
            NarisiDrevo(g, v.Levo, x - dx, y + 50, dx / 2);
            NarisiDrevo(g, v.Desno, x + dx, y + 50, dx / 2);
        }

    }

    
}
