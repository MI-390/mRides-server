using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Models
{
    public class Feedback
    {
        public string feedbackText { get; set; }
        public string givenAs { get; set; }
        public User givenBy { get; set; }
        public int Ride { get; set; }
        public int stars { get; set; }
        public DateTime time { get; set; }
    }
}
