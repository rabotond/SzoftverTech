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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            
            AdministrationPage page = new AdministrationPage();
            this.Close();
            page.ShowDialog();
            
        
        }

        private void OrokbeadClick(object sender, RoutedEventArgs e)
        {
            PutInAnimal other = new PutInAnimal();
            this.Close();
            other.Show();
        }

        private void OrokbefogadClick(object sender, RoutedEventArgs e)
        {
            AdoptionPage other = new AdoptionPage();
            this.Close();
            other.Show();
        }
    }
}
