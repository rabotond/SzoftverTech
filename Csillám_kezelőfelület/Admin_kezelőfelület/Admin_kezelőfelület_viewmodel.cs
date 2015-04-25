using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdatKezelő;
using System.ComponentModel;
using System.Data;
using System.Collections;


namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
  public  class Admin_kezelőfelület_viewmodel:INotifyPropertyChanged
    {
        List<AllatVM> allatok;
        List<UgyfelVM> ugyfelek; // help Fanninak:  Ha ez UgyfelVM akkor hogyan lesz belőle Ugyfel amit átadsz majd mint válaszottugyfel a NewUsernek. Módosításra. Szerintem ezért Null a NewUser Konstruktora
        IEnumerable eledel_kennel;

        public IEnumerable Eledel_kennel
        {
            get { return eledel_kennel; }
            set { eledel_kennel = value; onPropChanged("Eledel_kennel"); }
        } 
      AllatVM valasztottAllat;
        UgyfelVM valasztottUgyfel;
        Statisztika stat;

        public Statisztika Stat
        {
            get { return stat; }
            set { stat = value; onPropChanged("Stat"); }
        }

        public AllatVM ValasztottAllat
        {
            get { return valasztottAllat; }
            set { valasztottAllat = value; onPropChanged("ValasztottAllat"); }
        }

        public UgyfelVM ValasztottUgyfel
        {
            get { return valasztottUgyfel; }
            set { valasztottUgyfel = value; onPropChanged("ValasztottUgyfel"); }
        }
        
        public Admin_kezelőfelület_viewmodel() {  }

        public List<AllatVM> Allatok
        {
            get { return allatok; }
            set { allatok = value; onPropChanged("Allatok"); }
        }

        public List<UgyfelVM> Ügyfelek
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
