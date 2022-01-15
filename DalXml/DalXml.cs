using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DalApi;
using DalApi.DO;

namespace Dal
{
    sealed class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }
        string droneArray = @"DronesXml.xml";
        string stationArray = @"StationsXml.xml";
        string parcelArray = @"ParcelsXml.xml";
        string customerArray = @"CustomersXml.xml";
        string droneChargeArray = @"DronesChargeXml.xml";


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

        public double[] AskToCharge()
        {
            double[] battaryUse = new double[5];
            battaryUse[0] = Config.usingBattaryAvailable;
            battaryUse[1] = Config.usingBattaryLight;
            battaryUse[2] = Config.usingBattaryMedium;
            battaryUse[3] = Config.usingBattaryHeavy;
            battaryUse[4] = Config.chargingRatePerHour;
            return battaryUse;
        }

        //add:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(Drone tempDrone)
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);
            listOfAllDrones.ForEach(delegate (DalApi.DO.Drone d) { if (tempDrone.ID == d.ID) throw new IDexists("ID already exists\n"); });
            listOfAllDrones.Add(tempDrone);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(Station tempStation)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            listOfAllStations.ForEach(delegate (DalApi.DO.Station s) { if (tempStation.ID == s.ID) throw new IDexists("ID already exists\n"); });
            listOfAllStations.Add(tempStation);
            XMLTools.SaveListToXMLSerializer<Station>(listOfAllStations, stationArray);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(Parcel tempParcel)
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);
            //XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);
            //List<Customer> listOfAllCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customerArray);
            listOfAllParcels.ForEach(delegate (DalApi.DO.Parcel p) { if (tempParcel.ID == p.ID) throw new IDexists("ID already exists\n"); });
            var senderAndTarget = (from customer in getListCustomer()
                                   where (customer.ID == tempParcel.SenderID) || (customer.ID == tempParcel.TargetID)
                                   select customer).ToList();
            List<Customer> lst = (List<Customer>)senderAndTarget;
            if (lst.Count < 2) throw new IDdoesntExists("the sender or the target doesn't exists\n");
            listOfAllParcels.Add(tempParcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);
        }

        //delete:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int tempStationID)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);

            int stationIndex = listOfAllStations.FindIndex(x => x.ID == tempStationID);
            if (stationIndex == -1) throw new IDdoesntExists("this station doesn't exist\n");
            listOfAllStations.RemoveAt(stationIndex);
            XMLTools.SaveListToXMLSerializer<Station>(listOfAllStations, stationArray);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteDrone(int tempDroneID)
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);
            int droneIndex = listOfAllDrones.FindIndex(x => x.ID == tempDroneID);
            if (droneIndex == -1) throw new IDdoesntExists("this drone doesn't exist\n");
            listOfAllDrones.RemoveAt(droneIndex);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int tempParcelID)
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);

            int parcelIndex = listOfAllParcels.FindIndex(x => x.ID == tempParcelID);
            if (parcelIndex == -1) throw new IDdoesntExists("this customer doesn't exist\n");
            listOfAllParcels.RemoveAt(parcelIndex);
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);
        }

        //update:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateScheduled(Parcel tempParcel, int theDroneID)  //שיוך חבילה לרחפן
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);

            tempParcel.DroneId = theDroneID;
            tempParcel.Scheduled = DateTime.Now;
            int parcelIndex = listOfAllParcels.FindIndex(x => x.ID == tempParcel.ID);
            listOfAllParcels.RemoveAt(parcelIndex);
            listOfAllParcels.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updatePickedUp(Parcel tempParcel)  //איסוף חבילה ע"י רחפן
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);
            tempParcel = getParcel(tempParcel.ID);
            DalApi.DO.Drone tempDrone = listOfAllDrones.Find(x => x.ID == tempParcel.DroneId);  //find a drone that requested to take this parcel
            tempParcel.PickedUp = DateTime.Now;
            int parcelIndex = listOfAllParcels.FindIndex(x => x.ID == tempParcel.ID);
            listOfAllParcels.RemoveAt(parcelIndex);
            listOfAllParcels.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);


        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateSupply(Parcel tempParcel)  //אספקת חבילה ללקוח
        {

            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);

            tempParcel = getParcel(tempParcel.ID);
            DalApi.DO.Drone tempDrone = listOfAllDrones.Find(x => x.ID == tempParcel.DroneId);  //find a drone that requested to take this parcel
            tempParcel.Delivered = DateTime.Now;


            tempParcel.DroneId = 0;
            int parcelIndex = listOfAllParcels.FindIndex(x => x.ID == tempParcel.ID);
            listOfAllParcels.RemoveAt(parcelIndex);
            listOfAllParcels.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendToCharge(Drone tempDrone, Station tempStation)  //שליחת רחפן לטעינה
        {
            List<DroneCharge> listOfAllDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargeArray);
            DalApi.DO.DroneCharge tempDroneCharge = new();
            tempDroneCharge.ID = Config.numRunningOfChargeStation++;
            tempDroneCharge.DroneId = tempDrone.ID;
            tempDroneCharge.StationId = tempStation.ID;
            tempDroneCharge.Active = true;
            tempDroneCharge.EntranceTime = DateTime.Now;
            listOfAllDronesCharge.Add(tempDroneCharge);  //add the new charge slot to the in the list
            XMLTools.SaveListToXMLSerializer<DroneCharge>(listOfAllDronesCharge, droneChargeArray);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void releaseFromCharge(Drone tempDrone, Station tempStation)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            XMLTools.SaveListToXMLSerializer<Station>(listOfAllStations, stationArray);

            List<DroneCharge> listOfAllDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargeArray);
            var droneInChargeArray = from d in listOfAllDronesCharge
                                     where d.ID == tempDrone.ID && d.Active
                                     select d;
            DroneCharge tempDroneCharge = droneInChargeArray.FirstOrDefault();
            if (tempDroneCharge.DroneId != tempDrone.ID) throw new IDdoesntExists("the drone is not in chrage\n");
            listOfAllDronesCharge.Remove(tempDroneCharge);
            tempDroneCharge.LeavingTime = DateTime.Now;
            tempDroneCharge.Active = false;
            listOfAllDronesCharge.Add(tempDroneCharge);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(listOfAllDronesCharge, droneChargeArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(Drone tempDrone)
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);
            int droneIndex = listOfAllDrones.FindIndex(x => x.ID == tempDrone.ID);
            listOfAllDrones.RemoveAt(droneIndex);
            listOfAllDrones.Insert(droneIndex, tempDrone);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(Station tempStation)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            for (int i = 0; i < listOfAllStations.Count; i++)
            {
                if (tempStation.ID == listOfAllStations[i].ID)
                {

                    listOfAllStations.Remove(listOfAllStations[i]);
                    listOfAllStations.Add(tempStation);
                    break;
                }
            }
            XMLTools.SaveListToXMLSerializer<Station>(listOfAllStations, stationArray);

        }

        //show:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int myID)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            int StationIndex = listOfAllStations.FindIndex(x => x.ID == myID);
            if (!(StationIndex >= 0 && StationIndex <= listOfAllStations.Count)) throw new IDdoesntExists("the station doesnt exists\n");
            DalApi.DO.Station tempStation = listOfAllStations.Find(x => x.ID == myID);
            XMLTools.SaveListToXMLSerializer<Station>(listOfAllStations, stationArray);
            return tempStation;

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int myID)
        {

            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);

            int DroneIndex = listOfAllDrones.FindIndex(x => x.ID == myID);
            if (!(DroneIndex >= 0 && DroneIndex <= listOfAllDrones.Count)) throw new IDdoesntExists("the drone doesnt exists\n");
            DalApi.DO.Drone tempDrone = listOfAllDrones.Find(x => x.ID == myID);
            XMLTools.SaveListToXMLSerializer<Drone>(listOfAllDrones, droneArray);

            return tempDrone;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int myID)
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);
            int ParcelIndex = listOfAllParcels.FindIndex(x => x.ID == myID);
            if (!(ParcelIndex >= 0 && ParcelIndex <= listOfAllParcels.Count)) throw new IDdoesntExists("the parcel doesnt exists\n");
            DalApi.DO.Parcel tempParcel = listOfAllParcels.Find(x => x.ID == myID);
            XMLTools.SaveListToXMLSerializer<Parcel>(listOfAllParcels, parcelArray);

            return tempParcel;
        }

        //show lists:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getListStations()
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            var list = (from station in listOfAllStations
                        select station).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getPartOfStation(Predicate<Station> stationCondition)
        {
            List<Station> listOfAllStations = XMLTools.LoadListFromXMLSerializer<Station>(stationArray);
            List<Station> list = new();
            for (int i = 0; i < listOfAllStations.Count; i++)
            {
                if (stationCondition(listOfAllStations[i]) == true)
                {
                    list.Add(listOfAllStations[i]);
                }
            }
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> getListDrone()
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);

            var list = (from drone in listOfAllDrones
                        select drone).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> getPartOfDrone(Predicate<Drone> droneCondition)
        {
            List<Drone> listOfAllDrones = XMLTools.LoadListFromXMLSerializer<Drone>(droneArray);

            var list = (from drone in listOfAllDrones
                        where droneCondition(drone)
                        select drone).ToList();
            return list;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getListParcel()
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);

            var list = (from parcel in listOfAllParcels
                        select parcel).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getPartOfParcel(Predicate<Parcel> parcelCondition)
        {
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);

            var list = (from parcel in listOfAllParcels
                        where parcelCondition(parcel)
                        select parcel).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> getPartOfDroneCharge(Predicate<DroneCharge> droneChargeCondition)
        {

            List<DroneCharge> listOfAllDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargeArray);
            var list = (from droneCharge in listOfAllDronesCharge
                        where droneChargeCondition(droneCharge)
                        select droneCharge).ToList();
            XMLTools.SaveListToXMLSerializer<DroneCharge>(listOfAllDronesCharge, droneChargeArray);
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer tempCustomer)
        {
            XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);
            var listOfAllCustomersElem = (from customer in listOfAllCustomers.Elements()
                                          select new Customer { ID = int.Parse(customer.Element("ID").Value), Name = customer.Element("Name").Value, Phone = customer.Element("Phone").Value, Longitude = double.Parse(customer.Element("Longitude").Value), Latitude = double.Parse(customer.Element("Latitude").Value) }).ToList();
            listOfAllCustomersElem.ForEach(delegate (DalApi.DO.Customer c) { if (tempCustomer.ID == c.ID) throw new IDexists("ID already exists\n"); });
            XElement newCustomer = new XElement("Customer", new XElement("ID", tempCustomer.ID), new XElement("Name", tempCustomer.Name), new XElement("Phone", tempCustomer.Phone), new XElement("Longitude", tempCustomer.Longitude), new XElement("Latitude", tempCustomer.Latitude));
            listOfAllCustomers.Add(newCustomer);
            XMLTools.SaveListToXMLElement(listOfAllCustomers, customerArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteCustomer(int tempCustomerID)
        {
            XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);
            var tempCustomer = (from customer in listOfAllCustomers.Elements()
                                where int.Parse(customer.Element("ID").Value) == tempCustomerID
                                select customer).FirstOrDefault();
            if (int.Parse(tempCustomer.Element("ID").Value) != tempCustomerID) throw new IDdoesntExists("this customer doesn't exist\n");
            for (int i = 0; i < listOfAllParcels.Count; i++)
                if ((listOfAllParcels[i].TargetID == tempCustomerID || listOfAllParcels[i].SenderID == tempCustomerID) && listOfAllParcels[i].Delivered == null)
                    throw new DeleteProblemException("there is a parcel on the way for this customer\n");
            tempCustomer.Remove();
            XMLTools.SaveListToXMLElement(listOfAllCustomers, customerArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(Customer tempCustomer)
        {
            XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);
            List<Parcel> listOfAllParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelArray);
            var customerToUpdate = (from customer in listOfAllCustomers.Elements()
                                    where int.Parse(customer.Element("ID").Value) == tempCustomer.ID
                                    select customer).FirstOrDefault();
            if (int.Parse(customerToUpdate.Element("ID").Value) != tempCustomer.ID) throw new IDdoesntExists("this customer doesn't exist\n");
            customerToUpdate.Remove();
            customerToUpdate.Add();
            XMLTools.SaveListToXMLElement(listOfAllCustomers, customerArray);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomer(int myID)
        {
            XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);

            var tempCustomer = (from customer in listOfAllCustomers.Elements()
                                where int.Parse(customer.Element("ID").Value) == myID
                                select customer).FirstOrDefault();
            if (int.Parse(tempCustomer.Element("ID").Value) != myID) throw new IDdoesntExists("this customer doesn't exist\n");
            return new Customer { ID = int.Parse(tempCustomer.Element("ID").Value), Name = tempCustomer.Element("Name").Value, Phone = tempCustomer.Element("Phone").Value, Longitude = double.Parse(tempCustomer.Element("Longitude").Value), Latitude = double.Parse(tempCustomer.Element("Latitude").Value) };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getListCustomer()
        {
            XElement listOfAllCustomers = XMLTools.LoadListFromXMLElement(customerArray);
            var list = (from customer in listOfAllCustomers.Elements()
                        select customer).ToList();
            return (from customer in list
                    select new Customer { ID = int.Parse(customer.Element("ID").Value), Name = customer.Element("Name").Value, Phone = customer.Element("Phone").Value, Longitude = double.Parse(customer.Element("Longitude").Value), Latitude = double.Parse(customer.Element("Latitude").Value) }).ToList();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getPartOfCustomer(Predicate<Customer> customerCondition)
        {
            var list = (from customer in getListCustomer()
                        where customerCondition(customer)
                        select customer).ToList();
            return list;
        }
    }
}