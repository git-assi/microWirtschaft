using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.Welt
{
    public class clsWelt
    {
        public clsWelt()
        {             
            foreach (string strSiedlung in Config.clsWeltConfig.WarenConfig.m_lisDefaultSiedlungsNamen)
            {
                this.addSiedlung(strSiedlung);
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
                m_strInventur += Environment.NewLine + strInventur;
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
            }


        #endregion

        #region Siedlungen         

            public void addSiedlung(string strName)
            {
                Siedlungen.clsKolonie newKolonie = new Siedlungen.clsKolonie(strName);

                this.onWeltTick += new WeltTickHandler(newKolonie.raiseTick);

                this.onWeltInventur += new InventurHandler(newKolonie.raiseInventur);
                newKolonie.onInventur += new piratesWirtschaft.BasisKlassen.clsSiedlung.InventurHandler(newKolonie_onInventur);

                this.onfindSiedlung += new findSiedlungHandler(newKolonie.raiseFoundMe);
                newKolonie.onFoundMe += new piratesWirtschaft.BasisKlassen.clsSiedlung.FoundMeHandler(newKolonie_onFoundMe);
            }

        #endregion

    }
}

