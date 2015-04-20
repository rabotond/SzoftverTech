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

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        Statisztika stat;
        public Statistic(Statisztika ujstat)
        {
            InitializeComponent();
            stat = ujstat;
            //statgrid.ItemsSource = stat.Stat_listak;
            //statgrid.set
            //statgrid.ColumnFromDisplayIndex(0).SetValue(ColumnDefinition.NameProperty, stat.Stat_listak.ElementAt(0).Key.ToString());
        }
    }
}
