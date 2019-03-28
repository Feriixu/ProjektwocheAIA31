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
    public partial class KursLoeschenDialog : Form
    {
        public DialogResult result { set; get; }
        public string KursName;

        public KursLoeschenDialog(string kursName)
        {
            InitializeComponent();
            this.KursName = kursName;
            result = new DialogResult();
            result = DialogResult.Cancel;
            labelKursName.Text = "\"" + KursName + "\"";
        }

        private void buttonLoeschen_Click(object sender, EventArgs e)
        {
            if (textBoxEingabe.Text == this.KursName)
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
            MessageBox.Show("Kurs wurde nicht gelöscht.");
            result = DialogResult.Cancel;
            this.Close();
        }
    }
}
