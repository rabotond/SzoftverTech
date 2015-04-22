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
using AdatKezelő;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for EmptySpacesWindow.xaml
    /// </summary>
    public partial class EmptySpacesWindow : Window
    {
        user bejelentkezettUser;
        public EmptySpacesWindow(EmptySpaceWindowViewModel vm, user bejelentkezettUser)
        {
            InitializeComponent();
            this.bejelentkezettUser = bejelentkezettUser;
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PutInAnimal piaWindow = new PutInAnimal(this.bejelentkezettUser);
            piaWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            QueriePlaces qpWindow = new QueriePlaces(this.bejelentkezettUser);
            qpWindow.Show();
            this.Close();
        }
    }
}
