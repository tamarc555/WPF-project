using BlApi.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;

namespace BlApi
{
    namespace BO
    {
        public class Parcel
        {

            private int id;

            public int ID
            {
                get { return id; }
                set { id = value; }
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

            private DroneInParcel theDroneInParcel;

            public DroneInParcel TheDroneInParcel
            {
                get { return theDroneInParcel; }
                set { theDroneInParcel = value; }
            }

            private DateTime? requested;

            public DateTime? Requested
            {
                get { return requested; }
                set { requested = value; }
            }

            private DateTime? scheduled;

            public DateTime? Scheduled
            {
                get { return scheduled; }
                set { scheduled = value; }
            }

            private DateTime? pickedUp;

            public DateTime? PickedUp
            {
                get { return pickedUp; }
                set { pickedUp = value; }
            }

            private DateTime? delivered;

            public DateTime? Delivered
            {
                get { return delivered; }
                set { delivered = value; }
            }


            public Parcel(int _ID, CustomerInParcel _senderId, CustomerInParcel _targetId, WeightCategories _weight, Priorities _priority, DroneInParcel _theDroneInParcel, DateTime? _requested, DateTime? _scheduled, DateTime? _pickedUp, DateTime? _delivered)  //ctor
            {
                id = _ID;
                sender = _senderId;
                target = _targetId;
                weight = _weight;
                priority = _priority;
                theDroneInParcel = _theDroneInParcel;
                requested = _requested;
                scheduled = _scheduled;
                pickedUp = _pickedUp;
                delivered = _delivered;
            }

            public Parcel()  //ctor
            {
                id = 0;
                sender = new CustomerInParcel();
                target = new CustomerInParcel();
                weight = 0;
                priority = 0;
                theDroneInParcel = new DroneInParcel();
                requested = new DateTime?();
                scheduled = new DateTime?();
                pickedUp = new DateTime?();
                delivered = new DateTime?();
            }

            public override string ToString()
            {
                if (scheduled == null) return "ID: " + id + " sender: " + sender + " target: " + target + " weight: " + weight + " priority: " + priority + " requested: " + requested;
                if (pickedUp == null) return "ID: " + id + " sender: " + sender + " target: " + target + " weight: " + weight + " priority: " + priority + " the Drone In Parcel: " + theDroneInParcel + " requested: " + requested + " scheduled: " + scheduled;
                if (delivered == null) return "ID: " + id + " sender: " + sender + " target: " + target + " weight: " + weight + " priority: " + priority + " the Drone In Parcel: " + theDroneInParcel + " requested: " + requested + " scheduled: " + scheduled + " pickedUp: " + pickedUp;
                return "ID: " + id + " sender: " + sender + " target: " + target + " weight: " + weight + " priority: " + priority + " requested: " + requested + " scheduled: " + scheduled + " pickedUp: " + pickedUp + " delivered: " + delivered;
            }
        }
    }
}
    

