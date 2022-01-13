using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using BlApi.BO;

namespace BL
{
    internal class DroneSimulator
    {
        public DroneSimulator(BL blImp, int ID, Action update, Func<bool> checkStop)
        {
            var bl = blImp;
            var dal = bl.dal;
            var myDrone = bl.getPartOfDrone(x => x.ID == ID).FirstOrDefault();
            int parcelID = 0;
            int stationID = 0;
            bool flag = true;
            //Station bs = null;
            //double myDistance = 0.0;
            //int battaruUsage = 0;
            //Parcel? myParcel = null;
            //bool pickedUp = false;
            //Customer myCustomer = null;
            //Maintenance myMaintenance = Maintenance.Starting;
            
            //void initDelievry(int ID)
            //{
            //    myParcel = bl.getParcel(ID);
            //    myCustomer = bl.getCustomer((int)(pickedUp ? myParcel.Target.ID : myParcel.Sender.ID));
            //}
            do
            {
                
                switch (myDrone)
                {
                    case DroneToList { StatusOfDrone: BO.Enum.DroneStatuses.available }:
                        Thread.Sleep(1000);
                        parcelID = bl.nextParcel(myDrone);
                        switch (parcelID, myDrone.Battary)
                        {
                            case (0, 100):  //שוחרר מטעינה אבל אין חבילה
                                //סיום תהליכון כפוי
                                flag = false;
                                break;
                            case (0, _):  //פנוי ואין חבילה - שליחה לטעינה
                                stationID = bl.findClosestStation(myDrone);
                                if (stationID != -1)
                                    bl.sendToCharge(bl.getDrone(myDrone.ID));
                                break;
                            case (_, _): //שויכה חבילה - שליחה
                                bl.updateScheduled(bl.getDrone(myDrone.ID));
                                break;
                        }
                        break;
                    case DroneToList { StatusOfDrone: BO.Enum.DroneStatuses.scheduled }:
                        Thread.Sleep(1000);
                        bl.updatePickedUp(bl.getDrone(myDrone.ID));
                        break;
                    case DroneToList { StatusOfDrone: BO.Enum.DroneStatuses.maintenance }:
                        Thread.Sleep(1000);
                        bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                        //switch (myDrone.Battary)
                        //{
                        //    case (100):  //הרחפן טעון- שחרור
                        //        bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                        //        break;
                        //    case (_):  //הרחפן לא הגיע ל100%
                        //        break;
                        //}
                        break;
                    case DroneToList { StatusOfDrone: BO.Enum.DroneStatuses.delivery }:
                        Thread.Sleep(1000);
                        switch (myDrone.ParcelInDelivery.StatusOfParcel)
                        {
                            case (false):  //החבילה עוד לא נאספה
                                bl.updatePickedUp(bl.getDrone(myDrone.ID));
                                break;
                            case (true):  //החבילה נאספה והיא בדרך
                                bl.updateSupply(bl.getDrone(myDrone.ID));
                                break;
                        }
                        break;
                    default:
                        throw new Exception("ERROR");
                }
                update();
                Thread.Sleep(1000);
            } while (!checkStop()&&flag);
        }


        void startDroneSimulator(int ID, Action update, Func<bool> checkStop)
        {

            //void startDroneSimulator(int myDroneID, Action update, Func<bool> checkStop)
            //{
            //    var myDrone = getPartOfDrone(x => x.ID == myDroneID).FirstOrDefault();
            //    int parcelID = 0;
            //    int stationID = 0;
            //    Station bs = null;
            //    double myDistance = 0.0;
            //    int battaruUsage = 0;
            //    Parcel? myParcel = null;
            //    bool pickedUp = false;
            //    Customer myCustomer = null;
            //    //Maintenance myMaintenance = Maintenance.Starting;

            //    if (myDrone.Battary < 10)


            //       
            //    //myDrone
            //}
        }

    }
}
