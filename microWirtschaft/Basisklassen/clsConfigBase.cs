using System;
using System.Collections.Generic;
using System.Text;

namespace piratesWirtschaft.BasisKlassen
{
    public abstract class clsSiedlungConfig_base
    {
        public List<clsSiedlung> m_lisDefaultSiedlungen;

        public clsSiedlungConfig_base(List<clsSiedlung> lisDefaultSiedlungen)
        {
            m_lisDefaultSiedlungen = lisDefaultSiedlungen;
        }

    }

    public abstract class clsWarenConfig_base
    {
        public List<clsWare> m_lisDefaultWaren
        { 
            get
            {
                return this.createWarenDefaultList();
            }
        }
        
        protected abstract List<clsWare> createWarenDefaultList();
    }
}
