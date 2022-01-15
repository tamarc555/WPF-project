﻿using System;
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
            //var myDrone = bl.getPartOfDrone(x => x.ID == ID).FirstOrDefault();
            var myDrone = bl.getDrone(ID);
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
                
                switch (myDrone.StatusOfDrone)
                {
                    case  BO.Enum.DroneStatuses.available:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
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
                                    {
                                        bl.sendToCharge(bl.getDrone(myDrone.ID));
                                    }
                                    break;
                                case (_, _): //שויכה חבילה - שליחה
                                    bl.updateScheduled(bl.getDrone(myDrone.ID));
                                    break;
                            }
                        }
                        break;
                    case BO.Enum.DroneStatuses.scheduled:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            bl.updatePickedUp(bl.getDrone(myDrone.ID));
                            break;
                        }
                    case BO.Enum.DroneStatuses.maintenance :
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                            //switch (myDrone.Battary)
                            //{
                            //    case (100):  //הרחפן טעון- שחרור
                            //        bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                            //        break;
                            //    case (_):  //הרחפן לא הגיע ל100%
                            //        break;
                            //}
                        }
                        break;
                    case BO.Enum.DroneStatuses.delivery:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            switch (myDrone.TheParcelInDelivery.StatusOfParcel)
                            {
                                case (false):  //החבילה עוד לא נאספה
                                    bl.updatePickedUp(bl.getDrone(myDrone.ID));
                                    break;
                                case (true):  //החבילה נאספה והיא בדרך
                                    bl.updateSupply(bl.getDrone(myDrone.ID));
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("ERROR");
                }
               Thread.Sleep(1000);
                update();
                myDrone = bl.getDrone(myDrone.ID);

            } while (!checkStop()&&flag);
        }



    }
}
