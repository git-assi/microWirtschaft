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
        protected int m_intBedarfDelta = 0;

        public int intBedarfDelta
        {
            get
            {
                return m_intBedarfDelta;
            }
            set
            {
                m_intBedarfDelta = value;
            }
        }

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
            {
                m_intMenge = 0;
            }
            else
            {
                m_intMenge += intMenge;
            }
        }

        
        public clsWare(string strBez, Double dblFaktor_Verbrauch, Double dblFaktor_Produktion)
        {            
            m_strBez = strBez;
            m_dblFaktor_Verbrauch = dblFaktor_Verbrauch;
            m_dblFaktor_Produktion = dblFaktor_Produktion;
        }

        public int getGesamtBedarfByPopulation(int intEinwohner)
        {
            this.m_intBedarfDelta = -1 * Convert.ToInt32(intEinwohner * this.m_dblFaktor_Verbrauch);

            return Convert.ToInt32(intEinwohner * this.m_dblFaktor_Verbrauch);
        }

        
    }
    
}
