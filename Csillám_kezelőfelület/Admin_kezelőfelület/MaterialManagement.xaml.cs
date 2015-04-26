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
using Csillám_kezelőfelület.Admin_kezelőfelület;
using AdatKezelő;
using System.Collections;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for MaterialManagement.xaml
    /// </summary>
    public partial class MaterialManagement : Window
    {
        Admin_kezelőfelület_businessLogic BL;
        Admin_kezelőfelület_viewmodel VM;
        ELEDEL eledel;
        KENNEL kennel;

        public MaterialManagement(Admin_kezelőfelület_businessLogic ujbl,Admin_kezelőfelület_viewmodel vm)
        {       
            InitializeComponent();
            BL = ujbl; 
            VM=vm;
            DataContext = VM;
            Task.Factory.StartNew(() =>
            {
                List<eledel_kennel_VM> mater_adatok = BL.FrissitEledel_kennel();
                Dispatcher.Invoke(new Action(() => { VM.Eledel_kennel = mater_adatok;  }));               
            });
        }

        private void Visssza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mentés_Click(object sender, RoutedEventArgs e)
        {
            if (materialdata.SelectedItem!=null)
            {
                if (int.Parse(mennyitAD.Text) > 0 || int.Parse(mennyitVESZki.Text) > 0)
                {
                    eledel = new ELEDEL();
                    eledel.FAJTA = (materialdata.SelectedItem as eledel_kennel_VM).TIPUS;
                    BL.Eledelt_kennelt_hozzáad(eledel, null, int.Parse(mennyitAD.Text) + int.Parse(mennyitVESZki.Text)*-1);
                    BL.KennelTablaHelyKarbanTartas(eledel.FAJTA);
                }
                if (int.Parse(töröl.Text) > 0 || int.Parse(bővít.Text) > 0)
                {
                    kennel = new KENNEL();
                    kennel.TIPUS = (materialdata.SelectedItem as eledel_kennel_VM).TIPUS;
                    BL.Eledelt_kennelt_hozzáad(null, kennel, int.Parse(töröl.Text)*-1 + int.Parse(bővít.Text));
                    BL.KennelTablaHelyKarbanTartas(kennel.TIPUS);
                }
               
                MessageBox.Show("táblák sikeresen módosítva!!!");   
               
                Task.Factory.StartNew(() =>
                {
                    List<eledel_kennel_VM> mater_adatok = BL.FrissitEledel_kennel();
                    Dispatcher.Invoke(new Action(() => { VM.Eledel_kennel = mater_adatok; }));
                });
            }
            else 
            {
                MessageBox.Show("ERROR >>>>>>>>>>>>>>> nincs kijelölve rekord!!!"); 
            }
        }
    }
}
