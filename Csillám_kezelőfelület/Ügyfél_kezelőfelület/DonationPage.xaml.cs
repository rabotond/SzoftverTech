using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AdatKezelő;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    ///     Interaction logic for DonationPage.xaml
    /// </summary>
    public partial class DonationPage : Window
    {
        private readonly DonationPageViewModel donationPageViewModel;
        UGYFEL bejelentkezettFelhasználó;
        public DonationPage(UGYFEL bejelentkezettFelhasználó)
        {
            InitializeComponent();
            this.bejelentkezettFelhasználó = bejelentkezettFelhasználó;
            donationPageViewModel = new DonationPageViewModel();
            this.DataContext = donationPageViewModel;
        }

        private void AdományozClick(object sender, RoutedEventArgs e)
        {
            if (textBoxMennyiség.Text != "")
            {
                donationPageViewModel.Username = this.bejelentkezettFelhasználó.USERNAME;
                if (radioButtonÉtel.IsChecked == true)
                {
                    donationPageViewModel.Adományoz(Adomány_típus.Eledel);
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
            var loginPage = new LoginPage();
            loginPage.Show();
            Close();
        }


        private void textBoxMennyiség_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var jo = (e.Key >= Key.D0 && e.Key <= Key.D9)
                     ||(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                     || e.Key == Key.Back
                     || e.Key == Key.Tab
                     || e.Key == Key.Left
                     || e.Key == Key.Right
                     || e.Key == Key.Delete;
            e.Handled = !jo;
        }
    }

    internal class DonationPageBusinessLogic
    {
        private readonly IÜgyfél_kezelő ügyfélKezelő;

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

    internal class DonationPageViewModel : INotifyPropertyChanged
    {
        private readonly DonationPageBusinessLogic donationPageBusinessLogic;
        private string mennyiség;
        private string username;

        public DonationPageViewModel()
        {
            donationPageBusinessLogic = new DonationPageBusinessLogic();
        }

        public Adomány_típus Típus { get; set; }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Mennyiség
        {
            get { return mennyiség; }
            set
            {
                mennyiség = value;
                OnPropertyChanged("Mennyiség");
            }
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
            donationPageBusinessLogic.Adományoz(típus, int.Parse(mennyiség), username);
        }
    }
}