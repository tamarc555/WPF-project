using System;
using System.Collections.Generic;
using System.Linq;
using static BO.Exceptions;
using static BO.Enum;
using BlApi.BO;
using BlApi;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BL
{
    public class BL : IBL
    {
        static readonly IBL instance = new BL();
        public static IBL Instance { get => instance; }

        internal DalApi.IDal dal = DalApi.DalFactory.GetDal();
        private List<DroneToList> listDrone;  //האובייקט שמתחזק את רשימת הרחפנים

        public BL()
        {
            listDrone = new List<DroneToList>();
            DroneToList temp = new();

            initializedDrone();

        }
        private void initializedDrone()
        {
            lock (dal)
            {
                foreach (var drone in dal.getListDrone())
                {
                    Random r = new Random();
                    DroneToList droneToUpdate = new();
                    droneToUpdate.ID = drone.ID;
                    droneToUpdate.Model = drone.Model;
                    droneToUpdate.MaxWeigth = (WeightCategories)drone.MaxWeigth;
                    droneToUpdate.Battary = r.Next(20, 40);
                    var myParcel = (from parcel in getListParcel()
                                    where parcel.TheDroneInParcel.ID == droneToUpdate.ID && parcel.Delivered == null
                                    select parcel).FirstOrDefault();
                    if (myParcel != null)
                    {
                        if (myParcel.PickedUp != null)
                        {
                            droneToUpdate.StatusOfDrone = DroneStatuses.delivery;
                            droneToUpdate.DroneLocation = getCustomer(myParcel.Target.ID).CustomerLocation;
                            droneToUpdate.ParcelInDelivery = new ParcelInDelivery(myParcel.ID, true, myParcel.Priority, myParcel.Weight, myParcel.Sender, myParcel.Target, getCustomer(myParcel.Sender.ID).CustomerLocation, getCustomer(myParcel.Target.ID).CustomerLocation, getCustomer(myParcel.Sender.ID).CustomerLocation.calculateDistance(getCustomer(myParcel.Target.ID).CustomerLocation));
                        }
                        else
                        {
                            droneToUpdate.StatusOfDrone = DroneStatuses.scheduled;
                            droneToUpdate.DroneLocation = getCustomer(myParcel.Sender.ID).CustomerLocation;
                            droneToUpdate.ParcelInDelivery = new ParcelInDelivery(myParcel.ID, false, myParcel.Priority, myParcel.Weight, myParcel.Sender, myParcel.Target, getCustomer(myParcel.Sender.ID).CustomerLocation, getCustomer(myParcel.Target.ID).CustomerLocation, getCustomer(myParcel.Sender.ID).CustomerLocation.calculateDistance(getCustomer(myParcel.Target.ID).CustomerLocation));
                        }
                    }
                    else
                    {
                        droneToUpdate.StatusOfDrone = DroneStatuses.available;
                        droneToUpdate.DroneLocation = new location(r.NextDouble() * (33.5 - 29.3) + 29.3, r.NextDouble() * (36.3 - 33.7) + 33.7);
                    }
                    listDrone.Add(droneToUpdate);
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(Drone tempDrone)
        {
            try
            {
                lock (dal)
                {

                    if (getAvailableStations().FirstOrDefault().ID == 0) throw new IDdoesntExists("אין עמדות טעינה פנויות ולכן לא ניתן להוסיף רחפן חדש");
                    //אם אין עמדות טעינה פנויות לא ניתן להוסיף רחפן
                    if (tempDrone.ID <= 0 || tempDrone.ID > 999999999) throw new numNotInRange("The number of the ID is not in the number range\n");
                    int newWeight = (int)tempDrone.MaxWeigth;
                    if (newWeight < 1 || newWeight > 3) throw new numNotInRange("The number of the weight is not in the number range\n");
                    dal.addDrone(new DalApi.DO.Drone(tempDrone.ID, tempDrone.Model, (DO.WeightCategories)tempDrone.MaxWeigth));
                    Random r = new Random();
                    DroneToList droneToUpdate = new();
                    droneToUpdate.ID = tempDrone.ID;
                    droneToUpdate.Model = tempDrone.Model;
                    droneToUpdate.MaxWeigth = tempDrone.MaxWeigth;
                    droneToUpdate.Battary = r.Next(20, 40);
                    droneToUpdate.StatusOfDrone = DroneStatuses.available;
                    droneToUpdate.DroneLocation.longitude = r.NextDouble() * (33.5 - 29.3) + 29.3;
                    droneToUpdate.DroneLocation.latitude = r.NextDouble() * (36.3 - 33.7) + 33.7;
                    listDrone.Add(droneToUpdate);
                    Drone sendDrone = new();
                    sendDrone.ID = tempDrone.ID;
                    sendDrone.Model = tempDrone.Model;
                    sendDrone.MaxWeigth = tempDrone.MaxWeigth;
                    sendDrone.DroneLocation.longitude = droneToUpdate.DroneLocation.longitude;
                    sendDrone.DroneLocation.latitude = droneToUpdate.DroneLocation.latitude;
                    sendToCharge(sendDrone);
                }
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(Station tempStation)
        {
            try
            {
                lock (dal)
                {

                    if (properLocation(tempStation.StationLocation) == false) throw new RangeException("the location is out of Israel's range\n");
                    dal.addStation(new DalApi.DO.Station(tempStation.ID, tempStation.Name, tempStation.StationLocation.longitude, tempStation.StationLocation.latitude, tempStation.ChargeSlots));
                }
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this station\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer tempCustomer)
        {
            if (tempCustomer.ID <= 0 || tempCustomer.ID > 999999999) throw new numNotInRange("The number is not in the number range\n");
            if (properLocation(tempCustomer.CustomerLocation) == false) throw new RangeException("the location is out of Israel's range\n");
            try
            {
                lock (dal)
                {

                    dal.addCustomer(new DalApi.DO.Customer(tempCustomer.ID, tempCustomer.Name, tempCustomer.Phone, tempCustomer.CustomerLocation.longitude, tempCustomer.CustomerLocation.latitude));
                }
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this customer\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(Parcel tempParcel)
        {
            int newWeight = (int)tempParcel.Weight;
            if (newWeight <= 0 || newWeight > 3) throw new numNotInRange("The number is not in the number range\n");
            int newPriority = (int)tempParcel.Priority;
            if (newPriority <= 0 || newPriority > 3) throw new numNotInRange("The number is not in the number range\n");
            tempParcel.Requested = DateTime.Now;
            tempParcel.Scheduled = null;
            tempParcel.PickedUp = null;
            tempParcel.Delivered = null;
            tempParcel.TheDroneInParcel = null;
            try
            {
                lock (dal)
                {
                    dal.addParcel(new DalApi.DO.Parcel(tempParcel.ID, tempParcel.Sender.ID, tempParcel.Target.ID, (DO.WeightCategories)tempParcel.Weight, (DO.Priorities)tempParcel.Priority, tempParcel.Requested, tempParcel.Scheduled, tempParcel.PickedUp, tempParcel.Delivered, 0));
                }
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this parcel\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteCustomer(int tempCustomerID)
        {
            try
            {
                lock (dal)
                {

                    dal.deleteCustomer(tempCustomerID);
                }
            }
            catch (Exception ex)
            {
                throw new DeleteProblemException("can't delete this customer\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteDrone(int tempDroneID)
        {
            try
            {
                lock (dal)
                {
                    Drone tempDrone = getDrone(tempDroneID);
                    if ((tempDrone.StatusOfDrone == DroneStatuses.delivery) || (tempDrone.StatusOfDrone == DroneStatuses.scheduled)) throw new DeleteProblemException("the drone is used\n");
                    dal.deleteDrone(tempDroneID);
                    for (int i = 0; i < listDrone.Count; i++)
                    {
                        if (listDrone[i].ID == tempDroneID)
                        {
                            if (listDrone[i].StatusOfDrone == DroneStatuses.maintenance)
                            {
                                var item = (from station in getListStations()
                                            where (station.StationLocation.longitude == listDrone[i].DroneLocation.longitude && station.StationLocation.latitude == listDrone[i].DroneLocation.latitude)
                                            select station).FirstOrDefault();
                                if (item.ID == 0) throw new IDdoesntExists("can't find the station that the drone is in charge in\n");
                                item = getStation(item.ID);
                                item.ChargeSlots++;
                                updateStation(item);
                            }
                            listDrone.Remove(listDrone[i]);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DeleteProblemException("can't delete this drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int tempStationID)
        {
            try
            {
                lock (dal)
                {

                    Station tempStation = getStation(tempStationID);
                    var droneInCharge = (from drone in listDrone
                                         where (drone.StatusOfDrone == DroneStatuses.maintenance && drone.DroneLocation.longitude == tempStation.StationLocation.longitude && drone.DroneLocation.latitude == tempStation.StationLocation.latitude)
                                         select drone).ToList();
                    if (droneInCharge.Count > 0) throw new DeleteProblemException("there are drones charging at this station\n");
                    dal.deleteStation(tempStationID);
                }
            }
            catch (Exception ex)
            {
                throw new DeleteProblemException("can't delete this station\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int tempParcelID)
        {
            try
            {
                lock (dal)
                {

                    var droneWithParcel = (from drone in listDrone
                                           where drone.ParcelInDelivery.ID == tempParcelID
                                           select drone).ToList();
                    if (droneWithParcel.Count > 0) throw new DeleteProblemException("this parcel is schedualed to a drone\n");
                    dal.deleteParcel(tempParcelID);
                }
            }
            catch
            {
                throw new DeleteProblemException("can't delete this parcel\n");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateScheduled(Drone tempDrone)  //שיוך חבילה לרחפן
        {
            try
            {
                lock (dal)
                {

                    tempDrone = getDrone(tempDrone.ID);
                    if (tempDrone.StatusOfDrone != DroneStatuses.available) throw new noAvailableDrone("the drone is not avalible\n");
                    //search a parcel: 
                    List<Parcel> parcelAvalible = (List<Parcel>)getNotConnectedParcel();  //חבילות שטרם שויכו
                    if (parcelAvalible == null) throw new ArgumentNullException("There are no parcels available for delivery\n");
                    var distance = (from parcel in parcelAvalible
                                    where (calculateEnoughBattaryForDelievery(tempDrone, parcel)) == true && (parcel.Weight <= tempDrone.MaxWeigth)
                                    orderby getCustomer(parcel.Sender.ID).CustomerLocation.calculateDistance(tempDrone.DroneLocation)
                                    select parcel).ToList();
                    if (distance == null) throw new UpdateProblemException("the drone can't carry any parcel\n");
                    distance.Reverse();
                    var priority = (from parcel in distance
                                    orderby parcel.Priority, parcel.Weight
                                    select parcel).ToList();
                    priority.Reverse();
                    Parcel tempParcel = getParcel(priority.FirstOrDefault().ID);

                    dal.updateScheduled(new DalApi.DO.Parcel(tempParcel.ID, tempParcel.Sender.ID, tempParcel.Target.ID, (DO.WeightCategories)tempParcel.Weight, (DO.Priorities)tempParcel.Priority, tempParcel.Requested, tempParcel.Scheduled, tempParcel.PickedUp, tempParcel.Delivered, tempParcel.TheDroneInParcel.ID), tempDrone.ID);

                    for (int i = 0; i < listDrone.Count; i++)
                        if (listDrone[i].ID == tempDrone.ID)
                        {
                            listDrone[i].StatusOfDrone = DroneStatuses.scheduled;

                            ParcelInDelivery tempParcelInDelievery = new();
                            tempParcelInDelievery.ID = tempParcel.ID;
                            tempParcelInDelievery.StatusOfParcel = false; //ממתין לאיסוף
                            tempParcelInDelievery.Priority = tempParcel.Priority;
                            tempParcelInDelievery.Weight = tempParcel.Weight;
                            tempParcelInDelievery.Sender = tempParcel.Sender;
                            tempParcelInDelievery.Target = tempParcel.Target;
                            tempParcelInDelievery.PickUpOfParcel = getCustomer(tempParcel.Sender.ID).CustomerLocation;
                            tempParcelInDelievery.ProvidedOfParcel = getCustomer(tempParcel.Target.ID).CustomerLocation;
                            tempParcelInDelievery.Distance = tempParcelInDelievery.PickUpOfParcel.calculateDistance(tempParcelInDelievery.ProvidedOfParcel);
                            listDrone[i].ParcelInDelivery = tempParcelInDelievery;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't connect this parcel to drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updatePickedUp(Drone tempDrone)  //איסוף חבילה ע"י רחפן
        {
            try
            {
                lock (dal)
                {
                    List<DalApi.DO.Parcel> parcelRequested = (List<DalApi.DO.Parcel>)dal.getPartOfParcel(parcel => parcel.DroneId == tempDrone.ID);
                    if (parcelRequested == null) throw new UpdateProblemException("this drone is not associated with any parcel\n");
                    Parcel tempParcel = getParcel(parcelRequested[0].ID);
                    if (tempParcel.Scheduled == null) throw new UpdateProblemException("the parcel is not waiting to be picked up\n");
                    if (!(tempParcel.PickedUp == null)) throw new UpdateProblemException("the parcel already picked up\n");
                    dal.updatePickedUp(new DalApi.DO.Parcel(tempParcel.ID, tempParcel.Sender.ID, tempParcel.Target.ID, (DO.WeightCategories)tempParcel.Weight, (DO.Priorities)tempParcel.Priority, tempParcel.Requested, tempParcel.Scheduled, tempParcel.PickedUp, tempParcel.Delivered, tempDrone.ID));
                    for (int i = 0; i < listDrone.Count; i++)
                        if (listDrone[i].ID == tempDrone.ID)
                        {
                            listDrone[i].Battary -= (listDrone[i].DroneLocation.calculateDistance(getCustomer(tempParcel.Sender.ID).CustomerLocation) * dal.AskToCharge()[0]);
                            listDrone[i].DroneLocation = getCustomer(tempParcel.Sender.ID).CustomerLocation;
                            listDrone[i].ParcelInDelivery.StatusOfParcel = true; //בדרך ליעד
                            listDrone[i].StatusOfDrone = DroneStatuses.delivery;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't pick up this parcel\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateSupply(Drone tempDrone)  //אספקת חבילה ללקוח
        {
            try
            {
                lock (dal)
                {
                    var theParcelInDelievery = (from drone in listDrone
                                                where (drone.ID == tempDrone.ID) && (drone.StatusOfDrone == DroneStatuses.delivery) && (drone.ParcelInDelivery != null)
                                                select drone).ToList();
                    if (theParcelInDelievery.Count == 0) throw new UpdateProblemException("this drone is not in delievery\n");
                    for (int i = 0; i < listDrone.Count; i++)
                        if (listDrone[i].ID == tempDrone.ID)
                        {
                            dal.updateSupply(dal.getParcel(listDrone[i].ParcelInDelivery.ID));
                            listDrone[i].Battary -= (listDrone[i].ParcelInDelivery.Distance * dal.AskToCharge()[(int)listDrone[i].ParcelInDelivery.Weight]);
                            listDrone[i].DroneLocation = getCustomer(listDrone[i].ParcelInDelivery.Target.ID).CustomerLocation;
                            listDrone[i].StatusOfDrone = DroneStatuses.available;
                            listDrone[i].ParcelInDelivery = new();
                        }
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't supply this parcel", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendToCharge(Drone tempDrone)  //שליחת רחפן לטעינה
        {
            try
            {
                lock (dal)
                {
                    dal.getDrone(tempDrone.ID);  //check if the drone exists
                    Station tempStation = new();
                    int i = 0;
                    for (; i < listDrone.Count; i++)
                    {
                        if (listDrone[i].ID == tempDrone.ID)
                        {
                            if (!(listDrone[i].StatusOfDrone == DroneStatuses.available)) throw new noAvailableDrone("the drone is not avalible for charge\n");
                            List<Station> avalibleStations = (List<Station>)getAvailableStations();
                            if (avalibleStations.Count == 0) throw new noAvailableStation("no avalible station\n");
                            List<Station> enoughBattary = new();
                            for (int j = 0; j < avalibleStations.Count; j++)
                            {
                                if (calculateMinBattaryRequired(getDrone(listDrone[i].ID), avalibleStations[j]) == true)
                                    enoughBattary.Add(avalibleStations[j]);
                            }
                            if (enoughBattary == null) throw new notEnoughBattary("there is not enough battary to get to the station\n");
                            List<Station> lst = (List<Station>)enoughBattary;
                            double minDistance = listDrone[i].DroneLocation.calculateDistance(lst[0].StationLocation);
                            int minDistanceIndex = 0;
                            for (int j = 1; j < lst.Count; j++)
                            {
                                if (listDrone[i].DroneLocation.calculateDistance(lst[j].StationLocation) < minDistance)
                                {
                                    minDistance = listDrone[i].DroneLocation.calculateDistance(lst[j].StationLocation);
                                    minDistanceIndex = j;
                                }
                            }
                            tempStation = lst[minDistanceIndex];
                            listDrone[i].Battary -= (listDrone[i].DroneLocation.calculateDistance(tempStation.StationLocation) * dal.AskToCharge()[(int)WeightCategories.free]);
                            listDrone[i].DroneLocation = tempStation.StationLocation;
                            listDrone[i].StatusOfDrone = DroneStatuses.maintenance;
                            tempStation.ChargeSlots--;
                            dal.updateStation(new DalApi.DO.Station(tempStation.ID, tempStation.Name, tempStation.StationLocation.longitude, tempStation.StationLocation.latitude, tempStation.ChargeSlots));
                            break;
                        }
                        else if (i == listDrone.Count - 1) throw new IDdoesntExists("the drone wasn't found\n");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new UpdateProblemException("Can't send this drone to charge\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void releaseFromCharge(Drone tempDrone, double timeInCharge)
        {
            try
            {
                tempDrone = getDrone(tempDrone.ID);
                if (tempDrone.StatusOfDrone != DroneStatuses.maintenance) throw new IDdoesntExists("this drone is not in charge\n");
                DalApi.DO.Station tempStation = ((List<DalApi.DO.Station>)dal.getPartOfStation(myLocation => tempDrone.DroneLocation.longitude == myLocation.Longitude && tempDrone.DroneLocation.latitude == myLocation.Latitude))[0];

                for (int i = 0; i < listDrone.Count; i++)
                    if (listDrone[i].ID == tempDrone.ID)
                    {
                        listDrone[i].StatusOfDrone = DroneStatuses.available;
                        listDrone[i].Battary += timeInCharge * (double)dal.AskToCharge()[4];
                        if (listDrone[i].Battary > 100) listDrone[i].Battary = 100;
                    }
                tempStation.ChargeSlots++;
                dal.updateStation(tempStation);
                double x = listDrone[2].Battary;
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't release this drone to charge\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(Drone tempDrone)
        {
            try
            {
                lock (dal)
                {
                    dal.updateDrone(new DalApi.DO.Drone(tempDrone.ID, tempDrone.Model, (DO.WeightCategories)tempDrone.MaxWeigth));
                    for (int i = 0; i < listDrone.Count; i++)
                    {
                        if (listDrone[i].ID == tempDrone.ID)
                            listDrone[i].Model = tempDrone.Model;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UpdateProblemException("Can't update this drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(Customer tempCustomer)
        {
            try
            {
                lock (dal)
                {

                    string tempName = "0", tempPhone = "0";
                    if (tempCustomer.Name != "0")
                        tempName = tempCustomer.Name;
                    if (tempCustomer.Phone != "0")
                        tempPhone = tempCustomer.Phone;
                    tempCustomer = getCustomer(tempCustomer.ID);
                    if (tempName != "0")
                        tempCustomer.Name = tempName;
                    if (tempPhone != "0")
                        tempCustomer.Phone = tempPhone;
                    dal.updateCustomer(new DalApi.DO.Customer(tempCustomer.ID, tempCustomer.Name, tempCustomer.Phone, tempCustomer.CustomerLocation.longitude, tempCustomer.CustomerLocation.latitude));
                }
            }
            catch (Exception ex)
            {
                throw new UpdateProblemException("Can't update this customer\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(Station tempStation)
        {
            try
            {
                lock (dal)
                {

                    int tempChargeSlot = 0, tempName = 0;
                    if (tempStation.ChargeSlots >= 0) tempChargeSlot = tempStation.ChargeSlots;
                    if (tempStation.Name > 0) tempName = tempStation.Name;
                    tempStation = getStation(tempStation.ID);
                    if (tempChargeSlot >= 0) tempStation.ChargeSlots = tempChargeSlot;
                    if (tempName > 0) tempStation.Name = tempName;
                    dal.updateStation(new DalApi.DO.Station(tempStation.ID, tempStation.Name, tempStation.StationLocation.longitude, tempStation.StationLocation.latitude, tempStation.ChargeSlots));
                }
            }
            catch (Exception ex)
            {
                throw new UpdateProblemException("Can't update this station\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int myID)
        {
            try
            {
                lock (dal)
                {
                    DalApi.DO.Station tempStation = dal.getStation(myID);
                    location l = new location(tempStation.Longitude, tempStation.Latitude);
                    List<Drone> _listDroneInCharge = new List<Drone>();
                    for (int i = 0; i < listDrone.Count; i++)
                        if (listDrone[i].StatusOfDrone == DroneStatuses.maintenance && listDrone[i].DroneLocation.longitude == l.longitude && listDrone[i].DroneLocation.latitude == l.latitude)
                            _listDroneInCharge.Add(getDrone(listDrone[i].ID));
                    return new Station(tempStation.ID, tempStation.Name, l, tempStation.ChargeSlots, _listDroneInCharge);
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this station\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomerToList getCustomerToList(int myID)
        {
            try
            {
                int counterd = 0, counterp = 0;
                Customer tempCustomer = getCustomer(myID);
                for (int j = 0; j < tempCustomer.ParcelToCustomer.Count(); j++)
                    if (tempCustomer.ParcelToCustomer[j].Delivered != null)
                        counterd++;
                for (int j = 0; j < tempCustomer.ParcelFromCustomer.Count(); j++)
                    if (tempCustomer.ParcelFromCustomer[j].Delivered != null)
                        counterp++;
                return new CustomerToList(tempCustomer.ID, tempCustomer.Name, tempCustomer.Phone, counterd, tempCustomer.ParcelToCustomer.Count() - counterd, counterp, tempCustomer.ParcelFromCustomer.Count() - counterp);
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this station\n", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public StationToList getStationToList(int myID)
        {
            try
            {
                Station tempStation = getStation(myID);
                return new StationToList(tempStation.ID, tempStation.Name, tempStation.ChargeSlots, tempStation.ListDroneInCharge.Count());
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this station\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int myID)
        {
            try
            {
                int DroneIndex = listDrone.FindIndex(x => x.ID == myID);
                if (!(DroneIndex >= 0 && DroneIndex <= listDrone.Count)) throw new IDdoesntExists("the drone doesnt exists\n");
                return new Drone(listDrone[DroneIndex].ID, listDrone[DroneIndex].Model, listDrone[DroneIndex].MaxWeigth, listDrone[DroneIndex].Battary, listDrone[DroneIndex].StatusOfDrone, listDrone[DroneIndex].ParcelInDelivery, listDrone[DroneIndex].DroneLocation);
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> getListDroneToList()
        {
            try
            {
                lock (dal)
                {

                    var listOfMyDroneToList = (from drone in dal.getListDrone()
                                               select getDrone(drone.ID)).ToList();
                    List<DroneToList> listOfDroneToList = new List<DroneToList>();
                    for (int i = 0; i < listDrone.Count; i++)
                        listOfDroneToList.Add(new DroneToList(listDrone[i].ID, listDrone[i].Model, listDrone[i].MaxWeigth, listDrone[i].Battary, listDrone[i].StatusOfDrone, (listDrone[i].DroneLocation), listDrone[i].ParcelInDelivery));
                    return listOfDroneToList;
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this drone\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomer(int myID)
        {
            try
            {
                lock (dal)
                {

                    DalApi.DO.Customer tempCustomer = dal.getCustomer(myID);
                    Parcel tempParcel = new();
                    location l = new location(tempCustomer.Longitude, tempCustomer.Latitude);
                    List<Parcel> _fromCustomer = new();
                    for (int i = 0; i < listDrone.Count; i++)
                    {
                        if (listDrone[i].ParcelInDelivery != null && listDrone[i].ParcelInDelivery.Sender.ID == myID)
                        {
                            tempParcel = getParcel(listDrone[i].ParcelInDelivery.ID);
                            _fromCustomer.Add(new Parcel(listDrone[i].ParcelInDelivery.ID, listDrone[i].ParcelInDelivery.Sender, listDrone[i].ParcelInDelivery.Target, listDrone[i].ParcelInDelivery.Weight, listDrone[i].ParcelInDelivery.Priority, tempParcel.TheDroneInParcel, (DateTime?)tempParcel.Requested, (DateTime?)tempParcel.Scheduled, (DateTime?)tempParcel.PickedUp, (DateTime?)tempParcel.Delivered));
                        }
                    }

                    List<Parcel> _toCustomer = new();
                    for (int i = 0; i < listDrone.Count; i++)
                    {
                        if (listDrone[i].ParcelInDelivery != null && listDrone[i].ParcelInDelivery.Target.ID == myID)
                        {
                            tempParcel = getParcel(listDrone[i].ParcelInDelivery.ID);
                            _toCustomer.Add(new Parcel(listDrone[i].ParcelInDelivery.ID, listDrone[i].ParcelInDelivery.Sender, listDrone[i].ParcelInDelivery.Target, listDrone[i].ParcelInDelivery.Weight, listDrone[i].ParcelInDelivery.Priority, tempParcel.TheDroneInParcel, (DateTime?)tempParcel.Requested, (DateTime?)tempParcel.Scheduled, (DateTime?)tempParcel.PickedUp, (DateTime?)tempParcel.Delivered));

                        }
                    }
                    return new Customer(tempCustomer.ID, tempCustomer.Name, tempCustomer.Phone, l, _fromCustomer, _toCustomer);
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this customer\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomerByName(string myCustomerName)
        {
            try
            {
                var myCustomer = (from customer in getListCustomer()
                                  where customer.Name == myCustomerName
                                  select customer).FirstOrDefault();
                if (myCustomer.ID == 0) throw new IDdoesntExists("Can't get this customer\n");
                return myCustomer;
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this customer\n", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int myID)
        {
            try
            {
                lock (dal)
                {

                    DalApi.DO.Parcel tempParcel = dal.getParcel(myID);
                    CustomerInParcel senderC = new(tempParcel.SenderID, dal.getCustomer(tempParcel.SenderID).Name);
                    CustomerInParcel targetC = new(tempParcel.TargetID, dal.getCustomer(tempParcel.TargetID).Name);
                    location l = new();
                    double myBattary = 0;
                    for (int i = 0; i < listDrone.Count; i++)
                        if (listDrone[i].ID == tempParcel.DroneId)
                        {
                            l = listDrone[i].DroneLocation;
                            myBattary = listDrone[i].Battary;
                        }
                    DroneInParcel droneC = new(tempParcel.DroneId, myBattary, l);
                    return new Parcel(tempParcel.ID, senderC, targetC, (WeightCategories)tempParcel.Weight, (Priorities)tempParcel.Priority, droneC, (DateTime?)tempParcel.Requested, (DateTime?)tempParcel.Scheduled, (DateTime?)tempParcel.PickedUp, (DateTime?)tempParcel.Delivered);
                }
            }
            catch (Exception ex)
            {
                throw new IDdoesntExists("Can't get this parcel\n", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<myDroneToList> getPartOfDrone(Predicate<DroneToList> DroneCondition, List<myDroneToList> myList = null)
        {

            if (myList == null)
            {
                List<DroneToList> lst = (from drone in listDrone
                                         where DroneCondition(drone)
                                         select drone).ToList();
                myList = new List<myDroneToList>();
                for (int i = 0; i < lst.Count; i++)
                    myList.Add(new myDroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));
                return myList;
            }
            else
            {
                List<DroneToList> lst1 = new();
                for (int i = 0; i < myList.Count; i++)
                    lst1.Add(new DroneToList(myList[i].ID, myList[i].Model, (WeightCategories)myList[i].MaxWeigth, myList[i].Battary, (DroneStatuses)myList[i].StatusOfDrone, getDrone(myList[i].ID).DroneLocation, getDrone(myList[i].ID).TheParcelInDelivery));
                List<DroneToList> lst = (from drone in lst1
                                         where DroneCondition(drone)
                                         select drone).ToList();
                myList.Clear();
                for (int i = 0; i < lst.Count; i++)
                    myList.Add(new myDroneToList(lst[i].ID, lst[i].Model, (WeightCategories)lst[i].MaxWeigth, lst[i].Battary, (DroneStatuses)lst[i].StatusOfDrone, lst[i].DroneLocation.ToString(), lst[i].ParcelInDelivery.ID));
                return myList;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getAvailableStations()
        {
            lock (dal)
            {

                List<Station> newStationList = new();
                List<DalApi.DO.Station> tempStationList = (List<DalApi.DO.Station>)dal.getPartOfStation(station => station.ChargeSlots > 0);
                for (int i = 0; i < tempStationList.Count; i++)
                    newStationList.Add(getStation(tempStationList[i].ID));
                return newStationList;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getNotConnectedParcel()
        {
            lock (dal)
            {

                List<Parcel> newParcelList = new();
                List<DalApi.DO.Parcel> tempParcelList = (List<DalApi.DO.Parcel>)dal.getPartOfParcel(parcel => parcel.Scheduled == null);
                for (int i = 0; i < tempParcelList.Count; i++)
                    newParcelList.Add(getParcel(tempParcelList[i].ID));
                return newParcelList;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getListStations()
        {
            lock (dal)
            {

                var listOfStation = (from station in dal.getListStations()
                                     select getStation(station.ID)).ToList();
                return listOfStation;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> getListStationToList()
        {
            lock (dal)
            {
                var listOfStation = (from station in dal.getListStations()
                                     select getStation(station.ID)).ToList();
                List<StationToList> listOfStationToList = new List<StationToList>();
                for (int i = 0; i < listOfStation.Count; i++)
                    listOfStationToList.Add(new StationToList(listOfStation[i].ID, listOfStation[i].Name, listOfStation[i].ChargeSlots, listOfStation[i].ListDroneInCharge.Count));
                return listOfStationToList;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> getListParcelToList()
        {
            lock (dal)
            {

                ParcelStatuses myStatuse = 0;
                var listOfParcel = (from parcel in dal.getListParcel()
                                    select getParcel(parcel.ID)).ToList();
                List<ParcelToList> listOfParcelToList = new List<ParcelToList>();
                for (int i = 0; i < listOfParcel.Count; i++)
                {
                    if (listOfParcel[i].Delivered != null) myStatuse = ParcelStatuses.delivered;
                    else if (listOfParcel[i].PickedUp != null) myStatuse = ParcelStatuses.pickedUp;
                    else if (listOfParcel[i].Scheduled != null) myStatuse = ParcelStatuses.scheduled;
                    else myStatuse = ParcelStatuses.requested;
                    listOfParcelToList.Add(new ParcelToList(listOfParcel[i].ID, listOfParcel[i].Sender.Name, listOfParcel[i].Target.Name, listOfParcel[i].Weight, listOfParcel[i].Priority, myStatuse));
                }
                return listOfParcelToList;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> getListCustomerToList()
        {
            lock (dal)
            {

                var listOfCustomer = (from customer in dal.getListCustomer()
                                      select getCustomer(customer.ID)).ToList();
                List<CustomerToList> listOfCustomerToList = new List<CustomerToList>();
                int counterp = 0, counterd = 0;
                for (int i = 0; i < listOfCustomer.Count; i++)
                {
                    for (int j = 0; j < listOfCustomer[i].ParcelToCustomer.Count(); j++)
                        if (listOfCustomer[i].ParcelToCustomer[j].Delivered != null)
                            counterd++;
                    for (int j = 0; j < listOfCustomer[i].ParcelFromCustomer.Count(); j++)
                        if (listOfCustomer[i].ParcelFromCustomer[j].Delivered != null)
                            counterp++;
                    listOfCustomerToList.Add(new CustomerToList(listOfCustomer[i].ID, listOfCustomer[i].Name, listOfCustomer[i].Phone, counterp, listOfCustomer[i].ParcelFromCustomer.Count() - counterp, counterd, listOfCustomer[i].ParcelToCustomer.Count() - counterd));
                    counterd = 0;
                    counterp = 0;
                }
                return listOfCustomerToList;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getListParcel()
        {
            lock (dal)
            {

                var listOfParcel = (from parcel in dal.getListParcel()
                                    select getParcel(parcel.ID)).ToList();
                return listOfParcel;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getListCustomer()
        {
            lock (dal)
            {

                var listOfCustomer = (from customer in dal.getListCustomer()
                                      select getCustomer(customer.ID)).ToList();
                return listOfCustomer;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> getListDrone()
        {
            lock (dal)
            {

                var listOfDrone = (from drone in dal.getListDrone()
                                   select getDrone(drone.ID)).ToList();
                return listOfDrone;
            }
        }

        private bool calculateMinBattaryRequired(Drone tempDrone, Station tempStation, WeightCategories weightCategoey = 0)
        {
            lock (dal)
            {
                double myDistance = tempDrone.DroneLocation.calculateDistance(tempStation.StationLocation);
                double battaryRequired = dal.AskToCharge()[(int)weightCategoey] * myDistance;
                if (battaryRequired <= tempDrone.Battary)
                    return true;
                return false;
            }
        }

        private bool calculateEnoughBattaryForDelievery(Drone tempDrone, Parcel tempParcel)
        {
            lock (dal)
            {

                double toParcel, fromParcelToCustomer, fromCustomerToStation, totalWay;
                toParcel = tempDrone.DroneLocation.calculateDistance(getCustomer(tempParcel.Sender.ID).CustomerLocation);
                fromParcelToCustomer = (getCustomer(tempParcel.Sender.ID).CustomerLocation).calculateDistance(getCustomer(tempParcel.Target.ID).CustomerLocation);
                List<Station> lst = (List<Station>)getListStations();
                double minDistance = tempDrone.DroneLocation.calculateDistance(lst[0].StationLocation);
                int minDistanceIndex = 0;
                for (int j = 1; j < lst.Count; j++)
                {
                    if (tempDrone.DroneLocation.calculateDistance(lst[j].StationLocation) < minDistance)
                    {
                        minDistance = tempDrone.DroneLocation.calculateDistance(lst[j].StationLocation);
                        minDistanceIndex = j;
                    }
                }
                fromCustomerToStation = (getCustomer(tempParcel.Target.ID).CustomerLocation).calculateDistance(lst[minDistanceIndex].StationLocation);
                totalWay = (toParcel * dal.AskToCharge()[0]) + (fromParcelToCustomer * dal.AskToCharge()[(int)tempParcel.Weight]) + (fromCustomerToStation * dal.AskToCharge()[0]);
                return (tempDrone.Battary >= totalWay);
            }
        }

        private bool properLocation(location l)
        {
            if (((l.longitude > 33.5) || (l.longitude < 29.3)) && ((l.latitude > 36.3) || (l.latitude < 33.7))) return false;
            return true;
        }

        public int nextParcel(Drone drone)
        {
            lock (dal)
            {
                return dal.getPartOfParcel(p => p.Scheduled == null
                                           && (WeightCategories)(p.Weight) <= drone.MaxWeigth
                                           && calculateEnoughBattaryForDelievery(drone, getParcel(p.ID)))
                                           .OrderByDescending(p => p.Priority)
                                           .ThenByDescending(p => p.Weight)
                          .FirstOrDefault().ID;
            }
        }

        public int findClosestStation(Drone myDrone)
        {
            Drone tempDrone = getDrone(myDrone.ID);
            Station tempStation = new();
            int i = 0;
            for (; i < listDrone.Count; i++)
            {
                if (listDrone[i].ID == tempDrone.ID)
                {
                    if (!(listDrone[i].StatusOfDrone == DroneStatuses.available)) throw new noAvailableDrone("the drone is not avalible for charge\n");
                    List<Station> avalibleStations = (List<Station>)getAvailableStations();
                    if (avalibleStations.Count == 0) throw new noAvailableStation("no avalible station\n");
                    List<Station> enoughBattary = new();
                    for (int j = 0; j < avalibleStations.Count; j++)
                    {
                        if (calculateMinBattaryRequired(getDrone(listDrone[i].ID), avalibleStations[j]) == true)
                            enoughBattary.Add(avalibleStations[j]);
                    }
                    if (enoughBattary == null) return -1;
                    List<Station> lst = (List<Station>)enoughBattary;
                    double minDistance = listDrone[i].DroneLocation.calculateDistance(lst[0].StationLocation);
                    int minDistanceIndex = 0;
                    for (int j = 1; j < lst.Count; j++)
                    {
                        if (listDrone[i].DroneLocation.calculateDistance(lst[j].StationLocation) < minDistance)
                        {
                            minDistance = listDrone[i].DroneLocation.calculateDistance(lst[j].StationLocation);
                            minDistanceIndex = j;
                        }
                    }
                    return lst[minDistanceIndex].ID;
                }
            }
            return -1;
        }

        public void startDroneSimulator(int ID, Action update, Func<bool> checkStop)
        {
            new DroneSimulator(new BL(), ID, update, checkStop);
        }

    }
}