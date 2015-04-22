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

        private void Frissit_Click(object sender, RoutedEventArgs e)//frissíti a listboxokat a friss adatbázis adatokkal
        {
            VM.Allatok = BL.FrissitAllat();
            VM.Ügyfelek= BL.FrissitÜgyfel();
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
            this.Close();
             page.Show();
        }

        private void AllatTöröl_Click(object sender, RoutedEventArgs e)
        {
            BL.Állatot_töröl((ALLAT)állatgrid.SelectedItem);
            VM.Allatok.Remove((AllatVM)állatgrid.SelectedItem);
        }
        private void UgyfeletTöröl_Click(object sender, RoutedEventArgs e)
        {
            BL.Ügyfelet_töröl((UGYFEL)ugyfelgrid.SelectedItem);
            VM.Ügyfelek.Remove((UgyfelVM)ugyfelgrid.SelectedItem);
        }

        private void uygfelmodosit_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser(VM.ValasztottUgyfel, this.bejelentkezettUser); // Viewmodelben Fanni egy kis ötlet, help neked
            this.Close();
            page.Show();
        }
        private void allatmodosit_Click(object sender, RoutedEventArgs e)
        {
            NewAnimal page = new NewAnimal(VM.ValasztottAllat);
            page.Owner = this;
            page.Show();
        }

        private void Kimutatas_Click(object sender, RoutedEventArgs e)
        {
            StatisticalPage page = new StatisticalPage();
            page.Owner = this;
            page.Show();
        }

        private void Eledelek_Click(object sender, RoutedEventArgs e)
        {
            MaterialManagement page = new MaterialManagement();
            page.Owner = this;
            page.Show();
        }
    }
}
