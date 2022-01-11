using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class CustomerInParcel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public CustomerInParcel(int _ID =0, string _name = " ")  //ctor
        {
            id = _ID;
            name = _name;
        }
        public override string ToString()
        {
            return "ID: " + id + " name: " + name;
        }



    }
}
