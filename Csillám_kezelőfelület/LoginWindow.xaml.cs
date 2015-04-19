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
            this.DialogResult = loginWindowViewModel.Belépés(hova);
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
        public bool Belépés(string hova)
        {
            if (hova == "admin")
            {
                return loginWindowBusinessLogic.BelépésAdmin(this.username, this.jelszó);
            }
            else
            {
                return loginWindowBusinessLogic.BelépésÜgyfél(this.username, this.jelszó);
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
        public bool BelépésAdmin(string username, string jelszó)
        {
            var a = adminKezelő.Db.user.Where(x => x.name == username);
            if (a.Count() == 0)
            {
                MessageBox.Show("Nincs ilyen felhasználó!");
                return false;
            }
            user User = a.First();
            if (User.C_isadmin == false)
            {
                MessageBox.Show("Ide csak adminisztrációs fiókkal léphet be!");
                return false;
            }
            else if (User.C_isadmin == true && User.password == jelszó)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool BelépésÜgyfél(string username, string jelszó)
        {
            var a = adminKezelő.Db.user.Where(x => x.name == username);
            if (a.Count() == 0)
            {
                MessageBox.Show("Nincs ilyen felhasználó!");
                return false;
            }
            user User = a.First();
            if (User.password == jelszó)
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
