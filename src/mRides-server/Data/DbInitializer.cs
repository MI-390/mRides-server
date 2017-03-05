using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Data
{
    public class DbInitializer
    {
        public static void Initialize(ServerContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
