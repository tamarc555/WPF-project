using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using static PO.Enum;
using static PO.Exceptions;

namespace PL
{
    /// <summary>
    /// Interaction logic for listOfDrone.xaml
    /// </summary>
    public partial class listOfDrone : Window //, INotifyPropertyChanged
    {
        // private ObservableCollection<BlApi.BO.DroneToList> _myCollection = new ObservableCollection<BlApi.BO.DroneToList>();
       // public event PropertyChangedEventHandler PropertyChanged;
        private BlApi.IBL bl;
        bool _myFlag = false;
        ObservableCollection<DroneToList> _myCollection = new ObservableCollection<DroneToList>();
        //static readonly BlApi.IBL bl = BlApi.BlFactory.GetBl();
        //ObservableCollection<BlApi.BO.DroneToList> _myCollection = new(bl.getListDroneToList());
        //public ObservableCollection<BlApi.BO.DroneToList> _MyCollection
        //{
        //    get => _myCollection;
        //    private set
        //    {
        //        _myCollection = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_MyCollection)));
        //    }
        //}
        public listOfDrone()
        {
            InitializeComponent();
        }

        public listOfDrone(BlApi.IBL bL, bool myFlag)
        {
            InitializeComponent();
            _myFlag = myFlag;
            bl = bL;
            List<BlApi.BO.DroneToList> lst = (List<BlApi.BO.DroneToList>)bl.getListDroneToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));

            droneToListDataGrid.DataContext = _myCollection;
            //List<BlApi.BO.Drone> lst = (List<BlApi.BO.Drone>)bl.getListDrone();
            //for (int i = 0; i < lst.Count; i++)
            //  _myCollection.Add(lst[i]);
            //droneToListDataGrid.DataContext = _myCollection;
            droneToListDataGrid.IsReadOnly = true;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            if (myFlag == false)
            {
                deleteButton.Visibility = Visibility.Hidden;
                addDrone.Visibility = Visibility.Hidden;
            }
        }


        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((weightSelector.IsLoaded) == false||weightSelector.SelectedItem==null)
                return;
            if (StatusSelector.Text != "")
            {
                
                List<BlApi.BO.myDroneToList> myList = new List<BlApi.BO.myDroneToList>();
                for (int i = 0; i < _myCollection.Count; i++)
                    myList.Add(new BlApi.BO.myDroneToList(_myCollection[i].ID, _myCollection[i].Model, (BO.Enum.WeightCategories)_myCollection[i].MaxWeigth, _myCollection[i].Battary, (BO.Enum.DroneStatuses)_myCollection[i].StatusOfDrone, _myCollection[i].DroneLocation, _myCollection[i].ParcelInDelivery));

                List<BlApi.BO.myDroneToList> lst = (List<BlApi.BO.myDroneToList>)bl.getPartOfDrone(drone => (int)drone.MaxWeigth == (int)weightSelector.SelectedItem, myList);
                _myCollection.Clear();
                for (int i = 0; i < lst.Count; i++)
                    _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation, lst[i].ParcelInDelivery));

                //droneToListDataGrid.ItemsSource = bl.getPartOfDrone(drone => (int)drone.MaxWeigth == (int)weightSelector.SelectedItem, (IEnumerable<BlApi.BO.DroneToList>)droneToListDataGrid.ItemsSource);
            }
            else
            {
                _myCollection.Clear();
                List<BlApi.BO.myDroneToList> lst = (List<BlApi.BO.myDroneToList>)bl.getPartOfDrone(drone => (int)drone.MaxWeigth == (int)weightSelector.SelectedItem);
                for (int i = 0; i < lst.Count; i++)
                    _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation, lst[i].ParcelInDelivery));

                //droneToListDataGrid.ItemsSource = bl.getPartOfDrone(drone => (int)drone.MaxWeigth == (int)weightSelector.SelectedItem);
            }
        }

        //private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    BlApi.BO.Drone tempDrone = (BlApi.BO.Drone)droneToListDataGrid.SelectedItem;
        //       //(droneToListDataGrid.SelectedItem as BlApi.BO.Drone);
        //    if (tempDrone != null)
        //    {
        //        new addDroneWindow(bl, tempDrone.ID).ShowDialog();
        //        //droneToListDataGrid.ItemsSource = (System.Collections.IEnumerable)bl.getDrone(tempDrone.ID);
        //        droneToListDataGrid.ItemsSource = bl.getListDrone();

        //        //_myCollection.Remove(tempDrone);
        //        //BO.Drone updateDrone = bl.getDrone(tempDrone.ID);
        //        //_myCollection.Add(updateDrone);
        //        //droneToListDataGrid.ItemsSource = _myCollection;
        //    }
        //}

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.IsLoaded == false)
                return;
            if (StatusSelector.SelectedItem == null)
                return;
            if (weightSelector.Text != "")
            {
                //_myCollection = new();
                //List<BO.Drone> lst = (List<BO.Drone>)bl.getPartOfDrone(drone => drone.StatusOfDrone == (DroneStatuses)StatusSelector.SelectedItem, (IEnumerable<BL.BO.DroneToList>)droneToListDataGrid.ItemsSource);
                //for (int i = 0; i < lst.Count; i++)
                //    _myCollection.Add(lst[i]);
                //droneToListDataGrid.ItemsSource = _myCollection;
                List<BlApi.BO.myDroneToList> myList = new List<BlApi.BO.myDroneToList>();
                for (int i = 0; i < _myCollection.Count; i++)
                    myList.Add(new BlApi.BO.myDroneToList(_myCollection[i].ID, _myCollection[i].Model, (BO.Enum.WeightCategories)_myCollection[i].MaxWeigth, _myCollection[i].Battary, (BO.Enum.DroneStatuses)_myCollection[i].StatusOfDrone, _myCollection[i].DroneLocation, _myCollection[i].ParcelInDelivery));
                List<BlApi.BO.myDroneToList> lst = (List<BlApi.BO.myDroneToList>)bl.getPartOfDrone(drone => (int)drone.StatusOfDrone == (int)StatusSelector.SelectedItem, myList);

                _myCollection.Clear();
               
                 for (int i = 0; i < lst.Count; i++)
                    _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation, lst[i].ParcelInDelivery));

                //droneToListDataGrid.ItemsSource = bl.getPartOfDrone(drone => (int)drone.StatusOfDrone == (int)StatusSelector.SelectedItem, (IEnumerable< BlApi.BO.DroneToList>)droneToListDataGrid.ItemsSource);
            }
            else
            {
                //    _myCollection = new();
                //    List<BO.Drone> lst = (List<BO.Drone>)bl.getPartOfDrone(drone => drone.StatusOfDrone == (DroneStatuses)StatusSelector.SelectedItem);
                //    for (int i = 0; i < lst.Count; i++)
                //        _myCollection.Add(lst[i]);
                //    droneToListDataGrid.ItemsSource = _myCollection;
                _myCollection.Clear();
                List<BlApi.BO.myDroneToList> lst = (List<BlApi.BO.myDroneToList>)bl.getPartOfDrone(drone => (int)drone.StatusOfDrone == (int)StatusSelector.SelectedItem);
                for (int i = 0; i < lst.Count; i++)
                    _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation, lst[i].ParcelInDelivery));

                // droneToListDataGrid.ItemsSource = bl.getPartOfDrone(drone => (int)drone.StatusOfDrone == (int)StatusSelector.SelectedItem);

            }
        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            new addDroneWindow(bl).ShowDialog();
            _myCollection.Clear();
            List<BlApi.BO.DroneToList> lst = (List<BlApi.BO.DroneToList>)bl.getListDroneToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));
            /* droneToListDataGrid.ItemsSource = bl.getListDroneToList();*/
            // droneToListDataGrid.ItemsSource = _MyCollection;
        }


        private void resetButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (StatusSelector.Text != "")
            {
                StatusSelector.Text = "";
            }
            if (weightSelector.Text != "")
            {
                weightSelector.Text = "";
            }

            _myCollection.Clear();
            List<BlApi.BO.DroneToList> lst = (List<BlApi.BO.DroneToList>)bl.getListDroneToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));
            //_myCollection = new();
            //List<BO.Drone> lst = (List<BO.Drone>)bl.getListDrone();
            //for (int i = 0; i < lst.Count; i++)
            //    _myCollection.Add(lst[i]);
            /*droneToListDataGrid.ItemsSource = bl.getListDroneToList();*/
            //droneToListDataGrid.ItemsSource = _myCollection;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_myFlag == true)
            {
                DataGridCell cell = sender as DataGridCell;
                DroneToList d = cell.DataContext as DroneToList;
                new addDroneWindow(bl, d.ID).ShowDialog();
                _myCollection.Clear();
                List<BlApi.BO.DroneToList> lst = (List<BlApi.BO.DroneToList>)bl.getListDroneToList();
                for (int i = 0; i < lst.Count; i++)
                    _myCollection.Add(new DroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));
                /*droneToListDataGrid.ItemsSource = bl.getListDroneToList();*/
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            //var groups = droneToListDataGrid.ItemsSource.GroupBy(d => d.StatusOfDrone);
           // var groups = list//droneToListDataGrid.ItemsSource.GroupBy(d => d.StatusOfDrone);


            var lst = from drone in (IEnumerable<DroneToList>)droneToListDataGrid.ItemsSource
                      group drone by drone.StatusOfDrone into statusGroup
                      select new { StatusOfDrone = statusGroup.Key, lstSt = statusGroup };
            //_myCollection = new ObservableCollection<BlApi.BO.Drone>();
            _myCollection.Clear();
            foreach (var item in lst)
                foreach (var temp in item.lstSt)
                    _myCollection.Add((DroneToList)temp);
            //droneToListDataGrid.ItemsSource = _myCollection;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneToList d = (DroneToList)droneToListDataGrid.SelectedItem;
                bl.deleteDrone(d.ID);
                droneToListDataGrid.ItemsSource = bl.getListDroneToList();
                MessageBox.Show("הרחפן נמחק בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneToList d = (DroneToList)droneToListDataGrid.SelectedItem;
                if (d.ParcelInDelivery == 0) throw new RangeException("לרחפן זה אין חבילה שמשוייכת אליו");
                new parcelWindow(bl, d.ParcelInDelivery).ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
