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
        private int m_intMenge = 0;

        public int intMenge
        {
            get { return m_intMenge; }
            set
            {
                m_intMenge = value;
                if (m_intMenge < 0)
                    m_intMenge = 0;
            }
        }

        public clsWare(string strBez, Double dblFaktor_Verbrauch, Double dblFaktor_Produktion)
        {            
            m_strBez = strBez;
            m_dblFaktor_Verbrauch = dblFaktor_Verbrauch;
            m_dblFaktor_Produktion = dblFaktor_Produktion;
        }
    }
    
}
