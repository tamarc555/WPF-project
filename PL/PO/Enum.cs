using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Enum
    {
        public enum WeightCategories { free, light, medium, heavy }
        public enum Priorities { normal = 1, fast, emergency } //רגיל, מהיר, חירום
        public enum DroneStatuses { available = 1, maintenance, scheduled, delivery } //פנוי, תחזוקה, מתוזמן, משלוח
        public enum ParcelStatuses { requested, scheduled, pickedUp, delivered } //הוגדרה, שויכה, נאספה, סופקה


    }
}

