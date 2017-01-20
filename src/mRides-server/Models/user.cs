using System;
using System.Collections.Generic;
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
    }
}
