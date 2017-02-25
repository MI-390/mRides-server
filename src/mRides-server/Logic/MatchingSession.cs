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
        public object findRiders(int id, Request request)
        {
            _requestCatalog.createNewRequest(request, id);
            var l = request.destination.Split(',');
            GeoCoordinate userDest = new GeoCoordinate(Double.Parse(l[0]), Double.Parse(l[1]));

            var destRequests = _context.Requests
                    .Include(r => r.RiderRequests)
                        .ThenInclude(rr => rr.Rider)
                        .Where(r => r.Driver == null);
            List<Request> requestsList = new List<Request>();
            foreach(var r in destRequests)
            {
                var compareDest = r.RiderRequests.FirstOrDefault().destination?.Split(',');
                if (compareDest != null)
                {
                    var compareDestCoord = new GeoCoordinate(Double.Parse(compareDest[0]), Double.Parse(compareDest[1]));
                    if (compareDestCoord.GetDistanceTo(userDest) <= 5000)
                    {
                        requestsList.Add(r);
                    }
                }
            }


                    //.Where(r =>
                    //new GeoCoordinate(double.Parse(r.RiderRequests.FirstOrDefault().destination.Split(',')[0]), double.Parse(r.RiderRequests.FirstOrDefault().destination.Split(',')[1])).GetDistanceTo(userDest) <= 5000
                    //);;
            //NASSIM'S MAP ALGORITHM HERE
            var response = new
            {
                Requests = requestsList,
                driverRequestID = request.ID
            };
            
            return response;
            
        }

        public void confirm(int id, int driverReqId, int riderReqId)
        {
            //EVENTUALLY WE WILL GET CONFIMRATION FROM BOTH PARTIES
            _requestCatalog.mergeRiderRequestToRequest(driverReqId, riderReqId);

        }


    }
}
