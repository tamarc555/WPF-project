using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class DroneInCharge
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

        public DroneInCharge(int _Id, double _battary)
        {
            id = _Id;
            battary = _battary;
        }

        public override string ToString()
        {
            return "ID: " + id + " battary: " + battary;
        }

    }
}
