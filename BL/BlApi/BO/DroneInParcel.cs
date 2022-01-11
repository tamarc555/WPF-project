using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class DroneInParcel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private double battary;

        public double Battary
        {
            get { return battary; }
            set { battary = value; }
        }

        private location droneInParcelLocation;

        public location DroneInParcelLocation
        {
            get { return droneInParcelLocation; }
            set { droneInParcelLocation = value; }
        }

        public DroneInParcel(int _Id , double _battary , location _droneInParcelLocation)
        {
            id = _Id;
            battary = _battary;
            droneInParcelLocation = _droneInParcelLocation;
        }
        public DroneInParcel()
        {
            id = 0;
            battary = 0;
            droneInParcelLocation = new location();
        }

        public override string ToString()
        {
            return "ID: " + id + " battary: " + battary + " location: " + droneInParcelLocation;
        }

    }
}
