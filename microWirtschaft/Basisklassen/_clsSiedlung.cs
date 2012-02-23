using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{    
        public abstract class _clsSiedlung : clsWarenhaus
        {
            public _clsSiedlung(string strName, int intEinwohner)
            {
                m_strName = strName;
                m_intEinwohner = intEinwohner;
            }

            protected abstract int m_intMaxBauern
            {
                get;
            }

            protected abstract string strTyp
            {
                get;
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
                this.verbraucheLebensmittel();
                this.veränderePopulation();

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
            
                public delegate void FoundMeHandler(_clsSiedlung objSiedlung);
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
                
                //mögliches Wachstum
                clsWare wareWerkzeug = this.getWare("Werkzeug");

                    if (wareLebensmittel.intMenge > 0 && wareWerkzeug.intMenge > 0)
                    {
                        int intMöglicheNeueEinwohner = Convert.ToInt32(wareWerkzeug.m_dblFaktor_Verbrauch * wareWerkzeug.intMenge);
                        
                        Log("MöglicheNeueEinwohner: " + intMöglicheNeueEinwohner.ToString());
                        
                        if (intMöglicheNeueEinwohner > 0)
                        {
                            m_intEinwohner += intMöglicheNeueEinwohner;
                            wareWerkzeug.addMenge(-wareWerkzeug.intMenge);
                            Log("Verbrauch Werkzeug: " + wareWerkzeug.intMenge.ToString());
                        }
                    }
                    
                }
            

            protected void verbraucheLebensmittel()
            {
                clsWare objWare = this.getWare("Lebensmittel");               
                int intGesamtVerbrauch = objWare.getGesamtBedarfByPopulation(m_intEinwohner);
                Log("Lebensmittelverbrauch: " + intGesamtVerbrauch.ToString());

                int intVerfügbareArbeiter = (this.intEinwohner > this.m_intMaxBauern ? this.m_intMaxBauern : this.intEinwohner);

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
                return m_strName + "(" + this.strTyp + ")" + Environment.NewLine + Environment.NewLine + "Einwohner: " + m_intEinwohner.ToString() + Environment.NewLine + this.inventur();
            }
        }

        public class _clsKolonie : _clsSiedlung
        {

            public _clsKolonie(string strName)
                : base(strName, 5)
            {

            }

            public _clsKolonie(string strName, int intDefaultEinwohner)
                : base(strName, intDefaultEinwohner)
            {

            }

            protected override int m_intMaxBauern
            {
                get
                {
                    return this.intEinwohner;
                }
            }

            protected override string strTyp
            {
                get
                { return "Kolonie"; }
            }


        }

        public class _clsStadt : _clsKolonie
        {
            public _clsStadt(string strName)
                : base(strName, 20)
            {

            }


            protected override int m_intMaxBauern
            {
                get
                {
                    return this.intEinwohner / 2;
                }
            }
            protected void erzeugeGüter()
            {
                //base.erzeugeGüter();
                int intVerfügbareArbeiter = this.intEinwohner - 20;
                if (intVerfügbareArbeiter > 0)
                    this.addGutByName("Werkzeug", intVerfügbareArbeiter);
            }

            protected void verbraucheGüter()
            {
                //this.addWarenMenge("Werkzeug", -1 * (m_intEinwohner / 100));
            }

            protected override string strTyp
            {
                get
                { return "Stadt"; }
            }
        }
}
