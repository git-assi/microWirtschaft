using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{    
        public abstract class clsSiedlung : clsWarenhaus
        {
            public clsSiedlung(string strName, int intEinwohner)
            {
                m_strName = strName;
                m_intEinwohner = intEinwohner;
            }

            private string m_strName;

            public string strName
            {
                get { return m_strName; }                
            }
            private int m_intEinwohner = 0;
            public int intEinwohner
            {
                get
                {
                    return m_intEinwohner;
                }
            }

           

            public void raiseTick(Welt.clsWelt sender, Welt.clsWelt.TickEventArgs e)
            {
                this.verbraucheGüter();
                this.veränderePopulation();
                this.erzeugeGüter();

            }


            public delegate void delSingleStringHandler(string strInventur);
            
            #region Inventur
            public event delSingleStringHandler onInventur;

            
            public void raiseInventur()
            {
                if (onInventur != null)
                {
                    onInventur(this.getInventurString());
                }    
            }

            #endregion

            #region Logging
            public event delSingleStringHandler onLog;
            protected void Log(string strLog)
            {
                if (onLog != null)
                {
                    onLog(this.m_strName + ": " + strLog);
                }
            }

            #endregion

            #region FindMe
            
                public delegate void FoundMeHandler(clsSiedlung objSiedlung);
                public event FoundMeHandler onFoundMe;
                public void raiseFoundMe(string strName)
                {
                    if (strName == "" || this.strName.Contains(strName))
                        onFoundMe(this);
                    
                }

            #endregion

            private void veränderePopulation()
            {
                clsWare objWare = this.getWare("Lebensmittel");
                if (objWare.intMenge <= 0)
                {
                    //int intEinwohnerVerlust = 5;
                    int intEinwohnerVerlust = Convert.ToInt32(m_intEinwohner * objWare.m_dblFaktor_Verbrauch);
                    
                    Log("EinwohnerVerlust: " + intEinwohnerVerlust.ToString());
                    m_intEinwohner -= intEinwohnerVerlust;
                }
                else
                {
                    objWare = this.getWare("Werkzeug");

                    if (objWare.intMenge > 0)
                    {
                        int intMöglicheNeueEinwohner = Convert.ToInt32(objWare.m_dblFaktor_Verbrauch * objWare.intMenge);
                        
                        Log("MöglicheNeueEinwohner: " + intMöglicheNeueEinwohner.ToString());
                        Log("Verbrauch Werkzeug: " + objWare.intMenge.ToString());

                        m_intEinwohner += intMöglicheNeueEinwohner;
                        objWare.addMenge(-objWare.intMenge);
                    }
                    
                }
            }           

            protected void verbraucheGüter()
            {
                clsWare objWare = this.getWare("Lebensmittel");
                Log("LebensmittelbedarfBedarf: " + objWare.getGesamtBeadrfByPopulation(m_intEinwohner).ToString());
                objWare.addMenge(-objWare.getGesamtBeadrfByPopulation(m_intEinwohner));
            }

            protected abstract void erzeugeGüter();

            protected void addGutByName(string strProduktionsWare, int intAnzahlArbeiter)
            {

                clsWare objWare = this.getWare(strProduktionsWare);
                
                double dblAddMenge = objWare.m_dblFaktor_Produktion * intAnzahlArbeiter;
                int intProduzierteWaren = Convert.ToInt32(dblAddMenge);
                Log("Erzeuge " + strProduktionsWare + " mit " + intAnzahlArbeiter.ToString() + " Arbeitern ergibt " + dblAddMenge.ToString());
                
                objWare.setBedarf(objWare.getGesamtBeadrfByPopulation(m_intEinwohner), intProduzierteWaren);
                objWare.addMenge(intProduzierteWaren);
            }

            public string getInventurString()
            {
                return m_strName + Environment.NewLine + Environment.NewLine + "Einwohner: " + m_intEinwohner.ToString() + Environment.NewLine + this.inventur();
            }
        }
    
}
