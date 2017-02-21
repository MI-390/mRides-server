using System;
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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string prefferedLanguage { get; set;}
        public Boolean isSmoker { get; set; }
        public Boolean isHandicap { get; set; }
        public Boolean hasLuggage { get; set; }
        public long GSD { get; set; }

        //[InverseProperty("Driver")]
        public ICollection<Ride> Rides { get; set; }

        
        public ICollection<UserRides> UserRides { get; set; }

    }
}
