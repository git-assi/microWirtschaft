using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace piratesWirtschaft
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            //wiki = new Wiki.clsWikiInterface(false);
           // wiki.onStatus += new Wiki.clsWikiInterface.statusHandler(wiki_onStatus);

        }

        void m_objWelt_onLog(string strLog)
        {
            this.txtStatus.Text = strLog + Environment.NewLine + this.txtStatus.Text;
        }

        void wiki_onStatus(string strStatus)
        {
            try
            {
                this.txtStatus.Text = strStatus + Environment.NewLine + this.txtStatus.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "wiki_onStatus");
            }    
        }

        private Welt.clsWelt m_objWelt;

        //protected  Wiki.clsWikiInterface wiki;
      
        private void btnInventur_Click(object sender, EventArgs e)
        {

            try
            {
                textBox1.Text = "";
                m_objWelt.findSieldungen();
                btnInventur.Enabled = false;
                foreach (BasisKlassen.clsSiedlung objSied in m_objWelt.m_lisFoundSiedlung)
                {
                    textBox1.Text += objSied.getInventurString() + Environment.NewLine;
                    //wiki.setWikiSeitenText(objSied.strName, textBox1.Text);
                }
                
                
                btnInventur.Enabled = true;
                
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
                txtStatus.Text = Environment.NewLine + txtStatus.Text;
               
                m_objWelt.raiseTick();
                this.Text = m_objWelt.datWeltzeit.ToShortDateString();

                btnInventur_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_objWelt = new piratesWirtschaft.Welt.clsWelt();
            (new frmEditor(m_objWelt)).Show();
            m_objWelt.onLog += new Welt.clsWelt.singeStringHandler(m_objWelt_onLog);
            m_objWelt.raiseWeltInventur();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {            
            m_objWelt = new piratesWirtschaft.Welt.clsWelt();
            m_objWelt.onLog += new Welt.clsWelt.singeStringHandler(m_objWelt_onLog);
        
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = m_objWelt.strInventur;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
