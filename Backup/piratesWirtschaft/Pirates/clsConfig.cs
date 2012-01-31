using System;
using System.Collections.Generic;
using System.Text;
using piratesWirtschaft.Welt;
using piratesWirtschaft.BasisKlassen;

namespace piratesWirtschaft.Config
{
    public class clsWeltConfig : BasisKlassen.clsSiedlungConfig_base
    {       
        public static clsWeltConfig WarenConfig = new clsWeltConfig();

        public clsWeltConfig()
            : base(new List<string>() { "Port Royal", "Tortuga" })
        {
            
        }

        
    }

    public class clsWarenConfig : BasisKlassen.clsWarenConfig_base
    {                         
            public static clsWarenConfig WarenConfig = new clsWarenConfig();

            public clsWarenConfig() : base(new List<clsWare>() { new clsLebensmittel(), new clsWerkzeug(), new clsLuxusgüter(), new clsExportgüter() })
            {
                
            }

            public class clsLebensmittel : clsWare
            {
                public clsLebensmittel()
                    : base("Lebensmittel", 1.1, 1)
                {
                    this.intMenge = 20;
                }
            }           
            public class clsWerkzeug : clsWare
            {
                public clsWerkzeug()
                    : base("Werkzeug", 0.2, 0.5)
                {
                    this.intMenge = 5;
                }
            }
            public class clsLuxusgüter : clsWare
            {
                public clsLuxusgüter()
                    : base("Luxusgüter", 0.25, 0.45)
                {
                    this.intMenge = 0;
                }
            }
            public class clsExportgüter : clsWare
            {
                public clsExportgüter()
                    : base("Exportgüter", 0, 0.1)
                {
                    this.intMenge = 0;
                }
            }
    }
}
