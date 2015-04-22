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
using Csillám_kezelőfelület;

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
            /*
             * BEJELENTKEZÉS RÉSZHEZ, NE TÖRÖLD!!!
             * */
            //LoginWindow loginWindow = new LoginWindow("admin");
            //if (loginWindow.ShowDialog() == true)
            //{
            //    AdministrationPage page = new AdministrationPage(loginWindow.bejelentkezettFelhasználó);
            //    this.Close();
            //    page.ShowDialog();
            //}

            AdministrationPage page = new AdministrationPage(null);
            this.Close();
            page.ShowDialog();
            
        
        }

        private void OrokbeadClick(object sender, RoutedEventArgs e)
        {
            /*
             * BEJELENTKEZÉS RÉSZHEZ, NE TÖRÖLD!!!
             * */
            //LoginWindow loginWindow = new LoginWindow("ügyfél");
            //if (loginWindow.ShowDialog() == true)
            //{
            //    PutInAnimal other = new PutInAnimal(loginWindow.bejelentkezettFelhasználó);
            //    this.Close();
            //    other.Show();
            //}
            PutInAnimal other = new PutInAnimal(null);
            this.Close();
            other.Show();
        }

        private void OrokbefogadClick(object sender, RoutedEventArgs e)
        {
            /*
             * BEJELENTKEZÉS RÉSZHEZ, NE TÖRÖLD!!!
             * */
            //LoginWindow loginWindow = new LoginWindow("ügyfél");
            //if (loginWindow.ShowDialog() == true)
            //{
            //    AdoptionPage other = new AdoptionPage(loginWindow.bejelentkezettFelhasználó);
            //    this.Close();
            //    other.Show();
            //}
            AdoptionPage other = new AdoptionPage(null);
            this.Close();
            other.Show();
        }

        private void AdományozClick(object sender, RoutedEventArgs e)
        {
            DonationPage donationPage = new DonationPage();
            donationPage.Show();
            this.Close();
        }
    }
}
