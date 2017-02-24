using GeoCoordinatePortable;
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
        RequestCatalog _request;
        public MatchingSession(ServerContext context)
        {
            _context = context;
            _request = new RequestCatalog(_context);
        }
        public object findDrivers(int id, Request request)
        {
            var l = request.destination.Split(',');
            GeoCoordinate userDest = new GeoCoordinate(Double.Parse(l[0]), Double.Parse(l[1]));
            _context.Requests.Where(r =>
                new GeoCoordinate(double.Parse(r.destination.Split(',')[0]), double.Parse(r.destination.Split(',')[1])).GetDistanceTo(userDest) <= 5000
                );

        }

    }
}
