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

namespace PL
{
    /// <summary>
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        public LocationWindow()
        {
            InitializeComponent();
        }
        public LocationWindow(double myLat, double myLon)
        {
            InitializeComponent();
            webBrowser.Source = new Uri("https://www.google.com/maps/place/" + myLat + "," + myLon);
           // webBrowser.NavigateToString("https://www.google.com/maps/place/" + myLat + ","+ myLon);
        }

        //        private void Button_Click(object sender, RoutedEventArgs e)
        //        {
        //            Uri uri = new Uri(textBox.Text, UriKind.RelativeOrAbsolute);
        //            WebBrowser.Navigate(string());
        //        }
    }
}
