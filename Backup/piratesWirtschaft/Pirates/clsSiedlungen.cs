using System;
using System.Collections.Generic;
using System.Text;
using piratesWirtschaft.Welt;
using piratesWirtschaft.BasisKlassen;
using piratesWirtschaft.Config;

namespace piratesWirtschaft.Siedlungen
{  
      public class clsKolonie : clsSiedlung
    {
        public clsKolonie(string strName)
            : base(strName, new Config.clsWarenConfig.clsLebensmittel())
        {
            if (strName == "Tortuga")
                m_intEinwohner = 250;
            else
                m_intEinwohner = 175;
        }

        private void veränderePopulation()
        {
            if (m_intEinwohner >= 500)
            {
                //this = new clsStadt(this);
            }
        }
    }

    public class clsStadt : clsSiedlung
    {
        public clsStadt(clsKolonie objKolonie)
            : base(objKolonie.strName, new Config.clsWarenConfig.clsWerkzeug())
        {
            m_intEinwohner = 500;
        }

        protected new void verbraucheGüter()
        {
            //this.addWarenMenge("Werkzeug", -1 * (m_intEinwohner / 100));
        }
    }

     


    
}
