using DalApi.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {

        public double[] AskToCharge();

        public void initialize() { }



        //-----------------DRONE------------------------
        public void addDrone(Drone droneToAdd);
        public void deleteDrone(int tempDroneID);
        public void updateDrone(Drone tempDrone);
        public Drone getDrone(int myID);
        public IEnumerable<Drone> getListDrone();



        //-----------------STATION------------------------
        public void addStation(Station stationToAdd);
        public void deleteStation(int tempStationID);
        public void updateStation(Station tempStation);
        public Station getStation(int myID);
        public IEnumerable<Station> getListStations();
        public IEnumerable<Station> getPartOfStation(Predicate<Station> stationCondition);


        //-----------------CUSTOMER-----------------------
        public void addCustomer(Customer customerToAdd);
        public void deleteCustomer(int tempCustomerID);
        public void updateCustomer(Customer tempCustomer);
        public Customer getCustomer(int myID);
        public IEnumerable<Customer> getListCustomer();
        public IEnumerable<Customer> getPartOfCustomer(Predicate<Customer> customerCondition);



        //-----------------PARCEL------------------------
        public void addParcel(Parcel parcelToAdd);
        public void deleteParcel(int tempParcelID);
        public void updateScheduled(Parcel parcelToUpdate, int theDroneID);
        public void updatePickedUp(Parcel tempParcel);
        public void updateSupply(Parcel tempParcel);
        public void sendToCharge(Drone tempDrone, Station tempStation);
        public void releaseFromCharge(Drone droneToRelease, Station tempStation);
        public Parcel getParcel(int myID);
        public IEnumerable<Parcel> getListParcel();
        public IEnumerable<Parcel> getPartOfParcel(Predicate<Parcel> parcelCondition);


        //----------------DRONECHARGE--------------------------
        public IEnumerable<DroneCharge> getPartOfDroneCharge(Predicate<DroneCharge> droneChargeCondition);

    }
}
