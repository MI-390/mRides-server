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
    public class MatchingSession
    {

        ServerContext _context;
        RequestCatalog _requestCatalog;

        public MatchingSession(ServerContext context)
        {
            _context = context;
            _requestCatalog = new RequestCatalog(_context);
        }

        public object findRiders(int id, Request request, List<string> destinationCoordinates)
        {
            //Create new driver request
            _requestCatalog.create(request, id);

            //riderRequests is a list of requests of riders that are looking for a driver
            var riderRequests = _context.Requests
                    .Include(r => r.RiderRequests)
                        .ThenInclude(rr => rr.Rider)
                        .Where(r => r.Driver == null);
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

            var response = new
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
            //EVENTUALLY WE WILL GET CONFIMRATION FROM BOTH PARTIES
            return _requestCatalog.mergeRiderRequestToRequest(driverReqId, riderReqId);
        }

    }

}
