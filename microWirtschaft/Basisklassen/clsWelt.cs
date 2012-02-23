using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.Welt
{
    public class clsWelt
    {
        public clsWelt()
        {             
            foreach (BasisKlassen.clsSiedlung objSiedlung in Config.clsWeltConfig.WarenConfig.m_lisDefaultSiedlungen)
            {
                this.addSiedlung(objSiedlung);
            }

        }                   

        #region findSiedlung

        public delegate void findSiedlungHandler(string strName);
        public event findSiedlungHandler onfindSiedlung;
        public void findSieldung(string strName)
        {
            m_lisFoundSiedlung.Clear();
            if (onfindSiedlung != null)                
                onfindSiedlung(strName);                
        }
        public void findSieldungen()
        {
            m_lisFoundSiedlung.Clear();
            if (onfindSiedlung != null)
                onfindSiedlung("");
        }

        public List<BasisKlassen.clsSiedlung> m_lisFoundSiedlung = new List<piratesWirtschaft.BasisKlassen.clsSiedlung>();

        void newKolonie_onFoundMe(piratesWirtschaft.BasisKlassen.clsSiedlung objSiedlung)
        {
            m_lisFoundSiedlung.Add(objSiedlung);
        }


        #endregion

        #region Inventur
        private string m_strInventur = "";

        public string strInventur
        {
            get { return m_strInventur; }
        }

        void newKolonie_onInventur(string strInventur)
        {
            try
            {
                m_strInventur +=  strInventur + Environment.NewLine;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        public delegate void InventurHandler();

        public event InventurHandler onWeltInventur;

        public void raiseWeltInventur()
        {
            if (onWeltInventur != null)
            {
                m_strInventur = "";
                onWeltInventur();
            }
        }

        #endregion

        #region Zeit

        private DateTime m_datWeltzeit = new DateTime(1700, 1, 1);
        public DateTime datWeltzeit
        {
            get
            {
                return m_datWeltzeit;
            }

        }
        
        public class TickEventArgs
            {
                public DateTime datTick;
                public TickEventArgs()
                {
                    datTick = DateTime.Now;
                }
            }
           
            public delegate void WeltTickHandler(clsWelt sender, TickEventArgs e);           
            public event WeltTickHandler onWeltTick;

            public void raiseTick()
            {
                if (onWeltTick != null)
                {
                    onWeltTick(this, new TickEventArgs());
                }
                m_datWeltzeit = m_datWeltzeit.AddMonths(1);
            }


        #endregion

        #region Siedlungen         

            public void addSiedlung(BasisKlassen.clsSiedlung newSiedlung)
            {
                
                this.onWeltTick += new WeltTickHandler(newSiedlung.onTick);

                this.onWeltInventur += new InventurHandler(newSiedlung.raiseInventur);
                newSiedlung.onInventur += new piratesWirtschaft.BasisKlassen.clsSiedlung.delSingleStringHandler(newKolonie_onInventur);

                newSiedlung.onLog += new BasisKlassen.clsSiedlung.delSingleStringHandler(raiseLog);

                this.onfindSiedlung += new findSiedlungHandler(newSiedlung.raiseFoundMe);
                newSiedlung.onFoundMe += new piratesWirtschaft.BasisKlassen.clsSiedlung.FoundMeHandler(newKolonie_onFoundMe);
            }
            
            public delegate void singeStringHandler(string str);
            public event singeStringHandler onLog;
            public void raiseLog(string strLog)
            {
                if (onLog != null)
                {
                    onLog(this.datWeltzeit.ToString("MMMM.yyyy") + ": " + strLog);
                }
            }

        #endregion


    }
}

