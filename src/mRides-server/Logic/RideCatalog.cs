using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{
    public class RideCatalog
    {
        List<mRides_server.Models.Ride> Rides;
        private ServerContext _context;

        public RideCatalog(ServerContext context)
        {
            _context = context;
        }
        public List<mRides_server.Models.Ride> getRides()
        {
            Rides= _context.Rides.ToList();
            return Rides;
            
        }
        
    }
}
