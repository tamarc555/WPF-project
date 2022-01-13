using BlApi.BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static PO.Exceptions;

namespace PL
{
    /// <summary>
    /// Interaction logic for listOfStation.xaml
    /// </summary>
    public partial class listOfStation : Window
    {
        private ObservableCollection<BlApi.BO.StationToList> _myCollection = new ObservableCollection<BlApi.BO.StationToList>();

        private BlApi.IBL bl;
        public listOfStation()
        {
            InitializeComponent();

        }
        public listOfStation(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            List<BlApi.BO.StationToList> lst = (List<BlApi.BO.StationToList>)bl.getListStationToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(lst[i]);
            stationToListDataGrid.DataContext = _myCollection;
            stationToListDataGrid.IsReadOnly = true;
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            DataGridCell cell = sender as DataGridCell;
            StationToList d = cell.DataContext as StationToList;
            new stationWindow(bl, d.ID).ShowDialog();
            stationToListDataGrid.ItemsSource = bl.getListStationToList();
        }

        private void deleteStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StationToList s = (StationToList)stationToListDataGrid.SelectedItem;
                bl.deleteStation(s.ID);
                stationToListDataGrid.ItemsSource = bl.getListStationToList();
                MessageBox.Show("התחנה נמחקה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new stationWindow(bl).ShowDialog();
            stationToListDataGrid.ItemsSource = bl.getListStationToList();
        }
    }
}
