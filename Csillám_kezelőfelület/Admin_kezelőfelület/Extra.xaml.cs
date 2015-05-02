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
using Csillám_kezelőfelület.Admin_kezelőfelület;
using System.Diagnostics;
using System.IO;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    /// <summary>
    /// Interaction logic for Extra.xaml
    /// </summary>
    /// 

    public partial class Extra : Window
    {
        Admin_kezelőfelület_viewmodel VM;
        Admin_kezelőfelület_businessLogic BL;
        UGYFEL bejelentkezettUser;

        AllatVM allat;

        string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
            @"SzoftverTech\ReklamHost\bin\Debug\ReklamHost.exe");

        public Extra(UGYFEL bejelentkezettUser)
        {
            this.bejelentkezettUser = bejelentkezettUser;
            VM = new Admin_kezelőfelület_viewmodel();
            BL = new Admin_kezelőfelület_businessLogic();

            DataContext = VM;
            InitializeComponent();
            Task.Factory.StartNew(() =>
            {

                var allatok = BL.FrissitAllat();

                Dispatcher.Invoke(new Action(() => VM.Allatok = allatok));
            });
        }

        private void Reklam_Click(object sender, RoutedEventArgs e)
        {
            string animal = "";
            if ((VM.ValasztottAllat != null) && VM.ValasztottAllat.ELOJEGYZETT != true)
            {
                List<AllatVM> kivalsztottak = new List<AllatVM>();
                for (int i = 0; i < állatgrid.SelectedItems.Count; i++)
                {
                    kivalsztottak.Add((AllatVM)állatgrid.SelectedItems[i]);

                }

                Process proc = new Process();
                //MessageBox.Show(path);
                if (File.Exists(path))
                {
                    proc.StartInfo.FileName = path;
                    proc.Start();
                    ReklamService.ServiceReklamClient cliens = new ReklamService.ServiceReklamClient();
                     cliens.Hibalog("Megy");


                    // allat = VM.ValasztottAllat;
                    foreach (AllatVM allat in kivalsztottak)
                    {


                        animal = animal + "     név: " + allat.NEV + "  " +
                                  "Fajta: " + allat.FAJTA + "  " +
                                  "Szül. Idő: " + allat.SZULETESI_IDO + "  " +
                                  "Oltva: " + allat.OLTVA + "  " +
                                  "Nőstény: " + allat.NOSTENY + "  " +
                                  "oltott: " + allat.OLTVA + "  " +
                                  "Méret: " + allat.MERET + "  " +
                                  "Ivartalanított: " + allat.IVARTALANITOTT + "      ";
                    }
                    if (cliens.ReklamEmail(animal)) { MessageBox.Show("Sikeres!"); }
                    else { MessageBox.Show("Nem sikeres az email küldés"); }
                    proc.Close();
                    try
                    {
                        foreach (Process procka in Process.GetProcessesByName("ReklamHost"))
                        {
                            procka.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        cliens.ReklamEmail("Nem áll le a host csinálja valamit! " + ex.Message);
                        cliens.Hibalog("Nem áll le a host csinálja valamit! " + ex.Message);
                    }



                }
                else { MessageBox.Show("Nem található a Host file."); }
            }
            else { MessageBox.Show("Nincs sor kijelölve! vagy Előjegyzett állatot reklámoznál!"); }

        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("HibaLog.xml"))
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "HibaLog.xml";
                proc.Start();

            }
            else
            { MessageBox.Show("Nem létezik a log File"); }

        }
    }
}