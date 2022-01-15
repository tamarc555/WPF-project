using System;
using System.Collections.Generic;

namespace DS
{
    public class DataSource
    {
        public static List<DalApi.DO.Drone> droneArray = new List<DalApi.DO.Drone>();
        public static List<DalApi.DO.Station> stationArray = new List<DalApi.DO.Station>();
        public static List<DalApi.DO.Customer> customerArray = new List<DalApi.DO.Customer>();
        public static List<DalApi.DO.Parcel> parcelArray = new List<DalApi.DO.Parcel>();
        public static List<DalApi.DO.DroneCharge> droneChargeArray = new List<DalApi.DO.DroneCharge>();
        public class Config
        {
            public static int numRunningOfParcel = 1;  //מזהה רץ עבור חבילות במשלוח
            public static int numRunningOfChargeStation = 1;  //מזהה רץ עבור טעינות

            // צריכת חשמל לק"מ:
            public static double usingBattaryAvailable = 0.2;
            public static double usingBattaryLight = 0.4;
            public static double usingBattaryMedium = 0.6;
            public static double usingBattaryHeavy = 0.8;

            public static double chargingRatePerHour = 50;  //קצב טעינת רחפן בשעה
        }

        static DataSource()
        {
            Initialize();
        }


        static public void Initialize()
        {
            Random r = new Random();

            //2 stations:
            DalApi.DO.Station tempStation = new();
            tempStation.ID = 1; tempStation.Name = 1; tempStation.Longitude = 31.754307; tempStation.Latitude = 35.199146; tempStation.ChargeSlots = 10;
            stationArray.Add(tempStation);
            tempStation.ID = 2; tempStation.Name = 2; tempStation.Longitude = 29.561560; tempStation.Latitude = 34.941743; tempStation.ChargeSlots = 15;
            stationArray.Add(tempStation);

            //5 drones:
            DalApi.DO.Drone tempDrone = new();
            tempDrone.ID = 1; tempDrone.Model = "A"; tempDrone.MaxWeigth = DO.WeightCategories.heavy;
            droneArray.Add(tempDrone);
            tempDrone.ID = 2; tempDrone.Model = "B"; tempDrone.MaxWeigth = DO.WeightCategories.light;
            droneArray.Add(tempDrone);
            tempDrone.ID = 3; tempDrone.Model = "C"; tempDrone.MaxWeigth = DO.WeightCategories.medium;
            droneArray.Add(tempDrone);
            tempDrone.ID = 4; tempDrone.Model = "D"; tempDrone.MaxWeigth = DO.WeightCategories.heavy;
            droneArray.Add(tempDrone);
            tempDrone.ID = 5; tempDrone.Model = "E"; tempDrone.MaxWeigth = DO.WeightCategories.medium;
            droneArray.Add(tempDrone);

            //10 customers:
            DalApi.DO.Customer tempCustomer = new();
            tempCustomer.ID = 1; tempCustomer.Name = "Aa"; tempCustomer.Phone = "11"; tempCustomer.Longitude = 30.612731; tempCustomer.Latitude = 34.800758;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 2; tempCustomer.Name = "Bb"; tempCustomer.Phone = "22"; tempCustomer.Longitude = 29.896219; tempCustomer.Latitude = 35.061457;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 3; tempCustomer.Name = "Cc"; tempCustomer.Phone = "33"; tempCustomer.Longitude = 31.825819; tempCustomer.Latitude = 34.085257;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 4; tempCustomer.Name = "Dd"; tempCustomer.Phone = "44"; tempCustomer.Longitude = 30.982154; tempCustomer.Latitude = 35.147598;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 5; tempCustomer.Name = "Ee"; tempCustomer.Phone = "55"; tempCustomer.Longitude = 31.000445; tempCustomer.Latitude = 34.547128;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 6; tempCustomer.Name = "Ff"; tempCustomer.Phone = "66"; tempCustomer.Longitude = 29.785624; tempCustomer.Latitude = 35.359857;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 7; tempCustomer.Name = "Gg"; tempCustomer.Phone = "77"; tempCustomer.Longitude = 30.325698; tempCustomer.Latitude = 35.235874;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 8; tempCustomer.Name = "Hh"; tempCustomer.Phone = "88"; tempCustomer.Longitude = 29.852369; tempCustomer.Latitude = 34.782104;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 9; tempCustomer.Name = "Ii"; tempCustomer.Phone = "99"; tempCustomer.Longitude = 30.147852; tempCustomer.Latitude = 35.000124;
            customerArray.Add(tempCustomer);
            tempCustomer.ID = 10; tempCustomer.Name = "Jj"; tempCustomer.Phone = "10"; tempCustomer.Longitude = 31.134679; tempCustomer.Latitude = 35.999985;
            customerArray.Add(tempCustomer);

            //10 parcels:
            DalApi.DO.Parcel tempParcel = new();
            tempParcel.ID = 1; tempParcel.SenderID = 1; tempParcel.TargetID = 3; tempParcel.Weight = DO.WeightCategories.light; tempParcel.Priority = DO.Priorities.normal; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 2; tempParcel.SenderID = 5; tempParcel.TargetID = 2; tempParcel.Weight = DO.WeightCategories.heavy; tempParcel.Priority = DO.Priorities.fast; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 3; tempParcel.SenderID = 7; tempParcel.TargetID = 4; tempParcel.Weight = DO.WeightCategories.medium; tempParcel.Priority = DO.Priorities.normal; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 4; tempParcel.SenderID = 9; tempParcel.TargetID = 6; tempParcel.Weight = DO.WeightCategories.medium; tempParcel.Priority = DO.Priorities.emergency; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 5; tempParcel.SenderID = 2; tempParcel.TargetID = 5; tempParcel.Weight = DO.WeightCategories.light; tempParcel.Priority = DO.Priorities.fast; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 6; tempParcel.SenderID = 5; tempParcel.TargetID = 7; tempParcel.Weight = DO.WeightCategories.heavy; tempParcel.Priority = DO.Priorities.normal; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 7; tempParcel.SenderID = 8; tempParcel.TargetID = 3; tempParcel.Weight = DO.WeightCategories.light; tempParcel.Priority = DO.Priorities.fast; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 8; tempParcel.SenderID = 3; tempParcel.TargetID = 8; tempParcel.Weight = DO.WeightCategories.medium; tempParcel.Priority = DO.Priorities.normal; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 9; tempParcel.SenderID = 10; tempParcel.TargetID = 4; tempParcel.Weight = DO.WeightCategories.light; tempParcel.Priority = DO.Priorities.normal; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
            tempParcel.ID = 10; tempParcel.SenderID = 8; tempParcel.TargetID = 1; tempParcel.Weight = DO.WeightCategories.heavy; tempParcel.Priority = DO.Priorities.emergency; tempParcel.Requested = DateTime.Now;
            parcelArray.Add(tempParcel);
        }
    }
}
