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

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for QueriePlaces.xaml
    /// </summary>
    public partial class QueriePlaces : Window
    {
        QueueuPlacesViewModel VM;
        public QueriePlaces()
        {
            InitializeComponent();
            VM = new QueueuPlacesViewModel();
        }


       

        private void Search(object sender, RoutedEventArgs e)
        {
            // Válaszott alapján  Select adatbázisban és mondjuk Messageboxba kiírni a db számot.
        }
    }
    class QueueuPlacesViewModel
    {
        List<string> fajok;

        public List<string> Fajok
        {
            get { return fajok; }
            set { fajok = value; }
        }
        string selectedFaj;

        public string SelectedFaj
        {
            get { return selectedFaj; }
            set { selectedFaj = value; }
        }
        void feltolt ()
        {
            Ügyfél_Kezelő a = new Ügyfél_Kezelő();
            
            
        }
    }
    
}
