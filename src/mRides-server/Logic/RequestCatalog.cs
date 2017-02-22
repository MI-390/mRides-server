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
                .Where(s => s.type== "rider")
                .ToList();
        }
        public List<mRides_server.Models.Request> getRequestsForRiders()
        {
            return _context.Requests
                 .Where(s => s.type == "driver")
                 .ToList();
        }
        public void createNewRequest(Request request, User user)
        {
            
        }


    }
}
