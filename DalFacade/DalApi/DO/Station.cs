using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    namespace DO
    {
        public struct Station
        {
            public int ID { set; get; }
            public int Name { set; get; }
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public int ChargeSlots { set; get; }
            public Station(int _Id, int _name, double _longitude, double _latitude, int _chargeSlots)
            {

                ID = _Id;
                Name = _name;
                Longitude = _longitude;
                Latitude = _latitude;
                ChargeSlots = _chargeSlots;
            }

            public override string ToString()
            {
                return "ID: " + ID + " name: " + Name + " longitude: " + Longitude + " latitude: " + Latitude + " chargeSlots: " + ChargeSlots;
            }
        }
    }
}
