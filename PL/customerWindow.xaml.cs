using PL.PO;
using PO;
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
using static PO.Exceptions;

namespace PL
{
    /// <summary>
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class customerWindow : Window
    {
        private BlApi.IBL bl;

        public customerWindow()
        {
            InitializeComponent();
        }
        public customerWindow(BlApi.IBL bL, int num = 0)
        {
            InitializeComponent();
            bl = bL;
            if (num != 0) //update
            {
                BlApi.BO.Customer c = bl.getCustomer(num);
                Customer dataCntextCustomer = new Customer(c.ID, c.Name, c.Phone, c.CustomerLocation.longitude, c.CustomerLocation.latitude, new List<Parcel>(), new List<Parcel>());
                this.DataContext = dataCntextCustomer;
                addButton.Visibility = Visibility.Hidden;
                iDTextBox.IsEnabled = false;
                longitudeTextBox.IsEnabled = false;
                latitudeTextBox.IsEnabled = false;
                parcelDataGrid.ItemsSource = c.ParcelFromCustomer;
                iDColumn.IsReadOnly = true;
                priorityColumn.IsReadOnly = true;
                senderColumn.IsReadOnly = true;
                targetColumn.IsReadOnly = true;
                weightColumn.IsReadOnly = true;


                requestedColumn.IsReadOnly = true;
                scheduledColumn.IsReadOnly = true;
                pickedUpColumn.IsReadOnly = true;
                deliveredColumn.IsReadOnly = true;


                theDroneInParcelColumn.IsReadOnly = true;
                parcelDataGrid1.ItemsSource = c.ParcelToCustomer;
                iDColumn1.IsReadOnly = true;
                priorityColumn1.IsReadOnly = true;
                senderColumn1.IsReadOnly = true;
                targetColumn1.IsReadOnly = true;
                weightColumn1.IsReadOnly = true;


                requestedColumn1.IsReadOnly = true;
                scheduledColumn1.IsReadOnly = true;
                pickedUpColumn1.IsReadOnly = true;
                deliveredColumn1.IsReadOnly = true;


                theDroneInParcelColumn1.IsReadOnly = true;

            }
            if (num == 0) //add
            {
                updateButton.Visibility = Visibility.Hidden;
                parcelDataGrid.IsEnabled = false;
                parcelDataGrid1.IsEnabled = false;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Customer myCustomer = bl.getCustomer(int.Parse(iDTextBox.Text));
                myCustomer.Phone = phoneTextBox.Text;
                myCustomer.Name = nameTextBox.Text;
                bl.updateCustomer(myCustomer);
                this.Close();
                MessageBox.Show("פרטי הלקוח עודכנו בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                BlApi.BO.Customer myCustomer = new BlApi.BO.Customer(int.Parse(iDTextBox.Text), nameTextBox.Text, phoneTextBox.Text, new BlApi.location(double.Parse(longitudeTextBox.Text), double.Parse(latitudeTextBox.Text)), new List<BlApi.BO.Parcel>(), new List<BlApi.BO.Parcel>());
                bl.addCustomer(myCustomer);
                MessageBox.Show("הוספת הלקוח בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try { 
            DataGridCell cell = sender as DataGridCell;
            BlApi.BO.Parcel p = cell.DataContext as BlApi.BO.Parcel;
            new parcelWindow(bl, p.ID).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
