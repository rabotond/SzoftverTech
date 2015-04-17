using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdatKezelő;
using System.ComponentModel;


namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    class Admin_kezelőfelület_viewmodel:INotifyPropertyChanged
    {
        List<ALLAT> allatok;
        List<UGYFEL> ugyfelek;
        string valasztottElem;
        
        public Admin_kezelőfelület_viewmodel()
        {

        }    

        public string ValasztottElem
        {
            get { return valasztottElem; }
            set { valasztottElem = value; onPropChanged("ValasztottElem"); }
        }

        public List<ALLAT> Allatok
        {
            get { return allatok; }
            set { allatok = value; onPropChanged("Allatok"); }
        }

        public List<UGYFEL> Ügyfelek
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
