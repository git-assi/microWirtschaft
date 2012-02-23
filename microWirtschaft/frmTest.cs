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
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
            objTestobjekt = new BasisKlassen.clsSiedlung("test", 10);

            objTestobjekt.onLog += new BasisKlassen.clsSiedlung.delSingleStringHandler(objStadt_onLog);
            objTestobjekt.onInventur += new BasisKlassen.clsSiedlung.delSingleStringHandler(objStadt_onLog);
        }

        private BasisKlassen.clsSiedlung objTestobjekt; 

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                objTestobjekt.onTick(null, null);    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                    
        }

        void objStadt_onLog(string strInventur)
        {
            textBox1.Text = strInventur + Environment.NewLine + textBox1.Text; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objTestobjekt.raiseInventur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (objTestobjekt.objSiedlungsTyp is piratesWirtschaft.SiedlungsTypen.clsStadt)
                    objTestobjekt.objSiedlungsTyp = new piratesWirtschaft.SiedlungsTypen.clsKolonie();
                else
                    objTestobjekt.objSiedlungsTyp = new piratesWirtschaft.SiedlungsTypen.clsStadt();
                   


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
