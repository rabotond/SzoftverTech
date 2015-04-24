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
        ELEDEL eledel;
        KENNEL kennel;

        public MaterialManagement(Admin_kezelőfelület_businessLogic ujbl)
        {
            InitializeComponent();
            BL = ujbl;
            IEnumerable mater_adatok = BL.FrissitEledel_kennel();
            materialdata.ItemsSource = null;
            materialdata.ItemsSource = mater_adatok;
        }
        private void Visssza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mentés_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(mennyitAD.Text) > 0 || int.Parse(mennyitVESZki.Text) > 0)
            {
                //eledel = new ELEDEL();
                //eledel.FAJTA= materialdata.
            }
            else 
            { 
            }
            //BL.Eledelt_kennelt_hozzáad();
        }
    }
}
