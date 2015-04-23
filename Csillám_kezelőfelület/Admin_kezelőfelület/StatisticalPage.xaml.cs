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
using Csillám_kezelőfelület.Admin_kezelőfelület;

namespace Csillamponi_Allatmenhely
{
   
    public partial class StatisticalPage : Window
    {
        Admin_kezelőfelület_businessLogic bl;
        Admin_kezelőfelület_viewmodel vm;
        public StatisticalPage( Admin_kezelőfelület_viewmodel ujvm)
        {
            InitializeComponent();
            vm = ujvm;
            bl = new Admin_kezelőfelület_businessLogic();
        }


        private void statisztikaToXML_Click(object sender, RoutedEventArgs e)//kimutatás készítés xml
        {
            Statisztika_típus fajta;
            if (adomanyos.IsChecked == true)
            {
                fajta = Statisztika_típus.adomány;
            }
            else if (állományos.IsChecked == true)
            {
                fajta = Statisztika_típus.állatállomány;
            }
            else if (ügyféles.IsChecked == true)
            {
                fajta = Statisztika_típus.ügyfélállomány;
            }
            else
            {
                fajta = Statisztika_típus.összetett;
            }
            vm.Stat = bl.Statisztikát_készít(fajta, (DateTime)kezdet.SelectedDate, (DateTime)vege.SelectedDate);
            vm.Stat.makeXmlfromStat();
            vm.Stat.xDoc.Save("statisztika.xml");
            MessageBox.Show("az xml fájl elkészült statisztika néven");
        }

        private void statisztikaOnGUI_Click(object sender, RoutedEventArgs e)//új ablak ahol látszik a statisztika amit elkészítettünk
        {
            Statisztika_típus fajta;
            if (adomanyos.IsChecked == true)
            {
                fajta = Statisztika_típus.adomány;
            }
            else if (állományos.IsChecked == true)
            {
                fajta = Statisztika_típus.állatállomány;
            }
            else if (ügyféles.IsChecked == true)
            {
                fajta = Statisztika_típus.ügyfélállomány;
            }
            else
            {
                fajta = Statisztika_típus.összetett;
            }
            vm.Stat = bl.Statisztikát_készít(fajta, (DateTime)kezdet.SelectedDate, (DateTime)vege.SelectedDate);

            Statistic statgui = new Statistic(vm);
            statgui.Show();
        }

        private void Visssza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
