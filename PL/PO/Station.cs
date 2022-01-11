
using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PL
{
    namespace PO
    {
        public class Station
        {
            private int id;

            public int ID
            {
                get { return id; }
                set { id = value; }
            }

            private int name;

            public int Name
            {
                get { return name; }
                set { name = value; }
            }

            private double longitude;

            public double Longitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            private double latitude;

            public double Latitude
            {
                get { return latitude; }
                set { latitude = value; }
            }


            private int chargeSlots;

            public int ChargeSlots
            {
                get { return chargeSlots; }
                set { chargeSlots = value; }
            }

            private List<Drone> listDroneInCharge;

            public List<Drone> ListDroneInCharge
            {
                get { return listDroneInCharge; }
                set { listDroneInCharge = value; }
            }



            public Station(int _Id, int _name, double _longitude, double _latitude, int _chargeSlots, List<Drone> _listDroneInCharge)
            {
                id = _Id;
                name = _name;
                longitude = _longitude;
                latitude = _latitude;
                chargeSlots = _chargeSlots;
                listDroneInCharge = _listDroneInCharge;
            }

            public Station()
            {

                id = 0;
                name = 0;
                longitude = 0;
                latitude = 0;
                chargeSlots = 0;
                listDroneInCharge = new List<Drone>();
            }

            public override string ToString()
            {
                string tempList = null;
                for (int i = 0; i < listDroneInCharge.Count; i++)
                    tempList += listDroneInCharge[i].ToString() + "\n";
                return "ID: " + id + " name: " + name + " station location: longitude: " + longitude + " latitude: " + latitude + " chargeSlots: " + chargeSlots + "\nlist Drone in Charge: \n" + tempList;
            }
        }
    }
}


