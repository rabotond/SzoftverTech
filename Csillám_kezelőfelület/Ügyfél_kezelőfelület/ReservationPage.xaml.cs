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
using AdatKezelő.csillamRef;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    
    
    public partial class ReservationPage : Window
    {//Service1Client régi helyett
        
        ReservationPageViewModel VM;
        ALLAT allat = new ALLAT();
        Ügyfél_Kezelő kezelo;
        Service1Client client;
       UGYFEL bejelentkezettUser;
        public ReservationPage(ALLAT kapottallat, UGYFEL kapottbejelentkezettUser)
        {
            client = new Service1Client();
            kezelo = new Ügyfél_Kezelő();
            VM = new ReservationPageViewModel();
            allat = kapottallat;
            bejelentkezettUser = kapottbejelentkezettUser;
            InitializeComponent();
            try
            {
                allat = kapottallat;
                //InitializeComponent();
                Adatfeltölt();
                DataContext = VM;
                adatok.Content = Adatfeltölt();
                KépBetöltés();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba történt az adatok feldolgozása közben!");
            }
            
        }

        private void Előjegyzés(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(this.bejelentkezettUser.ToString());
           // MessageBox.Show(this.bejelentkezettUser.KERESZTNEV.ToString());
          //  MessageBox.Show(this.allat.NEV.ToString());
            
            //Előjegyzéskor küldjön egy emailt az adminnak, hogy vegye fel a kapcsolatot az ügyféllel
          
            // client.sendEmail("rohodi@hotmail.com", "Állat előjegyzés",
           //   this.bejelentkezettUser.VEZETEKNEV.ToString()+" "+ this.bejelentkezettUser.KERESZTNEV +" "+ "előjegyezte örökbefogadásra"+
           //   " "+ this.allat.NEV.ToString()+" "+ " nevű állatot, kérlek vedd fel az ügyféllel a kapcsolatot!");
            
            client.sendEmail("rohodi@hotmail.com", "Állat előjegyzés",
             String.Format("{0} {1} előjegyezte örökbefogadásra {2} nevű állatot, kérlek vedd fel az ügyféllel a kapcsolatot!",
             this.bejelentkezettUser.VEZETEKNEV ,this.bejelentkezettUser.KERESZTNEV, this.allat.NEV));
            MessageBox.Show("Az állatodat sikeresen előjegyeztük örökbefogadáshoz. A staff felfogja feled venni a kapcsolatot a részletekkel kapcsolatban");
            kezelo.Előjegyeztet(allat);
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
            try
            {
                BitmapImage bmi = new BitmapImage();
                Uri uri = new Uri(SaveClipboardImageToFile());
                bmi.BeginInit();
                bmi.UriSource = uri;
                bmi.EndInit();
                VM.Kép = bmi;
            }
            catch (Exception)
            {
                MessageBox.Show("A kép betöltése sikertelen!");
            }
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

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
            AdoptionPage adoptionPage = new AdoptionPage(this.bejelentkezettUser);
            adoptionPage.Show();
            this.Close();
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
