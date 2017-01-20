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

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{FirstName="Carson",LastName="Alexander"},
            new User{FirstName="2",LastName="4454"},

            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

        }
    
    }
}
