using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace piratesWirtschaft
{
    public partial class frmEditor : Form
    {

        Welt.clsWelt m_objWelt = null;

        public frmEditor(Welt.clsWelt objWelt)
        {
            InitializeComponent();
            m_objWelt = objWelt;
        }

        public BasisKlassen.clsWare objWare;

        
        private void frmEditor_Shown(object sender, EventArgs e)
        {
            try
            {
                this.fillSiedlungCombo();    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fillSiedlungCombo()
        {
            m_objWelt.findSieldungen();

            foreach (BasisKlassen.clsSiedlung aktSied in m_objWelt.m_lisFoundSiedlung)
            {
                cmbSiedlungen.Items.Add(aktSied);
                cmbSiedlungen.DisplayMember = "strName";
            }
        }
        
        private BasisKlassen.clsSiedlung objSelectedSiedlung
        {
            get
            {
                return (BasisKlassen.clsSiedlung)cmbSiedlungen.SelectedItem;

            }
    
        }

        private void fillWarenCombo()
        {
            try
            {
                foreach (string strWare in objSelectedSiedlung.getVerfügbareWaren())
                {
                    cmbWaren.Items.Add(strWare);
                }
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                }
        }

        private void cmbSiedlungen_SelectedValueChanged(object sender, EventArgs e)
        {
            fillWarenCombo();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            objSelectedSiedlung.getWare(cmbWaren.Text).addMenge(Convert.ToInt32(numMenge.Value));
        }

    }
}
