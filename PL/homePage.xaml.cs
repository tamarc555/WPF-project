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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for homePage.xaml
    /// </summary>
    public partial class homePage : Page
    {
        private BlApi.IBL bl = new BL.BL();
        bool flag = false;
        public homePage()
        {
            InitializeComponent();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new listOfParcel(bl,MainWindow.manager).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new listOfStation(bl, MainWindow.manager).ShowDialog();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new listOfCustomer(bl, MainWindow.manager).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new listOfDrone(bl, MainWindow.manager).ShowDialog();
        }
    }
}
