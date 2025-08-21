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
            glavno.FormClosed += Glavno_FormClosed;
            glavno.Show();
            Hide(); // skrijemo začetno okno
        }

        private void Glavno_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close(); // ko zapremo glavno okno, zapremo tudi začetno - program se konča
        }
    }
}
