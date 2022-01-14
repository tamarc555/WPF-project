using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum;

namespace PO
{
    public class ParcelInDelivery : INotifyPropertyChanged
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

        private bool statusOfParcel;

        public bool StatusOfParcel
        {
            get { return statusOfParcel; }
            set { statusOfParcel = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StatusOfParcel"));
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

        private WeightCategories weight;

        public WeightCategories Weight
        {
            get { return weight; }
            set { weight = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
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

        private string pickUpOfParcel;

        public string PickUpOfParcel
        {
            get { return pickUpOfParcel; }
            set { pickUpOfParcel = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PickUpOfParcel"));
            }
        }

        private string providedOfParcel;

        public string ProvidedOfParcel
        {
            get { return providedOfParcel; }
            set { providedOfParcel = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ProvidedOfParcel"));
            }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { distance = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Distance"));
            }
        }

        public ParcelInDelivery(int _ID, bool _status, Priorities _priotity, WeightCategories _weight, int _sender, int _target, string _pickUpPlace, string _providedPlace, double _distance)
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
            sender = 0;
            target = 0;
            pickUpOfParcel = "";
            providedOfParcel = "";
            distance = 0;
        }

        public override string ToString()
        {
            return "ID: " + id + " status Of Parcel: " + statusOfParcel + " priority: " + priority + " weight: " + weight + " sender: " + sender + " target: " + target  + " location of pick up: " + pickUpOfParcel + " location of provided: " + providedOfParcel + "distance: " + distance;
        }
    }
}
