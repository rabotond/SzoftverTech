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
using Csillamponi_Allatmenhely;
using AdatKezelő;

namespace Csillám_kezelőfelület
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginWindowViewModel loginWindowViewModel;
        public UGYFEL bejelentkezettFelhasználó;
        string hova;
        public LoginWindow(string hova)
        {
            InitializeComponent();
            loginWindowViewModel = new LoginWindowViewModel();
            this.DataContext = loginWindowViewModel;
            this.hova = hova;
        }

        private void MégseClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BelépésClick(object sender, RoutedEventArgs e)
        {
            this.loginWindowViewModel.Jelszó = passwordBoxJelszó.Password;
            bool sikeres = loginWindowViewModel.Belépés(hova, out this.bejelentkezettFelhasználó);
            this.DialogResult = sikeres;
        }
    }
    class LoginWindowViewModel
    {
        LoginWindowBusinessLogic loginWindowBusinessLogic;
        public LoginWindowViewModel()
        {
            loginWindowBusinessLogic = new LoginWindowBusinessLogic();
        }
        string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        string jelszó;
        public string Jelszó
        {
            get { return jelszó; }
            set { jelszó = value; }
        }
        public bool Belépés(string hova, out UGYFEL felhasználó)
        {
            if (hova == "admin")
            {
                return loginWindowBusinessLogic.BelépésAdmin(this.username, this.jelszó, out felhasználó);
            }
            else
            {
                return loginWindowBusinessLogic.BelépésÜgyfél(this.username, this.jelszó, out felhasználó);
            }
        }

    }
    class LoginWindowBusinessLogic
    {
        Admin_kezelő adminKezelő;
        public LoginWindowBusinessLogic()
        {
            adminKezelő = new Admin_kezelő();
        }
        public bool BelépésAdmin(string username, string jelszó, out UGYFEL felhasználó)
        {
            var a = adminKezelő.Db.UGYFEL.Where(x => x.USERNAME == username);
            felhasználó = a.First();
            if (a.Count() == 0)
            {
                MessageBox.Show("Nincs ilyen felhasználó!");
                return false;
            }
            
            if (felhasználó.ISADMIN == false)
            {
                MessageBox.Show("Ide csak adminisztrációs fiókkal léphet be!");
                return false;
            }
            else if (felhasználó.ISADMIN == true && felhasználó.PASSWORD == jelszó)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool BelépésÜgyfél(string username, string jelszó, out UGYFEL felhasználó)
        {
            var a = adminKezelő.Db.UGYFEL.Where(x => x.USERNAME == username);
            felhasználó = a.First();
            if (a.Count() == 0)
            {
                MessageBox.Show("Nincs ilyen felhasználó!");
                return false;
            }
            if (felhasználó.PASSWORD == jelszó)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
