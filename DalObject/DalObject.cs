using DalApi;
using DalApi.DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    sealed class DalObject : IDal
    {
        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }
        DalObject()
        {

        }
        public double[] AskToCharge()
        {
            double[] battaryUse = new double[5];
            battaryUse[0] = DataSource.Config.usingBattaryAvailable;
            battaryUse[1] = DataSource.Config.usingBattaryLight;
            battaryUse[2] = DataSource.Config.usingBattaryMedium;
            battaryUse[3] = DataSource.Config.usingBattaryHeavy;
            battaryUse[4] = DataSource.Config.chargingRatePerHour;
            return battaryUse;
        }

        //add:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(Drone tempDrone)
        {
            DataSource.droneArray.ForEach(delegate (DalApi.DO.Drone d) { if (tempDrone.ID == d.ID) throw new IDexists("ID already exists\n"); });
            DataSource.droneArray.Add(tempDrone);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(Station tempStation)
        {
            DataSource.stationArray.ForEach(delegate (DalApi.DO.Station s) { if (tempStation.ID == s.ID) throw new IDexists("ID already exists\n"); });
            DataSource.stationArray.Add(tempStation);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer tempCustomer)
        {
            DataSource.customerArray.ForEach(delegate (DalApi.DO.Customer c) { if (tempCustomer.ID == c.ID) throw new IDexists("ID already exists\n"); });
            DataSource.customerArray.Add(tempCustomer);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(Parcel tempParcel)
        {
            DataSource.parcelArray.ForEach(delegate (DalApi.DO.Parcel p) { if (tempParcel.ID == p.ID) throw new IDexists("ID already exists\n"); });
            var senderAndTarget = (from customer in DataSource.customerArray
                                   where (customer.ID == tempParcel.SenderID) || (customer.ID == tempParcel.TargetID)
                                   select customer).ToList();
            List<Customer> lst = (List<Customer>)senderAndTarget;
            if (lst.Count < 2) throw new IDdoesntExists("the sender or the target doesn't exists\n");
            DataSource.parcelArray.Add(tempParcel);
        }

        //delete:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int tempStationID)
        {
            int stationIndex = DataSource.stationArray.FindIndex(x => x.ID == tempStationID);
            if (stationIndex == -1) throw new IDdoesntExists("this station doesn't exist\n");
            DataSource.stationArray.RemoveAt(stationIndex);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void deleteDrone(int tempDroneID)
        {
            int droneIndex = DataSource.droneArray.FindIndex(x => x.ID == tempDroneID);
            if (droneIndex == -1) throw new IDdoesntExists("this drone doesn't exist\n");
            DataSource.droneArray.RemoveAt(droneIndex);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void deleteCustomer(int tempCustomerID)
        {
            int customerIndex = DataSource.customerArray.FindIndex(x => x.ID == tempCustomerID);
            if (customerIndex == -1) throw new IDdoesntExists("this customer doesn't exist\n");
            for (int i = 0; i < DataSource.parcelArray.Count; i++)
            if ((DataSource.parcelArray[i].TargetID == tempCustomerID || DataSource.parcelArray[i].SenderID == tempCustomerID )&& DataSource.parcelArray[i].Delivered == null)
                throw new DeleteProblemException("there is a parcel on the way for this customer\n");
            DataSource.customerArray.RemoveAt(customerIndex);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int tempParcelID)
        {
            int parcelIndex = DataSource.parcelArray.FindIndex(x => x.ID == tempParcelID);
            if (parcelIndex == -1) throw new IDdoesntExists("this customer doesn't exist\n");
            DataSource.parcelArray.RemoveAt(parcelIndex);
        }

        //update:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateScheduled(Parcel tempParcel, int theDroneID)  //שיוך חבילה לרחפן
        {
            tempParcel.DroneId = theDroneID;
            tempParcel.Scheduled = DateTime.Now;
            int parcelIndex = DataSource.parcelArray.FindIndex(x => x.ID == tempParcel.ID);
            DataSource.parcelArray.RemoveAt(parcelIndex);
            DataSource.parcelArray.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updatePickedUp(Parcel tempParcel)  //איסוף חבילה ע"י רחפן
        {
            tempParcel = getParcel(tempParcel.ID);
            DalApi.DO.Drone tempDrone = DataSource.droneArray.Find(x => x.ID == tempParcel.DroneId);  //find a drone that requested to take this parcel
            tempParcel.PickedUp = DateTime.Now;
            int parcelIndex = DataSource.parcelArray.FindIndex(x => x.ID == tempParcel.ID);
            DataSource.parcelArray.RemoveAt(parcelIndex);
            DataSource.parcelArray.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateSupply(Parcel tempParcel)  //אספקת חבילה ללקוח
        {
            tempParcel = getParcel(tempParcel.ID);
            DalApi.DO.Drone tempDrone = DataSource.droneArray.Find(x => x.ID == tempParcel.DroneId);  //find a drone that requested to take this parcel
            tempParcel.Delivered = DateTime.Now;


            tempParcel.DroneId = 0;
            int parcelIndex = DataSource.parcelArray.FindIndex(x => x.ID == tempParcel.ID);
            DataSource.parcelArray.RemoveAt(parcelIndex);
            DataSource.parcelArray.Insert(parcelIndex, tempParcel);  //repalce the updated parcel in the list
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendToCharge(Drone tempDrone, Station tempStation)  //שליחת רחפן לטעינה
        {

            DalApi.DO.DroneCharge tempDroneCharge = new();
            tempDroneCharge.ID = DataSource.Config.numRunningOfChargeStation++;
            tempDroneCharge.DroneId = tempDrone.ID;
            tempDroneCharge.StationId = tempStation.ID;
            tempDroneCharge.Active = true;
            tempDroneCharge.EntranceTime = DateTime.Now;
            DataSource.droneChargeArray.Add(tempDroneCharge);  //add the new charge slot to the in the list
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void releaseFromCharge(Drone tempDrone, Station tempStation)
        {
            var droneInChargeArray = from d in DataSource.droneChargeArray
                                     where d.ID == tempDrone.ID && d.Active
                                     select d;
            DroneCharge tempDroneCharge = droneInChargeArray.FirstOrDefault();
            if (tempDroneCharge.DroneId != tempDrone.ID) throw new IDdoesntExists("the drone is not in chrage\n");
            DataSource.droneChargeArray.Remove(tempDroneCharge);
            tempDroneCharge.LeavingTime = DateTime.Now;
            tempDroneCharge.Active = false;
            DataSource.droneChargeArray.Add(tempDroneCharge);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(Drone tempDrone)
        {

            int droneIndex = DataSource.droneArray.FindIndex(x => x.ID == tempDrone.ID);
            DataSource.droneArray.RemoveAt(droneIndex);
            DataSource.droneArray.Insert(droneIndex, tempDrone);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(Station tempStation)
        {
            for (int i = 0; i < DataSource.stationArray.Count; i++)
            {
                if (tempStation.ID == DataSource.stationArray[i].ID)
                {

                    DataSource.stationArray.Remove(DataSource.stationArray[i]);
                    DataSource.stationArray.Add(tempStation);
                    break;
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(Customer tempCustomer)
        {
            for (int i = 0; i < DataSource.customerArray.Count; i++)
            {
                if (tempCustomer.ID == DataSource.customerArray[i].ID)
                {
                    DataSource.customerArray.Remove(DataSource.customerArray[i]);
                    DataSource.customerArray.Add(tempCustomer);
                    break;
                }
            }
        }


        //show:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int myID)
        {
            int StationIndex = DataSource.stationArray.FindIndex(x => x.ID == myID);
            if (!(StationIndex >= 0 && StationIndex <= DataSource.stationArray.Count)) throw new IDdoesntExists("the station doesnt exists\n");
            DalApi.DO.Station tempStation = DataSource.stationArray.Find(x => x.ID == myID);
            return tempStation;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int myID)
        {
            int DroneIndex = DataSource.droneArray.FindIndex(x => x.ID == myID);
            if (!(DroneIndex >= 0 && DroneIndex <= DataSource.droneArray.Count)) throw new IDdoesntExists("the drone doesnt exists\n");
            DalApi.DO.Drone tempDrone = DataSource.droneArray.Find(x => x.ID == myID);
            return tempDrone;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomer(int myID)
        {
            int CustomerIndex = DataSource.customerArray.FindIndex(x => x.ID == myID);
            if (!(CustomerIndex >= 0 && CustomerIndex <= DataSource.customerArray.Count)) throw new IDdoesntExists("the customer doesnt exists\n");
            DalApi.DO.Customer tempCustomer = DataSource.customerArray.Find(x => x.ID == myID);
            return tempCustomer;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int myID)
        {
            int ParcelIndex = DataSource.parcelArray.FindIndex(x => x.ID == myID);
            if (!(ParcelIndex >= 0 && ParcelIndex <= DataSource.parcelArray.Count)) throw new IDdoesntExists("the parcel doesnt exists\n");
            DalApi.DO.Parcel tempParcel = DataSource.parcelArray.Find(x => x.ID == myID);
            return tempParcel;
        }

        //show lists:
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getListStations()
        {
            var list = (from station in DataSource.stationArray
                        select station).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getPartOfStation(Predicate<Station> stationCondition)
        {
            List<Station> list = new();
            for (int i = 0; i < DataSource.stationArray.Count; i++)
            {
                if ((stationCondition(DataSource.stationArray[i])) == true)
                {
                    list.Add(DataSource.stationArray[i]);
                    //Console.WriteLine(stationArray[i]);
                }
            }
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> getListDrone()
        {
            var list = (from drone in DataSource.droneArray
                        select drone).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> getPartOfDrone(Predicate<Drone> droneCondition)
        {
            var list = (from drone in DataSource.droneArray
                        where droneCondition(drone)
                        select drone).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getListCustomer()
        {
            var list = (from customer in DataSource.customerArray
                        select customer).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getPartOfCustomer(Predicate<Customer> customerCondition)
        {
            var list = (from customer in DataSource.customerArray
                        where customerCondition(customer)
                        select customer).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getListParcel()
        {
            var list = (from parcel in DataSource.parcelArray
                        select parcel).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getPartOfParcel(Predicate<Parcel> parcelCondition)
        {
            var list = (from parcel in DataSource.parcelArray
                        where parcelCondition(parcel)
                        select parcel).ToList();
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> getPartOfDroneCharge(Predicate<DroneCharge> droneChargeCondition)
        {
            var list = (from droneCharge in DataSource.droneChargeArray
                        where droneChargeCondition(droneCharge)
                        select droneCharge).ToList();
            return list;
        }
    }
}
