using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;


namespace BlApi.BO
{
    public class ParcelToList
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string sender;

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private string target;

        public string Target
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

        private ParcelStatuses theParcelStatus;

        public ParcelStatuses TheParcelStatus
        {
            get { return theParcelStatus; }
            set { theParcelStatus = value; }
        }

        public ParcelToList(int _ID, string _senderId, string _targetId, WeightCategories _weight, Priorities _priority, ParcelStatuses _theParcelStatus)  //ctor
        {
            id = _ID;
            sender = _senderId;
            target = _targetId;
            weight = _weight;
            priority = _priority;
            theParcelStatus = _theParcelStatus;
        }

        public override string ToString()
        {
            return "ID: " + id + " senderId: " + sender + " targetId: " + target + " weight: " + weight + " priority: " + priority + " Parcel Status: " + theParcelStatus;
        }
    }
}
