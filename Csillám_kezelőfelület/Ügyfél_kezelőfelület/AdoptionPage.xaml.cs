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
    /// Interaction logic for AdoptionPage.xaml
    /// </summary>
    public partial class AdoptionPage : Window
    {
        public AdoptionPage()
        {

            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // ide egy állatra való kattintáskor kéne az ID alapján azzal meghívni az előjegyez ( Reservation page) -et. Ami abból már kitudja listázni az állatnak az adatait.
        }
       

            
    }
}
