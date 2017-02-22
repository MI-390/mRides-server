using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Request
    {
        public int ID { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string UserType { get; set; }
        public int numberOfSeats { get; set; }



    }
}
