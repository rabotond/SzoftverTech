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

        public Admin_kezelőfelület_viewmodel()
        {

        }

        List<ALLAT> allatok;

        public List<ALLAT> Allatok
        {
            get { return allatok; }
            set { allatok = value; onPropChanged("Allatok"); }
        }
        List<UGYFEL> ugyfelek;

        public List<UGYFEL> Ugyfelek
        {
            get { return ugyfelek; }
            set { ugyfelek = value; onPropChanged("Ugyfelek"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropChanged(string p)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }

        }
    }
}
