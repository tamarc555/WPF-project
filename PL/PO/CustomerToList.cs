using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class CustomerToList : INotifyPropertyChanged
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

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        private int numOfParcelProvided;

        public int NumOfParcelProvided
        {
            get { return numOfParcelProvided; }
            set { numOfParcelProvided = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfParcelProvided"));
            }
        }

        private int numOfParcelNotProvided;

        public int NumOfParcelNotProvided
        {
            get { return numOfParcelNotProvided; }
            set { numOfParcelNotProvided = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfParcelNotProvided"));
            }
        }

        private int numOfParcelReceived;

        public int NumOfParcelReceived
        {
            get { return numOfParcelReceived; }
            set { numOfParcelReceived = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfParcelReceived"));
            }
        }

        private int numOfParcelInDelivery;

        public int NumOfParcelInDelivery
        {
            get { return numOfParcelInDelivery; }
            set { numOfParcelInDelivery = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfParcelInDelivery"));
            }
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
