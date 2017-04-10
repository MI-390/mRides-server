using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{
    public class RideCatalog : ICatalog<Ride>
    {
        List<mRides_server.Models.Ride> Rides;
        private ServerContext _context;

        public RideCatalog(ServerContext context)
        {
            _context = context;
        }

        public List<mRides_server.Models.Ride> getRides()
        {
            Rides = _context.Rides.ToList();
            return Rides;

        }

        /// <summary>
        /// Method used to retrieve a ride given a ride ID
        /// </summary>
        public Ride getRide(int rideId)
        {
            var ride = _context.Rides
                .Include(r => r.UserRides)
                .First(r => r.ID == rideId);
            return ride;
        }

        /// <summary>
        /// Method used to create a ride given a ride object and a user's ID
        /// </summary>
        public Ride create(Ride ride)
        {

            _context.Rides.Add(ride);
            _context.SaveChanges();
            return ride;
        }

        /// <summary>
        /// Method used to add a given rider to a ride
        /// </summary>
        public Ride addRiderToRide(int rideId, int userid)
        {
            Ride ride = _context.Rides
                 .Include(r => r.UserRides)
                     .First(r => r.ID == rideId);
            UserRides ur = new UserRides
            {
                Rider = _context.Users.Find(userid),
                Ride = ride
            };
            ride.UserRides.Add(ur);
            _context.SaveChanges();
            return ride;
        }

        /// <summary>
        /// Method used to add a driver to a ride
        /// </summary>
        public void addDriverToRide(int rideId, int userid)
        {
            Ride ride = _context.Rides
                 .Include(r => r.UserRides)
                     .First(r => r.ID == rideId);
            UserRides ur = new UserRides
            {
                Rider = _context.Users.Find(userid),
                Ride = ride
            };
            ride.UserRides.Add(ur);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method used to update the distanceTravelled attribute of a given ride
        /// </summary>
        public void setDistanceTravelled(int rideId, double distanceMetric)
        {
            Ride r = getRide(rideId);
            r.distanceTravelled = distanceMetric;
            _context.SaveChanges();
        }

        /// <summary>
        /// Method used to update the duration attribute of a given ride
        /// </summary>
        public void setDuration(int rideId, long durationMetric)
        {
            Ride r = getRide(rideId);
            r.duration = durationMetric;
            _context.SaveChanges();
        }

        public Ride create(Ride obj, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
