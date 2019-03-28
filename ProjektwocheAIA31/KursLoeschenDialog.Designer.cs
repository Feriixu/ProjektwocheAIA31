namespace ProjektwocheAIA31
{
    partial class KursLoeschenDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KursLoeschenDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEingabe = new System.Windows.Forms.TextBox();
            this.buttonAbbrechen = new System.Windows.Forms.Button();
            this.buttonLoeschen = new System.Windows.Forms.Button();
            this.labelKursName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zum löschen des Kurses geben sie den Kursnamen erneut ein:";
            // 
            // textBoxEingabe
            // 
            this.textBoxEingabe.Location = new System.Drawing.Point(15, 50);
            this.textBoxEingabe.Name = "textBoxEingabe";
            this.textBoxEingabe.Size = new System.Drawing.Size(402, 20);
            this.textBoxEingabe.TabIndex = 1;
            // 
            // buttonAbbrechen
            // 
            this.buttonAbbrechen.Location = new System.Drawing.Point(15, 103);
            this.buttonAbbrechen.Name = "buttonAbbrechen";
            this.buttonAbbrechen.Size = new System.Drawing.Size(402, 37);
            this.buttonAbbrechen.TabIndex = 3;
            this.buttonAbbrechen.Text = "Abbrechen";
            this.buttonAbbrechen.UseVisualStyleBackColor = true;
            this.buttonAbbrechen.Click += new System.EventHandler(this.buttonAbbrechen_Click);
            // 
            // buttonLoeschen
            // 
            this.buttonLoeschen.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonLoeschen.Location = new System.Drawing.Point(15, 76);
            this.buttonLoeschen.Name = "buttonLoeschen";
            this.buttonLoeschen.Size = new System.Drawing.Size(403, 23);
            this.buttonLoeschen.TabIndex = 3;
            this.buttonLoeschen.Text = "Unwiederruflich Löschen";
            this.buttonLoeschen.UseVisualStyleBackColor = true;
            this.buttonLoeschen.Click += new System.EventHandler(this.buttonLoeschen_Click);
            // 
            // labelKursName
            // 
            this.labelKursName.AutoSize = true;
            this.labelKursName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKursName.Location = new System.Drawing.Point(12, 30);
            this.labelKursName.Name = "labelKursName";
            this.labelKursName.Size = new System.Drawing.Size(46, 17);
            this.labelKursName.TabIndex = 4;
            this.labelKursName.Text = "label2";
            // 
            // KursLoeschenDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 152);
            this.Controls.Add(this.labelKursName);
            this.Controls.Add(this.buttonLoeschen);
            this.Controls.Add(this.buttonAbbrechen);
            this.Controls.Add(this.textBoxEingabe);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KursLoeschenDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KursLoeschenDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEingabe;
        private System.Windows.Forms.Button buttonAbbrechen;
        private System.Windows.Forms.Button buttonLoeschen;
        private System.Windows.Forms.Label labelKursName;
    }
}