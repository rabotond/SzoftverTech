using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AdatKezelő;
using System.Threading;
using System.Threading.Tasks;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    ///     Interaction logic for AdoptionPage.xaml
    /// </summary>
    public partial class AdoptionPage : Window
    {
        private readonly Ügyfél_Kezelő dbhozzáférés = new Ügyfél_Kezelő();
        private readonly IÜgyfél_kezelő ügyfél = new Ügyfél_Kezelő();
        private readonly AdaptionPageViewModel VM;
        private ALLAT atadommajdallat;
        private UGYFEL bejelentkezettUser;
        Thread t;

        public AdoptionPage(UGYFEL bejelentkezettUser)
        {
            VM = new AdaptionPageViewModel();
            atadommajdallat = new ALLAT();
            DataContext = VM;
            this.bejelentkezettUser = bejelentkezettUser;
            
            InitializeComponent();
            StartAdvTask();
        }

        private void StartAdvTask()
        {
            t = new Thread(new ThreadStart(MoveLabel));
            t.IsBackground = true;
            t.Start();
        }

        private void MoveLabel()
        {
            string tempString= "";

                    int count = VM.AvdString.Length - 3;
                    while (true)
                    {
                        tempString = VM.AvdString.Substring(count);
                        
                        tempString = tempString + VM.AvdString.Substring(0, count);

                        VM.AvdString = tempString;

                        count -= 3;
                        Thread.Sleep(100);
                    }
              
        }

        private void Keresés(object sender, RoutedEventArgs e)
        {
            if (ügyfél.Összetett_keresés(Oltasok(), fiuvagylany(), szin.ToString(), VM.SelectedFaj, név.ToString(),
                    Ivartalanított(), Beteg()).Any())
            {
                VM.Allatok = ügyfél.Összetett_keresés(Oltasok(), fiuvagylany(), szin.ToString(), VM.SelectedFaj, név.ToString(),
                    Ivartalanított(), Beteg());
            }
            else
            {
                MessageBox.Show("Nincs ilyen állat. Kérlek adj meg más feltételeket");
            }
            VM.Allatok = ügyfél.Összetett_keresés(Oltasok(), fiuvagylany(), szin.ToString(), VM.SelectedFaj, név.ToString(),
                    Ivartalanított(), Beteg());
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VM.Selectedallat != null)
            {
                atadommajdallat = VM.Selectedallat;
                var other = new ReservationPage(atadommajdallat);
                Close();
                other.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var nodup = new List<string>();
            foreach (var item in dbhozzáférés.Db.ALLAT)
            {
                nodup.Add(item.FAJTA);
            }
            nodup = nodup.ConvertAll(d => d.ToLower());
            VM.Faj = nodup.Distinct().ToList();
        }

        private bool fiuvagylany()
        {
            if (VM.Fiu == "nőstény")
            {
                return true;
            }
            return false;
        }

        private bool Beteg()
        {
            if (checkBoxBeteg.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        private bool Ivartalanított()
        {
            if (checkBoxIvartalanított.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        private bool Oltasok()
        {
            if (oltas.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            LoginPage piaWindow = new LoginPage();
            piaWindow.Show();
            //MessageBox.Show("végén ha lesz authentikáció akkor cooomment out");
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            t.Abort();
        }
    }

    internal class AdaptionPageViewModel : INotifyPropertyChanged
    {
        private List<string> faj;

        public AdaptionPageViewModel()
        {
            Allatok = new List<ALLAT>();
            faj = new List<string>();
            Fiulany = new List<string>();
            Fiulany.Add("hím");
            Fiulany.Add("nőstény");
            ugyfelKezelo = new Ügyfél_Kezelő();
            avdString = ugyfelKezelo.GenerateAdvString();
        }
        private Ügyfél_Kezelő ugyfelKezelo;
        string avdString ;
        public string AvdString
        {
            get { return avdString; }
            set{
              avdString = value;
              OnPropertyChanged("AvdString");
            }
        }


        public ALLAT Selectedallat { get; set; }
        public List<ALLAT> Allatok { get; set; }

        public List<string> Faj
        {
            get { return faj; }
            set
            {
                faj = value;
                OnPropertyChanged("Faj");
            }
        }

        public string SelectedFaj { get; set; }
        public List<string> Fiulany { get; set; }
        public string Fiu { get; set; }
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