using System;
using System.Collections.Generic;
using System.Text;
using piratesWirtschaft.Welt;
using piratesWirtschaft.BasisKlassen;
using piratesWirtschaft.Config;

namespace piratesWirtschaft.SiedlungsTypen
{

    public abstract class clsSiedlungsTyp
    {
        public clsSiedlungsTyp()
        {
           
        }
        
        public abstract string strTyp { get; }
        public delegate void delWarenAddHandler(string strWarenName, int intMenge);
        public event delWarenAddHandler onAddGüter;
        public void RaiseOnAddGüter(string strWarenName, int intMenge)
        {
            onAddGüter(strWarenName, intMenge);
        }
        public abstract int intMinEinwohner { get; }
        public abstract int intMaxBauern { get; }
        public abstract void erzeugeGüter(int intEinwohner);

        internal abstract void möglichesWachstum(clsSiedlung objSiedlung);

        public delegate void delSingleStringHandler(string strInventur);
        public event delSingleStringHandler onLog;
        protected  void Log(string strText)
        {
            onLog(strText);
        }
        
    }

    public class clsKolonie : clsSiedlungsTyp  
    {

        public override string strTyp
        {
            get
            { return "Kolonie"; }
        }

        public override int intMaxBauern 
        {
            get
            { return 250; }
        }

        public override int intMinEinwohner { get { return 0; } }

        public override void erzeugeGüter(int intEinwohner)
        {
        
        }

        internal override void möglichesWachstum(clsSiedlung objSiedlung )
        {
            //mögliches Wachstum
            clsWare wareWerkzeug =  objSiedlung.getWare("Werkzeug");
            clsWare wareLebensmittel = objSiedlung.getWare("Lebensmittel");


                if (wareLebensmittel.intMenge > 0 && wareWerkzeug.intMenge > 0)
                {
                    int intMöglicheNeueEinwohner = Convert.ToInt32(wareWerkzeug.m_dblFaktor_Verbrauch * wareWerkzeug.intMenge);

                    Log("MöglicheNeueEinwohner: " + intMöglicheNeueEinwohner.ToString());
                        
                    if (intMöglicheNeueEinwohner > 0)
                    {
                        objSiedlung.intEinwohner += intMöglicheNeueEinwohner;
                        Log("Verbrauch Werkzeug: " + wareWerkzeug.intMenge.ToString());
                        wareWerkzeug.addMenge(-wareWerkzeug.intMenge);
                    }
                }
            }
        }

    

    public class clsStadt : clsSiedlungsTyp 
    {

        public override int intMaxBauern { get { return 25; } }
        public override int intMinEinwohner { get { return 30; } }

        public override void erzeugeGüter(int intEinwohner)
        {
            int intVerfügbareArbeiter = intEinwohner - intMaxBauern;
            if (intVerfügbareArbeiter > 0)
                base.RaiseOnAddGüter("Werkzeug", intVerfügbareArbeiter);
        }

        public override string strTyp
        {
            get
            { return "Stadt"; }
        }

        internal override void möglichesWachstum(clsSiedlung objSiedlung)
        {
            //mögliches Wachstum
            clsWare wareLuxusgüter = objSiedlung.getWare("Luxusgüter");
            clsWare wareLebensmittel = objSiedlung.getWare("Lebensmittel");


            if (wareLebensmittel.intMenge > 0 && wareLuxusgüter.intMenge > 0)
            {
                int intMöglicheNeueEinwohner = Convert.ToInt32(wareLuxusgüter.m_dblFaktor_Verbrauch * wareLuxusgüter.intMenge);

                Log("MöglicheNeueEinwohner: " + intMöglicheNeueEinwohner.ToString());

                if (intMöglicheNeueEinwohner > 0)
                {
                    objSiedlung.intEinwohner += intMöglicheNeueEinwohner;
                    Log("Verbrauch Werkzeug: " + wareLuxusgüter.intMenge.ToString());
                    wareLuxusgüter.addMenge(-wareLuxusgüter.intMenge);
                }
            }

            
        }
        
    }
   
}
