using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdatKezelő;
using System.ComponentModel;
using System.Data;


namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    class Admin_kezelőfelület_viewmodel:INotifyPropertyChanged
    {
        List<object> allatok;
        List<object> ugyfelek;

        string valasztottElem;
        
        public Admin_kezelőfelület_viewmodel()
        {

        }    

        public string ValasztottElem
        {
            get { return valasztottElem; }
            set { valasztottElem = value; onPropChanged("ValasztottElem"); }
        }

        public List<object> Allatok
        {
            get { return allatok; }
            set { allatok = value; onPropChanged("Allatok"); }
        }

        public List<object> Ügyfelek
        {
            get { return ugyfelek; }
            set { ugyfelek = value; onPropChanged("Ügyfelek"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }

        }
    }
}
