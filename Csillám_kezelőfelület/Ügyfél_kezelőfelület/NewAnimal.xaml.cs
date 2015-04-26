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
    /// </summary>//
    public partial class NewAnimal : Window
    {
        private readonly UGYFEL bejelentkezettUser;
        private readonly NewAnimalViewModel VM;
        private Admin_kezelőfelület_businessLogic bl;
        private string képforrás;
        private static string képutja;
        public AllatVM modosítandoallat;
        private bool ujallat = false;

        public NewAnimal(UGYFEL bennvan)
        {
            bejelentkezettUser = bennvan;
            InitializeComponent();
            VM = new NewAnimalViewModel();
            bl = new Admin_kezelőfelület_businessLogic();
            DataContext = VM;
            ujallat = true;
        }

        public NewAnimal(AllatVM modositando)
        {   
            InitializeComponent();
            VM = new NewAnimalViewModel(); 
            bl = new Admin_kezelőfelület_businessLogic();
            DataContext = VM;
            képutja = modositando.kep;
            modosítandoallat = modositando;
            feltolt_adatokkal();
            ujallat = false;
        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
            if (ujallat)
            {
                Uj_allat_mentés();
            }
            else
            {
                Modositottmentés();
            }
        }

        void Uj_allat_mentés()
        {
            var ügyfél = new Ügyfél_Kezelő();
            ALLAT allat = new ALLAT();
           
            if (VM.Szin != null && VM.Tömeg != null && VM.Fajta != null  && VM.Méret != null && képforrás != null && VM.Neve != null)
            {
                if (képutja != null)
                {
                    SaveClipboardImageToFile(képforrás);
                    allat.KEP = trimfoto(képutja);
                }
                allat.ALLATID = Guid.NewGuid();
                allat.CHIPES = (this.chipes.Text == chip_es_elojegyez.igen.ToString());
                allat.ELOJEGYZETT = (this.elojegyzett.Text == chip_es_elojegyez.igen.ToString());
                allat.FAJTA = this.fajta.Text;
                allat.MERET = int.Parse(this.Méret.Text);
                allat.TOMEG = int.Parse(this.tomeg.Text);
                allat.NEV = this.név.Text;
                allat.NOSTENY = (this.nem.Text == fiu_lany.nőstény.ToString());
                allat.OLTVA = this.oltott.IsChecked;
                allat.SZIN = this.szin.Text;
                allat.SZULETESI_IDO = this.született.SelectedDate;
                allat.IVARTALANITOTT = this.ivartalan.Text == chip_es_elojegyez.igen.ToString();
                allat.BETEGSEGEK = this.Betegségek.Text;
                bl.Állatot_hozzáad(allat);

                KennelTablaKarbantartas(this.fajta.Text);
                MessageBox.Show("Mentve");
            }
            else
            {
                MessageBox.Show("Üres mező");
            }
            ügyfél.Örökbe_ad(allat, bejelentkezettUser);
        }

        void Modositottmentés()
        {
            var ügyfél = new Ügyfél_Kezelő();
            var kicsoda = new UGYFEL();
            ALLAT allat = new ALLAT();
           
                if (képforrás != null)
                {
                    SaveClipboardImageToFile(képforrás);
                    modosítandoallat.kep = trimfoto(képutja);
                }
                    modosítandoallat.CHIPES = (this.chipes.Text == chip_es_elojegyez.igen.ToString());
                    modosítandoallat.ELOJEGYZETT = (this.elojegyzett.Text == chip_es_elojegyez.igen.ToString());
                    modosítandoallat.FAJTA = this.fajta.Text;
                    modosítandoallat.MERET = int.Parse(this.Méret.Text);
                    modosítandoallat.TOMEG = int.Parse(this.tomeg.Text);
                    modosítandoallat.NEV = this.név.Text;
                    modosítandoallat.NOSTENY = (this.nem.Text == fiu_lany.nőstény.ToString());
                    modosítandoallat.OLTVA = this.oltott.IsChecked;
                    modosítandoallat.SZIN = this.szin.Text;
                    modosítandoallat.SZULETESI_IDO = this.született.SelectedDate;
                    modosítandoallat.IVARTALANITOTT = this.ivartalan.Text == chip_es_elojegyez.igen.ToString();
                    modosítandoallat.BETEGSEGEK = this.Betegségek.Text;
                    
                    bl.Állatot_módosít(modosítandoallat);
                
                MessageBox.Show("Mentve");
        }
        
        private void KennelTablaKarbantartas(string allatfaj)
        {
            bl.KennelTablaHelyKarbanTartas(allatfaj);
        }
        
        public void feltolt_adatokkal()
        {
            név.Text = modosítandoallat.NEV;
            VM.Fajta= modosítandoallat.FAJTA;
            született.SelectedDate = modosítandoallat.SZULETESI_IDO;  
            Méret.Text = Convert.ToString(modosítandoallat.MERET);
            tomeg.Text = modosítandoallat.TOMEG.ToString();
            szin.Text = modosítandoallat.SZIN;
            Betegségek.Text = modosítandoallat.BETEGSEGEK;
            if (modosítandoallat.CHIPES==true)
            {
                VM.Chip = chip_es_elojegyez.igen;    
            }
            else
            {
                VM.Chip = chip_es_elojegyez.nem;
            }
            if (modosítandoallat.NOSTENY == true)
            {
                VM.Fiulany = fiu_lany.nőstény;
            }
            else
            {
                VM.Fiulany = fiu_lany.hím;
            }

            if (modosítandoallat.ELOJEGYZETT == true)
            {
                VM.Elojegyez = chip_es_elojegyez.igen;
            }
            else
            {
                VM.Elojegyez = chip_es_elojegyez.nem;
            }

            if (modosítandoallat.IVARTALANITOTT == true)
            {
                VM.Ivar = chip_es_elojegyez.igen;
            }
            else
            {
                VM.Ivar = chip_es_elojegyez.nem;
            }

            if (modosítandoallat.OLTVA == true)
            {
                VM.Oltasok = oltas.igen;
            }
            else
            {
                VM.Oltasok = oltas.nem;
            }
          Modositott_allat_kép_feltöltés();
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
       
        public static string trimfoto(string képutja)
        {
            string temp = képutja;
            int a = temp.LastIndexOf("\\");
            temp = temp.Remove(0, a);
            return temp;
        }

        public static void SaveClipboardImageToFile(string picturepath)
        {

            string allatpath = @"állatok";
            string currentdirectory = Environment.CurrentDirectory;
            currentdirectory = currentdirectory.Remove(currentdirectory.Length - 31);
            currentdirectory = Path.Combine(currentdirectory, allatpath);
            Image img = Image.FromFile(picturepath);
            currentdirectory = currentdirectory+ trimfoto(képutja);
            img.Save(currentdirectory);
        }

        public void Modositott_allat_kép_feltöltés()
        {
            string allatpath = @"állatok";
            string currentdirectory = Environment.CurrentDirectory;
            currentdirectory = currentdirectory.Remove(currentdirectory.Length - 31);
            currentdirectory = Path.Combine(currentdirectory, allatpath);
            currentdirectory = currentdirectory+ trimfoto(képutja);

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(currentdirectory, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            VM.Kép = bi;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    internal class NewAnimalViewModel : INotifyPropertyChanged
    {
         chip_es_elojegyez chip;
         chip_es_elojegyez elojegyez;
         ételek ételek;
         fiu_lany fiulany;
         chip_es_elojegyez ivar;
         BitmapImage kép;
         string tömeg;
         string méret;
         oltas oltasok;
         string neve;
         string szin;
         string _selectedKennel;
         List<string> _kennelList;

        public NewAnimalViewModel()
        {
            KennelList = new List<string>();
            kennelfeltolt();
        }

        public string Méret { get { return méret; } set{méret=value;OnPropertyChanged("Méret");}}
        public string Tömeg { get { return tömeg; } set { tömeg = value; OnPropertyChanged("Tömeg"); } }
        public string Szin { get { return szin; } set { szin = value; OnPropertyChanged("Szin"); } }

        public string Neve { get { return neve; } set { neve = value; OnPropertyChanged("Neve"); } }

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
        public string Fajta { get; set; }
        
        public List<string> Eledeltipus { get; set; }
        public string Elozotulaj { get; set; }
      

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
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
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
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
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
            get { return Enum.GetValues(typeof(chip_es_elojegyez)); }
            set { }
        }

        public fiu_lany Fiulany
        {
            get { return fiulany; }
            set{  fiulany = value;  OnPropertyChanged("Fiulany"); }
        }

        public Array Fiulanylista
        {
            get { return Enum.GetValues(typeof(fiu_lany)); }
            set { }
        }

        public bool Megkapta
        {
            get { return oltasok == oltas.igen; } // from form
            set
            {
                if (value)
                {
                    Oltasok = oltas.igen; OnPropertyChanged("Megkapta");
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
                    Oltasok = oltas.nem; OnPropertyChanged("Nemkaptameg");
                }
            } // if changes
        }

        public void kennelfeltolt()
        {
            Ügyfél_Kezelő ugy = new Ügyfél_Kezelő();

            foreach (var VARIABLE in ugy.KennelListafeltolt())
            {
                KennelList.Add(VARIABLE);
            }
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
            get { return Enum.GetValues(typeof(ételek)); }
            set { }
        }

        public List<string> KennelList
        {
            get { return _kennelList; }
            set { _kennelList = value; OnPropertyChanged("KennelList"); }
        }

        public string SelectedKennel
        {
            get { return _selectedKennel; }
            set { _selectedKennel = value; OnPropertyChanged("SelectedKennel"); }
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