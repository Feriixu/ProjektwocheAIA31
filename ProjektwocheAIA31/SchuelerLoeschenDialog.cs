using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektwocheAIA31
{
    public partial class SchuelerLoeschenDialog : Form
    {
        public DialogResult result { set; get; }
        public string Nachname;

        public SchuelerLoeschenDialog(string kursName)
        {
            InitializeComponent();
            this.Nachname = kursName;
            result = new DialogResult();
            result = DialogResult.Cancel;
            labelNachname.Text = "\"" + Nachname + "\"";
        }

        private void buttonLoeschen_Click(object sender, EventArgs e)
        {
            if (textBoxEingabe.Text == this.Nachname)
            {
                result = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Falsche Eingabe");
            }
        }

        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Schueler wurde nicht gelöscht.");
            result = DialogResult.Cancel;
            this.Close();
        }
    }
}
