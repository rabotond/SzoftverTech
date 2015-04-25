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
        UgyfelVM módosítandó;
        UGYFEL bejelentkezettUser;
        public CreateUser(UGYFEL bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            createUserViewModel = new CreateUserViewModel();
            this.DataContext = createUserViewModel;
            this.módosítandó = null;
        }
        public CreateUser(UgyfelVM modositando, UGYFEL bejelentkezettUser)
        {
            // modosítandó null-al érkezik helyből ott a hiba. már hívásnál a ValaszottUgyfel Nem Ugyfel típusú vagy mi ezért null de nekem még nagyon idegen az adminkezelő sorry nem találtam meg hol a hiba :)
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            this.módosítandó = modositando;
            this.DataContext = modositando;
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UgyfelVM)
            {
                createUserViewModel.ÜgyfélMódosítás(this.módosítandó); //még nem jó
                UGYFEL módosítandó = (UGYFEL)this.DataContext;
                createUserViewModel.ÜgyfélMódosítás(this.módosítandó);
            }
            if (this.DataContext is CreateUserViewModel)
            {
                if (textBoxEmail.Text != "" &&
                    textBoxHszam.Text != "" &&
                    textBoxIrszám.Text != "" &&
                    textBoxKeresztnév.Text != "" &&
                    textBoxTelefon.Text != "" &&
                    textBoxTelepülés.Text != "" &&
                    textBoxUtca.Text != "" &&
                    textBoxVezetéknév.Text != "" &&
                    textBoxUsername.Text != "" &&
                    passwordBoxPassword1.Password != "" &&
                    passwordBoxPassword2.Password != "")
                {
                    if (passwordBoxPassword1.Password == passwordBoxPassword2.Password)
                    {
                        createUserViewModel.PASSWORD = passwordBoxPassword1.Password;
                        if (createUserViewModel.ÚjÜgyfél())
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("A két jelszó mezőnek egyeznie kell!");
                    }
                }
                else
                {
                    MessageBox.Show("Minden adatot meg kell adni!");
                }
            }
        }

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
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
        string username;
        public string USERNAME
        {
            get { return username; }
            set { username = value; OnPropertyChanged("USERNAME"); }
        }
        string password;
        public string PASSWORD
        {
            get { return password; }
            set { password = value; OnPropertyChanged("PASSWORD"); }
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
        public bool ÚjÜgyfél()
        {
            UgyfelVM ügyfél = new UgyfelVM();
            ügyfél.EMAIL = this.EMAIL;
            ügyfél.HAZSZAM = decimal.Parse(this.HAZSZAM);
            ügyfél.IRSZ = decimal.Parse(this.IRSZ);
            ügyfél.KERESZTNEV = this.KERESZTNEV;
            ügyfél.TELEFON = decimal.Parse(this.TELEFON);
            ügyfél.UGYFELID = Guid.NewGuid();
            ügyfél.UTCA = this.UTCA;
            ügyfél.VAROS = this.VAROS;
            ügyfél.USERNAME = this.username;
            ügyfél.PASSWORD = this.password;
            ügyfél.VEZETEKNEV = this.VEZETEKNEV;
            ügyfél.isadmin = false;
            return createUserBusinessLogic.Mentés("új", ügyfél);
        }
        
        public void ÜgyfélMódosítás(UgyfelVM ügyfél)
        {
            createUserBusinessLogic.Mentés("módosít", ügyfél);
        }
    }
    class CreateUserBusinessLogic
    {
        IAdmin_kezelő adminKezelő;
        Admin_kezelő dbCheck;
        public CreateUserBusinessLogic()
        {
            adminKezelő = new Admin_kezelő();
            dbCheck = new Admin_kezelő();
        }
        public bool Mentés(string mit, UgyfelVM ügyfél)
        {
            var a = dbCheck.Db.UGYFEL.Where(x => x.USERNAME == ügyfél.USERNAME);
            if (a.Count() == 0)
            {
                if (mit == "új")
                {
                    adminKezelő.Ügyfelet_hozzáad(ügyfél);
                    MessageBox.Show("Regisztráció kész!");
                    return true;
                }
                if (mit == "módosít")
                {
                    adminKezelő.Ügyfelet_módosít(ügyfél);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Ezzel a felhasználónévvel már létezik felhasználó!\nKérlek válassz másik felhasználónevet!");
                return false;
            }
            return false;
        }
    }
}
