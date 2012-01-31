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
                this.addWare(aktWare);
            }
        }
        private void addWare(clsWare newWare)
        {
            m_dicWaren.Add(newWare.m_strBez, newWare);
        }

        protected clsWare getWare(string strBez)
        {
            clsWare obj = m_dicWaren[strBez];
            //obj.intMenge = 0;
            return obj;
        }
        
        protected Double getWarenFaktor(string strBez)
        {
            return getWare(strBez).m_dblFaktor_Verbrauch;
        }
        
        protected int getWarenMenge(string strBez)
        {
            return getWare(strBez).intMenge;
        }

        protected void addWarenMenge(string strBez, int intAddMenge)
        {
            getWare(strBez).intMenge += intAddMenge;
        }
        
        protected string inventur()
        {
            string strMeldung = "";
            foreach (clsWare aktWare in m_dicWaren.Values)
            {
                strMeldung += aktWare.m_strBez + ": " + aktWare.intMenge.ToString() + Environment.NewLine;
            }
            return strMeldung;
        }
    }
}
