using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi.BO;

namespace BlApi
{ 
    public interface IBL
    {
        //---------------STATION----------------------
        public void addStation(Station tempStation);
        public Station getStation(int myID);
        public IEnumerable<Station> getListStations();
        //public IEnumerable<Station> getPartOfStation(Predicate<Station> stationCondition);
        public IEnumerable<StationToList> getListStationToList();
        public void updateStation(Station tempStation);
        public void deleteStation(int tempStationID);
        public StationToList getStationToList(int myID);

        public IEnumerable<Station> getAvailableStations();




        //----------------DRONE------------------------

        public void addDrone(Drone tempDrone);
        public Drone getDrone(int myID);
        public IEnumerable<Drone> getListDrone();
        // public IEnumerable<DroneToList> getPartOfDrone(Predicate<DroneToList> DroneCondition1, Predicate<DroneToList> DroneCondition2 = null);
        public IEnumerable<DroneToList> getPartOfDrone(Predicate<DroneToList> DroneCondition, IEnumerable<DroneToList> myList = null);
        public IEnumerable<DroneToList> getListDroneToList();

        public void updateScheduled(Drone tempDrone);
        public void updatePickedUp(Drone tempDrone);
        public void updateSupply(Drone tempDrone);

        //public IEnumerable<Drone> getPartOfListDrone(Predicate<Drone> droneCondition);
        public void updateDrone(Drone tempDrone);
        public void sendToCharge(Drone tempDrone);
        public void releaseFromCharge(Drone droneToRelease, double timeInCharge);
        public void deleteDrone(int tempDroneID);



        //---------------PARCEL----------------------------
        public void addParcel(Parcel tempParcel);
        public Parcel getParcel(int myID);
        public IEnumerable<Parcel> getListParcel();
        public IEnumerable<Parcel> getNotConnectedParcel();
        public IEnumerable<ParcelToList> getListParcelToList();

        //public IEnumerable<Parcel> getPartOfParcel(Predicate<Parcel> parcelCondition);
        public void deleteParcel(int tempParcelID);



        //---------------CUSTOMER--------------------------
        public void addCustomer(Customer tempCustomer);
        public Customer getCustomer(int myID);
        public CustomerToList getCustomerToList(int myID);
        public IEnumerable<Customer> getListCustomer();
        public IEnumerable<CustomerToList> getListCustomerToList();

        //public IEnumerable<Customer> getPartOfCustomer(Predicate<Customer> customerCondition);
        public void updateCustomer(Customer tempCustomer);
        public void deleteCustomer(int tempCustomerID);
        public Customer getCustomerByName(string myCustomerName);

       public void startDroneSimulator(int ID, Action update, Func<bool> checkStop);
    }
}