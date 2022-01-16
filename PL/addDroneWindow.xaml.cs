using PL;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for addDroneWindow.xaml
    /// </summary>
    public partial class addDroneWindow : Window, INotifyPropertyChanged
    {
        private BlApi.IBL bl;
        BackgroundWorker Worker;
        public event PropertyChangedEventHandler PropertyChanged;
        bool auto;
        public bool Auto;
        bool Closing = false;
        bool Charge, Schedule, Release, Pickup, Deliver;

        private void updateDrone() => Worker.ReportProgress(0);
        private bool checkStop() => Worker.CancellationPending;

        public addDroneWindow()
        {
            InitializeComponent();
        }
        DroneToList myWindowDrone = null;

        public addDroneWindow(BlApi.IBL bL, DroneToList tempDr = null, int num2 = 0)
        {
            InitializeComponent();
            bl = bL;
            statusOfDroneComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            maxWeigthComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            timeInChargeText.Visibility = Visibility.Hidden;
            timeInChargeBox.Visibility = Visibility.Hidden;
            vButton.Visibility = Visibility.Hidden;

            parcelInDeliveryTextBox.IsEnabled = false;
            statusOfDroneComboBox.IsEnabled = false;
            battaryTextBox.IsEnabled = false;
            droneLocationTextBox.IsEnabled = false;
            if (tempDr == null && num2 == 0) //הוספת רחפן
            {
                updateButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Hidden;
                ParcelButton.Visibility = Visibility.Hidden;
                automatic.Visibility = Visibility.Hidden;
                manualButton.Visibility = Visibility.Hidden;
            }
            else if (tempDr.ID > 0 ) //עדכון רחפן
            {
                myWindowDrone = tempDr;
                this.DataContext = tempDr;
                plusButton.Visibility = Visibility.Hidden;
                iDTextBox.IsEnabled = false;
                maxWeigthComboBox.IsEnabled = false;
                maxWeigthComboBox.Text = tempDr.MaxWeigth.ToString();
                statusOfDroneComboBox.Text = tempDr.StatusOfDrone.ToString();

            }
            else if (num2 != 0)
            {
                BlApi.BO.Drone tempD = bl.getDrone(num2);
                PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                this.DataContext = d;
                plusButton.Visibility = Visibility.Hidden;
                updateButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Hidden;
                ParcelButton.Visibility = Visibility.Hidden;
                maxWeigthComboBox.Text = d.MaxWeigth.ToString();
                statusOfDroneComboBox.Text = tempD.StatusOfDrone.ToString();
                iDTextBox.IsEnabled = false;
                battaryTextBox.IsEnabled = false;
                modelTextBox.IsEnabled = false;
                maxWeigthComboBox.IsEnabled = false;
                statusOfDroneComboBox.IsEnabled = false;
                droneLocationTextBox.IsEnabled = false;
                automatic.Visibility = Visibility.Hidden;
                manualButton.Visibility = Visibility.Hidden;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void maxWeigthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void parcelInDeliveryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Drone tempDrone = bl.getDrone(int.Parse(iDTextBox.Text));
                if (tempDrone.Model != modelTextBox.Text)
                {
                    tempDrone.Model = modelTextBox.Text;
                    bl.updateDrone(tempDrone);

                    MessageBox.Show("פרטי הרחפן עודכנו בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void modelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                BlApi.BO.Drone tempDrone = new();
                tempDrone.ID = int.Parse(iDTextBox.Text);
                tempDrone.Model = modelTextBox.Text;
                tempDrone.MaxWeigth = (BO.Enum.WeightCategories)maxWeigthComboBox.SelectedItem;
                bl.addDrone(tempDrone);
                MessageBox.Show("הוספת הרחפן בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((int)myWindowDrone.StatusOfDrone == (int)DroneStatuses.maintenance)  //בטעינה- שחרור
                {
                    updateButton.Visibility = Visibility.Hidden;
                    ParcelButton.Visibility = Visibility.Hidden;
                    timeInChargeBox.Visibility = Visibility.Visible;
                    timeInChargeText.Visibility = Visibility.Visible;
                    vButton.Visibility = Visibility.Visible;
                }
                if (statusOfDroneComboBox.Text == "available") //שליחה לטעינה
                {
                    bl.sendToCharge(bl.getDrone(int.Parse(iDTextBox.Text)));
                    BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                    myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                    myWindowDrone.DroneLocation = tempDrone1.DroneLocation.ToString();
                    myWindowDrone.Battary = tempDrone1.Battary;
                    MessageBox.Show("שליחת הרחפן לטעינה בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (statusOfDroneComboBox.Text == "scheduled" || statusOfDroneComboBox.Text == "delivery") throw new NotSupportedException("לא ניתן לשלוח את הרחפן לטעינה\nהרחפן במשלוח\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void timeInChargeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void vButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.releaseFromCharge(bl.getDrone(int.Parse(iDTextBox.Text)), double.Parse(timeInChargeBox.Text));
                BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                myWindowDrone.Battary = tempDrone1.Battary;
                MessageBox.Show("שחרור הרחפן מטעינה בוצע בהצלחה \n", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
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
                if (bl.getDrone(int.Parse(iDTextBox.Text)).TheParcelInDelivery == null || bl.getDrone(int.Parse(iDTextBox.Text)).TheParcelInDelivery.ID == 0) //requested or after delievery
                {
                    bl.updateScheduled(bl.getDrone(int.Parse(iDTextBox.Text)));
                    BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                    myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                    myWindowDrone.DroneLocation = tempDrone1.DroneLocation.ToString();
                    myWindowDrone.Battary = tempDrone1.Battary;
                    myWindowDrone.ParcelInDelivery = tempDrone1.TheParcelInDelivery.ID;
                    MessageBox.Show("הרחפן שוייך בהצלחה\n", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                BlApi.BO.Parcel tempPacel = bl.getParcel(bl.getDrone(int.Parse(iDTextBox.Text)).TheParcelInDelivery.ID);
                if (tempPacel.Scheduled != null && tempPacel.PickedUp == null) //החבילה שויכה וצריך לאסוף אותה
                {
                    bl.updatePickedUp(bl.getDrone(int.Parse(iDTextBox.Text)));
                    BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                    myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                    myWindowDrone.DroneLocation = tempDrone1.DroneLocation.ToString();
                    myWindowDrone.Battary = tempDrone1.Battary;
                    myWindowDrone.ParcelInDelivery = tempDrone1.TheParcelInDelivery.ID;
                    MessageBox.Show("החבילה נאספה בהצלחה\n", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (tempPacel.PickedUp != null && tempPacel.Delivered == null) //החבילה נאספה וצריך לספק אותה
                {
                    bl.updateSupply(bl.getDrone(int.Parse(iDTextBox.Text)));
                    BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                    myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                    myWindowDrone.DroneLocation = tempDrone1.DroneLocation.ToString();
                    myWindowDrone.Battary = tempDrone1.Battary;
                    myWindowDrone.ParcelInDelivery = tempDrone1.TheParcelInDelivery.ID;
                    MessageBox.Show("החבילה סופקה בהצלחה\n", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void automatic_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("התהליך האוטומטי הופעל");
            Auto = true;
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.RunWorkerAsync(int.Parse(iDTextBox.Text));
        }
       // static bool myCheakStop = true;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //bl.startDroneSimulator((int)e.Argument, updateDrone, checkStop);
            var myDrone = bl.getDrone(myWindowDrone.ID);
            double timeInC = 0;
            int parcelID = 0;
            int stationID = 0;
            bool flag = true;

            do
            {

                switch (myDrone.StatusOfDrone)
                {
                    case BO.Enum.DroneStatuses.available:
                        lock (bl)
                        {
                            parcelID = bl.nextParcel(myDrone);
                            switch (parcelID, myDrone.Battary)
                            {
                                case (0, 100):  //שוחרר מטעינה אבל אין חבילה
                                                //סיום תהליכון כפוי
                                    flag = false;
                                    break;
                                case (0, _):  //פנוי ואין חבילה - שליחה לטעינה
                                    stationID = bl.findClosestStation(myDrone);
                                    if (stationID != -1)
                                    {
                                        bl.sendToCharge(bl.getDrone(myDrone.ID));
                                    }
                                    break;
                                case (_, _): //שויכה חבילה - שליחה
                                    bl.updateScheduled(bl.getDrone(myDrone.ID));
                                    break;
                            }
                        }
                        break;
                    case BO.Enum.DroneStatuses.scheduled:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            bl.updatePickedUp(bl.getDrone(myDrone.ID));
                            break;
                        }
                    case BO.Enum.DroneStatuses.maintenance:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                            switch (myWindowDrone.Battary)
                            {
                                case (2):  //הרחפן טעון- שחרור
                                    bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                                    break;
                                case (_):  //הרחפן לא הגיע ל100%
                                    timeInC += 0.5;
                                    break;
                            }
                        }
                        break;
                    case BO.Enum.DroneStatuses.delivery:
                        lock (bl)
                        {
                            switch (myDrone.TheParcelInDelivery.StatusOfParcel)
                            {
                                case (false):  //החבילה עוד לא נאספה
                                    bl.updatePickedUp(bl.getDrone(myDrone.ID));
                                    break;
                                case (true):  //החבילה נאספה והיא בדרך
                                    bl.updateSupply(bl.getDrone(myDrone.ID));
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("ERROR");
                }
                Thread.Sleep(1000);
                updateDroneView(myDrone);

                Thread.Sleep(1000);
                 myDrone = bl.getDrone(myDrone.ID);
                Thread.Sleep(1000);

            } while (flag&& Auto&& !checkStop());
        }

        private void manualButton_Click(object sender, RoutedEventArgs e)
        {
            Auto = false;
            Worker = null;
            MessageBox.Show("התהליך האוטומטי הופסק");
            Closing = true;
            this.Close();
        }

        private void myLocation_Click(object sender, RoutedEventArgs e)
        {
            BlApi.BO.Drone drone = bl.getDrone(int.Parse(iDTextBox.Text));
            new LocationWindow(drone.DroneLocation.latitude, drone.DroneLocation.longitude).ShowDialog();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateDroneView(bl.getDrone(myWindowDrone.ID));
            Thread.Sleep(1000);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Auto = false;
            Worker = null;
            updateDroneView(bl.getDrone(myWindowDrone.ID));
            MessageBox.Show("התהליך האוטומטי הושלם");
            if (Closing) Close();
        }

        void updateFlags(BlApi.BO.Drone tempDrone)
        {
            Charge = Release = Schedule = Pickup = Deliver = false;
            switch (tempDrone.StatusOfDrone)
            {
                case BO.Enum.DroneStatuses.available:
                    Charge = Schedule = true;
                    break;
                case BO.Enum.DroneStatuses.maintenance:
                    Release = true;
                    break;
                case BO.Enum.DroneStatuses.delivery:
                    if (tempDrone.TheParcelInDelivery.StatusOfParcel == false)
                        Pickup = true;
                    else
                        Deliver = true;
                    break;
            }
        }

        private void updateDroneView(BlApi.BO.Drone tempD)
        {
            myWindowDrone.Model = tempD.Model;
            myWindowDrone.StatusOfDrone = (DroneStatuses)tempD.StatusOfDrone;
            myWindowDrone.DroneLocation = tempD.DroneLocation.ToString();
            myWindowDrone.Battary = tempD.Battary;
            myWindowDrone.ParcelInDelivery = tempD.TheParcelInDelivery.ID;
            updateFlags(tempD);
        }
    }
}
