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

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for NewAnimal.xaml
    /// </summary>
    public partial class NewAnimal : Window
    {
        NewAnimalViewModel VM;
        public NewAnimal()
        {
            InitializeComponent();
            VM = new NewAnimalViewModel();
            DataContext = VM;
            
        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
            VM.Neve = név.ToString();
            VM.Kor = kor.ToString();
            VM.Fajta = fajta.ToString();
            VM.Nem = nem.ToString();
            VM.Tomeg = tomeg.ToString();
            VM.Szin = szin.ToString();
            VM.Megjegyzes = megjegyzes.ToString();
           //VM.Eledeltipus
            //VM.Chipelojegyez   chip igazhamis
            //VM.Chipelojegyez   elojegyez igazhamis

        }

        private void Foto_feltolt(object sender, RoutedEventArgs e)
        {

        }
    }

    class NewAnimalViewModel :  INotifyPropertyChanged
    {
       
        string neve;

        public string Neve
        {
            get { return neve; }
            set { neve = value; }
        }
        string kor;

        public string Kor
        {
            get { return kor; }
            set { kor = value; }
        }
        string fajta;

        public string Fajta
        {
            get { return fajta; }
            set { fajta = value; }
        }
        string nem;

        public string Nem
        {
            get { return nem; }
            set { nem = value; }
        }
        List<string> eledeltipus;

        public List<string> Eledeltipus
        {
            get { return eledeltipus; }
            set { eledeltipus = value; }
        }
        string elozotulaj;

        public string Elozotulaj
        {
            get { return elozotulaj; }
            set { elozotulaj = value; }
        }
        string eredet;

        public string Eredet
        {
            get { return eredet; }
            set { eredet = value; }
        }
        oltas oltasok;


        string tomeg;

        public string Tomeg
        {
            get { return tomeg; }
            set { tomeg = value; }
        }
        string szin;

        public string Szin
        {
            get { return szin; }
            set { szin = value; }
        }
       
        string megjegyzes;

        public string Megjegyzes
        {
            get { return megjegyzes; }
            set { megjegyzes = value; }
        }
        chip_es_elojegyez chipelojegyez;

        public chip_es_elojegyez Chipelojegyez
        {
            get { return chipelojegyez; }
            set { chipelojegyez = value; OnPropertyChanged("Chipelojegyez"); }
        }
       
        /*
        public Array ChipElojegyezLista
        {
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
            set { }
        }
       
      
        */

        public bool Megkapta
        {
            get { return oltasok == oltas.igen; }  // from form
            set { if (value) { Oltasok = oltas.igen; } } // if changes
        }
        public bool Nemkaptameg
        {
            get { return oltasok == oltas.nem; }  // from form
            set { if (value) { Oltasok = oltas.nem; } } // if changes
        }
        public bool Nincsinfo
        {
            get { return oltasok == oltas.info; }  // from form
            set { if (value) { Oltasok = oltas.info; } } // if changes
        }
        public oltas Oltasok
        {
            get { return oltasok; }
            set { oltasok = value; OnPropertyChanged("Oltasok"); OnPropertyChanged("Megkapta"); OnPropertyChanged("Nemkaptameg"); OnPropertyChanged("Nincsinfo"); }
        }
        ételek ételek;

        public ételek Ételek
        {
            get { return ételek; }
            set { ételek = value; OnPropertyChanged("Ételek"); }
        }
        /*
        public Array Étellista
        {
            get { return Enum.GetValues(typeof(ételek)); }
            set { }
        }
        */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    enum oltas
    {
        igen,nem,info
    }
    enum ételek
    {
        kutyakaja,macskakaja,wombatétel,madáreleség,egyéb
    }
    enum chip_es_elojegyez
    {
        igen,nem
    }

    class  EnumToRadioButtonConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter); // given parameter is the same as radiobutton
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(true))
            {
                return parameter;
            }
            else
            {
                return Binding.DoNothing;
            }
        }
    }
}
