using PO;
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
    /// Interaction logic for listOfParcel.xaml
    /// </summary>
    public partial class listOfParcel : Window
    {
        private ObservableCollection<BlApi.BO.ParcelToList> _myCollection = new ObservableCollection<BlApi.BO.ParcelToList>();

        private BlApi.IBL bl;


        public listOfParcel()
        {
            InitializeComponent();
        }

        public listOfParcel(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            List<BlApi.BO.ParcelToList> lst = (List<BlApi.BO.ParcelToList>)bl.getListParcelToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(lst[i]);
            parcelToListDataGrid.DataContext = _myCollection;
            parcelToListDataGrid.IsReadOnly = true;
            senderButton.Visibility = Visibility.Hidden;
            targetButton.Visibility = Visibility.Hidden;
            droneButton.Visibility = Visibility.Hidden;
        }

        //private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    { 
        //    DataGridCell cell = sender as DataGridCell;
        //    ParcelToList d = cell.DataContext as ParcelToList;
        //    if (d.ID == 0) throw new IDdoesntExists("לא ניתן לעדכן חבילה")
        //    new parcelWindow(bl, d.ID).ShowDialog();
        //    parcelToListDataGrid.ItemsSource = bl.getListParcelToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //add
            new parcelWindow(bl).ShowDialog();
            parcelToListDataGrid.ItemsSource = bl.getListParcelToList();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //delete
            try
            {
                BlApi.BO.ParcelToList p = (BlApi.BO.ParcelToList)parcelToListDataGrid.SelectedItem;
                bl.deleteParcel(p.ID);
                parcelToListDataGrid.ItemsSource = bl.getListParcelToList();
                MessageBox.Show("החבילה נמחקה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //menu bottun
        {
            try
            { 
                if(parcelToListDataGrid.SelectedItem == null) throw new IDdoesntExists("לא נבחרה חבילה");
                senderButton.Visibility = Visibility.Visible;
                targetButton.Visibility = Visibility.Visible;
                droneButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//sender Button
        {
            BlApi.BO.ParcelToList p = (BlApi.BO.ParcelToList)parcelToListDataGrid.SelectedItem;
            BlApi.BO.Customer c = bl.getCustomerByName(p.Sender);
            new customerWindow(bl, c.ID).ShowDialog();

            senderButton.Visibility = Visibility.Hidden;
            targetButton.Visibility = Visibility.Hidden;
            droneButton.Visibility = Visibility.Hidden;
        }

        private void targetButton_Click(object sender, RoutedEventArgs e)
        {
            BlApi.BO.ParcelToList p = (BlApi.BO.ParcelToList)parcelToListDataGrid.SelectedItem;
            BlApi.BO.Customer c = bl.getCustomerByName(p.Target);
            new customerWindow(bl, c.ID).ShowDialog();

            senderButton.Visibility = Visibility.Hidden;
            targetButton.Visibility = Visibility.Hidden;
            droneButton.Visibility = Visibility.Hidden;
        }

        private void droneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.ParcelToList p = (BlApi.BO.ParcelToList)parcelToListDataGrid.SelectedItem;
                BlApi.BO.Parcel p1 = bl.getParcel(p.ID);
                new addDroneWindow(bl, -1, p1.TheDroneInParcel.ID).ShowDialog();

                senderButton.Visibility = Visibility.Hidden;
                targetButton.Visibility = Visibility.Hidden;
                droneButton.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
