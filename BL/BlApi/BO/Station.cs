using BL;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    namespace BO
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

            private location stationLocation;

            public location StationLocation
            {
                get { return stationLocation; }
                set { stationLocation = value; }
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



            public Station(int _Id, int _name, location _stationLocation, int _chargeSlots, List<Drone> _listDroneInCharge)
            {
                id = _Id;
                name = _name;
                stationLocation = _stationLocation;
                chargeSlots = _chargeSlots;
                listDroneInCharge = _listDroneInCharge;
            }

            public Station()
            {

                id = 0;
                name = 0;
                stationLocation = new location();
                chargeSlots = 0;
                listDroneInCharge = new List<Drone>();
            }

            public override string ToString()
            {
                string tempList = null;
                for (int i = 0; i < listDroneInCharge.Count; i++)
                    tempList += listDroneInCharge[i].ToString()  + "\n";
                return "ID: " + id + " name: " + name + " station location: " + stationLocation + " chargeSlots: " + chargeSlots + "\nlist Drone in Charge: \n" + tempList;
            }
        }
    }
}

