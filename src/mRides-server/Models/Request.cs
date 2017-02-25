using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string destination { get; set; }
        public string location { get; set; }
        public DateTime dateTime { get; set; }
        public Boolean isWeekly { get; set; }
        [NotMapped]
        public string type;
        //public string type;

        //1 Driver per Ride
        public int? DriverID { get; set; }
        public User Driver { get; set; }
        
        //Many Riders through an association table
        public ICollection<RiderRequest> RiderRequests { get; set; }
    }
}
