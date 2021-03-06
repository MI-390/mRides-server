﻿using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{

    public class UserCatalog : ICatalog<User>
    {
        ServerContext _context;
        public UserCatalog()
        {

        }
        public UserCatalog(ServerContext context)
        {
            _context = context;
        }

        public User create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public virtual User get(int id)
        {
            return _context.Users.Find(id);
        }
        public object getUserByFacebookId(long facebookId)
        {
            return _context.Users.FirstOrDefault(u=>u.facebookID==facebookId);
        }
        public Boolean updateFcmToken(int id,string fcmToken)
        {
            //Delete if a fcmToken Already exists
            var existingFcmTokenUsers = _context.Users.Where(u => u.fcmToken == fcmToken);
            foreach(User u in existingFcmTokenUsers)
            {
                u.fcmToken = null;
            }

            //Get the user with id
            var user=_context.Users.FirstOrDefault(u=>u.ID==id);
            if (user != null)
            {
                user.fcmToken = fcmToken;
            }
            _context.SaveChanges();
            if(user!=null && user.fcmToken==fcmToken)
                return true;
            return false;
        }
        public bool updateUserSettings(int userId, User user)
        {
            // Null check
            if(user == null)
            {
                return false;
            }
            // Ensure that the user is only able to update its own user settings
            if (userId != user.ID)
            {
                return false;
            }
            // Update the settings
            User dbUser = _context.Users.Find(user.ID);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.isSmoker = user.isSmoker;
            dbUser.isHandicap = user.isHandicap;
            dbUser.hasLuggage = user.hasLuggage;
            dbUser.hasPet = user.hasPet;
            dbUser.genderPreference = user.genderPreference;
            dbUser.gender = user.gender;
            _context.SaveChanges();
            return true;
        }
        public List<Feedback> getReviews(int id)
        {
            List<Feedback> feedbacks = new List<Feedback>();
            User user = _context.Users
                        .Include(u => u.RidesAsRider)
                            .ThenInclude(Ur => Ur.Ride)
                            .ThenInclude(r => r.Driver)
                        .Include(u => u.RidesAsDriver)
                            .ThenInclude(r => r.UserRides)
                        .First(u => id == u.ID);
            //Adding feedback as riders(given by a driver)
            ICollection<mRides_server.Models.UserRides> ridesRider = user.RidesAsRider;
            foreach (UserRides u in ridesRider)
            {
                if (u.driverFeedback != null)
                {
                    var feedback = new Feedback
                    {
                        feedbackText = u.driverFeedback,
                        givenAs = "rider",
                        givenBy = u.Ride?.Driver,
                        Ride = u.RideId,
                        stars = u.driverStars,
                        time = (u.Ride?.dateTime == null) ? new DateTime() : u.Ride.dateTime
                    };
                    feedbacks.Add(feedback);
                }

            }

            ICollection<mRides_server.Models.Ride> ridesAsDriver = user.RidesAsDriver;
            foreach (Ride r in ridesAsDriver)
            {
                foreach (UserRides u in r.UserRides)
                {
                    if (u.riderFeedback != null)
                    {
                        var feedback = new Feedback
                        {
                            feedbackText = u.riderFeedback,
                            givenAs = "driver",
                            givenBy = _context.Users.FirstOrDefault(us => us.ID == u.RiderId),
                            Ride = u.RideId,
                            stars = u.riderStars,
                            time = (u.Ride?.dateTime == null) ? new DateTime() : u.Ride.dateTime
                        };
                        feedbacks.Add(feedback);
                    }
                }
            }
            return feedbacks;
        }

        public void leaveReview(int rideid, int reviewerId, int revieweeId, string review, int stars)
        {
            Ride ride = _context.Rides
                        .Include(r => r.UserRides)
                        .First(r => r.ID == rideid);
            //If driver is leaving a review for a rider
            if (reviewerId == ride.DriverID)
            {
                UserRides Ur = ride.UserRides.First(r => r.RiderId == revieweeId);
                Ur.driverFeedback = review;
                Ur.driverStars = stars;
                // _context.Rides.Add(ride);
            }
            else
            {
                UserRides Ur = ride.UserRides.First(r => r.RiderId == reviewerId);
                Ur.riderFeedback = review;
                Ur.riderStars = stars;
                //_context.Rides.Add(ride);
            }
            _context.SaveChanges();
            //ICollection<mRides_server.Models.UserRides> ur = ride.UserRides.Where(r=>r.RideId==);
        }

        public User create(User obj, int userId)
        {
            throw new NotImplementedException();
        }

        public void setGSD(int userId, long amountGSD)
        {
            User u = get(userId);
            u.GSD = amountGSD;
            _context.SaveChanges();
        }

        public long getGSD(int userId)
        {
            User u = get(userId);
            return u.GSD;
        }

        public string getGender(int userId)
        {
            User u = get(userId);
            return u.gender;
        }

        public void setGender(int userId, string userGender)
        {
            User u = get(userId);
            u.gender = userGender;
            _context.SaveChanges();
        }

        public string getFcmToken(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.ID == userId).fcmToken;
          
         }
        public ICollection<Request> getRequests(int userId)
        {
            User user=_context.Users
                .Include(u => u.RequestAsRider)
                    .ThenInclude(rr => rr.Request)
                .Include(u => u.RequestsAsDriver)
                .First(u => u.ID == userId);
            ICollection<Request> requests = new List<Request>();
            requests = user.RequestsAsDriver;
            if (user.RequestAsRider.Count() != 0)
            {
                foreach(var rr in user.RequestAsRider)
                {
                    requests.Add(rr.Request);
                }
            }
            return requests;
           


            }
        
    }
}
