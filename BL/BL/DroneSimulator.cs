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
            var myDrone = bl.getDrone(ID);
            double timeInC = 0;
            int parcelID = 0;
            int stationID = 0;
            bool flag = true;

            do
            {

                switch (myDrone.StatusOfDrone)
                {
                    case BO.Enum.DroneStatuses.available:
                        lock (bl)
                        {
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
                    case BO.Enum.DroneStatuses.maintenance:
                        lock (bl)
                        {
                            //  Thread.Sleep(1000);
                            bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                            switch (timeInC)
                            {
                                case (2):  //הרחפן טעון- שחרור
                                    bl.releaseFromCharge(bl.getDrone(myDrone.ID), 2);
                                    timeInC = 0;
                                    break;
                                case (_):  //הרחפן לא הגיע ל100%
                                    timeInC += 0.5;
                                    break;
                            }
                        }
                        break;
                    case BO.Enum.DroneStatuses.delivery:
                        lock (bl)
                        {
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
                Thread.Sleep(1000);
                myDrone = bl.getDrone(myDrone.ID);
                Thread.Sleep(1000);

            } while (!checkStop() && flag);
        }
    }
}
