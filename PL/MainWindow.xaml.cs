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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBL bl = new BL.BL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new listOfDrone(bl).ShowDialog();
            //listOfDrone windowListOfDrone = new listOfDrone();
            //windowListOfDrone.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new listOfStation(bl).ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new listOfCustomer(bl).ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new listOfParcel(bl).ShowDialog();
        }
    }
}
