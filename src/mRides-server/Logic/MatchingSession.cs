﻿using FirebaseNet.Messaging;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Logic
{
    public class MatchingSessionResponse
    {
        public List<Request> Requests { get; set; }
        public int driverRequestID { get; set; }
    }
    public class MatchingSession
    {

       
        RequestCatalog _requestCatalog;
        UserCatalog _userCatalog;

        public MatchingSession(RequestCatalog requestCatalog, UserCatalog userCatalog)
        {
            _requestCatalog = requestCatalog;
            _userCatalog = userCatalog;
        }

        public MatchingSessionResponse findRiders(int id, Request request)
        {
            //Create new driver request
            _requestCatalog.create(request, id);
            List<string> destinationCoordinates = request.destinationCoordinates;
            //riderRequests is a list of requests of riders that are looking for a driver
            var riderRequests = _requestCatalog.getNullDriver();
            List<Request> filteredRequests = new List<Request>();

            foreach (Request riderRequest in riderRequests)
            {
                string riderRequestDestination = riderRequest.RiderRequests.FirstOrDefault().destination;
                string[] rD = riderRequestDestination.Split(',');
                GeoCoordinate geoRiderRequestDestination = new GeoCoordinate(Double.Parse(rD[0]), Double.Parse(rD[1]));

                string riderRequestLocation = riderRequest.RiderRequests.FirstOrDefault().location;
                string[] rL = riderRequestLocation.Split(',');
                GeoCoordinate geoRiderRequestLocation = new GeoCoordinate(Double.Parse(rL[0]), Double.Parse(rL[1]));

                foreach (var destinationCoordinate in destinationCoordinates)
                {
                    var c = destinationCoordinate.Split(',');
                    GeoCoordinate geoDestinationCoordinate = new GeoCoordinate(Double.Parse(c[0]), Double.Parse(c[1]));
                    if (geoRiderRequestDestination.GetDistanceTo(geoDestinationCoordinate) <= 1000)
                    {
                        foreach (var destinationCoordinate2 in destinationCoordinates)
                        {
                            var c2 = destinationCoordinate2.Split(',');
                            GeoCoordinate geoDestinationCoordinate2 = new GeoCoordinate(Double.Parse(c2[0]), Double.Parse(c2[1]));
                            if (geoRiderRequestLocation.GetDistanceTo(geoDestinationCoordinate2) <= 1000)
                            {
                                filteredRequests.Add(riderRequest);
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            MatchingSessionResponse response = new MatchingSessionResponse
            {
                Requests = filteredRequests,
                driverRequestID = request.ID
            };

            return response;
        }

        public object findDrivers(int id, Request request, List<string> destinationCoordinates)
        {
            //    //Create new rider request
            //    _requestCatalog.create(request, id);

            //    //driverRequests is a list of requests of drivers that are looking for a rider
            //    var driverRequests = _context.Requests
            //            .Include(r => r.DriverRequests)
            //                .ThenInclude(rr => rr.Driver)
            //                .Where(r => r.Rider == null);

            //    List<Request> filteredRequests = new List<Request>();

            //    foreach (Request driverRequest in driverRequests)
            //    {
            //        string driverRequestDestination = driverRequest.DriverRequests.FirstOrDefault().destination;
            //        string[] dD = driverRequestDestination.Split(',');
            //        GeoCoordinate geoDriverRequestDestination = new GeoCoordinate(Double.Parse(dD[0]), Double.Parse(dD[1]));

            //        string driverRequestLocation = driverRequest.DriverRequests.FirstOrDefault().location;
            //        string[] dL = driverRequestLocation.Split(',');
            //        GeoCoordinate geoDriverRequestLocation = new GeoCoordinate(Double.Parse(dL[0]), Double.Parse(dL[1]));

            //        foreach (var destinationCoordinate in destinationCoordinates)
            //        {
            //            var c = destinationCoordinate.Split(',');
            //            GeoCoordinate geoDestinationCoordinate = new GeoCoordinate(Double.Parse(c[0]), Double.Parse(c[1]));
            //            if (geoDriverRequestDestination.GetDistanceTo(geoDestinationCoordinate) <= 1000)
            //            {
            //                foreach (var destinationCoordinate2 in destinationCoordinates)
            //                {
            //                    var c2 = destinationCoordinate2.Split(',');
            //                    GeoCoordinate geoDestinationCoordinate2 = new GeoCoordinate(Double.Parse(c2[0]), Double.Parse(c2[1]));
            //                    if (geoDriverRequestLocation.GetDistanceTo(geoDestinationCoordinate2) <= 1000)
            //                    {
            //                        filteredRequests.Add(driverRequest);
            //                        break;
            //                    }
            //                }
            //                break;
            //            }
            //        }
            //    }

            //    var response = new
            //    {
            //        Requests = filteredRequests,
            //        driverRequestID = request.ID
            //    };

            //    return response;
            return null;
        }

        public Request confirm(int id, int driverReqId, int riderReqId)
        {
            if (id == driverReqId)
            {
                var request=_requestCatalog.mergeRiderRequestToRequest(driverReqId, riderReqId);
                sendPushNotification(riderReqId, "You have been matched");
                return request;
            }
            return null;
            
            

        }
        public List<Request> filterByPreferences(IQueryable<Request> requests,int id)
        {
            var user = _userCatalog.get(id);
            return requests.Where(r =>
                r.RiderRequests.First().Rider.genderPreference == user.genderPreference &&
                r.RiderRequests.First().Rider.hasLuggage ==user.hasLuggage &&
                r.RiderRequests.First().Rider.isHandicap ==user.isHandicap &&
                r.RiderRequests.First().Rider.hasPet ==user.hasPet &&
                r.RiderRequests.First().Rider.isSmoker ==user.isSmoker
                ).ToList();
        }
        public void sendPushNotification(int userId, string message)
        {
            var fcmToken = _userCatalog.getFcmToken(userId);
            Message message1 = new Message
            {
                To = fcmToken,
                Notification = new AndroidNotification { Body = message }

            };
            FCMClient client1 = new FCMClient("AAAAWdiPzA0:APA91bHuL6OOYCKjZVByO-W1e9w0fX15k92Xx1vaxnOelk7K8al6wIIIpVIuUTfp5TUqzI4ordc1NSZ0A8k1l5RSMGDndYDubo1gtssKmvGGFtLocOEI6rfo1_k2bguJwmhvZd9ko0lj");
            client1.SendMessageAsync(message1);
        }
    }

}
