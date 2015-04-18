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
using System.ComponentModel;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for DonationPage.xaml
    /// </summary>
    public partial class DonationPage : Window
    {
        DonationPageBusinessLogic donationPageBusinessLogic;
        DonationPageViewModel donationPageViewModel;
        public DonationPage()
        {
            InitializeComponent();
            donationPageBusinessLogic = new DonationPageBusinessLogic();
            donationPageViewModel = new DonationPageViewModel();
        }

        private void AdományozClick(object sender, RoutedEventArgs e)
        {
            if (textBoxMennyiség.Text != "" && textBoxUsername.Text != "")
            {
                if (radioButtonÉtel.IsChecked == true)
                {
                    donationPageViewModel.Adományoz(Adomány_típus.Étel);
                }
                else if (radioButtonPénz.IsChecked == true)
                {
                    donationPageViewModel.Adományoz(Adomány_típus.Pénz);
                }
                else
                {
                    MessageBox.Show("Kérem válasszon adomány típust!");
                }
            }
            else
            {
                MessageBox.Show("Nincs kitöltve minden adat!");
            }
        }

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Binding mennyiségBinding = new Binding("Mennyiség");
            mennyiségBinding.Source = donationPageViewModel.Mennyiség;
            textBoxMennyiség.SetBinding(TextBox.TextProperty, mennyiségBinding);
            Binding usernameBinding = new Binding("Username");
            usernameBinding.Source = donationPageViewModel.Username;
            textBoxUsername.SetBinding(TextBox.TextProperty, usernameBinding);
        }
    }
    class DonationPageBusinessLogic
    {
        IÜgyfél_kezelő ügyfélKezelő;
        public DonationPageBusinessLogic()
        {
            ügyfélKezelő = new Ügyfél_Kezelő();
        }
        public void Adományoz(Adomány_típus típus, int mennyiség, string ki)
        {
            ügyfélKezelő.Adományoz(típus, mennyiség, ki);
            MessageBox.Show("Köszönjük!");
        }
    }
    class DonationPageViewModel : INotifyPropertyChanged
    {
        Adomány_típus típus;
        public Adomány_típus Típus
        {
            get { return típus; }
            set { típus = value; }
        }
        string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }
        int mennyiség;
        public int Mennyiség
        {
            get { return mennyiség; }
            set { mennyiség = value; OnPropertyChanged("Mennyiség"); }
        }
        DonationPageBusinessLogic donationPageBusinessLogic;
        public DonationPageViewModel()
        {
            donationPageBusinessLogic = new DonationPageBusinessLogic();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void Adományoz(Adomány_típus típus)
        {
            donationPageBusinessLogic.Adományoz(típus, this.mennyiség, this.username);
        }
    }
}
