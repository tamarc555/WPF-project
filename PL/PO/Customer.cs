using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using PO;

namespace PL
{
    namespace PO
    {

        public class Customer : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private int id;

            public int ID
            {
                get { return id; }
                set
                {
                    id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }

            private string name;

            public string Name
            {
                get { return name; }
                set 
                { 
                    name = value; 
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }

            private string phone;

            public string Phone
            {
                get { return phone; }
                set 
                { 
                    phone = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
                }
            }

            private double longitude;

            public double Longitude
            {
                get { return longitude; }
                set 
                { 
                    longitude = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
                }
            }

            private double latitude;

            public double Latitude
            {
                get { return latitude; }
                set 
                { 
                    latitude = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
                }
            }

            private List<Parcel> parcelFromCustomer;

            public List<Parcel> ParcelFromCustomer
            {
                get { return parcelFromCustomer; }
                set
                {
                    parcelFromCustomer = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ParcelFromCustomer"));
                }
            }

            private List<Parcel> parcelToCustomer;

            public List<Parcel> ParcelToCustomer
            {
                get { return parcelToCustomer; }
                set 
                {
                    parcelToCustomer = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ParcelToCustomer"));
                }
            }

            public Customer(int _ID, string _name, string _phone, double _longitude, double _latitude, List<Parcel> _parcelFromCustomer, List<Parcel> _parcelToCustomer)  //ctor
            {
                id = _ID;
                name = _name;
                phone = _phone;
                longitude = _longitude;
                latitude = _latitude;
                parcelFromCustomer = _parcelFromCustomer;
                parcelToCustomer = _parcelToCustomer;
            }
            public Customer()  //ctor
            {
                id = 0;
                name = " ";
                phone = " ";
                longitude = 0;
                latitude = 0;
                parcelFromCustomer = new List<Parcel>();
                parcelToCustomer = new List<Parcel>();
            }

            public override string ToString()
            {
                string tempList1 = "\n";
                for (int i = 0; i < parcelFromCustomer.Count; i++)
                    tempList1 += parcelFromCustomer[i].ToString() + "\n";
                string tempList2 = "\n";
                for (int i = 0; i < parcelToCustomer.Count; i++)
                    tempList2 += parcelToCustomer[i].ToString() + "\n";
                return "ID: " + id + " name: " + name + " phone: " + phone + " location: longitude: " + longitude + " latitude: " + latitude + " parcel From Customer: " + tempList1 + "parcel To Customer: " + tempList2;
            }
        }
    }
}

