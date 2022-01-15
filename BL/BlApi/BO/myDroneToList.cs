using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;

namespace BlApi.BO
{
    public class myDroneToList
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        private WeightCategories maxWeigth;

        public WeightCategories MaxWeigth
        {
            get { return maxWeigth; }
            set { maxWeigth = value; }
        }

        private double battary;

        public double Battary
        {
            get { return battary; }
            set { battary = value; }
        }


        private DroneStatuses statusOfDrone;

        public DroneStatuses StatusOfDrone
        {
            get { return statusOfDrone; }
            set { statusOfDrone = value; }
        }

        private string droneLocation;

        public string DroneLocation
        {
            get { return droneLocation; }
            set { droneLocation = value; }
        }

        private int parcelInDelivery;

        public int ParcelInDelivery
        {
            get { return parcelInDelivery; }
            set { parcelInDelivery = value; }
        }


        public myDroneToList(int _ID, string _model, WeightCategories _maxWeigth, double _battary, DroneStatuses _statusOfDrone, string _droneLocation, int _parcelInDelivery)  //ctor
        {
            id = _ID;
            model = _model;
            maxWeigth = _maxWeigth;
            battary = _battary;
            statusOfDrone = _statusOfDrone;
            droneLocation = _droneLocation;
            parcelInDelivery = _parcelInDelivery;
        }

        public myDroneToList()  // default ctor
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
            return "ID: " + id + " model: " + model + " maximun weight: " + maxWeigth + " battary: " + battary + " status Of Drone: " + statusOfDrone + " the drone location is: " + droneLocation + " parcel In Delivery: " + parcelInDelivery;
        }
    }
}

