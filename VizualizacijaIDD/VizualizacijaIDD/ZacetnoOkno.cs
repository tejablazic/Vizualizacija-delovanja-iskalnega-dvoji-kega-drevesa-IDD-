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
    public partial class ZacetnoOkno: Form
    {

        public ZacetnoOkno()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var glavno = new OknoVizualizacijaIDD();
            glavno.FormClosed += zapriGlavno; // ko se glavno okno zapre, se izvede metoda zapriOkno
            glavno.Show(); // prikažemo glavno okno
            Hide(); // skrijemo začetno okno
        }

        private void zapriGlavno(object sender, FormClosedEventArgs e)
        {
            Close(); // ko zapremo glavno okno, zapremo tudi začetno, program se konča
        }
    }
}
