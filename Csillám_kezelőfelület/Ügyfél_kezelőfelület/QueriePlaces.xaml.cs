using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AdatKezelő;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    ///     Interaction logic for QueriePlaces.xaml
    /// </summary>
    public partial class QueriePlaces : Window
    {
        private readonly UGYFEL bejelentkezettUser;
        private readonly Ügyfél_Kezelő dbhozzáférés = new Ügyfél_Kezelő();
        private readonly QueueuPlacesViewModel VM;

        public QueriePlaces(UGYFEL bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            VM = new QueueuPlacesViewModel();
            DataContext = VM;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var piWindow = new PutInAnimal(bejelentkezettUser);
            piWindow.Show();
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(VM.SelectedFaj))
            {
                MessageBox.Show("Válassza ki a beadni kívánt állat fajtáját!");
            }
            else
            {
                var a = new Ügyfél_Kezelő();
                var uresHelyekSzama = a.Van_e_üres_kennel(VM.SelectedFaj);
                var vm = new EmptySpaceWindowViewModel();
                vm.EmptySpaces = uresHelyekSzama;
                var esWindow = new EmptySpacesWindow(vm, bejelentkezettUser);
                esWindow.Show();
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var nodup = dbhozzáférés.Db.ALLAT.Select(item => item.FAJTA).ToList();
            nodup = nodup.ConvertAll(d => d.ToLower());
            VM.Fajok = nodup.Distinct().ToList();
        }
    }

    internal class QueueuPlacesViewModel : INotifyPropertyChanged
    {
        private List<string> fajok;

        public QueueuPlacesViewModel()
        {
            fajok = new List<string>();
            feltolt();
        }

        public List<string> Fajok
        {
            get { return fajok; }
            set
            {
                fajok = value;
                OnPropertyChanged("Fajok");
            }
        }

        public string SelectedFaj { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void feltolt()
        {
            var db = new csillamponiDBEntities();
            var lekerdezettFajok = from allat in db.ALLAT
                select allat.FAJTA;

            foreach (var faj in lekerdezettFajok.ToList())
            {
                fajok.Add(faj);
            }
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}