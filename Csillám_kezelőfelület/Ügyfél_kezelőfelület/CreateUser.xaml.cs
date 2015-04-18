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
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        CreateUserViewModel createUserViewModel;
        UGYFEL módosítandó;
        public CreateUser()
        {
            InitializeComponent();
            createUserViewModel = new CreateUserViewModel();
            this.DataContext = createUserViewModel;
        }
        public CreateUser(UGYFEL modositando)
        {
            InitializeComponent();
            this.módosítandó = modositando;
            this.DataContext = modositando;
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UGYFEL)
            {
                createUserViewModel.ÜgyfélMódosítás(módosítandó); //még nem jó
                //UGYFEL módosítandó = (UGYFEL)this.DataContext;
                //createUserViewModel.ÜgyfélMódosítás(módosítandó);
            }
            if (this.DataContext is CreateUserViewModel)
            {
                createUserViewModel.ÚjÜgyfél();
            }
        }

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
            AdministrationPage administrationPage = new AdministrationPage();
            administrationPage.Show();
            this.Close();
        }
    }
    class CreateUserViewModel : INotifyPropertyChanged
    {
        string vezetéknév;
        public string VEZETEKNEV
        {
            get { return vezetéknév; }
            set { vezetéknév = value; OnPropertyChanged("VEZETEKNEV"); }
        }
        string keresztnév;
        public string KERESZTNEV
        {
            get { return keresztnév; }
            set { keresztnév = value; OnPropertyChanged("KERESZTNEV"); }
        }
        string irsz;
        public string IRSZ
        {
            get { return irsz; }
            set { irsz = value; OnPropertyChanged("IRSZ"); }
        }
        string város;
        public string VAROS
        {
            get { return város; }
            set { város = value; OnPropertyChanged("VAROS"); }
        }
        string utca;
        public string UTCA
        {
            get { return utca; }
            set { utca = value; OnPropertyChanged("UTCA"); }
        }
        string házszám;
        public string HAZSZAM
        {
            get { return házszám; }
            set { házszám = value; OnPropertyChanged("HAZSZAM"); }
        }
        string telefon;
        public string TELEFON
        {
            get { return telefon; }
            set { telefon = value; OnPropertyChanged("TELEFON"); }
        }
        string email;
        public string EMAIL
        {
            get { return email; }
            set { email = value; OnPropertyChanged("EMAIL"); }
        }
        CreateUserBusinessLogic createUserBusinessLogic;
        public CreateUserViewModel()
        {
            createUserBusinessLogic = new CreateUserBusinessLogic();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void ÚjÜgyfél()
        {
            UGYFEL ügyfél = new UGYFEL();
            ügyfél.EMAIL = this.EMAIL;
            ügyfél.HAZSZAM = decimal.Parse(this.HAZSZAM);
            ügyfél.IRSZ = decimal.Parse(this.IRSZ);
            ügyfél.KERESZTNEV = this.KERESZTNEV;
            ügyfél.TELEFON = decimal.Parse(this.TELEFON);
            ügyfél.UGYFELID = Guid.NewGuid();
            ügyfél.UTCA = this.UTCA;
            ügyfél.VAROS = this.VAROS;
            ügyfél.VEZETEKNEV = this.VEZETEKNEV;
            createUserBusinessLogic.Mentés("új", ügyfél);
        }
        public void ÜgyfélMódosítás(UGYFEL ügyfél)
        {
            createUserBusinessLogic.Mentés("módosít", ügyfél);
        }
    }
    class CreateUserBusinessLogic
    {
        IAdmin_kezelő adminKezelő;
        public CreateUserBusinessLogic()
        {
            adminKezelő = new Admin_kezelő();
        }
        public void Mentés(string mit, UGYFEL ügyfél)
        {
            if (mit == "új")
            {
                adminKezelő.Ügyfelet_hozzáad(ügyfél);
            }
            if (mit == "módosít")
            {
                adminKezelő.Ügyfelet_módosít(ügyfél);
            }
        }
    }
}
