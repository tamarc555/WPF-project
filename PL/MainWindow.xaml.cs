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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBL bl = new BL.BL();
        bool flag = false;
        public static bool manager = false;
        public MainWindow()
        {
            InitializeComponent();
             myBorder.Visibility = Visibility.Hidden;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            BlApi.BO.Station tempStation;
            List<BlApi.BO.StationToList> listStations = (List<BlApi.BO.StationToList>)bl.getListStationToList();
            for (int i = 0; i < listStations.Count; i++)
            { 
                tempStation = bl.getStation(listStations[i].ID);
                tempStation.ChargeSlots = (listStations[i].NumOfAvalibleChargeSlot + listStations[i].NumOfNotAvalibleChargeSlot);
                bl.updateStation(tempStation);
            }
            this.Close();
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            BlApi.BO.Station tempStation;
            List<BlApi.BO.StationToList> listStations = (List<BlApi.BO.StationToList>)bl.getListStationToList();
            for (int i = 0; i < listStations.Count; i++)
            {
                tempStation = bl.getStation(listStations[i].ID);
                tempStation.ChargeSlots = (listStations[i].NumOfAvalibleChargeSlot + listStations[i].NumOfNotAvalibleChargeSlot);
                bl.updateStation(tempStation);
            }
            this.Close();
        }

        

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (myBorder.Visibility == Visibility.Hidden)
                myBorder.Visibility = Visibility.Visible;
            else myBorder.Visibility = Visibility.Hidden;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new homePage();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new hoursPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new connectPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new managerEnterce();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (manager == true)
                MessageBox.Show("התנתקת מהמערכת, לא תוכל לבצע שינויים נוספים\n נשמח לראותך שוב!");
            if (manager == false)
                MessageBox.Show("התנתקת מהמערכת, \n נשמח לראותך שוב!");
            manager = false;
            myFrame.Content = new homePage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new delieveryPage();
        }
    }
}
