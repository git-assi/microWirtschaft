using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace piratesWirtschaft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Welt.clsWelt m_objWelt;
        
      
        private void btnInventur_Click(object sender, EventArgs e)
        {
            try
            {
                m_objWelt.findSieldung("Tortuga");
                textBox1.Text = "";
                foreach (BasisKlassen.clsSiedlung objSied in m_objWelt.m_lisFoundSiedlung)
                {
                    textBox1.Text += objSied.strInventur + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTick_Click(object sender, EventArgs e)
        {
            try
            {
                m_objWelt.raiseTick();
                m_objWelt.raiseWeltInventur();
                textBox1.Text = m_objWelt.strInventur;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_objWelt = new piratesWirtschaft.Welt.clsWelt();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {            
            m_objWelt = new piratesWirtschaft.Welt.clsWelt();
        }
    }
}
