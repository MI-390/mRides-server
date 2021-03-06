﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class User
    {
        //The ID property will automatically become the primary key column of the database table that corresponds to this class
        public int ID { get; set; }
        public long facebookID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string prefferedLanguage { get; set;}
        public Boolean isSmoker { get; set; }
        public Boolean isHandicap { get; set; }
        public Boolean hasLuggage { get; set; }
        public long GSD { get; set; }
        public Boolean hasPet { get; set; }
        public string gender { get; set; }
        public string genderPreference { get; set; }
        public string fcmToken { get; set; }
        //Rides as Driver
        public ICollection<Ride> RidesAsDriver { get; set; }

        //Rides as Rider
        public ICollection<UserRides> RidesAsRider { get; set; }

        //Requests as Driver
        public ICollection<Request> RequestsAsDriver { get; set; }

        //Requests as Rider
        public ICollection<RiderRequest> RequestAsRider { get; set; }

    }
}
