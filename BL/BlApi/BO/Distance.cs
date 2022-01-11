using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    internal static class Distance
    {
        public static double calculateDistance(this location from, location to)
        {
            int R = 6371 * 1000;
            double p1 = from.latitude * Math.PI / 180;
            double p2 = to.latitude * Math.PI / 180;
            double deltaP = (to.latitude - from.latitude) * Math.PI / 180;
            double deltaLanmbda = (to.longitude - from.longitude) * Math.PI / 180;

            double a = Math.Sin(deltaP / 2) * Math.Sin(deltaP / 2) + Math.Cos(p1) * Math.Cos(p2) * Math.Sin(deltaLanmbda / 2) * Math.Sin(deltaLanmbda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000;
            if (d < 0) d = -d;
            return d/100;
        }
    }
}
