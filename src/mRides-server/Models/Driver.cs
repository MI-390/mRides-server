using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Driver:User
    {
        public int seatsAvailable { get; set; }
        //[InverseProperty("Driver")]
        public ICollection<Ride> Rides { get; set; }

    }
}
