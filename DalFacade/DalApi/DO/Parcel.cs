using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    namespace DO
    {
        public struct Parcel
        {
            public int ID { set; get; }
            public int SenderID { set; get; }
            public int TargetID { set; get; }
            public WeightCategories Weight { set; get; }
            public Priorities Priority { set; get; }
            public DateTime? Requested { set; get; }
            public DateTime? Scheduled { set; get; }
            public DateTime? PickedUp { set; get; }
            public DateTime? Delivered { set; get; }
            public int DroneId { set; get; }
            public Parcel(int _ID, int _senderId, int _targetId, WeightCategories _weight, Priorities _priority, DateTime? _requested, DateTime? _scheduled, DateTime? _pickedUp, DateTime? _delivered, int _droneId)  //ctor
            {
                ID = _ID;
                SenderID = _senderId;
                TargetID = _targetId;
                Weight = _weight;
                Priority = _priority;
                Requested = _requested;
                Scheduled = _scheduled;
                PickedUp = _pickedUp;
                Delivered = _delivered;
                DroneId = _droneId;
            }
            public override string ToString()
            {
                return "ID: " + ID + " senderId: " + SenderID + " targetId: " + TargetID + " weight: " + Weight + " priority: " + Priority + " requested: " + Requested + " scheduled: " + Scheduled + " pickedUp: " + PickedUp + " delivered: " + Delivered + " droneId: " + DroneId;
            }
        }
    }
}
