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
        public struct Drone
        {
            public int ID { set; get; }

            public string Model { set; get; }
            public WeightCategories MaxWeigth { set; get; }
            public Drone(int _ID, string _model, WeightCategories _maxWeigth)  //ctor
            {
                ID = _ID;
                Model = _model;
                MaxWeigth = _maxWeigth;
            }

            public override string ToString()
            {
                return "ID: " + ID + " model: " + Model + " maximun weight: " + MaxWeigth; ;
            }
        }
    }
}
