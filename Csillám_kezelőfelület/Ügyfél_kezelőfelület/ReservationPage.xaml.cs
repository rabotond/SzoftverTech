﻿using System;
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
using System.ComponentModel;
using AdatKezelő;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Window
    {
        public ReservationPage()
        {
            InitializeComponent();//
        }

        private void Előjegyzés(object sender, RoutedEventArgs e)
        {
            ALLAT allat = new ALLAT();
            // melyiket is ?
            allat.ELOJEGYZETT=true;   
        }

        private void Regisztráció(object sender, RoutedEventArgs e)
        {
            CreateUser other = new CreateUser();
            this.Close();
            other.Show();
        }
    }
    class ReservationPageViewModel {
        BitmapImage kép;

        public BitmapImage Kép
        {
            get { return kép; }
            set { kép = value; OnPropertyChanged("Kép"); }
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
}
