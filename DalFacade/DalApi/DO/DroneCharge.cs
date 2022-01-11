using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int ID { set; get; }
            public int DroneId { set; get; }
            public int StationId { set; get; }
            public bool Active { set; get; }
            public DateTime? EntranceTime { set; get; }
            public DateTime? LeavingTime { set; get; }
            public override string ToString()
            {
                return "ID: " + ID + " drone's Id: " + DroneId + " station's Id: " + StationId + " active: " + Active + " entrance time: " + EntranceTime + " leaving time: " + LeavingTime;
            }
        }
    }
}
