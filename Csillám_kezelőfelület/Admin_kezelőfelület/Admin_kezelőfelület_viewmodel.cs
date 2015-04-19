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
        List<ALLAT> allatok;
        List<UGYFEL> ugyfelek;
        ALLAT valasztottAllat;
        UGYFEL valasztottUgyfel;

        public ALLAT ValasztottAllat
        {
            get { return valasztottAllat; }
            set { valasztottAllat = value; onPropChanged("ValasztottAllat"); }
        }

        public UGYFEL ValasztottUgyfel
        {
            get { return valasztottUgyfel; }
            set { valasztottUgyfel = value; onPropChanged("ValasztottUgyfel"); }
        }
        
        public Admin_kezelőfelület_viewmodel() {  }    

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
