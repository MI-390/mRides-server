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
        public void createNewRide(Ride ride, int userId)
        {

            if (ride.type == "driver")
            {
                ride.Driver = _context.Users.Find(userId);
                _context.Rides.Add(ride);
            }
            else
            {
                User rider = _context.Users
                    .Include(s => s.RidesAsRider)
                    .First(s => s.ID == userId);


                UserRides r = new UserRides
                {
                    Ride=ride,
                    Rider = rider
                    
                };
                rider.RidesAsRider.Add(r);
            }
            // 
            _context.SaveChanges();
        }
         public void addRiderToRide(int rideId,int userid)
        {
           Ride ride= _context.Rides
                .Include(r=>r.UserRides)
                    .First(r => r.ID == rideId);
            UserRides ur = new UserRides
            {
                Rider = _context.Users.Find(userid),
                Ride = ride
            };
           ride.UserRides.Add(ur);
            _context.SaveChanges();
        }

    }
}
