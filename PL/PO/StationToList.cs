using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class StationToList : INotifyPropertyChanged
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

        private int name;

        public int Name
        {
            get { return name; }
            set { name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        
        private int numOfAvalibleChargeSlot;

        public int NumOfAvalibleChargeSlot
        {
            get { return numOfAvalibleChargeSlot; }
            set { numOfAvalibleChargeSlot = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfAvalibleChargeSlot"));
            }
        }

        private int numOfNotAvalibleChargeSlot;

        public int NumOfNotAvalibleChargeSlot
        {
            get { return numOfNotAvalibleChargeSlot; }
            set { numOfNotAvalibleChargeSlot = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOfNotAvalibleChargeSlot"));
            }
        }

        public StationToList(int _Id, int _name, int _numOfAvalibleChargeSlot, int _numOfNotAvalibleChargeSlot)
        {
            id = _Id;
            name = _name;
            numOfAvalibleChargeSlot = _numOfAvalibleChargeSlot;
            numOfNotAvalibleChargeSlot = _numOfNotAvalibleChargeSlot;
        }


        public override string ToString()
        {
            return "ID: " + id + " name: " + name + "number Of Avalible Charge Slots: " + numOfAvalibleChargeSlot + "number Of Not Avalible Charge Slots: " + numOfNotAvalibleChargeSlot;
        }
    }
}

