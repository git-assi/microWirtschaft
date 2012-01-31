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
              : base(strName, 5)
          {
              
          }

          public clsKolonie(string strName, int intDefaultEinwohner)
              : base(strName, (strName == "Tortuga" ? 25 : intDefaultEinwohner))
        {
            
        }

          protected override void erzeugeGüter()
        {
            int intVerfügbareArbeiter = (this.intEinwohner > 20 ? 20 : this.intEinwohner);
            //clsWare objLebensmittel = this.getWare("Lebensmittel");
            this.addGutByName("Lebensmittel", intVerfügbareArbeiter);
        }

        
    }

    public class clsStadt : clsKolonie
    {
        public clsStadt(string strName)
            : base(strName, 20)
        {
            
        }

        protected override void erzeugeGüter()
        {
            base.erzeugeGüter();
            int intVerfügbareArbeiter = this.intEinwohner - 20;
            if (intVerfügbareArbeiter > 0)
                this.addGutByName("Werkzeug", intVerfügbareArbeiter);
        }

        protected new void verbraucheGüter()
        {
            //this.addWarenMenge("Werkzeug", -1 * (m_intEinwohner / 100));
        }
    }

     


    
}
