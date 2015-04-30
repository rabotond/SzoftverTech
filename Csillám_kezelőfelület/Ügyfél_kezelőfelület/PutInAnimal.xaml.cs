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
    public partial class PutInAnimal : Window
    {
        UGYFEL bejelentkezettUser;
        public PutInAnimal(UGYFEL bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
        }

        private void ÜreshelyekLekérdezése(object sender, RoutedEventArgs e)
        {
            QueriePlaces other = new QueriePlaces(this.bejelentkezettUser);
            this.Close();
            other.Show();
        }

        private void Adatfelvétel(object sender, RoutedEventArgs e)
        {
            NewAnimal other = new NewAnimal(bejelentkezettUser);
           // this.Close();
            other.Show();
        }

        private void Támogatás(object sender, RoutedEventArgs e)
        {
            DonationPage oter = new DonationPage(this.bejelentkezettUser);
            this.Close();
            oter.Show();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            LoginPage qpWindow = new LoginPage();
            qpWindow.Show();
            this.Close();
            //MessageBox.Show("ha lesz authentikáció akkor uncomment");
        }
    }
}
