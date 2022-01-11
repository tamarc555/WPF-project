using System;

namespace DalApi
{
    namespace DO
    {
        public struct Customer
        {
            public int ID { set; get; }

            public string Name { set; get; }

            public string Phone { set; get; }

            public double Longitude { set; get; }

            public double Latitude { set; get; }
            public Customer(int _ID, string _name, string _phone, double _longitude, double _latitude)  //ctor
            {
                ID = _ID;
                Name = _name;
                Phone = _phone;
                Longitude = _longitude;
                Latitude = _latitude;
            }
            public override string ToString()
            {
                return "ID: " + ID + " name: " + Name + " phone: " + Phone + " longitude: " + Longitude + " latitude: " + Latitude;
            }
        }
    }
}
