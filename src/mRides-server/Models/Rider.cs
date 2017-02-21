using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Rider:User
    {
        public int numberOfRiders { get; set; }
        public ICollection<UserRides> UserRides { get; set; }

    }
}
