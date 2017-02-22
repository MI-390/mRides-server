using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{
    public class RequestCatalog
    {
        List<mRides_server.Models.Request> Requests;
        private ServerContext _context;

        public RequestCatalog(ServerContext context)
        {
            _context = context;
        }
        public List<mRides_server.Models.Request> getRequests()
        {
            Requests= _context.Requests.ToList();
            return Requests;
        }
        public List<mRides_server.Models.Request> getRequestsForDrivers()
        {
           return  _context.Requests
                .Include(s=>s.Driver)
                .Where(s => s.Driver== null)
                .ToList();
        }
        public List<mRides_server.Models.Request> getRequestsForRiders()
        {
            return null;
           // return _context.Requests
                 //.Where(s => s.type == "driver")
                 //.ToList();
        }
        public void createNewRequest(Request request, int userId,string type)
        {
            if (type == "driver")
            {
                request.Driver = _context.Users.Find(userId);
            }
            else
            {
                User rider = _context.Users.Find(userId);
                RiderRequest r = new RiderRequest
                {
                    Request = request,
                    Rider = rider
                };
                request.RiderRequests.Add(r);
            }
        
            _context.Requests.Add(request);
            _context.SaveChanges();
        }


    }
}
