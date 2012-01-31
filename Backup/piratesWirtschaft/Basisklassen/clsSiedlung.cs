using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{    
        public abstract class clsSiedlung : clsWarenhaus
        {
            public clsSiedlung(string strName, clsWare objProduktionsWare)
            {
                m_strName = strName;
                m_strProduktionsWare = objProduktionsWare.m_strBez;
            }

            private string m_strName;

            public string strName
            {
                get { return m_strName; }                
            }
            private string m_strProduktionsWare;
            protected int m_intEinwohner = 0;

            private int m_intLebensmittelbedarfBedarf
            {
                get
                {
                    return Convert.ToInt32(m_intEinwohner * new Config.clsWarenConfig.clsLebensmittel().m_dblFaktor_Verbrauch);
                }
            }

            public void raiseTick(Welt.clsWelt sender, Welt.clsWelt.TickEventArgs e)
            {
                this.verbraucheGüter();
                this.erzeugeGüter();
                this.veränderePopulation();
            }

            #region Inventur

                public delegate void InventurHandler(string strInventur);
                public event InventurHandler onInventur;
                public void raiseInventur()
            {
                if (onInventur != null)
                {
                    onInventur(this.strInventur);
                }
            }

            #endregion

            #region FindMe
            
                public delegate void FoundMeHandler(clsSiedlung objSiedlung);
                public event FoundMeHandler onFoundMe;
                public void raiseFoundMe(string strName)
                {
                    if (this.strName.Contains(strName))
                        onFoundMe(this);
                    
                }

            #endregion

            private void veränderePopulation()
            {
                clsWare objWare = this.getWare("Lebensmittel");
                if (objWare.intMenge <= 0)
                {
                    m_intEinwohner -= (m_intEinwohner / 10);                    
                }
                else
                {
                    objWare = this.getWare("Werkzeug");

                    int intMöglicheNeueEinwohner = (m_intEinwohner / 25);
                    int intNötigeWaren = Convert.ToInt32(intMöglicheNeueEinwohner * objWare.m_dblFaktor_Verbrauch);

                    if (objWare.intMenge > intNötigeWaren)
                    {
                        objWare.intMenge -= intNötigeWaren;
                        m_intEinwohner += intMöglicheNeueEinwohner;
                    }

                }
            }           

            protected void verbraucheGüter()
            {
                if (this.strName == "Tortuga")
                    this.getWare("Lebensmittel").intMenge = 0;                
                else
                    this.getWare("Lebensmittel").intMenge -= m_intLebensmittelbedarfBedarf;
            }

            protected void erzeugeGüter()
            {
                clsWare objWare = this.getWare("Lebensmittel");
                double dblAddMenge = objWare.m_dblFaktor_Produktion * m_intEinwohner;
                objWare.intMenge += Convert.ToInt32(dblAddMenge);
            }

            public string strInventur
            {
                get
                {
                    return m_strName + Environment.NewLine + Environment.NewLine + "Einwohner: " + m_intEinwohner.ToString() + Environment.NewLine + this.inventur();
                }
            }
        }
    
}
