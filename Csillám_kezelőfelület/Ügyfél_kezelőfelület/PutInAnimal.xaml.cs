using AdatKezelő;
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
    /// Interaction logic for AdoptionPage.xaml
    /// </summary>
    public partial class PutInAnimal : Window
    {
        Ugyfelkezelo ugyfelkezelo;
        public PutInAnimal()
        {
            InitializeComponent();
            ugyfelkezelo = new Ugyfelkezelo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QueriePlaces querieWindow = new QueriePlaces(ugyfelkezelo);
            querieWindow.Show();
        }
    }
}
