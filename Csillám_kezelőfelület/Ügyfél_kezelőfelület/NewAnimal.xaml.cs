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
        public AllatVM modosítandoallat;
        private bool uj_vagy_modositott = false;

        public NewAnimal(UGYFEL bennvan)
        {
            bejelentkezettUser = bennvan;
            InitializeComponent();
            VM = new NewAnimalViewModel();
            bl = new Admin_kezelőfelület_businessLogic();
            DataContext = VM;
            uj_vagy_modositott = true;
        }

        public NewAnimal(AllatVM modositando)
        {
            modosítandoallat = modositando;
            InitializeComponent();
            VM = new NewAnimalViewModel();
            DataContext = VM;
            feltolt_adatokkal();
            uj_vagy_modositott = false;
            bl = new Admin_kezelőfelület_businessLogic();
        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
            if (modosítandoallat==null)
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
            string faj = "";

            if (VM.Szin != "" && VM.Tomeg != "" && VM.Fajta != "" && VM.Betegség != "" && képforrás != null && VM.Méret != "" && képforrás != "" && VM.Neve != "")
            {
                if (képutja != null)
                {
                    SaveClipboardImageToFile(képforrás);
                    allat.KEP = trimfoto(képutja);
                }
                allat.ALLATID = Guid.NewGuid();
                allat.CHIPES = (this.chipes.Text == chip_es_elojegyez.igen.ToString());
                allat.ELOJEGYZETT = (this.elojegyzett.Text == chip_es_elojegyez.igen.ToString());
                allat.FAJTA =faj= this.fajta.Text;
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
                
                KennelTablaKarbantartas(faj);
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
            allat.KENNEL = new KENNEL();
            string faj = "";

            if (VM.Szin != "" && VM.Tomeg != "" && VM.Fajta != "" && VM.Betegség != "" && képforrás != null && VM.Méret != "" && képforrás != "" && VM.Neve != "")
            {
                if (képutja != null)
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
                
                KennelTablaKarbantartas(faj);
                MessageBox.Show("Mentve");
            }
            else
            {
                MessageBox.Show("Üres mező");
            }
            ügyfél.Örökbe_ad(allat, kicsoda);
        }
        
        private void KennelTablaKarbantartas(string allatfaj)
        {
            bl.KennelTablaHelyKarbanTartas(allatfaj);
        }
        
        public void feltolt_adatokkal()
        {
            név.Text = modosítandoallat.NEV;
            fajta.SelectedItem= modosítandoallat.FAJTA;
            született.SelectedDate = modosítandoallat.SZULETESI_IDO;
            if (modosítandoallat.NOSTENY == true)
            {
                nem.SelectedItem = fiu_lany.nőstény;
            }
            else { nem.SelectedItem = fiu_lany.hím; }

            Méret.Text = Convert.ToString(modosítandoallat.MERET);
            tomeg.Text = modosítandoallat.TOMEG.ToString();
            szin.Text = modosítandoallat.SZIN;
            if (modosítandoallat.ELOJEGYZETT == true)
            {
                elojegyzett.SelectedItem = chip_es_elojegyez.igen;
            }
            else { elojegyzett.SelectedItem = chip_es_elojegyez.nem; }

            if (modosítandoallat.IVARTALANITOTT == true)
            {
                ivartalan.SelectedItem = chip_es_elojegyez.igen;
            }
            else { ivartalan.SelectedItem = chip_es_elojegyez.nem; }
            if (modosítandoallat.OLTVA == true)
            {
                oltott.IsChecked = true;
            }
            else
            {
                nemoltott.IsChecked = true;
            }
            Betegségek.Text = modosítandoallat.BETEGSEGEK;
            var bi = new BitmapImage();
            képforrás = képutja = modosítandoallat.kep;
            bi.BeginInit();
            bi.UriSource = new Uri(képforrás, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            if (bi != null)
            {
                VM.Kép = bi;
            }
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
        public string _selectedKennel;

        public NewAnimalViewModel()
        {
            KennelList = new List<string>();
            kennelfeltolt();
        }

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

        private List<string> _kennelList;

        public void kennelfeltolt()
        {
            Ügyfél_Kezelő ugy = new Ügyfél_Kezelő();

            foreach (var VARIABLE in ugy.KennelListafeltolt())
            {
                KennelList.Add(VARIABLE);
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
            set
            {
                fiulany = value;
                OnPropertyChanged("Fiulany");
            }
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
            set { _selectedKennel = value; }
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