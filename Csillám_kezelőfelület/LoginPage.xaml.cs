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
using AdatKezelő;

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

            Eledel_kezelo kezelo = new Eledel_kezelo();
            kezelo.eledelt_fogyaszt();
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow("admin");
            if (loginWindow.ShowDialog() == true)
            {
                AdministrationPage page = new AdministrationPage(loginWindow.bejelentkezettFelhasználó);
                this.Close();
                page.ShowDialog();
            }   
        }

        private void OrokbeadClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow("ügyfél");
            if (loginWindow.ShowDialog() == true)
            {
                PutInAnimal other = new PutInAnimal(loginWindow.bejelentkezettFelhasználó);
                this.Close();
                other.Show();
            }
        }

        private void OrokbefogadClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow("ügyfél");
            if (loginWindow.ShowDialog() == true)
            {
                AdoptionPage other = new AdoptionPage(loginWindow.bejelentkezettFelhasználó);
                this.Close();
                other.Show();
            }
        }

        private void AdományozClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow("ügyfél");
            if (loginWindow.ShowDialog() == true)
            {
                DonationPage donationPage = new DonationPage(loginWindow.bejelentkezettFelhasználó);
                donationPage.Show();
                this.Close();
            }
        }

        private void RegisztrációClick(object sender, RoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser(new UGYFEL());
            createUser.Show();
        }
    }
}
