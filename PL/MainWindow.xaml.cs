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
        public MainWindow()
        {
            InitializeComponent();
            exitManagment.Visibility = Visibility.Hidden;
            myBorder.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new listOfDrone(bl,flag).ShowDialog();
            //listOfDrone windowListOfDrone = new listOfDrone();
            //windowListOfDrone.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new listOfStation(bl,flag).ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new listOfCustomer(bl,flag).ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new listOfParcel(bl,flag).ShowDialog();
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

        private void ManagerLogin_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Ayelet" && passwordTextBox.Password == "1234qwer")
            {
                MessageBox.Show("ברוכה הבאה איילת, הנך מוזמנת לבצע עדכונים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
                NameTextBox.IsEnabled = false;
                passwordTextBox.IsEnabled = false;
                flag = true;
                ManagerLogin.Visibility = Visibility.Hidden;
                exitManagment.Visibility = Visibility.Visible;
            }
            else if (NameTextBox.Text == "Tamar" && passwordTextBox.Password == "tamar55")
            {
                MessageBox.Show("ברוכה הבאה תמר, הנך מוזמנת לבצע עדכונים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
                NameTextBox.IsEnabled = false;
                passwordTextBox.IsEnabled = false;
                flag = true;
                ManagerLogin.Visibility = Visibility.Hidden;
                exitManagment.Visibility = Visibility.Visible;
            }
            else 
            {
                MessageBox.Show("שם המשתמש או הסיסמא אינם רשומים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            exitManagment.Visibility = Visibility.Hidden;
            ManagerLogin.Visibility = Visibility.Visible;
            NameTextBox.IsEnabled = true;
            passwordTextBox.IsEnabled = true;
            flag = false;

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

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

            new hoursPage();
        }

    }
}
