using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AdatKezelő;
using Csillám_kezelőfelület.Admin_kezelőfelület;
using Microsoft.Win32;

// StreamResourceInfo

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    ///     Interaction logic for NewAnimal.xaml
    /// </summary>
    public partial class NewAnimal : Window
    {
        private readonly UGYFEL bejelentkezettUser;
        private readonly NewAnimalViewModel VM;
        private Admin_kezelőfelület_businessLogic bl;
        private string képforrás;
        private static string képutja;

        public NewAnimal(UGYFEL bennvan)
        {
            bejelentkezettUser = bennvan;
            InitializeComponent();
            VM = new NewAnimalViewModel();
            bl = new Admin_kezelőfelület_businessLogic();
            DataContext = VM;
        }

        public NewAnimal(ALLAT modositando)
        {
            InitializeComponent();
            VM = new NewAnimalViewModel();
            DataContext = VM;
        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
            var ügyfél = new Ügyfél_Kezelő();
            var kicsoda = new UGYFEL();
            var allat = new ALLAT();
            string faj = "";

            /* if(this.Owner.GetType()==typeof(AdministrationPage))//ha az adminfelületről lett hívva a window----fanni's shit
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
            {*/
            feltolt_adatokkal();
            if (VM.Szin != null && VM.Tomeg != null && VM.Fajta != null && VM.Betegség != null && képforrás != null && VM.Méret != null && képforrás!="" && VM.Neve!=null)
            {
                SaveClipboardImageToFile(képforrás);
                allat.SZIN = VM.Szin;
                allat.FAJTA = VM.Fajta;
                faj = allat.FAJTA;
                allat.SZULETESI_IDO = született.SelectedDate;
                //allat.TOMEG = decimal.Parse(VM.Tomeg);
                allat.BETEGSEGEK = VM.Betegség;
              //  allat.MERET = decimal.Parse(VM.Méret);
                allat.KEP = képforrás;
                allat.NEV = VM.Neve;
                allat.CHIPES = VM.Chip == chip_es_elojegyez.igen;
                allat.ELOJEGYZETT = VM.Elojegyez == chip_es_elojegyez.igen;
                allat.IVARTALANITOTT = VM.Ivar == chip_es_elojegyez.igen;
                allat.NOSTENY = VM.Fiulany != fiu_lany.hím;
                allat.OLTVA = VM.Oltasok == oltas.igen;
                KennelTablaKarbantartas(faj);
                MessageBox.Show("Mentve");
            }
            else
            {
                MessageBox.Show("Üres mező");
            }
            ügyfél.Örökbe_ad(allat, kicsoda);
            //allat.KENNEL = ""; //  ???????  nem értem , ez egy másik tábla nem is állat
            //allat.ELOZO_TULAJ = 12;  ?? GUID

        }
        private void KennelTablaKarbantartas(string allatfaj)
        {
            bl.KennelTablaHelyKarbanTartas(allatfaj);
        }
       public void feltolt_adatokkal()
        {
            VM.Neve = név.ToString();
            VM.Kor = született.ToString();
            VM.Fajta = fajta.Text;
            VM.Tomeg = tomeg.ToString();
            VM.Szin = szin.ToString();
            VM.Megjegyzes = megjegyzes.ToString();
            VM.Betegség = Betegségek.ToString();
            VM.Méret = Méret.ToString();
        }
        private void Foto_feltolt(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            var result = dlg.ShowDialog();
            if (result == true)
            {
                var bi = new BitmapImage();
                képforrás = dlg.FileName;

                var filename = dlg.FileName;
                képutja = dlg.FileName;
                bi.BeginInit();
                bi.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                if (bi != null)
                {
                    VM.Kép = bi;
                }
            }
        }
      public static  string trimfoto(string képutja)
        {
            string temp = képutja;
            int a = temp.LastIndexOf("\\");
             temp =temp.Remove(0,a);
             return temp;
        }

        public static void SaveClipboardImageToFile(string picturepath)
        {
            string allatpath = @"állatok";
            string currentdirectory = Environment.CurrentDirectory;
            currentdirectory = currentdirectory.Remove(currentdirectory.Length - 31);
            string gesturefile = Path.Combine(currentdirectory, allatpath); 
            Image img = Image.FromFile(picturepath);
            gesturefile = gesturefile + trimfoto(képutja);
            img.Save(gesturefile);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            var piaWindow = new PutInAnimal(bejelentkezettUser);
            piaWindow.Show();
            Close();
        }
    }


    internal class NewAnimalViewModel : INotifyPropertyChanged
    {
        private chip_es_elojegyez chip;
        private chip_es_elojegyez elojegyez;
        private ételek ételek;
        private fiu_lany fiulany;
        private chip_es_elojegyez ivar;
        private BitmapImage kép;
        private oltas oltasok;
        public string Méret { get; set; }
        public string Betegség { get; set; }

        public BitmapImage Kép
        {
            get { return kép; }
            set
            {
                kép = value;
                OnPropertyChanged("Kép");
            }
        }

        public string Neve { get; set; }
        public string Kor { get; set; }
        public string Fajta { get; set; }
        public string Nem { get; set; }
        public List<string> Eledeltipus { get; set; }
        public string Elozotulaj { get; set; }
        public string Eredet { get; set; }
        public string Tomeg { get; set; }
        public string Szin { get; set; }
        public string Megjegyzes { get; set; }

        public chip_es_elojegyez Chip
        {
            get { return chip; }
            set
            {
                chip = value;
                OnPropertyChanged("Chip");
            }
        }

        public Array ChipLista
        {
            get { return Enum.GetValues(typeof (chip_es_elojegyez)); }
            set { }
        }

        public chip_es_elojegyez Elojegyez
        {
            get { return elojegyez; }
            set
            {
                elojegyez = value;
                OnPropertyChanged("Elojegyez");
            }
        }

        public Array ElojegyezLista
        {
            get { return Enum.GetValues(typeof (chip_es_elojegyez)); }
            set { }
        }

        public chip_es_elojegyez Ivar
        {
            get { return ivar; }
            set
            {
                ivar = value;
                OnPropertyChanged("Ivar");
            }
        }

        public Array IvarLista
        {
            get { return Enum.GetValues(typeof (chip_es_elojegyez)); }
            set { }
        }

        public fiu_lany Fiulany
        {
            get { return fiulany; }
            set
            {
                fiulany = value;
                OnPropertyChanged("Fiulany");
            }
        }

        public Array Fiulanylista
        {
            get { return Enum.GetValues(typeof (fiu_lany)); }
            set { }
        }

        public bool Megkapta
        {
            get { return oltasok == oltas.igen; } // from form
            set
            {
                if (value)
                {
                    Oltasok = oltas.igen;
                }
            } // if changes
        }

        public bool Nemkaptameg
        {
            get { return oltasok == oltas.nem; } // from form
            set
            {
                if (value)
                {
                    Oltasok = oltas.nem;
                }
            } // if changes
        }

        public oltas Oltasok
        {
            get { return oltasok; }
            set
            {
                oltasok = value;
                OnPropertyChanged("Oltasok");
                OnPropertyChanged("Megkapta");
                OnPropertyChanged("Nemkaptameg");
                OnPropertyChanged("Nincsinfo");
            }
        }

        public ételek Ételek
        {
            get { return ételek; }
            set
            {
                ételek = value;
                OnPropertyChanged("Ételek");
            }
        }

        public Array Étellista
        {
            get { return Enum.GetValues(typeof (ételek)); }
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

    internal enum oltas
    {
        igen,
        nem
    }

    internal enum ételek
    {
        kutyakaja,
        macskakaja,
        wombatétel,
        madáreleség,
        egyéb
    }

    internal enum chip_es_elojegyez
    {
        igen,
        nem
    }

    internal enum fiu_lany
    {
        hím,
        nőstény
    }

   
}