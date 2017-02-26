using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class UserRides
    {
        public int ID { get; set; }

        public int RiderId { get; set; }
        public User Rider { get; set; }

        public int RideId{ get; set; }
        public Ride Ride { get; set; }

        public string location { get; set; }
        public string driverFeedback { get; set; }
        public string riderFeedback { get; set; }
        public int stars { get; set; }


    }
}
