using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{  
    public class clsWare
    {
        public string m_strBez;
        public Double m_dblFaktor_Verbrauch;
        public Double m_dblFaktor_Produktion;
        protected int m_intMenge = 0;
        protected int m_intBedarf = 0;


        public int intMenge
        {
            get 
            { 
                return (m_intMenge >= 0 ? m_intMenge : 0); 
            }
        }

        public void addMenge(int intMenge)
        {

            if ((m_intMenge + intMenge) < 0)
                m_intMenge = 0;
            else
                m_intMenge += intMenge;
            
        }

        public int intBedarf
        {
            get
            {
                return m_intBedarf;
            }
        }

        public clsWare(string strBez, Double dblFaktor_Verbrauch, Double dblFaktor_Produktion)
        {            
            m_strBez = strBez;
            m_dblFaktor_Verbrauch = dblFaktor_Verbrauch;
            m_dblFaktor_Produktion = dblFaktor_Produktion;
        }

        public int getGesamtBeadrfByPopulation(int intEinwohner)
        {
            return Convert.ToInt32(intEinwohner * this.m_dblFaktor_Verbrauch);
        }

        internal void setBedarf(int intProduktion, int intGesamtBedarf)
        {
            m_intBedarf = intGesamtBedarf - intProduktion;
        }
    }
    
}
