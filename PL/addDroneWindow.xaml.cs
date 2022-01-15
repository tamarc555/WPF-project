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
        public bool Auto;// { get => auto; set => this.setAndNotify(PropertyChanged, nameof(Auto), out auto, value); }
        bool Closing = false;
        bool Charge, Schedule, Release, Pickup, Deliver;
       
        private void updateDrone() => Worker.ReportProgress(0);
        private bool checkStop() => Worker.CancellationPending;

        public addDroneWindow()
        {
            InitializeComponent();
        }
        //public addDroneWindow(BlApi.IBL bL, int num=0, int num2=0)
        //{
        //    InitializeComponent();
        //    bl = bL;
        //    statusOfDroneComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        //    maxWeigthComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        //    timeInChargeText.Visibility = Visibility.Hidden;
        //    timeInChargeBox.Visibility = Visibility.Hidden;
        //    vButton.Visibility = Visibility.Hidden;

        //    parcelInDeliveryTextBox.IsEnabled = false;
        //    statusOfDroneComboBox.IsEnabled = false;
        //    battaryTextBox.IsEnabled = false;
        //    droneLocationTextBox.IsEnabled = false;
        //    if (num == 0) //הוספת רחפן
        //    {
        //        updateButton.Visibility = Visibility.Hidden;
        //        ChargeButton.Visibility = Visibility.Hidden;
        //        ParcelButton.Visibility = Visibility.Hidden;
        //        automatic.Visibility = Visibility.Hidden;
        //        manualButton.Visibility = Visibility.Hidden;
        //    }
        //    if (num > 0) //עדכון רחפן
        //    {
        //        BlApi.BO.Drone tempD = bl.getDrone(num);
        //        PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
        //        //DroneToList d = new DroneToList(tempD.ID, tempD.Model, (BO.Enum.WeightCategories)tempD.MaxWeigth, tempD.Battary, (BO.Enum.DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation, tempD.TheParcelInDelivery);

        //        this.DataContext = d;
        //        plusButton.Visibility = Visibility.Hidden;
        //        iDTextBox.IsEnabled = false;
        //        maxWeigthComboBox.IsEnabled = false;
        //        //iDTextBox.Text = tempDrone.ID.ToString();
        //      //  battaryTextBox.Text = tempDrone.Battary.ToString();
        //        maxWeigthComboBox.Text = d.MaxWeigth.ToString();
        //       // modelTextBox.Text = tempDrone.Model;
        //        statusOfDroneComboBox.Text = tempD.StatusOfDrone.ToString();
        //       // if (tempDrone.TheParcelInDelivery != null) parcelInDeliveryTextBox.Text = tempDrone.TheParcelInDelivery.ID.ToString();
        //      //  droneLocationTextBox.Text = tempDrone.DroneLocation.ToString();

        //    }
        //    if (num < 0)
        //    {
        //        BlApi.BO.Drone tempD = bl.getDrone(num2);
        //        PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);

        //        this.DataContext = d;
        //        plusButton.Visibility = Visibility.Hidden;
        //        updateButton.Visibility = Visibility.Hidden;
        //        ChargeButton.Visibility = Visibility.Hidden;
        //        ParcelButton.Visibility = Visibility.Hidden;
        //        maxWeigthComboBox.Text = d.MaxWeigth.ToString();
        //        statusOfDroneComboBox.Text = tempD.StatusOfDrone.ToString();
        //        iDTextBox.IsEnabled = false;
        //      //  iDTextBox.Text = tempDrone.ID.ToString();
        //     //   battaryTextBox.Text = tempDrone.Battary.ToString();
        //        battaryTextBox.IsEnabled = false;
        //      //  maxWeigthComboBox.Text = tempDrone.MaxWeigth.ToString();
        //     //   modelTextBox.Text = tempDrone.Model.ToString();
        //        modelTextBox.IsEnabled = false;
        //        maxWeigthComboBox.IsEnabled = false;
        //    //    statusOfDroneComboBox.Text = tempDrone.StatusOfDrone.ToString();
        //        statusOfDroneComboBox.IsEnabled = false;
        //      //  if (tempDrone.TheParcelInDelivery != null) { parcelInDeliveryTextBox.Text = tempDrone.TheParcelInDelivery.ID.ToString(); statusOfDroneComboBox.IsEnabled = false; }
        //      //  droneLocationTextBox.Text = tempDrone.DroneLocation.ToString();
        //        droneLocationTextBox.IsEnabled = false;
        //        automatic.Visibility = Visibility.Hidden;
        //        manualButton.Visibility = Visibility.Hidden;
        //    }
        //}
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
            if (tempDr == null && num2==0) //הוספת רחפן
            {
                updateButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Hidden;
                ParcelButton.Visibility = Visibility.Hidden;
                automatic.Visibility = Visibility.Hidden;
                manualButton.Visibility = Visibility.Hidden;
            }
            if (tempDr.ID > 0) //עדכון רחפן
            {
                //BlApi.BO.Drone tempD = bl.getDrone(num);
                //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                ////DroneToList d = new DroneToList(tempD.ID, tempD.Model, (BO.Enum.WeightCategories)tempD.MaxWeigth, tempD.Battary, (BO.Enum.DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation, tempD.TheParcelInDelivery);
                myWindowDrone = tempDr;
                this.DataContext = tempDr;
                plusButton.Visibility = Visibility.Hidden;
                iDTextBox.IsEnabled = false;
                maxWeigthComboBox.IsEnabled = false;
                //iDTextBox.Text = tempDrone.ID.ToString();
                //  battaryTextBox.Text = tempDrone.Battary.ToString();
                maxWeigthComboBox.Text = tempDr.MaxWeigth.ToString();
                // modelTextBox.Text = tempDrone.Model;
                statusOfDroneComboBox.Text = tempDr.StatusOfDrone.ToString();
                // if (tempDrone.TheParcelInDelivery != null) parcelInDeliveryTextBox.Text = tempDrone.TheParcelInDelivery.ID.ToString();
                //  droneLocationTextBox.Text = tempDrone.DroneLocation.ToString();

            }
            if (num2 !=0)
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
                //  iDTextBox.Text = tempDrone.ID.ToString();
                //   battaryTextBox.Text = tempDrone.Battary.ToString();
                battaryTextBox.IsEnabled = false;
                //  maxWeigthComboBox.Text = tempDrone.MaxWeigth.ToString();
                //   modelTextBox.Text = tempDrone.Model.ToString();
                modelTextBox.IsEnabled = false;
                maxWeigthComboBox.IsEnabled = false;
                //    statusOfDroneComboBox.Text = tempDrone.StatusOfDrone.ToString();
                statusOfDroneComboBox.IsEnabled = false;
                //  if (tempDrone.TheParcelInDelivery != null) { parcelInDeliveryTextBox.Text = tempDrone.TheParcelInDelivery.ID.ToString(); statusOfDroneComboBox.IsEnabled = false; }
                //  droneLocationTextBox.Text = tempDrone.DroneLocation.ToString();
                droneLocationTextBox.IsEnabled = false;
                automatic.Visibility = Visibility.Hidden;
                manualButton.Visibility = Visibility.Hidden;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void iDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

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
                //BO.Drone tempDrone = sender as BO.Drone;
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
                //PO.DroneToList tempDrone = (PO.DroneToList)statusOfDroneComboBox.DataContext;
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
                    //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
                    //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                    //DataContext = d;
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
                //updateDroneView();
                BlApi.BO.Drone tempDrone1 = bl.getDrone(int.Parse(iDTextBox.Text));
                //DroneToList myUpdateDrone = new DroneToList(tempDrone1.ID, tempDrone1.Model, (WeightCategories)tempDrone1.MaxWeigth, tempDrone1.Battary, (DroneStatuses)tempDrone1.StatusOfDrone, tempDrone1.DroneLocation.ToString(), tempDrone1.TheParcelInDelivery.ID);
                //myWindowDrone.StatusOfDrone = myUpdateDrone.StatusOfDrone;
                myWindowDrone.StatusOfDrone = (DroneStatuses)tempDrone1.StatusOfDrone;
                myWindowDrone.Battary = tempDrone1.Battary;
                MessageBox.Show("שחרור הרחפן מטעינה בוצע בהצלחה \n", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
                //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                //DataContext = d;
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
                    //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
                    //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                    //DataContext = d;

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
                    //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
                    //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                    //DataContext = d;

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
                    //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
                    //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
                    //DataContext = d;

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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.startDroneSimulator((int)e.Argument, updateDrone, checkStop);
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
            updateDroneView();
            Thread.Sleep(1000);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Auto = false;
            Worker = null;
            updateDroneView();
            MessageBox.Show("התהליך האוטומטי הושלם");
            //BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
            //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
            //DataContext = d;
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

        private void updateDroneView()
        {
            //    lock(bl)
            ////    {

            //Drone tempD = bl.getDrone((int)e.ProgressPercentage);
            BlApi.BO.Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
            myWindowDrone.Model = tempD.Model;
            myWindowDrone.StatusOfDrone = (DroneStatuses)tempD.StatusOfDrone;
            myWindowDrone.DroneLocation = tempD.DroneLocation.ToString();
            myWindowDrone.Battary = tempD.Battary;
            myWindowDrone.ParcelInDelivery = tempD.TheParcelInDelivery.ID;
            //PO.DroneToList d = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
            //DataContext = d;
            //Drone tempD = bl.getDrone(int.Parse(iDTextBox.Text));
            //DataContext = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);
            //Thread.Sleep(1000);    
            updateFlags(tempD);
                //this.setAndNotify(PropertyChanged, nameof(Drone), out tempD, tempD);
          //  }
        }
        //DataContext = new PO.DroneToList(tempD.ID, tempD.Model, (WeightCategories)tempD.MaxWeigth, tempD.Battary, (DroneStatuses)tempD.StatusOfDrone, tempD.DroneLocation.ToString(), tempD.TheParcelInDelivery.ID);


    }
}
