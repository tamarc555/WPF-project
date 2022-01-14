using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum;

namespace PL.PO
{
    public class DroneToList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }

        private WeightCategories maxWeigth;

        public WeightCategories MaxWeigth
        {
            get { return maxWeigth; }
            set { maxWeigth = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MaxWeigth"));
            }
        }

        private double battary;

        public double Battary
        {
            get { return battary; }
            set { battary = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Battary"));
            }
        }


        private DroneStatuses statusOfDrone;

        public DroneStatuses StatusOfDrone
        {
            get { return statusOfDrone; }
            set { statusOfDrone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StatusOfDrone"));
            }
        }

        private string droneLocation;

        public string DroneLocation
        {
            get { return droneLocation; }
            set { droneLocation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DroneLocation"));
            }
        }

        private int parcelInDelivery;

        public int ParcelInDelivery
        {
            get { return parcelInDelivery; }
            set { parcelInDelivery = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelInDelivery"));
            }
        }


        public DroneToList(int _ID, string _model, WeightCategories _maxWeigth, double _battary, DroneStatuses _statusOfDrone, string _droneLocation, int _parcelInDelivery)  //ctor
        {
            id = _ID;
            model = _model;
            maxWeigth = _maxWeigth;
            battary = _battary;
            statusOfDrone = _statusOfDrone;
            droneLocation = _droneLocation;
            parcelInDelivery = _parcelInDelivery;
        }

        public DroneToList()  // default ctor
        {
            id = 0;
            model = " ";
            maxWeigth = 0;
            battary = 0;
            statusOfDrone = 0;
            droneLocation = ""; // = new location();
            parcelInDelivery = 0; // new ParcelInDelivery();
        }

        public override string ToString()
        {
            return "ID: " + id + " model: " + model + " maximun weight: " + maxWeigth + " battary: " + (int)battary + "% status Of Drone: " + statusOfDrone + " the drone location is: " + droneLocation + " parcel In Delivery: " + parcelInDelivery;
        }
    }
}
