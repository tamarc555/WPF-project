using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum;

namespace PL
{
    namespace PO
    {
        public class Drone
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

            private ParcelInDelivery theParcelInDelivery;

            public ParcelInDelivery TheParcelInDelivery
            {
                get { return theParcelInDelivery; }
                set { theParcelInDelivery = value; }
            }


            private location droneLocation;

            public location DroneLocation
            {
                get { return droneLocation; }
                set { droneLocation = value; }
            }

            public Drone(int _ID, string _model, WeightCategories _maxWeigth, double _battary, DroneStatuses _statusOfDrone, ParcelInDelivery _theParcelInDelivery, location _droneLocation)  //ctor
            {
                id = _ID;
                model = _model;
                maxWeigth = _maxWeigth;
                battary = _battary;
                statusOfDrone = _statusOfDrone;
                theParcelInDelivery = _theParcelInDelivery;
                droneLocation = _droneLocation;
            }

            public Drone()  //default ctor
            {
                id = 0;
                model = " ";
                maxWeigth = 0;
                battary = 0;
                statusOfDrone = 0;
                theParcelInDelivery = new ParcelInDelivery();
                droneLocation = new location();
            }

            public override string ToString()
            {
                if (theParcelInDelivery == null || theParcelInDelivery.ID == 0) return "ID: " + id + " model: " + model + " maximun weight: " + maxWeigth + "% battary: " + (int)battary + " status Of drone: " + statusOfDrone + " the drone location is: " + droneLocation;
                return "ID: " + id + " model: " + model + " maximun weight: " + maxWeigth + " battary: " + (int)battary + "% status Of drone: " + statusOfDrone + " The Parcel In Delivery " + theParcelInDelivery + " the drone location is: " + droneLocation;
            }
        }
    }
}
