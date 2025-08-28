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
            this.rb0_5 = new System.Windows.Forms.RadioButton();
            this.rb1x = new System.Windows.Forms.RadioButton();
            this.rbRocno = new System.Windows.Forms.RadioButton();
            this.rb1_5x = new System.Windows.Forms.RadioButton();
            this.lblRazlaga = new System.Windows.Forms.Label();
            this.animacijaTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnUstvari
            // 
            this.btnUstvari.Location = new System.Drawing.Point(124, 9);
            this.btnUstvari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUstvari.Name = "btnUstvari";
            this.btnUstvari.Size = new System.Drawing.Size(78, 36);
            this.btnUstvari.TabIndex = 3;
            this.btnUstvari.Text = "Ustvari";
            this.btnUstvari.UseVisualStyleBackColor = true;
            this.btnUstvari.Click += new System.EventHandler(this.btnUstvari_Click);
            // 
            // tbxUstvari
            // 
            this.tbxUstvari.Location = new System.Drawing.Point(22, 18);
            this.tbxUstvari.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxUstvari.Name = "tbxUstvari";
            this.tbxUstvari.Size = new System.Drawing.Size(98, 20);
            this.tbxUstvari.TabIndex = 2;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(124, 50);
            this.btnDodaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(78, 36);
            this.btnDodaj.TabIndex = 5;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // tbxDodaj
            // 
            this.tbxDodaj.Location = new System.Drawing.Point(22, 59);
            this.tbxDodaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDodaj.Name = "tbxDodaj";
            this.tbxDodaj.Size = new System.Drawing.Size(98, 20);
            this.tbxDodaj.TabIndex = 4;
            // 
            // btnOdstrani
            // 
            this.btnOdstrani.Location = new System.Drawing.Point(124, 93);
            this.btnOdstrani.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOdstrani.Name = "btnOdstrani";
            this.btnOdstrani.Size = new System.Drawing.Size(78, 36);
            this.btnOdstrani.TabIndex = 7;
            this.btnOdstrani.Text = "Odstrani";
            this.btnOdstrani.UseVisualStyleBackColor = true;
            this.btnOdstrani.Click += new System.EventHandler(this.btnOdstrani_Click);
            // 
            // tbxOdstrani
            // 
            this.tbxOdstrani.Location = new System.Drawing.Point(22, 102);
            this.tbxOdstrani.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxOdstrani.Name = "tbxOdstrani";
            this.tbxOdstrani.Size = new System.Drawing.Size(98, 20);
            this.tbxOdstrani.TabIndex = 6;
            // 
            // btnIsci
            // 
            this.btnIsci.Location = new System.Drawing.Point(124, 136);
            this.btnIsci.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnIsci.Name = "btnIsci";
            this.btnIsci.Size = new System.Drawing.Size(78, 36);
            this.btnIsci.TabIndex = 9;
            this.btnIsci.Text = "Išči";
            this.btnIsci.UseVisualStyleBackColor = true;
            this.btnIsci.Click += new System.EventHandler(this.btnIsci_Click);
            // 
            // tbxIsci
            // 
            this.tbxIsci.Location = new System.Drawing.Point(22, 145);
            this.tbxIsci.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxIsci.Name = "tbxIsci";
            this.tbxIsci.Size = new System.Drawing.Size(98, 20);
            this.tbxIsci.TabIndex = 8;
            // 
            // btnPonastavi
            // 
            this.btnPonastavi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPonastavi.Location = new System.Drawing.Point(22, 399);
            this.btnPonastavi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPonastavi.Name = "btnPonastavi";
            this.btnPonastavi.Size = new System.Drawing.Size(101, 43);
            this.btnPonastavi.TabIndex = 10;
            this.btnPonastavi.Text = "Ponastavi";
            this.btnPonastavi.UseVisualStyleBackColor = true;
            this.btnPonastavi.Click += new System.EventHandler(this.btnPonastavi_Click);
            // 
            // pnlPrikaz
            // 
            this.pnlPrikaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlPrikaz.Location = new System.Drawing.Point(223, 9);
            this.pnlPrikaz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlPrikaz.Name = "pnlPrikaz";
            this.pnlPrikaz.Size = new System.Drawing.Size(413, 427);
            this.pnlPrikaz.TabIndex = 11;
            this.pnlPrikaz.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrikaz_Paint);
            // 
            // btnNaprej
            // 
            this.btnNaprej.Location = new System.Drawing.Point(135, 292);
            this.btnNaprej.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNaprej.Name = "btnNaprej";
            this.btnNaprej.Size = new System.Drawing.Size(56, 26);
            this.btnNaprej.TabIndex = 12;
            this.btnNaprej.Text = "Naprej";
            this.btnNaprej.UseVisualStyleBackColor = true;
            this.btnNaprej.Click += new System.EventHandler(this.btnNaprej_Click);
            // 
            // btnNazaj
            // 
            this.btnNazaj.Location = new System.Drawing.Point(74, 292);
            this.btnNazaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNazaj.Name = "btnNazaj";
            this.btnNazaj.Size = new System.Drawing.Size(56, 26);
            this.btnNazaj.TabIndex = 13;
            this.btnNazaj.Text = "Nazaj";
            this.btnNazaj.UseVisualStyleBackColor = true;
            this.btnNazaj.Click += new System.EventHandler(this.btnNazaj_Click);
            // 
            // lblHitrost
            // 
            this.lblHitrost.AutoSize = true;
            this.lblHitrost.Location = new System.Drawing.Point(20, 211);
            this.lblHitrost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHitrost.Name = "lblHitrost";
            this.lblHitrost.Size = new System.Drawing.Size(66, 13);
            this.lblHitrost.TabIndex = 14;
            this.lblHitrost.Text = "Izberi hitrost:";
            // 
            // rb0_5
            // 
            this.rb0_5.AutoSize = true;
            this.rb0_5.Location = new System.Drawing.Point(85, 207);
            this.rb0_5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb0_5.Name = "rb0_5";
            this.rb0_5.Size = new System.Drawing.Size(45, 17);
            this.rb0_5.TabIndex = 15;
            this.rb0_5.TabStop = true;
            this.rb0_5.Text = "0.5x";
            this.rb0_5.UseVisualStyleBackColor = true;
            this.rb0_5.CheckedChanged += new System.EventHandler(this.rb0_5x_CheckedChanged);
            // 
            // rb1x
            // 
            this.rb1x.AutoSize = true;
            this.rb1x.Location = new System.Drawing.Point(85, 229);
            this.rb1x.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1x.Name = "rb1x";
            this.rb1x.Size = new System.Drawing.Size(36, 17);
            this.rb1x.TabIndex = 16;
            this.rb1x.TabStop = true;
            this.rb1x.Text = "1x";
            this.rb1x.UseVisualStyleBackColor = true;
            this.rb1x.CheckedChanged += new System.EventHandler(this.rb1x_CheckedChanged);
            // 
            // rbRocno
            // 
            this.rbRocno.AutoSize = true;
            this.rbRocno.Location = new System.Drawing.Point(85, 271);
            this.rbRocno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbRocno.Name = "rbRocno";
            this.rbRocno.Size = new System.Drawing.Size(106, 17);
            this.rbRocno.TabIndex = 17;
            this.rbRocno.TabStop = true;
            this.rbRocno.Text = "ročno premikanje";
            this.rbRocno.UseVisualStyleBackColor = true;
            this.rbRocno.CheckedChanged += new System.EventHandler(this.rbRocno_CheckedChanged);
            // 
            // rb1_5x
            // 
            this.rb1_5x.AutoSize = true;
            this.rb1_5x.Location = new System.Drawing.Point(85, 250);
            this.rb1_5x.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1_5x.Name = "rb1_5x";
            this.rb1_5x.Size = new System.Drawing.Size(45, 17);
            this.rb1_5x.TabIndex = 18;
            this.rb1_5x.TabStop = true;
            this.rb1_5x.Text = "1.5x";
            this.rb1_5x.UseVisualStyleBackColor = true;
            this.rb1_5x.CheckedChanged += new System.EventHandler(this.rb1_5x_CheckedChanged);
            // 
            // lblRazlaga
            // 
            this.lblRazlaga.AutoSize = true;
            this.lblRazlaga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazlaga.Location = new System.Drawing.Point(24, 344);
            this.lblRazlaga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRazlaga.Name = "lblRazlaga";
            this.lblRazlaga.Size = new System.Drawing.Size(0, 20);
            this.lblRazlaga.TabIndex = 20;
            // 
            // OknoVizualizacijaIDD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 445);
            this.Controls.Add(this.lblRazlaga);
            this.Controls.Add(this.rb1_5x);
            this.Controls.Add(this.rbRocno);
            this.Controls.Add(this.rb1x);
            this.Controls.Add(this.rb0_5);
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
        private System.Windows.Forms.RadioButton rb0_5;
        private System.Windows.Forms.RadioButton rb1x;
        private System.Windows.Forms.RadioButton rbRocno;
        private System.Windows.Forms.RadioButton rb1_5x;
        private System.Windows.Forms.Label lblRazlaga;
        private System.Windows.Forms.Timer animacijaTimer;
    }
}

