using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class StationToList
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int name;

        public int Name
        {
            get { return name; }
            set { name = value; }
        }

        
        private int numOfAvalibleChargeSlot;

        public int NumOfAvalibleChargeSlot
        {
            get { return numOfAvalibleChargeSlot; }
            set { numOfAvalibleChargeSlot = value; }
        }

        private int numOfNotAvalibleChargeSlot;

        public int NumOfNotAvalibleChargeSlot
        {
            get { return numOfNotAvalibleChargeSlot; }
            set { numOfNotAvalibleChargeSlot = value; }
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

