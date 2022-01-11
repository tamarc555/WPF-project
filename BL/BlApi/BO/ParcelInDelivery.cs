using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;

namespace BlApi.BO
{
    public class ParcelInDelivery
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private bool statusOfParcel;

        public bool StatusOfParcel
        {
            get { return statusOfParcel; }
            set { statusOfParcel = value; }
        }

        private Priorities priority;

        public Priorities Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private WeightCategories weight;

        public WeightCategories Weight
        {
            get { return weight; }
            set { weight = value; }
        }


        private CustomerInParcel sender;

        public CustomerInParcel Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private CustomerInParcel target;

        public CustomerInParcel Target
        {
            get { return target; }
            set { target = value; }
        }

        private location pickUpOfParcel;

        public location PickUpOfParcel
        {
            get { return pickUpOfParcel; }
            set { pickUpOfParcel = value; }
        }

        private location providedOfParcel;

        public location ProvidedOfParcel
        {
            get { return providedOfParcel; }
            set { providedOfParcel = value; }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public ParcelInDelivery(int _ID, bool _status, Priorities _priotity, WeightCategories _weight, CustomerInParcel _sender, CustomerInParcel _target, location _pickUpPlace, location _providedPlace, double _distance)
        {
            id = _ID;
            statusOfParcel = _status;
            priority = _priotity;
            weight = _weight;
            sender = _sender;
            target = _target;
            pickUpOfParcel = _pickUpPlace;
            providedOfParcel = _providedPlace;
            distance = _distance;
        }

        public ParcelInDelivery()
        {
            id = 0;
            statusOfParcel = false;
            priority = 0;
            weight = 0;
            sender = new CustomerInParcel(0, " ");
            target = new CustomerInParcel(0, " ");
            pickUpOfParcel = new location();
            providedOfParcel = new location();
            distance = 0;
        }

        public override string ToString()
        {
            return "ID: " + id + " status Of Parcel: " + statusOfParcel + " priority: " + priority + " weight: " + weight + " sender: " + sender + " target: " + target  + " location of pick up: " + pickUpOfParcel + " location of provided: " + providedOfParcel + "distance: " + distance;
        }
    }
}
