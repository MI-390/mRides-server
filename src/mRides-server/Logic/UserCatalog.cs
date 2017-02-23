using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Logic
{
    public class UserCatalog
    {
        ServerContext _context;
        public UserCatalog(ServerContext context)
            {
                _context = context;
            }
        public void createUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void updateUser(User user)
        {
            //_context.Users.Find(userId);
            
        }
    }
}
