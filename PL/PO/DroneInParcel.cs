using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DroneInParcel : INotifyPropertyChanged
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

        private double battary;

        public double Battary
        {
            get { return battary; }
            set { battary = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Battary"));
            }
        }

        private location droneInParcelLocation;

        public location DroneInParcelLocation
        {
            get { return droneInParcelLocation; }
            set { droneInParcelLocation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DroneInParcelLocation"));
            }
        }

        public DroneInParcel(int _Id, double _battary, location _droneInParcelLocation)
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
