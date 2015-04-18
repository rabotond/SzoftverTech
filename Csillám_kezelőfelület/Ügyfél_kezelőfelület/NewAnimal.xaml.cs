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
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using AdatKezelő;
using System.Globalization;
using Csillám_kezelőfelület.Admin_kezelőfelület;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for NewAnimal.xaml
    /// </summary>
    public partial class NewAnimal : Window
    {
        string képutja = null;
        NewAnimalViewModel VM;
        string képforrás;
        Admin_kezelőfelület_businessLogic bl;

        public NewAnimal()
        {
        
                InitializeComponent();
            VM = new NewAnimalViewModel();
            bl = new Admin_kezelőfelület_businessLogic();
                DataContext = VM;
          
            //ItemsSource="{Binding Path=ChipElojegyezLista}"
            
        }
        public NewAnimal(ALLAT modositando)//ezt az ablakot hazsnáljuk módosításra is , csak ilyenkro már előre feltöltjük adatokkal
        {

            InitializeComponent();
            VM = new NewAnimalViewModel();
            DataContext = VM;

            //ItemsSource="{Binding Path=ChipElojegyezLista}"

        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
            if(this.Owner.GetType()==typeof(AdministrationPage))//ha az adminfelületről lett hívva a window----fanni's shit
            {
                ALLAT ujallat = new ALLAT();
                ujallat.ALLATID = Guid.NewGuid();
                ujallat.CHIPES = (this.chipes.Text == chip_es_elojegyez.igen.ToString());
                ujallat.ELOJEGYZETT = (this.elojegyzett.Text == chip_es_elojegyez.igen.ToString());
                ujallat.FAJTA = this.fajta.Text;
                ujallat.MERET=int.Parse(this.Méret.Text);
                ujallat.NEV = this.név.Text;
                ujallat.NOSTENY = (this.nem.Text == fiu_lany.nőstény.ToString());
                ujallat.OLTVA = this.oltott.IsChecked;
                ujallat.SZIN = this.szin.Text;
                ujallat.SZULETESI_IDO = this.született.SelectedDate;
                ujallat.IVARTALANITOTT = this.ivartalan.Text == chip_es_elojegyez.igen.ToString();
                ujallat.BETEGSEGEK = this.Betegségek.Text;
                //ujallat.ELOZO_TULAJ
               // ujallat.KEP = this.kép.Source.ToString();
                ujallat.TOMEG = int.Parse(this.tomeg.Text);
                bl.Állatot_hozzáad(ujallat);
                
            }
            else
            {
                VM.Neve = név.ToString();
                VM.Kor = született.ToString();
                VM.Fajta = fajta.ToString();
                VM.Tomeg = tomeg.ToString();
                VM.Szin = szin.ToString();
                VM.Megjegyzes = megjegyzes.ToString();
                VM.Betegség = Betegségek.ToString();
                VM.Méret = Méret.ToString();
            
               // SaveClipboardImageToFile(képutja);

                Ügyfél_Kezelő ügyfél = new Ügyfél_Kezelő();
                UGYFEL kicsoda = new UGYFEL();
                ALLAT allat = new ALLAT();          
            

                if (VM.Chip==chip_es_elojegyez.igen)
                {
                    allat.CHIPES = true;    
                }
                else
                {
                    allat.CHIPES = false;    
                }
                if (VM.Elojegyez==chip_es_elojegyez.igen)
                {
                    allat.ELOJEGYZETT = true;    
                }
                else
                {
                    allat.ELOJEGYZETT = false;
                }
                //allat.ELOZO_TULAJ = 12;  ?? GUID
            
                if(VM.Ivar == chip_es_elojegyez.igen )
                {
                    allat.IVARTALANITOTT = true;
                }
                else
                {
                    allat.IVARTALANITOTT = false;
                }
                //allat.KENNEL = ""; //  ???????  nem értem , ez egy másik tábla nem is állat

                allat.KEP = képforrás;
                //allat.MERET =decimal.Parse(VM.Méret);
            
                allat.NEV = VM.Neve;
            
                if (VM.Fiulany==fiu_lany.hím)
                {
                    allat.NOSTENY = false;
                }
                else
                {
                    allat.NOSTENY=true;
                }
                if (VM.Oltasok==oltas.igen)
                {
                    allat.OLTVA = true;    
                }
                else
                {
                    allat.OLTVA = false;
                }
                SaveClipboardImageToFile(képforrás); 
                if (VM.Szin!=null && VM.Tomeg!= null && VM.Fajta!= null && VM.Betegség!=null && képforrás!=null)
                {
                    SaveClipboardImageToFile(képforrás); 
                    allat.SZIN = VM.Szin;
                    allat.FAJTA = VM.Fajta;
                    //allat.SZULETESI_IDO = DateTime.ParseExact(VM.Kor, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    allat.TOMEG = decimal.Parse(VM.Tomeg);
                    allat.BETEGSEGEK = VM.Betegség;
                }
                else
                {
                    System.Windows.MessageBox.Show("Üres mező");
                }
            
                ügyfél.Örökbe_ad(allat, kicsoda);
            }
        }

        private void Foto_feltolt(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                BitmapImage bi = new BitmapImage();
                képforrás = dlg.FileName;

                string filename = dlg.FileName;
                képutja = dlg.FileName;
                bi.BeginInit();
                bi.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                var uriSource = new Uri(filename, UriKind.Relative);
                //VM.Kép.Source = new BitmapImage(uriSource);
                if (bi!= null)
                {
                    VM.Kép = bi;    
                }
                //

            }
        }
        public static void SaveClipboardImageToFile(string picturepath)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(picturepath, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            string allatpath = @"...\állatok\" ;
            string gesturefile = System.IO.Path.Combine(Environment.CurrentDirectory, allatpath);// fos
            using (var fileStream = new FileStream(gesturefile, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                
                encoder.Frames.Add(BitmapFrame.Create(bi));
                encoder.Save(fileStream);
                //fullPath =System.IO.Path.GetFullPath(fullPath);
                
            }
        }  
       
    }



     class NewAnimalViewModel :  INotifyPropertyChanged
    {
        string méret;

        public string Méret
        {
            get { return méret; }
            set { méret = value; }
        }

        string betegség;

        public string Betegség
        {
            get { return betegség; }
            set { betegség = value; }
        }


        BitmapImage kép;

        public BitmapImage Kép
        {
            get { return kép; }
            set { kép = value; OnPropertyChanged("Kép"); }
        }  
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
        chip_es_elojegyez chip;

        public chip_es_elojegyez Chip
        {
            get { return chip; }
            set { chip = value; OnPropertyChanged("Chip"); }
        }
      
        public Array ChipLista
        {
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
            set { }
        }
        chip_es_elojegyez elojegyez;

        public chip_es_elojegyez Elojegyez
        {
            get { return elojegyez; }
            set { elojegyez = value; OnPropertyChanged("Elojegyez"); }
        }

        public Array ElojegyezLista
        {
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
            set { }
        }
        chip_es_elojegyez ivar;

        public chip_es_elojegyez Ivar
        {
            get { return ivar; }
            set { ivar = value; OnPropertyChanged("Ivar"); }
        }
        public Array IvarLista
        {
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
            set { }
        }
        fiu_lany fiulany;

        public fiu_lany Fiulany
        {
            get { return fiulany; }
            set { fiulany = value; OnPropertyChanged("Fiulany"); }
        }
        public Array Fiulanylista
        {
            get { return Enum.GetValues(typeof(fiu_lany)); }
            set { }
        }

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
        
        public Array Étellista
        {
            get { return Enum.GetValues(typeof(ételek)); }
            set { }
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
    enum oltas
    {
        igen,nem
    }
    enum ételek
    {
        kutyakaja,macskakaja,wombatétel,madáreleség,egyéb
    }
    enum chip_es_elojegyez
    {
        igen,nem
    }
    enum fiu_lany
    {
        hím,nőstény
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
                return System.Windows.Data.Binding.DoNothing;
            }
        }
    }
}
