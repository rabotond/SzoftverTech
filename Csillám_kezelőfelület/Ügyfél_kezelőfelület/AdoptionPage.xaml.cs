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
using System.ComponentModel;

using AdatKezelő;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for AdoptionPage.xaml
    /// </summary>
    public partial class AdoptionPage : Window
    {
        AdaptionPageViewModel VM;
        IÜgyfél_kezelő ügyfél = new Ügyfél_Kezelő();
        Ügyfél_Kezelő dbhozzáférés = new Ügyfél_Kezelő();
        ALLAT atadommajdallat;
        user bejelentkezettUser;
        public AdoptionPage(user bejelentkezettUser)
        {
            VM = new AdaptionPageViewModel();
            atadommajdallat = new ALLAT();
            DataContext = VM;
            this.bejelentkezettUser = bejelentkezettUser;
            InitializeComponent();
        }

        private void Keresés(object sender, RoutedEventArgs e)
        {
            if (  ügyfél.Összetett_keresés(steril(), fiuvagylany(), szin.ToString(), VM.SelectedFaj,név.ToString()).Any()==true)
            {
                VM.Allatok = ügyfél.Összetett_keresés(steril(), fiuvagylany(), szin.ToString(), VM.SelectedFaj, név.ToString());
                
            }
            else
            {
                MessageBox.Show("Nincs ilyen állat. Kérlek adj meg más feltételeket");
            }
            VM.Allatok = ügyfél.Összetett_keresés(steril(), fiuvagylany(), szin.ToString(), VM.SelectedFaj, név.ToString());
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.Selectedallat!=null)
            {
                atadommajdallat = VM.Selectedallat;
                ReservationPage other = new ReservationPage(atadommajdallat);
                this.Close();
                other.Show();    
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> nodup = new List<string>();
            foreach (var item in dbhozzáférés.Db.ALLAT)
            {
                nodup.Add(item.FAJTA);
            }
            VM.Faj = nodup.Distinct().ToList();
        }
        bool fiuvagylany ()
        {
            if (VM.Fiu == "nőstény")
            {
                return true;
            }
            return false;
        }
        bool steril ()
        {
            if (oltas.IsChecked ==true)
            {
                return true;
            }
            return false;
        }
       

            
    }
    class AdaptionPageViewModel : INotifyPropertyChanged
    {
        ALLAT selectedallat;
        
        public ALLAT Selectedallat
        {
            get { return selectedallat; }
            set { selectedallat = value; }
        }
        List<ALLAT> allatok;

        public List<ALLAT> Allatok
        {
            get { return allatok; }
            set { allatok = value; }
        }
        public AdaptionPageViewModel ()
        {
            allatok = new List<ALLAT>();
            faj = new List<string>();
            fiulany = new List<string>();
            fiulany.Add("hím");
            fiulany.Add("nőstény");
        }
        List<string> faj;

        public List<string> Faj
        {
            get { return faj; }
            set { faj = value; OnPropertyChanged("Faj"); }
        }
        string selectedFaj;

        public string SelectedFaj
        {
            get { return selectedFaj; }
            set { selectedFaj = value; }
        }
        List<string> fiulany;

        public List<string> Fiulany
        {
            get { return fiulany; }
            set { fiulany = value; }
        }
        string fiu;

        public string Fiu
        {
            get { return fiu; }
            set { fiu = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
       
    }
}
