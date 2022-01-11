using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class CustomerToList
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private int numOfParcelProvided;

        public int NumOfParcelProvided
        {
            get { return numOfParcelProvided; }
            set { numOfParcelProvided = value; }
        }

        private int numOfParcelNotProvided;

        public int NumOfParcelNotProvided
        {
            get { return numOfParcelNotProvided; }
            set { numOfParcelNotProvided = value; }
        }

        private int numOfParcelReceived;

        public int NumOfParcelReceived
        {
            get { return numOfParcelReceived; }
            set { numOfParcelReceived = value; }
        }

        private int numOfParcelInDelivery;

        public int NumOfParcelInDelivery
        {
            get { return numOfParcelInDelivery; }
            set { numOfParcelInDelivery = value; }
        }

        public CustomerToList(int _ID, string _name, string _phone, int _numOfParcelProvided, int _numOfParcelNotProvided, int _numOfParcelReceived, int _numOfParcelInDelivery)  //ctor
        {
            id = _ID;
            name = _name;
            phone = _phone;
            numOfParcelProvided = _numOfParcelProvided;
            numOfParcelNotProvided = _numOfParcelNotProvided;
            numOfParcelReceived = _numOfParcelReceived;
            numOfParcelInDelivery = _numOfParcelInDelivery;
        }
        public override string ToString()
        {
            return "ID: " + id + " name: " + name + " phone: " + phone + " number of provided parcels: " + numOfParcelProvided + " number Of Parcel thet Not Provided: " + numOfParcelNotProvided + " number Of Parcel that Received: " + numOfParcelReceived + " number Of Parcel that are In Delivery: " + numOfParcelInDelivery;
        }
    }
}
