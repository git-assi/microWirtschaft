using System;
using System.Collections.Generic;
using System.Text;
using piratesWirtschaft.SiedlungsTypen;

namespace piratesWirtschaft.BasisKlassen
{    
        public class clsSiedlung : clsWarenhaus
        {
            public clsSiedlungsTyp objSiedlungsTyp;

            public clsSiedlung(string strName, int intEinwohner)
            {
                m_strName = strName;
                m_intEinwohner = intEinwohner;

                objSiedlungsTyp = new clsKolonie();
                objSiedlungsTyp.onAddGüter += new clsSiedlungsTyp.delWarenAddHandler(this.addGutByName);
                objSiedlungsTyp.onLog += new clsSiedlungsTyp.delSingleStringHandler(this.Log );
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
                set
                {
                    m_intEinwohner += value;
                }
            }


            public void onTick(Welt.clsWelt sender, Welt.clsWelt.TickEventArgs e)
            {
                this.verbraucheLebensmittel();
                this.veränderePopulation();
                this.objSiedlungsTyp.erzeugeGüter(m_intEinwohner);
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
                    //Nahrungsknappheit
                    clsWare wareLebensmittel = this.getWare("Lebensmittel");
                    if (wareLebensmittel.intBedarfDelta > 0 && wareLebensmittel.intMenge <= 0)
                    {
                        int intEinwohnerVerlust = Convert.ToInt32(wareLebensmittel.intBedarfDelta * wareLebensmittel.m_dblFaktor_Verbrauch);

                        Log("EinwohnerVerlust: " + intEinwohnerVerlust.ToString());
                        m_intEinwohner -= intEinwohnerVerlust;
                    }

                    objSiedlungsTyp.möglichesWachstum(this);
                }
                     
            protected void verbraucheLebensmittel()
            {
                clsWare objWare = this.getWare("Lebensmittel");               
                int intGesamtVerbrauch = objWare.getGesamtBedarfByPopulation(m_intEinwohner);
                Log("Lebensmittelverbrauch: " + intGesamtVerbrauch.ToString());

                int intVerfügbareArbeiter = (this.intEinwohner > objSiedlungsTyp.intMaxBauern ? objSiedlungsTyp.intMaxBauern : this.intEinwohner);

                int intProduzierteWaren = Convert.ToInt32(objWare.m_dblFaktor_Produktion * intVerfügbareArbeiter);
                Log("Lebensmittelproduktion: " + intProduzierteWaren.ToString());

                objWare.intBedarfDelta = intGesamtVerbrauch - intProduzierteWaren;
                Log("Lebensmitteldelta = " + (objWare.intBedarfDelta >= 0 ? "Bedarf" : "Überschuss") + ": " + Math.Abs(objWare.intBedarfDelta).ToString());
                
                objWare.addMenge(- intGesamtVerbrauch + intProduzierteWaren);
            }

            protected void addGutByName(string strProduktionsWare, int intAnzahlArbeiter)
            {

                clsWare objWare = this.getWare(strProduktionsWare);
                
                double dblAddMenge = objWare.m_dblFaktor_Produktion * intAnzahlArbeiter;
                int intProduzierteWaren = Convert.ToInt32(dblAddMenge);
                Log("Erzeuge " + strProduktionsWare + " mit " + intAnzahlArbeiter.ToString() + " Arbeitern ergibt " + dblAddMenge.ToString());
                
                objWare.addMenge(intProduzierteWaren);
            }

            public string getInventurString()
            {
                return m_strName + "(" + this.objSiedlungsTyp.strTyp + ")" + Environment.NewLine + Environment.NewLine + "Einwohner: " + m_intEinwohner.ToString() + Environment.NewLine + this.inventur();
            }

            private void neuerTyp(clsSiedlung objSiedlung)
            {
                //if (objSiedlung.objSiedlungsTyp is clsKolonie)
                //{
                //    clsStadt newStadt = new clsStadt();
                //    if (objSiedlung.intEinwohner > newStadt.intMinEinwohner)
                //    {
                //        Log("zur Stadt gewachsen");
                //        objSiedlung.objSiedlungsTyp = newStadt;
                //        objSiedlung.objSiedlungsTyp.onAddGüter += new clsSiedlungsTyp.delWarenAddHandler(RaiseOnAddGüter);

                //    }
                //}
                //else if (objSiedlung.intEinwohner < this.intMinEinwohner)
                //{
                //    Log("zur Kolonie geschrumpft");
                //    objSiedlung.objSiedlungsTyp = new clsKolonie();
                //    objSiedlung.objSiedlungsTyp.onAddGüter += new clsSiedlungsTyp.delWarenAddHandler(RaiseOnAddGüter);
                //}

            }
        }
    
}
