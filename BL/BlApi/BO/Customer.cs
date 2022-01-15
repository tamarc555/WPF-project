using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BlApi;

namespace BlApi.BO
{

    public class Customer
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

        private location customerLocation;

        public location CustomerLocation
        {
            get { return customerLocation; }
            set { customerLocation = value; }
        }

        private List<Parcel> parcelFromCustomer;

        public List<Parcel> ParcelFromCustomer
        {
            get { return parcelFromCustomer; }
            set { parcelFromCustomer = value; }
        }

        private List<Parcel> parcelToCustomer;

        public List<Parcel> ParcelToCustomer
        {
            get { return parcelToCustomer; }
            set { parcelToCustomer = value; }
        }

        public Customer(int _ID, string _name, string _phone, location _customerLocation, List<Parcel> _parcelFromCustomer, List<Parcel> _parcelToCustomer)  //ctor
        {
            id = _ID;
            name = _name;
            phone = _phone;
            customerLocation = _customerLocation;
            parcelFromCustomer = _parcelFromCustomer;
            parcelToCustomer = _parcelToCustomer;
        }
        public Customer()  //ctor
        {
            id = 0;
            name = " ";
            phone = " ";
            customerLocation = new location();
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
            return "ID: " + id + " name: " + name + " phone: " + phone + " location: " + customerLocation + "parcel From Customer: " + tempList1 + "parcel To Customer: " + tempList2;
        }
    }
}

