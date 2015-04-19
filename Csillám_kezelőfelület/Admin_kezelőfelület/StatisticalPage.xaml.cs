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
        Statisztika stat;
        public StatisticalPage()
        {
            InitializeComponent();
            bl = new Admin_kezelőfelület_businessLogic();
           
        }


        private void statisztikaToXML_Click(object sender, RoutedEventArgs e)//kimutatás készítés xml
        {
            string fajta;
            if(adomanyos.IsChecked==true)
            {
                fajta="adomány";
            }
            else if(állományos.IsChecked==true)
            {
                 fajta="állomány";
            }
            else
            {
                 fajta="összetett";
            }
           stat= bl.Statisztikát_készít(fajta,(DateTime)kezdet.SelectedDate,(DateTime)vege.SelectedDate);
        }

        private void statisztikaOnGUI_Click(object sender, RoutedEventArgs e)
        {
            //új ablak ahol látszik a statisztika amit elkészítettünk
        }
    }
}
