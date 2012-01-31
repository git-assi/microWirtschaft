using System;
using System.Collections.Generic;
using System.Text;
using piratesWirtschaft.Config;

namespace piratesWirtschaft.BasisKlassen
{
    public abstract class clsWarenhaus
    {
        public clsWarenhaus()
        {
            m_dicWaren = new Dictionary<string, clsWare>();
            initWaren();
        }

        private Dictionary<string, clsWare> m_dicWaren = null;
        private void initWaren()
        {
            //init
            foreach (clsWare aktWare in clsWarenConfig.WarenConfig.m_lisDefaultWaren)
            {
                m_dicWaren.Add(aktWare.m_strBez, aktWare);
            }
        }
    

        //protected clsWare getWare(string strBez)
        public clsWare getWare(string strBez)
        {
            return m_dicWaren[strBez];
        }
        
        protected Double getWarenFaktor(string strBez)
        {
            return getWare(strBez).m_dblFaktor_Verbrauch;
        }

        public List<string> getVerfügbareWaren()
        {
            List<string> lisWaren = new List<string>();

            foreach (clsWare objWare in m_dicWaren.Values)
            {
                lisWaren.Add(objWare.m_strBez);
            }
            return lisWaren;
        }
        

      

       
        
        protected string inventur()
        {
            string strMeldung = "";
            foreach (clsWare aktWare in m_dicWaren.Values)
            {
                if (aktWare.intMenge > 0)
                    strMeldung += aktWare.m_strBez + " Bestand: " + aktWare.intMenge.ToString() + Environment.NewLine;
                if (aktWare.intBedarf != 0)
                    strMeldung += aktWare.m_strBez + " "  + (aktWare.intBedarf > 0 ? "Überschuss" : "Bedarf") + ": " + aktWare.intBedarf.ToString() + Environment.NewLine;
            }
            return strMeldung;
        }
    }
}
