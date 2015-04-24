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
using  System.IO;
using Path = System.IO.Path;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    
    
    public partial class ReservationPage : Window
    {
        ReservationPageViewModel VM;
        ALLAT allat = new ALLAT();
        Ügyfél_Kezelő kezelo;
        public ReservationPage(ALLAT kapottallat)
        {
            kezelo = new Ügyfél_Kezelő();
            VM = new ReservationPageViewModel(); 
            allat = kapottallat;
            InitializeComponent();
            Adatfeltölt();
            DataContext = VM;
            KépBetöltés();
        }

        private void Előjegyzés(object sender, RoutedEventArgs e)
        {
            kezelo.Előjegyeztet(allat);
            MessageBox.Show("Az állatodat sikeresen előjegyeztük örökbefogadáshoz. A staff felfogja feled venni a kapcsolatot a részletekkel kapcsolatban");
        }

        private void Regisztráció(object sender, RoutedEventArgs e)
        {
            CreateUser other = new CreateUser(null);
            this.Close();
            other.Show();
        }
        string Adatfeltölt ()
        {
            string animal = "név: " +allat.NEV+ Environment.NewLine+
                "Fajta: "+allat.FAJTA+Environment.NewLine+
                "Szül. Idő: "+ allat.SZULETESI_IDO +Environment.NewLine+
                "Oltva: "+allat.OLTVA+Environment.NewLine+
                "Nőstény: "+allat.NOSTENY+Environment.NewLine+
                "oltott: "+allat.OLTVA+Environment.NewLine+
                "Méret: "+allat.MERET+Environment.NewLine+
                "Ivartalanított: "+allat.IVARTALANITOTT;

            return animal;
        }
        void KépBetöltés ()
        {
            BitmapImage bmi = new BitmapImage();
            Uri uri = new Uri(SaveClipboardImageToFile());
            bmi.BeginInit();
            bmi.UriSource = uri;
            bmi.EndInit();
            VM.Kép = bmi;
        }
        public string SaveClipboardImageToFile()
        {
            string allatpath = @"állatok";
            string currentdirectory = Environment.CurrentDirectory;
            currentdirectory = currentdirectory.Remove(currentdirectory.Length - 31);
            string gesturefile =  Path.Combine(currentdirectory, allatpath);
            gesturefile = gesturefile + allat.KEP;
            return gesturefile;
        }
       
    }
    class ReservationPageViewModel {
        BitmapImage kép;

        public BitmapImage Kép
        {
            get { return kép; }
            set { kép = value; OnPropertyChanged("Kép"); }
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
