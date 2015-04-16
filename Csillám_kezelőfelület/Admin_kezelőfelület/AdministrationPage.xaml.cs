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

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Window
    {
        public AdministrationPage()
        {
            InitializeComponent();
        }

        private void UjallatClick(object sender, RoutedEventArgs e)
        {
            NewAnimal page = new NewAnimal();
            this.Close();
            page.Show();
        }
    }
}
