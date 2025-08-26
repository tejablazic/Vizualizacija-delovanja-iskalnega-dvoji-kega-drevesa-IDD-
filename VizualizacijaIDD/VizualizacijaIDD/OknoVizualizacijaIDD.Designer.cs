namespace VizualizacijaIDD
{
    partial class OknoVizualizacijaIDD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnUstvari = new System.Windows.Forms.Button();
            this.tbxUstvari = new System.Windows.Forms.TextBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.tbxDodaj = new System.Windows.Forms.TextBox();
            this.btnOdstrani = new System.Windows.Forms.Button();
            this.tbxOdstrani = new System.Windows.Forms.TextBox();
            this.btnIsci = new System.Windows.Forms.Button();
            this.tbxIsci = new System.Windows.Forms.TextBox();
            this.btnPonastavi = new System.Windows.Forms.Button();
            this.pnlPrikaz = new System.Windows.Forms.Panel();
            this.btnNaprej = new System.Windows.Forms.Button();
            this.btnNazaj = new System.Windows.Forms.Button();
            this.lblHitrost = new System.Windows.Forms.Label();
            this.rdbPol = new System.Windows.Forms.RadioButton();
            this.rdbEna = new System.Windows.Forms.RadioButton();
            this.rdbRocno = new System.Windows.Forms.RadioButton();
            this.rdbEnaInPol = new System.Windows.Forms.RadioButton();
            this.lblRazlaga = new System.Windows.Forms.Label();
            this.animacijaTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnUstvari
            // 
            this.btnUstvari.Location = new System.Drawing.Point(102, 10);
            this.btnUstvari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUstvari.Name = "btnUstvari";
            this.btnUstvari.Size = new System.Drawing.Size(56, 19);
            this.btnUstvari.TabIndex = 3;
            this.btnUstvari.Text = "Ustvari";
            this.btnUstvari.UseVisualStyleBackColor = true;
            this.btnUstvari.Click += new System.EventHandler(this.btnUstvari_Click);
            // 
            // tbxUstvari
            // 
            this.tbxUstvari.Location = new System.Drawing.Point(9, 10);
            this.tbxUstvari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxUstvari.Name = "tbxUstvari";
            this.tbxUstvari.Size = new System.Drawing.Size(76, 20);
            this.tbxUstvari.TabIndex = 2;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(102, 32);
            this.btnDodaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(56, 19);
            this.btnDodaj.TabIndex = 5;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // tbxDodaj
            // 
            this.tbxDodaj.Location = new System.Drawing.Point(9, 32);
            this.tbxDodaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDodaj.Name = "tbxDodaj";
            this.tbxDodaj.Size = new System.Drawing.Size(76, 20);
            this.tbxDodaj.TabIndex = 4;
            // 
            // btnOdstrani
            // 
            this.btnOdstrani.Location = new System.Drawing.Point(102, 55);
            this.btnOdstrani.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOdstrani.Name = "btnOdstrani";
            this.btnOdstrani.Size = new System.Drawing.Size(56, 19);
            this.btnOdstrani.TabIndex = 7;
            this.btnOdstrani.Text = "Odstrani";
            this.btnOdstrani.UseVisualStyleBackColor = true;
            this.btnOdstrani.Click += new System.EventHandler(this.btnOdstrani_Click);
            // 
            // tbxOdstrani
            // 
            this.tbxOdstrani.Location = new System.Drawing.Point(9, 55);
            this.tbxOdstrani.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxOdstrani.Name = "tbxOdstrani";
            this.tbxOdstrani.Size = new System.Drawing.Size(76, 20);
            this.tbxOdstrani.TabIndex = 6;
            // 
            // btnIsci
            // 
            this.btnIsci.Location = new System.Drawing.Point(102, 78);
            this.btnIsci.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnIsci.Name = "btnIsci";
            this.btnIsci.Size = new System.Drawing.Size(56, 19);
            this.btnIsci.TabIndex = 9;
            this.btnIsci.Text = "Išči";
            this.btnIsci.UseVisualStyleBackColor = true;
            this.btnIsci.Click += new System.EventHandler(this.btnIsci_Click);
            // 
            // tbxIsci
            // 
            this.tbxIsci.Location = new System.Drawing.Point(9, 78);
            this.tbxIsci.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxIsci.Name = "tbxIsci";
            this.tbxIsci.Size = new System.Drawing.Size(76, 20);
            this.tbxIsci.TabIndex = 8;
            // 
            // btnPonastavi
            // 
            this.btnPonastavi.Location = new System.Drawing.Point(70, 337);
            this.btnPonastavi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPonastavi.Name = "btnPonastavi";
            this.btnPonastavi.Size = new System.Drawing.Size(66, 19);
            this.btnPonastavi.TabIndex = 10;
            this.btnPonastavi.Text = "Ponastavi";
            this.btnPonastavi.UseVisualStyleBackColor = true;
            this.btnPonastavi.Click += new System.EventHandler(this.btnPonastavi_Click);
            // 
            // pnlPrikaz
            // 
            this.pnlPrikaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlPrikaz.Location = new System.Drawing.Point(178, 9);
            this.pnlPrikaz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlPrikaz.Name = "pnlPrikaz";
            this.pnlPrikaz.Size = new System.Drawing.Size(413, 348);
            this.pnlPrikaz.TabIndex = 11;
            this.pnlPrikaz.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrikaz_Paint);
            // 
            // btnNaprej
            // 
            this.btnNaprej.Location = new System.Drawing.Point(101, 314);
            this.btnNaprej.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNaprej.Name = "btnNaprej";
            this.btnNaprej.Size = new System.Drawing.Size(56, 19);
            this.btnNaprej.TabIndex = 12;
            this.btnNaprej.Text = "Naprej";
            this.btnNaprej.UseVisualStyleBackColor = true;
            // 
            // btnNazaj
            // 
            this.btnNazaj.Location = new System.Drawing.Point(40, 314);
            this.btnNazaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNazaj.Name = "btnNazaj";
            this.btnNazaj.Size = new System.Drawing.Size(56, 19);
            this.btnNazaj.TabIndex = 13;
            this.btnNazaj.Text = "Nazaj";
            this.btnNazaj.UseVisualStyleBackColor = true;
            // 
            // lblHitrost
            // 
            this.lblHitrost.AutoSize = true;
            this.lblHitrost.Location = new System.Drawing.Point(9, 132);
            this.lblHitrost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHitrost.Name = "lblHitrost";
            this.lblHitrost.Size = new System.Drawing.Size(66, 13);
            this.lblHitrost.TabIndex = 14;
            this.lblHitrost.Text = "Izberi hitrost:";
            // 
            // rdbPol
            // 
            this.rdbPol.AutoSize = true;
            this.rdbPol.Location = new System.Drawing.Point(74, 128);
            this.rdbPol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbPol.Name = "rdbPol";
            this.rdbPol.Size = new System.Drawing.Size(45, 17);
            this.rdbPol.TabIndex = 15;
            this.rdbPol.TabStop = true;
            this.rdbPol.Text = "0.5x";
            this.rdbPol.UseVisualStyleBackColor = true;
            // 
            // rdbEna
            // 
            this.rdbEna.AutoSize = true;
            this.rdbEna.Location = new System.Drawing.Point(74, 150);
            this.rdbEna.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbEna.Name = "rdbEna";
            this.rdbEna.Size = new System.Drawing.Size(36, 17);
            this.rdbEna.TabIndex = 16;
            this.rdbEna.TabStop = true;
            this.rdbEna.Text = "1x";
            this.rdbEna.UseVisualStyleBackColor = true;
            // 
            // rdbRocno
            // 
            this.rdbRocno.AutoSize = true;
            this.rdbRocno.Location = new System.Drawing.Point(74, 192);
            this.rdbRocno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbRocno.Name = "rdbRocno";
            this.rdbRocno.Size = new System.Drawing.Size(106, 17);
            this.rdbRocno.TabIndex = 17;
            this.rdbRocno.TabStop = true;
            this.rdbRocno.Text = "ročno premikanje";
            this.rdbRocno.UseVisualStyleBackColor = true;
            // 
            // rdbEnaInPol
            // 
            this.rdbEnaInPol.AutoSize = true;
            this.rdbEnaInPol.Location = new System.Drawing.Point(74, 171);
            this.rdbEnaInPol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbEnaInPol.Name = "rdbEnaInPol";
            this.rdbEnaInPol.Size = new System.Drawing.Size(45, 17);
            this.rdbEnaInPol.TabIndex = 18;
            this.rdbEnaInPol.TabStop = true;
            this.rdbEnaInPol.Text = "1.5x";
            this.rdbEnaInPol.UseVisualStyleBackColor = true;
            // 
            // lblRazlaga
            // 
            this.lblRazlaga.AutoSize = true;
            this.lblRazlaga.Location = new System.Drawing.Point(7, 250);
            this.lblRazlaga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRazlaga.Name = "lblRazlaga";
            this.lblRazlaga.Size = new System.Drawing.Size(0, 13);
            this.lblRazlaga.TabIndex = 20;
            // 
            // OknoVizualizacijaIDD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.lblRazlaga);
            this.Controls.Add(this.rdbEnaInPol);
            this.Controls.Add(this.rdbRocno);
            this.Controls.Add(this.rdbEna);
            this.Controls.Add(this.rdbPol);
            this.Controls.Add(this.lblHitrost);
            this.Controls.Add(this.btnNazaj);
            this.Controls.Add(this.btnNaprej);
            this.Controls.Add(this.pnlPrikaz);
            this.Controls.Add(this.btnPonastavi);
            this.Controls.Add(this.btnIsci);
            this.Controls.Add(this.tbxIsci);
            this.Controls.Add(this.btnOdstrani);
            this.Controls.Add(this.tbxOdstrani);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.tbxDodaj);
            this.Controls.Add(this.btnUstvari);
            this.Controls.Add(this.tbxUstvari);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OknoVizualizacijaIDD";
            this.Text = "Vizualizacija IDD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUstvari;
        private System.Windows.Forms.TextBox tbxUstvari;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.TextBox tbxDodaj;
        private System.Windows.Forms.Button btnOdstrani;
        private System.Windows.Forms.TextBox tbxOdstrani;
        private System.Windows.Forms.Button btnIsci;
        private System.Windows.Forms.TextBox tbxIsci;
        private System.Windows.Forms.Button btnPonastavi;
        private System.Windows.Forms.Panel pnlPrikaz;
        private System.Windows.Forms.Button btnNaprej;
        private System.Windows.Forms.Button btnNazaj;
        private System.Windows.Forms.Label lblHitrost;
        private System.Windows.Forms.RadioButton rdbPol;
        private System.Windows.Forms.RadioButton rdbEna;
        private System.Windows.Forms.RadioButton rdbRocno;
        private System.Windows.Forms.RadioButton rdbEnaInPol;
        private System.Windows.Forms.Label lblRazlaga;
        private System.Windows.Forms.Timer animacijaTimer;
    }
}

