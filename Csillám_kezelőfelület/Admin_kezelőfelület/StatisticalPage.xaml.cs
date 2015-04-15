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
    /// Interaction logic for StatisticalPage.xaml
    /// </summary>
    public partial class StatisticalPage : Window
    {
        public StatisticalPage()
        {
            InitializeComponent();
        }
        
	internal Button buttonKimuatásXLS;
	internal DatePicker datePickerKezdet;
	internal DatePicker datePickerVég;
	internal System.Windows.Controls.Label label1;
	internal System.Windows.Controls.Label label2;
	internal RadioButton radioButtonPénzügyi;
	internal RadioButton radioButtonStatisztika;


	/// 
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ButtonKimutatásTXT_Click(object sender, RoutedEventArgs e){

	}

	/// 
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ButtonKimutatásXLS_Click(object sender, RoutedEventArgs e){

	}

	public void Statisztika_készítő(){

	}
    }
}
