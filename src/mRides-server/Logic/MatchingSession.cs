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
            //_requestCatalog.createNewRequest(request, id);
            var l = request.destination.Split(',');
            GeoCoordinate userDest = new GeoCoordinate(Double.Parse(l[0]), Double.Parse(l[1]));

            var destRequests = _context.Requests
                    .Include(r => r.RiderRequests)
                        .ThenInclude(rr => rr.Rider)
                        .Where(r=> new GeoCoordinate(double.Parse(r.RiderRequests.First().destination.Split(',')[0]), double.Parse(r.RiderRequests.First().destination.Split(',')[1])).GetDistanceTo(userDest) <= 5000);


                    //.Where(r =>
                    //new GeoCoordinate(double.Parse(r.RiderRequests.FirstOrDefault().destination.Split(',')[0]), double.Parse(r.RiderRequests.FirstOrDefault().destination.Split(',')[1])).GetDistanceTo(userDest) <= 5000
                    //);;
            //NASSIM'S MAP ALGORITHM HERE
            var response = new
            {
                Requests = destRequests,
                driverRequestID = request.ID
            };
            return response;
            
        }


    }
}
