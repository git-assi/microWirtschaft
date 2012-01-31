using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{
    public abstract class clsSiedlungConfig_base
    {
        public List<string> m_lisDefaultSiedlungsNamen;

        public clsSiedlungConfig_base(List<string> lisDefaultSiedlungsNamen)
        {            
            m_lisDefaultSiedlungsNamen = lisDefaultSiedlungsNamen;
        }


    }

    public abstract class clsWarenConfig_base
    {
        public List<clsWare> m_lisDefaultWaren;

        public clsWarenConfig_base(List<clsWare> lisDefaultWaren)
        {
            m_lisDefaultWaren = lisDefaultWaren;
        }       
    }
}
