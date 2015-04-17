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

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for DonationPage.xaml
    /// </summary>
    public partial class DonationPage : Window
    {
        DonationPageBusinessLogic donationPageBusinessLogic;
        public DonationPage()
        {
            InitializeComponent();
            donationPageBusinessLogic = new DonationPageBusinessLogic();
        }

        private void AdományozClick(object sender, RoutedEventArgs e)
        {
            if (textBoxMennyiség.Text != "" && textBoxUsername.Text != "")
            {
                if (radioButtonÉtel.IsChecked == true)
                {
                    donationPageBusinessLogic.Adományoz(Adomány_típus.Étel, int.Parse(textBoxMennyiség.Text),
                textBoxMennyiség.Text);
                }
                else if (radioButtonPénz.IsChecked == true)
                {
                    donationPageBusinessLogic.Adományoz(Adomány_típus.Pénz, int.Parse(textBoxMennyiség.Text),
                textBoxMennyiség.Text);
                }
                else
                {
                    MessageBox.Show("Kérem válasszon adomány típust!");
                }
            }
            else
            {
                MessageBox.Show("Nincs kitöltve minden adat!");
            }
        }

        private void VisszaClick(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
    class DonationPageBusinessLogic
    {
        IÜgyfél_kezelő ügyfélKezelő;
        public DonationPageBusinessLogic()
        {
            ügyfélKezelő = new Ügyfél_Kezelő();
        }
        public void Adományoz(Adomány_típus típus, int mennyiség, string ki)
        {
            ügyfélKezelő.Adományoz(típus, mennyiség, ki);
            MessageBox.Show("Köszönjük!");
        }
    }
}
