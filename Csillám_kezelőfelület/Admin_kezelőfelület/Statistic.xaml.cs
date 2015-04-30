using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdatKezelő;
using System.Collections;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        Admin_kezelőfelület_viewmodel VM;
        public Statistic(Admin_kezelőfelület_viewmodel vm)
        {
            InitializeComponent();
            VM = vm;
            List<object> statVM = new List<object>();
            foreach (Statisztika_adatrecord akt in VM.Stat.Napok)
            {
                if (vm.Stat.Tipus == Statisztika_típus.adomány && (akt.befolytEledelAdomany_kg!=0 || akt.befolytPenzadomany_huf!=0))
                {
                     statVM.Add(new { NAP = akt.Nap, befolytPenz = akt.befolytPenzadomany_huf, befoyltEledel = akt.befolytEledelAdomany_kg });
                }
                else if (vm.Stat.Tipus == Statisztika_típus.állatállomány && (akt.hozottAllat != 0 || akt.elvittAllat != 0))
                {
                    statVM.Add(new { NAP = akt.Nap, hozott_Állatok_száma = akt.hozottAllat, elvitt_állatok_száma = akt.elvittAllat });
                }
                else if (vm.Stat.Tipus == Statisztika_típus.ügyfélállomány && (akt.regisztraltDarab != 0))
                {
                    statVM.Add(new { NAP = akt.Nap, Regisztraltak = akt.regisztraltDarab });
                }
                else if (vm.Stat.Tipus == Statisztika_típus.ugyfeladatok && (akt.pénztAdomanyozott_huf != 0 || akt.eledeltAdomanyozott_kg !=0 ))
                {
                    statVM.Add(new { ÜGYFÉL = akt.Ugyfel.UGYFELID, pénztAdomanyozott_huf = akt.pénztAdomanyozott_huf, eledeltAdomanyozott_kg=akt.eledeltAdomanyozott_kg });
                }
                else//összetett
                {
                    if (akt.befolytEledelAdomany_kg != 0 || akt.befolytPenzadomany_huf != 0 || akt.hozottAllat != 0 || akt.elvittAllat != 0 || akt.regisztraltDarab != 0)
                    {
                        statVM.Add(new { NAP = akt.Nap, Regisztraltak = akt.regisztraltDarab,
                            hozott_Állatok_száma = akt.hozottAllat, 
                            elvitt_állatok_száma = akt.elvittAllat, 
                            befolytPenz = akt.befolytPenzadomany_huf, 
                            befoyltEledel = akt.befolytEledelAdomany_kg });
                    }
                }
            }
            IEnumerable stat_adatok = statVM;
            statgrid.ItemsSource = null;
            statgrid.ItemsSource = stat_adatok;      
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
