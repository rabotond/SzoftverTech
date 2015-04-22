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
using System.Collections;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        Admin_kezelőfelület_viewmodel VM;
        public Statistic(Admin_kezelőfelület_viewmodel vm)
        {
            InitializeComponent();
            VM = vm;
            IEnumerable stat_adatok = VM.Stat.Napok;
            statgrid.ItemsSource =null;
            statgrid.ItemsSource = stat_adatok;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
