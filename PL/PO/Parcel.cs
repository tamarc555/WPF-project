using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum;

namespace PL
{
    namespace PO
    {
        public class Parcel : INotifyPropertyChanged
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

            private int sender;

            public int Sender
            {
                get { return sender; }
                set { sender = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
                }
            }

            private int target;

            public int Target
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

            private int theDroneInParcel;

            public int TheDroneInParcel
            {
                get { return theDroneInParcel; }
                set { theDroneInParcel = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("TheDroneInParcel"));
                }
            }

            private DateTime? requested;

            public DateTime? Requested
            {
                get { return requested; }
                set { requested = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Requested"));
                }
            }

            private DateTime? scheduled;

            public DateTime? Scheduled
            {
                get { return scheduled; }
                set { scheduled = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Scheduled"));
                }
            }

            private DateTime? pickedUp;

            public DateTime? PickedUp
            {
                get { return pickedUp; }
                set { pickedUp = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PickedUp"));
                }
            }

            private DateTime? delivered;

            public DateTime? Delivered
            {
                get { return delivered; }
                set { delivered = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Delivered"));
                }
            }


            public Parcel(int _ID, int _senderId, int _targetId, WeightCategories _weight, Priorities _priority, int _theDroneInParcel, DateTime? _requested, DateTime? _scheduled, DateTime? _pickedUp, DateTime? _delivered)  //ctor
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
                sender = 0;
                target = 0;
                weight = 0;
                priority = 0;
                theDroneInParcel = 0;
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


