using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Ride
    {
        public int ID { get; set; }
        public string destination { get; set; }
        public string location { get; set; }
        public DateTime dateTime { get; set; }
        public Boolean isWeekly { get; set; }


        //1 Driver per Ride
        public int DriverID { get; set; }
        public User Driver { get; set; }
        
        //Many Riders through an association table
        public ICollection<UserRides> UserRides { get; set; }
    }
}
