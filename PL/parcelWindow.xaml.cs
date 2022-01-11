using PL.PO;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for parcelWindow.xaml
    /// </summary>
    public partial class parcelWindow : Window
    {
        private BlApi.IBL bl;
        public parcelWindow()
        {
            InitializeComponent();
        }
        public parcelWindow(BlApi.IBL bL, int num = 0)
        {
            InitializeComponent();
            bl = bL;
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            if (num == 0) //add parcel 
            {
                //updateButton.Visibility = Visibility.Hidden;
                requestedDatePicker.IsEnabled = false;
                scheduledDatePicker.IsEnabled = false;
                pickedUpDatePicker.IsEnabled = false;
                deliveredDatePicker.IsEnabled = false;
                theDroneInParcelTextBox.IsEnabled = false;
            }
            //if (num != 0)  //update
            //{
            //    addButton.Visibility = Visibility.Hidden;
            //    BlApi.BO.Parcel p = bl.getParcel(num);
            //    Parcel dataContextParcel = new Parcel(p.ID, p.Sender.ID, p.Target.ID, (WeightCategories)p.Weight, (Priorities)p.Priority, p.TheDroneInParcel.ID, p.Requested, p.Scheduled, p.PickedUp, p.Delivered);
            //    this.DataContext = dataContextParcel;
            //    iDTextBox.IsEnabled = false;
        //}
            if (num != 0)  //הצגת נתונים
            {
                addButton.Visibility = Visibility.Hidden;
                BlApi.BO.Parcel p = bl.getParcel(num);
                Parcel dataContextParcel = new Parcel(p.ID, p.Sender.ID, p.Target.ID, (WeightCategories)p.Weight, (Priorities)p.Priority, p.TheDroneInParcel.ID, p.Requested, p.Scheduled, p.PickedUp, p.Delivered);
                this.DataContext = dataContextParcel;
                iDTextBox.IsEnabled = false;
                weightComboBox.IsEnabled = false;
                weightComboBox.Text = ((WeightCategories)(p.Weight)).ToString();
                priorityComboBox.IsEnabled = false;
                priorityComboBox.Text = p.Priority.ToString();
                senderTextBox.IsEnabled = false;
                targetTextBox.IsEnabled = false;
                requestedDatePicker.IsEnabled = false;
                scheduledDatePicker.IsEnabled = false;
                pickedUpDatePicker.IsEnabled = false;
                deliveredDatePicker.IsEnabled = false;
                theDroneInParcelTextBox.IsEnabled = false;
            }
}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Parcel myParcel = new BlApi.BO.Parcel(int.Parse(iDTextBox.Text), new BlApi.BO.CustomerInParcel(bl.getCustomer(int.Parse(senderTextBox.Text)).ID, bl.getCustomer(int.Parse(senderTextBox.Text)).Name), new BlApi.BO.CustomerInParcel(bl.getCustomer(int.Parse(targetTextBox.Text)).ID, bl.getCustomer(int.Parse(targetTextBox.Text)).Name), (BO.Enum.WeightCategories)(weightComboBox.SelectedItem), (BO.Enum.Priorities)(priorityComboBox.SelectedItem), new BlApi.BO.DroneInParcel(), DateTime.Now, null, null, null);
                bl.addParcel(myParcel);
                MessageBox.Show("הוספת החבילה בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //public class NotBooleanToVisibilityConvereter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        bool boolValue = (bool)value;
        //        if (boolValue) return Visibility.Hidden;
        //        else return Visibility.Visible;
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    }

}

