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
    /// <summary>
    /// Interaction logic for AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Window
    {
        Admin_kezelőfelület_businessLogic BL;
        Admin_kezelőfelület_viewmodel VM;
        UGYFEL bejelentkezettUser;

        public AdministrationPage(UGYFEL bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            BL = new Admin_kezelőfelület_businessLogic();
            VM = new Admin_kezelőfelület_viewmodel();
            DataContext = VM;

            Task.Factory.StartNew(() =>
            {

                var allatok = BL.FrissitAllat();
                Dispatcher.Invoke(new Action(() => VM.Allatok = allatok));
            });

            Task.Factory.StartNew(() =>
            {
                var ugyfelek = BL.FrissitÜgyfel();
                Dispatcher.Invoke(new Action(() => VM.Ügyfelek = ugyfelek));

            });
        }
        private void Frissit()
            {     Task.Factory.StartNew(() =>
            {

                var allatok = BL.FrissitAllat();
                Dispatcher.Invoke(new Action(() => VM.Allatok = allatok));
            });

            Task.Factory.StartNew(() =>
            {
                var ugyfelek = BL.FrissitÜgyfel();
                Dispatcher.Invoke(new Action(() => VM.Ügyfelek = ugyfelek));

            });}

        private void Frissit_Click(object sender, RoutedEventArgs e)//frissíti a datagrideket a friss adatbázis adatokkal
        {
            Frissit();
        }

        private void UjallatClick(object sender, RoutedEventArgs e)
        {
            NewAnimal page = new NewAnimal(bejelentkezettUser);
            page.Owner = this;
            page.Show();
        }

        private void Ujugyfel_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser(this.bejelentkezettUser);
            page.Show();
        }

        private void AllatTöröl_Click(object sender, RoutedEventArgs e)
        {
            if (VM.ValasztottAllat != null)
            {
                try
                {
                    BL.Állatot_töröl((AllatVM)állatgrid.SelectedItem);
                    VM.Allatok.Remove((AllatVM)állatgrid.SelectedItem);
                    MessageBox.Show("Törölve!");
                    Frissit();
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Ez egy üres rekord!");
                }
            }
            else
            {
                MessageBox.Show("Ez egy üres rekord!");
            }   
        }
        private void UgyfeletTöröl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.Ügyfelet_töröl((UgyfelVM)ugyfelgrid.SelectedItem);
                VM.Ügyfelek.Remove((UgyfelVM)ugyfelgrid.SelectedItem);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Ez egy üres rekord!");
            }
            
        }

        private void uygfelmodosit_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser(VM.ValasztottUgyfel, this.bejelentkezettUser); // Viewmodelben Fanni egy kis ötlet, help neked
            page.Show();
        }
        private void allatmodosit_Click(object sender, RoutedEventArgs e)
        {
            if (VM.ValasztottAllat != null)
            {
                NewAnimal page = new NewAnimal(VM.ValasztottAllat);
                page.Owner = this;
                page.Show();
            }
            else
            {
                MessageBox.Show("Ez egy üres rekord!");
            }
        }

        private void Kimutatas_Click(object sender, RoutedEventArgs e)
        {
            StatisticalPage page = new StatisticalPage(VM);
            page.Owner = this;
            page.Show();
        }

        private void Eledelek_Click(object sender, RoutedEventArgs e)
        {
            MaterialManagement page = new MaterialManagement(BL,VM);
            page.Owner = this;
            page.Show();
        }

        private void KennelSyn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.KennelTablaSync();
                MessageBox.Show("Kennel tábla frissítve!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült szinkronizálni a kennel táblát. \n Hiba részletei: " + ex.ToString());
            }
        }

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
}
