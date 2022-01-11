using PL.PO;
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
    /// Interaction logic for stationWindow.xaml
    /// </summary>
    public partial class stationWindow : Window
    {
        private BlApi.IBL bl;
        public stationWindow()
        {
            InitializeComponent();
        }
        public stationWindow(BlApi.IBL bL, int num = 0)
        {
            InitializeComponent();
            bl = bL;
            if (num == 0) //add station 
            {
                updateButton.Visibility = Visibility.Hidden;
                droneToListDataGrid.IsEnabled = false;
            }
            if (num != 0)  //update
            {
                addButton.Visibility = Visibility.Hidden;
                BlApi.BO.Station s = bl.getStation(num);
                PL.PO.Station dataContextStation = new Station(s.ID, s.Name, s.StationLocation.longitude, s.StationLocation.latitude, s.ChargeSlots, new List<Drone>());
                this.DataContext = dataContextStation;
                iDTextBox.IsEnabled = false;
                latitudeTextBox.IsEnabled = false;
                longitudeTextBox.IsEnabled = false;
                droneToListDataGrid.ItemsSource = s.ListDroneInCharge;
                iDColumn.IsReadOnly = true;
                battaryColumn.IsReadOnly = true;
                droneLocationColumn.IsReadOnly = true;
                maxWeigthColumn.IsReadOnly = true;
                modelColumn.IsReadOnly = true;
                parcelInDeliveryColumn.IsReadOnly = true;
                statusOfDroneColumn.IsReadOnly = true;
            }
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            BlApi.BO.Drone d = cell.DataContext as BlApi.BO.Drone;
            new addDroneWindow(bl, -1, d.ID).ShowDialog();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Station myStation = bl.getStation(int.Parse(iDTextBox.Text));
                myStation.ChargeSlots = int.Parse(chargeSlotsTextBox.Text);
                myStation.Name = int.Parse(nameTextBox.Text);
                bl.updateStation(myStation);
                this.Close();
                MessageBox.Show("פרטי התחנה עודכנו בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Station myStation = new BlApi.BO.Station(int.Parse(iDTextBox.Text), int.Parse(nameTextBox.Text), new BlApi.location(double.Parse(longitudeTextBox.Text), double.Parse(latitudeTextBox.Text)), int.Parse(chargeSlotsTextBox.Text), new List<BlApi.BO.Drone>());
                bl.addStation(myStation);
                MessageBox.Show("הוספת התחנה בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
}
}