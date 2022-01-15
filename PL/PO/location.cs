using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class location
    {
        public double longitude;
        public double latitude;

        public location(double _longitude = 0, double _latitude = 0)
        {
            longitude = _longitude;
            latitude = _latitude;
        }

        public override string ToString()
        {
            double _latitude = latitude;
            double _longitude = longitude;
            string lat()
            {
                string ch = "N";
                if (_latitude < 0)
                {
                    ch = "S";
                    _latitude = -_latitude;
                }
                int deg = (int)_latitude;
                int min = (int)(60 * (_latitude - deg));
                double sec = (_latitude - deg) * 3600 - min * 60;

                return $"{deg}° {min}' {sec}'' {ch}";
            }

            string log()
            {
                string ch = "E";
                if (_longitude < 0)
                {
                    ch = "W";
                    _longitude = -_longitude;
                }
                int deg = (int)_longitude;
                int min = (int)(60 * (_longitude - deg));
                double sec = (_longitude - deg) * 3600 - min * 60;

                return $"{deg}° {min}' {sec}'' {ch}";
            }

            return "("+log() + "," + lat()+")";
        }
    }
}
