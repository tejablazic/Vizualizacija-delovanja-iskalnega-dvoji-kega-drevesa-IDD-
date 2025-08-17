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
        private Label lblNaslov;
        private TextBox tbxNavodila;
        private Button btnStart;

        public ZacetnoOkno()
        {
            Text = "Vizualizacija IDD – Navodila";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(640, 360);

            lblNaslov = new Label
            {
                Text = "Dobrodošli v vizualizaciji iskalnega dvojiškega drevesa (IDD)",
                AutoSize = false,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 48,
                TextAlign = ContentAlignment.MiddleCenter
            };

            tbxNavodila = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Text =
@"Navodila:
• V polje 'Ustvari' vnesi seznam števil, ločenih z vejicami (npr. 50,25,75,12,...), nato klikni USTVARI.
• Z gumbi DODAJ, ODSTRANI in IŠČI lahko izvajaš operacije nad drevesom.
• Izbrano (dodano/iskano/brisano) vozlišče se obarva.
• Drevo se prilagaja velikosti okna; povečevalno okno mu pusti več prostora na desni.

Klikni START za začetek."
            };

            btnStart = new Button
            {
                Text = "START",
                Height = 36,
                Dock = DockStyle.Bottom,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnStart.Click += BtnStart_Click;

            Controls.Add(tbxNavodila);
            Controls.Add(btnStart);
            Controls.Add(lblNaslov);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Odpri glavno okno, skrij začetno; ko glavno zapreš, zapri še začetno -> app se zaključi
            this.Hide();
            var glavno = new OknoVizualizacijaIDD
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            glavno.FormClosed += (s, args) => this.Close();
            glavno.Show();
        }
    }
}
