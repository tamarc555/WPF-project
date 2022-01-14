using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum;


namespace PO
{
    public class ParcelToList : INotifyPropertyChanged
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

        private string sender;

        public string Sender
        {
            get { return sender; }
            set { sender = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
            }
        }

        private string target;

        public string Target
        {
            get { return target; }
            set { target = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Target"));
            }
        }

        private WeightCategories weight;

        public WeightCategories Weight
        {
            get { return weight; }
            set { weight = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
            }
        }

        private Priorities priority;

        public Priorities Priority
        {
            get { return priority; }
            set { priority = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
            }
        }

        private ParcelStatuses theParcelStatus;

        public ParcelStatuses TheParcelStatus
        {
            get { return theParcelStatus; }
            set { theParcelStatus = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TheParcelStatus"));
            }
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
