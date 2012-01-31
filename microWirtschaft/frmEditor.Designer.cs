namespace piratesWirtschaft
{
    partial class frmEditor
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSet = new System.Windows.Forms.Button();
            this.cmbSiedlungen = new System.Windows.Forms.ComboBox();
            this.cmbWaren = new System.Windows.Forms.ComboBox();
            this.numMenge = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numMenge)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(12, 92);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(149, 23);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // cmbSiedlungen
            // 
            this.cmbSiedlungen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiedlungen.FormattingEnabled = true;
            this.cmbSiedlungen.Location = new System.Drawing.Point(12, 12);
            this.cmbSiedlungen.Name = "cmbSiedlungen";
            this.cmbSiedlungen.Size = new System.Drawing.Size(149, 21);
            this.cmbSiedlungen.TabIndex = 3;
            this.cmbSiedlungen.SelectedValueChanged += new System.EventHandler(this.cmbSiedlungen_SelectedValueChanged);
            // 
            // cmbWaren
            // 
            this.cmbWaren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaren.FormattingEnabled = true;
            this.cmbWaren.Location = new System.Drawing.Point(12, 39);
            this.cmbWaren.Name = "cmbWaren";
            this.cmbWaren.Size = new System.Drawing.Size(149, 21);
            this.cmbWaren.TabIndex = 4;
            // 
            // numMenge
            // 
            this.numMenge.Location = new System.Drawing.Point(12, 66);
            this.numMenge.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMenge.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numMenge.Name = "numMenge";
            this.numMenge.Size = new System.Drawing.Size(149, 20);
            this.numMenge.TabIndex = 5;
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 134);
            this.Controls.Add(this.numMenge);
            this.Controls.Add(this.cmbWaren);
            this.Controls.Add(this.cmbSiedlungen);
            this.Controls.Add(this.btnSet);
            this.Name = "frmEditor";
            this.Text = "frmEditor";
            this.Shown += new System.EventHandler(this.frmEditor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numMenge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.ComboBox cmbSiedlungen;
        private System.Windows.Forms.ComboBox cmbWaren;
        private System.Windows.Forms.NumericUpDown numMenge;

    }
}