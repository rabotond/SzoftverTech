using AdatKezelő;
using Csillám_kezelőfelület.Ügyfél_kezelőfelület;
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

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for QueriePlaces.xaml
    /// </summary>
    public partial class QueriePlaces : Window
    {
        public QueriePlaces(IUgyfelkezelo kezelo)
        {
            InitializeComponent();
            QueriePlacesViewModel vm = new QueriePlacesViewModel(kezelo);
            DataContext = vm.AllatFajok;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
