using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;

namespace BlApi.BO
{
    public class ParcelInCustomer
    {

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private WeightCategories weight;

        public WeightCategories Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private Priorities priority;

        public Priorities Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private ParcelStatuses theParcelStatus;

        public ParcelStatuses TheParcelStatus
        {
            get { return theParcelStatus; }
            set { theParcelStatus = value; }
        }

        private CustomerInParcel theCustomer;

        public CustomerInParcel TheCustomer
        {
            get { return theCustomer; }
            set { theCustomer = value; }
        }


        public ParcelInCustomer(int _ID, WeightCategories _weight, Priorities _priority, ParcelStatuses _theParcelStatus, CustomerInParcel _theCustomer)  //ctor
        {
            id = _ID;
            weight = _weight;
            priority = _priority;
            theParcelStatus = _theParcelStatus;
            theCustomer = _theCustomer;
        }

        public override string ToString()
        {
            return "ID: " + id  + " weight: " + weight + " priority: " + priority + " Parcel Status: " + theParcelStatus + " the Customer: " + theCustomer;
        }
    }
}
