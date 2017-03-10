using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class DriverRequest
    {
        public int ID { get; set; }

        public int DriverID { get; set; }
        public User Driver { get; set; }

        public int RequestID { get; set; }
        public Request Request { get; set; }

        public string location { get; set; }
        public string destination { get; set; }
    }
}
