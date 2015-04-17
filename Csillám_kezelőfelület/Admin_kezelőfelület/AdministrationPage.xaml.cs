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
using Csillám_kezelőfelület.Admin_kezelőfelület;

namespace Csillamponi_Allatmenhely
{
    /// <summary>
    /// Interaction logic for AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Window
    {
        Admin_kezelőfelület_businessLogic BL;
        Admin_kezelőfelület_viewmodel VM;

        public AdministrationPage()
        {
            InitializeComponent();

            BL = new Admin_kezelőfelület_businessLogic();
            VM = new Admin_kezelőfelület_viewmodel();
            DataContext = VM;
        }

        private void Frissit_Click(object sender, RoutedEventArgs e)//frissíti a listboxokat a friss adatbázis adatokkal
        {
            VM.Allatok = BL.FrissitAllat();
            VM.Ügyfelek = BL.FrissitÜgyfel();
        }

        private void UjallatClick(object sender, RoutedEventArgs e)
        {
            NewAnimal page = new NewAnimal();
            this.Close();
            page.Show();
        }

        private void Ujugyfel_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser();
            this.Close();
             page.Show();
        }

        private void AllatTöröl_Click(object sender, RoutedEventArgs e)
        {
            BL.Állatot_töröl((ALLAT)állatok.SelectedItem);
            VM.Allatok.Remove((ALLAT)állatok.SelectedItem);
        }
        private void UgyfeletTöröl_Click(object sender, RoutedEventArgs e)
        {
            BL.Ügyfelet_töröl((UGYFEL)ügyfelek.SelectedItem);
            VM.Ügyfelek.Remove((UGYFEL)ügyfelek.SelectedItem);
        }

        private void uygfelmodosit_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser((UGYFEL)ügyfelek.SelectedItem);
            this.Close();
            page.Show();
        }
        private void allatmodosit_Click(object sender, RoutedEventArgs e)
        {
            NewAnimal page = new NewAnimal((ALLAT)állatok.SelectedItem);
            this.Close();
             page.Show();
        }

        private void Kimutatas_Click(object sender, RoutedEventArgs e)
        {
            StatisticalPage page = new StatisticalPage();
            page.Show();
        }
    }
}
