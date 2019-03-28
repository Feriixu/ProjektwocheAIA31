using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjektwocheAIA31
{
    public partial class Dashboard : Form
    {
        MySqlConnection myConnection; //Für die Verbindung mit der DB
        MySqlDataAdapter myAdapter; //Um die Tabellen zu laden
        DataTable myDataTableKurse = new DataTable(); //Hält die Tabelle für Kurse
        DataTable myDataTableSchueler = new DataTable(); //Hält die Tabelle für Kurse

        MySqlDataAdapter zeitplanungDataAdapter;
        MySqlCommandBuilder zeitplanungCommandBuilder;
        DataTable myDataTableZeitplanung = new DataTable(); //Hält die Tabelle für die Zeitplanung

        string selectedIdKurse = ""; //Speichert die Auswahl der Kurse
        string selectedIdSchueler = ""; //Speichert die Auswahl der Kurse

        public Dashboard()
        {
            InitializeComponent();

            zeitplanungDataAdapter = new MySqlDataAdapter()
            {
                SelectCommand = new MySqlCommand("SELECT * FROM zeitplanung;", myConnection)
            };
            zeitplanungCommandBuilder = new MySqlCommandBuilder(zeitplanungDataAdapter);

            myConnection = new MySqlConnection();
            myConnection.StateChange += myConnection_StateChange;
        }

        private void DatenbanktoolFürProjektwoche_Load(object sender, EventArgs e)
        {
            timerRefresh.Enabled = true;
        }
        private void buttonVerbinden_Click(object sender, EventArgs e)
        {
            try
            {
                //Verbindungs String mit Adresse, DB name und Login Daten
                string myConnectionString = string.Format("SERVER={0};" + "DATABASE={1};" + "UID={2};" + "PASSWORD={3}; Convert Zero Datetime=True", textBoxServerAdresse.Text, textBoxServerDBName.Text, textBoxLoginBenutzer.Text, textBoxLoginPasswort.Text);
                myConnection.ConnectionString = myConnectionString;
                myConnection.Open(); //Öffnet die Verbindung

                //Gib eine Message Box aus wenn erfolgreich
                if (myConnection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Verbindung hergestellt!", "MySQL Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unknown Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonTrennen_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection.Close(); //Schließt die Verbindung
                //MessageBox wenn erfolgreich
                if (myConnection.State == ConnectionState.Closed)
                    MessageBox.Show("Connection closed!", "MySQL Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unknown Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshTablesAndCheckConnection()
        {
            if (myConnection != null)
            {
                if (myConnection.State == ConnectionState.Open) //Nur wenn die Verbindung besteht
                {
                    try
                    {
                        #region Kurse Refreshen
                        if (listBoxKurse.SelectedItem != null)
                            selectedIdKurse = ((DataRowView)listBoxKurse.SelectedItem)["workshopID"].ToString(); //Speichert die letzte Auswahl

                        myDataTableKurse = new DataTable(); //Leert die Tabelle
                        myAdapter = new MySqlDataAdapter("SELECT * FROM workshop", myConnection); //Holt die Tabelle
                        myAdapter.Fill(myDataTableKurse); //Füllt die Variable mit der Tabelle
                        
                        listBoxKurse.DataSource = myDataTableKurse; //Setzt die Datenquelle
                        listBoxKurse.DisplayMember = "titel"; //Wählt die anzuzeigenden Daten
                        listBoxKurse.ValueMember = "workshopID"; //Wählt die Daten die als ID benutzt werden sollen

                        for (int i = 0; i < listBoxKurse.Items.Count; i++)
                        {
                            var item = listBoxKurse.Items[i]; //Speichert aktuelles Item
                            if (((DataRowView)item)["workshopID"].ToString() == selectedIdKurse) //Wenn das Item die gleiche ID wie das zuletzt angewählte hat
                            {
                                listBoxKurse.SelectedIndex = i; //Wähle dieses Item aus
                            }
                            else if (i == (listBoxKurse.Items.Count - 1))
                            {
                                //MessageBox.Show("Das ausgewählte Item existiert nicht mehr.");
                            }
                        }

                        #endregion
                        #region Schueler Refreshen
                        if (listBoxSchueler.SelectedItem != null)
                            selectedIdSchueler = ((DataRowView)listBoxSchueler.SelectedItem)["schuelerID"].ToString(); //Speichert die letzte Auswahl

                        myDataTableSchueler = new DataTable(); //Leert die Tabelle
                        myAdapter = new MySqlDataAdapter("SELECT * FROM schueler", myConnection); //Holt die Tabelle
                        myAdapter.Fill(myDataTableSchueler); //Füllt die Variable mit der Tabelle

                        listBoxSchueler.DataSource = myDataTableSchueler; //Setzt die Datenquelle
                        listBoxSchueler.DisplayMember = "nachname"; //Wählt die anzuzeigenden Daten
                        listBoxSchueler.ValueMember = "schuelerID"; //Wählt die Daten die als ID benutzt werden sollen

                        for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                        {
                            var item = listBoxSchueler.Items[i]; //Speichert aktuelles Item
                            if (((DataRowView)item)["schuelerID"].ToString() == selectedIdSchueler) //Wenn das Item die gleiche ID wie das zuletzt angewählte hat
                            {
                                listBoxSchueler.SelectedIndex = i; //Wähle dieses Item aus
                            }
                            else if (i == (listBoxSchueler.Items.Count - 1))
                            {
                                //MessageBox.Show("Das ausgewählte Item existiert nicht mehr.");
                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        myConnection.Dispose();
                        MessageBox.Show(ex.ToString(), "Error While Refreshing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            refreshTablesAndCheckConnection();
        }
        private void myConnection_StateChange(object sender, EventArgs e)
        {
            if (myConnection.State == ConnectionState.Open)
            {
                #region UI veränderungen bei Verbindung
                //UI veränderungen
                buttonVerbinden.Enabled = false;
                buttonTrennen.Enabled = true;
                buttonKursHinzufuegen.Enabled = true;
                buttonKursLoeschen.Enabled = true;
                buttonKursAenderungenUebernehmen.Enabled = true;
                listBoxKurse.Enabled = true;
                buttonSchuelerHinzufuegen.Enabled = true;
                buttonSchuelerLoeschen.Enabled = true;
                buttonSchuelerAenderungenUebernehmen.Enabled = true;
                listBoxSchueler.Enabled = true;
                toolStripStatusLabelVerbindungsStatus.Text = "Verbunden";
                toolStripStatusLabelVerbindungsStatus.ForeColor = Color.DarkGreen;
                toolStripStatusLabelBenutzer.Text = textBoxLoginBenutzer.Text;
                toolStripStatusLabelDatenbank.Text = textBoxServerDBName.Text;
                toolStripStatusLabelAdresse.Text = textBoxServerAdresse.Text;

                #endregion
            }
            else
            {
                #region UI veränderungen bei geschlossener Verbindung
                //UI veränderungen
                buttonVerbinden.Enabled = true;
                buttonTrennen.Enabled = false;
                buttonKursHinzufuegen.Enabled = false;
                buttonKursLoeschen.Enabled = false;
                buttonKursAenderungenUebernehmen.Enabled = false;
                listBoxKurse.Enabled = false;
                buttonSchuelerHinzufuegen.Enabled = false;
                buttonSchuelerLoeschen.Enabled = false;
                buttonSchuelerAenderungenUebernehmen.Enabled = false;
                listBoxSchueler.Enabled = false;
                toolStripStatusLabelVerbindungsStatus.Text = "Getrennt";
                toolStripStatusLabelVerbindungsStatus.ForeColor = Color.Red;
                toolStripStatusLabelBenutzer.Text = "-nicht verbunden-";
                toolStripStatusLabelDatenbank.Text = "-nicht verbunden-";
                toolStripStatusLabelAdresse.Text = "-nicht verbunden-";

                #endregion
            }
        }

        #region Kurs Events
        private void buttonKursHinzufuegen_Click(object sender, EventArgs e)
        {
            MySqlCommand command = myConnection.CreateCommand();
            command.CommandText = "INSERT INTO workshop (titel) VALUES ('" + textBoxNeuerKursname.Text + "');";
            command.ExecuteNonQuery();
        }

        private void buttonKursLoeschen_Click(object sender, EventArgs e)
        {
            KursLoeschenDialog dialog = new KursLoeschenDialog(((DataRowView)listBoxKurse.SelectedItem)["titel"].ToString());
            dialog.ShowDialog();
            if (dialog.result == DialogResult.OK)
            {
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = "DELETE FROM workshop WHERE workshopID = " + ((DataRowView)listBoxKurse.SelectedItem)["workshopID"].ToString() + ";";
                command.ExecuteNonQuery();
            }
        }

        private void buttonKursAenderungenUebernehmen_Click(object sender, EventArgs e)
        {
            timerRefresh.Stop();

            DialogResult result = MessageBox.Show("Sind sie sich sicher, dass sie die Änderungen unwiderruflich speichern wollen?", "Speichern", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = "UPDATE workshop SET " +
                    "nr = '" + textBoxKursNr.Text + "', " +
                    "titel = '" + textBoxKursTitel.Text + "', " +
                    "kosten = '" + textBoxKursKosten.Text + "', " +
                    "beschreibung = '" + richTextBoxKursBeschreibung.Text + "', " +
                    "voraussetzungen = '" + textBoxKursVorraussetzungen.Text + "', " +
                    "teilnehmerMax = '" + textBoxKursMax.Text + "', " +
                    "teilnehmerMin = '" + textBoxKursMin.Text + "', " +
                    "schwerpunkt = '" + textBoxKursSchwerpunkt.Text + "'" +
                    "WHERE workshopID = '" + textBoxKursID.Text + "';";
                command.ExecuteNonQuery();

                #region Checkboxen Auswerten und Rows in den DataTable speichern
                DataRow newRow;
                if (checkBoxKurs9Uhr.Checked) //Checkboxen überprüfen
                {
                    if (!CheckIfZeitplanungExists(9, checkBoxKurs9UhrZweistuendig.Checked))
                    {
                        newRow = myDataTableZeitplanung.NewRow();
                        newRow["workshopID"] = textBoxKursNr.Text;
                        newRow["start"] = "9:00";
                        newRow["ende"] = checkBoxKurs9UhrZweistuendig.Checked ? "11:00" : "10:00";
                        myDataTableZeitplanung.Rows.Add(newRow);
                    }
                }
                else
                {
                    DeleteZeitplanung(9);
                }

                if (checkBoxKurs10Uhr.Checked && checkBoxKurs10Uhr.Enabled)
                {
                    if (!CheckIfZeitplanungExists(10, checkBoxKurs10UhrZweistuendig.Checked))
                    {
                        newRow = myDataTableZeitplanung.NewRow();
                        newRow["workshopID"] = textBoxKursNr.Text;
                        newRow["start"] = "10:00";
                        newRow["ende"] = checkBoxKurs10UhrZweistuendig.Checked ? "12:00" : "11:00";
                        myDataTableZeitplanung.Rows.Add(newRow);
                    }
                }
                else
                {
                    DeleteZeitplanung(10);
                }

                if (checkBoxKurs11Uhr.Checked && checkBoxKurs11Uhr.Enabled)
                {
                    if (!CheckIfZeitplanungExists(11, checkBoxKurs11UhrZweistuendig.Checked))
                    {
                        newRow = myDataTableZeitplanung.NewRow();
                        newRow["workshopID"] = textBoxKursNr.Text;
                        newRow["start"] = "11:00";
                        newRow["ende"] = checkBoxKurs10UhrZweistuendig.Checked ? "13:00" : "12:00";
                        myDataTableZeitplanung.Rows.Add(newRow);
                    }
                }
                else
                {
                    DeleteZeitplanung(11);
                }

                if (checkBoxKurs12Uhr.Checked && checkBoxKurs12Uhr.Enabled)
                {
                    if (!CheckIfZeitplanungExists(12, false))
                    {
                        newRow = myDataTableZeitplanung.NewRow();
                        newRow["workshopID"] = textBoxKursNr.Text;
                        newRow["start"] = "12:00";
                        newRow["ende"] = "13:00";
                        myDataTableZeitplanung.Rows.Add(newRow);
                    }
                }
                else
                {
                    DeleteZeitplanung(12);
                }

                UpdateZeitplanungDB();
                #endregion
            }

            timerRefresh.Start();
        }

        private void UpdateZeitplanungDB()
        {
            try
            {
                zeitplanungCommandBuilder.GetUpdateCommand();
                zeitplanungCommandBuilder.GetInsertCommand();
                zeitplanungCommandBuilder.GetDeleteCommand();
                zeitplanungDataAdapter.Update(myDataTableZeitplanung);
                dataGridViewZeitplanung.DataSource = myDataTableZeitplanung;
            }
            catch (Exception)
            {
            }
        }

        private void DeleteZeitplanung(int stunde)
        {
            for (int i = myDataTableZeitplanung.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = myDataTableZeitplanung.Rows[i];
                if (DateTime.Parse(row["start"].ToString()).Hour == stunde && row["workshopID"] == textBoxKursID.text)
                {
                    row.Delete();
                    UpdateZeitplanungDB();
                }
            }
        }

        private bool CheckIfZeitplanungExists(int stunde, bool zweistündig)
        {
            int dauer = zweistündig ? 2 : 1;
            foreach (DataRow row in myDataTableZeitplanung.Rows)
            {
                if (DateTime.Parse(row["start"].ToString()).Hour == stunde)
                {
                    int rowDauer = DateTime.Parse(row["ende"].ToString()).Hour - DateTime.Parse(row["start"].ToString()).Hour;
                    if (rowDauer == dauer)
                    {
                        return true;
                    }
                    else
                    {
                        row.Delete();
                        UpdateZeitplanungDB();
                        return false;
                    }
                }
            }
            return false;
        }

        private void listBoxKurse_MouseClick(object sender, MouseEventArgs e)
        {
            refreshKursInfo();
            refreshZeitplanungInfo();
        }

        private void refreshZeitplanungInfo()
        {
            // Daten Holen
            zeitplanungDataAdapter.SelectCommand = new MySqlCommand ("Select * from zeitplanung z WHERE z.workshopID=" + ((DataRowView)listBoxKurse.SelectedItem)["workshopID"].ToString(), myConnection);
            myDataTableZeitplanung = new DataTable();
            zeitplanungDataAdapter.Fill(myDataTableZeitplanung);
            dataGridViewZeitplanung.DataSource = myDataTableZeitplanung;

            // Reset Checkboxen
            checkBoxKurs9Uhr.Checked = false;
            checkBoxKurs9UhrZweistuendig.Checked = false;
            checkBoxKurs10Uhr.Checked = false;
            checkBoxKurs10UhrZweistuendig.Checked = false;
            checkBoxKurs11Uhr.Checked = false;
            checkBoxKurs11UhrZweistuendig.Checked = false;
            checkBoxKurs12Uhr.Checked = false;
            // Daten auswerten
            foreach (DataRow zeitplanung in myDataTableZeitplanung.Rows)
            {
                DateTime startZeit = DateTime.Parse(zeitplanung["start"].ToString());
                DateTime endZeit = DateTime.Parse(zeitplanung["ende"].ToString());
                switch (startZeit.Hour)
                {
                    case 9:
                        checkBoxKurs9Uhr.Checked = true;
                        CalcDuration(startZeit.Hour, endZeit.Hour);
                        break;
                    case 10:
                        checkBoxKurs10Uhr.Checked = true;
                        CalcDuration(startZeit.Hour, endZeit.Hour);
                        break;
                    case 11:
                        checkBoxKurs11Uhr.Checked = true;
                        CalcDuration(startZeit.Hour, endZeit.Hour);
                        break;
                    case 12:
                        checkBoxKurs12Uhr.Checked = true;
                        CalcDuration(startZeit.Hour, endZeit.Hour);
                        break;
                }
            }
        }

        private void CalcDuration(int startZeit, int endZeit)
        {
            int duration = endZeit - startZeit;
            Console.WriteLine("duration: " + duration);
            if (duration == 2)
            {
                switch (startZeit)
                {
                    case 9:
                        checkBoxKurs9UhrZweistuendig.Checked = true;
                        break;
                    case 10:
                        checkBoxKurs10UhrZweistuendig.Checked = true;
                        break;
                    case 11:
                        checkBoxKurs11UhrZweistuendig.Checked = true;
                        break;
                }
            }
        }

        private void listBoxKurse_KeyDown(object sender, KeyEventArgs e)
        {
            refreshKursInfo();
        }
        private void refreshKursInfo()
        {
            if (listBoxKurse.SelectedItem != null)
            {
                var selectedItem = listBoxKurse.SelectedItem;
                textBoxKursID.Text = ((DataRowView)selectedItem)["workshopID"].ToString();
                textBoxKursNr.Text = ((DataRowView)selectedItem)["nr"].ToString();
                textBoxKursTitel.Text = ((DataRowView)selectedItem)["titel"].ToString();
                textBoxKursKosten.Text = ((DataRowView)selectedItem)["kosten"].ToString();
                richTextBoxKursBeschreibung.Text = ((DataRowView)selectedItem)["beschreibung"].ToString();
                textBoxKursVorraussetzungen.Text = ((DataRowView)selectedItem)["voraussetzungen"].ToString();
                textBoxKursMax.Text = ((DataRowView)selectedItem)["teilnehmerMin"].ToString();
                textBoxKursMin.Text = ((DataRowView)selectedItem)["teilnehmerMax"].ToString();
                textBoxKursSchwerpunkt.Text = ((DataRowView)selectedItem)["schwerpunkt"].ToString();
            }
        }
        #endregion

        #region Schueler Events
        private void buttonSchuelerHinzufuegen_Click(object sender, EventArgs e)
        {
            MySqlCommand command = myConnection.CreateCommand();
            command.CommandText = "INSERT INTO schueler (nachname) VALUES ('" + textBoxNeuerSchuelerNachname.Text + "');";
            command.ExecuteNonQuery();
        }
        private void buttonSchuelerLoeschen_Click(object sender, EventArgs e)
        {
            SchuelerLoeschenDialog dialog = new SchuelerLoeschenDialog(((DataRowView)listBoxSchueler.SelectedItem)["nachname"].ToString());
            dialog.ShowDialog();
            if (dialog.result == DialogResult.OK)
            {
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = "DELETE FROM schueler WHERE schuelerID = " + ((DataRowView)listBoxSchueler.SelectedItem)["schuelerID"].ToString() + ";";
                command.ExecuteNonQuery();
            }
        }
        private void buttonSchuelerAenderungenUebernehmen_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Sind sie sich sicher, dass sie die Änderungen unwiderruflich speichern wollen?", "Speichern", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = "UPDATE schueler SET " +
                    "vorname = '" + textBoxSchuelerVorname.Text + "', " +
                    "nachname = '" + textBoxSchuelerNachname.Text + "', " +
                    "email = '" + textBoxSchuelerEmail.Text + "', " +
                    "klasseID = '" + textBoxSchuelerKlasseID.Text + "', " +
                    "voraussichtlicherAbschluss = '" + textBoxSchuelerVoraussichtlicherAbschluss.Text + "', " +
                    "schule = '" + textBoxSchuelerSchule.Text + "', " +
                    "betreuendeLehrkraft = '" + textBoxSchuelerBetreuendeLehrkraft.Text + "' " +
                    "WHERE schuelerID = '" + textBoxSchuelerID.Text + "';";

                command.ExecuteNonQuery();
            }

        }
        private void listBoxSchueler_MouseClick(object sender, MouseEventArgs e)
        {
            refreshSchuelerInfo();
        }
        private void listBoxSchueler_KeyDown(object sender, KeyEventArgs e)
        {
            refreshSchuelerInfo();
        }
        private void refreshSchuelerInfo()
        {
            if (listBoxSchueler.SelectedItem != null)
            {
                var selectedItem = listBoxSchueler.SelectedItem;
                textBoxSchuelerID.Text = ((DataRowView)selectedItem)["schuelerID"].ToString();
                textBoxSchuelerVorname.Text = ((DataRowView)selectedItem)["vorname"].ToString();
                textBoxSchuelerNachname.Text = ((DataRowView)selectedItem)["nachname"].ToString();
                textBoxSchuelerEmail.Text = ((DataRowView)selectedItem)["email"].ToString();
                textBoxSchuelerKlasseID.Text = ((DataRowView)selectedItem)["klasseID"].ToString();
                textBoxSchuelerVoraussichtlicherAbschluss.Text = ((DataRowView)selectedItem)["voraussichtlicherAbschluss"].ToString();
                textBoxSchuelerSchule.Text = ((DataRowView)selectedItem)["schule"].ToString();
                textBoxSchuelerBetreuendeLehrkraft.Text = ((DataRowView)selectedItem)["betreuendeLehrkraft"].ToString();
            }
        }
        #endregion

        private void checkBoxKurs9UhrZweistuendig_CheckedChanged(object sender, EventArgs e)
        {
            refreshZeitplanungCheckboxen();
        }
        private void checkBoxKurs10UhrZweistuendig_CheckedChanged(object sender, EventArgs e)
        {
            refreshZeitplanungCheckboxen();
        }
        private void checkBoxKurs11UhrZweistuendig_CheckedChanged(object sender, EventArgs e)
        {
            refreshZeitplanungCheckboxen();
        }
        private void refreshZeitplanungCheckboxen()
        {
            if (checkBoxKurs9UhrZweistuendig.Checked)
            {
                checkBoxKurs10Uhr.Enabled = false;
                checkBoxKurs10UhrZweistuendig.Enabled = false;
            }
            else
            {
                checkBoxKurs10Uhr.Enabled = true;
                checkBoxKurs10UhrZweistuendig.Enabled = true;
            }
            if (checkBoxKurs10UhrZweistuendig.Enabled && checkBoxKurs10UhrZweistuendig.Checked)
            {
                checkBoxKurs11Uhr.Enabled = false;
                checkBoxKurs11UhrZweistuendig.Enabled = false;
            }
            else
            {
                checkBoxKurs11Uhr.Enabled = true;
                checkBoxKurs11UhrZweistuendig.Enabled = true;
            }

            if (checkBoxKurs11UhrZweistuendig.Enabled && checkBoxKurs11UhrZweistuendig.Checked)
                checkBoxKurs12Uhr.Enabled = false;
            else
                checkBoxKurs12Uhr.Enabled = true;
        }
    }
}
