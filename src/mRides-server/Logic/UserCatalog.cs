using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{
    public class UserCatalog
    {
        ServerContext _context;
        public UserCatalog(ServerContext context)
        {
                _context = context;
        }
        public void createUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void updateUser(User user)
        {
            //_context.Users.Find(userId);
            
        }
        public object getReviews(int id)
        {
            List<object> feedbacks = new List<object>();
            User user = _context.Users
                        .Include(u => u.RidesAsRider)
                            .ThenInclude(Ur => Ur.Ride)
                            .ThenInclude(r => r.Driver)
                        .Include(u => u.RidesAsDriver)
                            .ThenInclude(r=>r.UserRides)
                        .First(u => id == u.ID);
            //Adding feedback as riders(given by a driver)
            ICollection<mRides_server.Models.UserRides> ridesRider = user.RidesAsRider;
            foreach (UserRides u in ridesRider)
            {
                var feedback = new
                {
                    feedback = u.driverFeedback,
                    givenAs = "rider",
                    givenBy = u.Ride.Driver,
                    Ride = u.RideId
                };
                feedbacks.Add(feedback);
            }

            ICollection<mRides_server.Models.Ride> ridesAsDriver = user.RidesAsDriver;
            foreach(Ride r in ridesAsDriver)
            {
                foreach (UserRides u in r.UserRides)
                {
                    var feedback = new
                    {
                        feedback = u.riderFeedback,
                        givenAs = "driver",
                        givenBy = _context.Users.Find(u.RiderId),
                        Ride = u.RideId
                    };
                    feedbacks.Add(feedback);
                }
                
            }
            return feedbacks;

        }

        public void leaveReview(int rideid,int reviewerId,int revieweeId, string review)
        {
            Ride ride=_context.Rides
                        .Include(r => r.UserRides)
                        .First(r => r.ID == rideid);
            //If driver is leaving a review for a rider
            if(ride.DriverID==reviewerId)
            {

                UserRides Ur = ride.UserRides.First(r => r.RideId == revieweeId);
                Ur.driverFeedback = review;
            }
            ICollection<mRides_server.Models.UserRides> ur = ride.UserRides.Where(r=>r.RideId==);
            
        }
    }
}
