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

namespace PL
{
    /// <summary>
    /// Interaction logic for listOfCustomer.xaml
    /// </summary>
    /// 
    public partial class listOfCustomer : Window
    {
        private ObservableCollection<BlApi.BO.CustomerToList> _myCollection = new ObservableCollection<BlApi.BO.CustomerToList>();
        bool _myFlag;
        private BlApi.IBL bl;

        public listOfCustomer()
        {
            InitializeComponent();
        }
        public listOfCustomer(BlApi.IBL bL, bool myFlag)
        {
            _myFlag = myFlag;
            InitializeComponent();
            bl = bL;
            List<BlApi.BO.CustomerToList> lst = (List<BlApi.BO.CustomerToList>)bl.getListCustomerToList();
            for (int i = 0; i < lst.Count; i++)
                _myCollection.Add(lst[i]);
            customerToListDataGrid.DataContext = _myCollection;
            customerToListDataGrid.IsReadOnly = true;
            if (myFlag == false)
            {
                addButton.Visibility = Visibility.Hidden;
                deleteButton.Visibility = Visibility.Hidden;
            }
        }
        
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_myFlag == true)
            {
                DataGridCell cell = sender as DataGridCell;
                BlApi.BO.CustomerToList d = cell.DataContext as BlApi.BO.CustomerToList;
                new customerWindow(bl, d.ID).ShowDialog();
                customerToListDataGrid.ItemsSource = bl.getListCustomerToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)  //add
        {
            new customerWindow(bl).ShowDialog();
            customerToListDataGrid.ItemsSource = bl.getListCustomerToList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.CustomerToList s = (BlApi.BO.CustomerToList)customerToListDataGrid.SelectedItem;
                bl.deleteCustomer(s.ID);
                customerToListDataGrid.ItemsSource = bl.getListCustomerToList();
                MessageBox.Show("פרטי הלקוח נמחקו בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
