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
using System.Collections.ObjectModel;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for QueriePlaces.xaml
    /// </summary>
    public partial class QueriePlaces : Window
    {
        QueueuPlacesViewModel VM;
        user bejelentkezettUser;
        public QueriePlaces(user bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            VM = new QueueuPlacesViewModel();
            DataContext = VM;
        }


       

     

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            PutInAnimal piWindow = new PutInAnimal(this.bejelentkezettUser);
            piWindow.Show();
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(VM.SelectedFaj))
            {
                MessageBox.Show("Válassza ki a beadni kívánt állat fajtáját!");
            }
            else
            {
                Ügyfél_Kezelő a = new Ügyfél_Kezelő();
                int uresHelyekSzama = a.Van_e_üres_kennel(VM.SelectedFaj);
                EmptySpaceWindowViewModel vm = new EmptySpaceWindowViewModel();
                vm.EmptySpaces = uresHelyekSzama;
                EmptySpacesWindow esWindow = new EmptySpacesWindow(vm, this.bejelentkezettUser);
                esWindow.Show();
                this.Close();
            }
        }
    }
    class QueueuPlacesViewModel
    {
        public QueueuPlacesViewModel()
        {
            fajok = new ObservableCollection<string>();
            feltolt();

        }
        ObservableCollection<string> fajok;

        public ObservableCollection<string> Fajok
        {
            get { return fajok; }
            set { fajok = value; }
        }
        string selectedFaj;

        public string SelectedFaj
        {
            get { return selectedFaj; }
            set { selectedFaj = value; }
        }
        void feltolt ()
        {
            csillamponimenhelyDBEntities db = new csillamponimenhelyDBEntities();
            var lekerdezettFajok = from allat in db.ALLAT
                        select allat.FAJTA;

            foreach (var faj in lekerdezettFajok.ToList())
            {
                fajok.Add(faj);
            }
        }
    }
    
}
