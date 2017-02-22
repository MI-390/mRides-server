using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mRides_server.Data;
using mRides_server.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace mRides_server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ConsoleController : Controller
    {
        private ServerContext _context;
        public ConsoleController(ServerContext context)
        {
            _context = context;
        }

      
        
        [HttpPost]
        public object Post([FromBody]User user1)
        {
            var r = _context.Users.Include(c => c.RidesAsDriver).Single(u=>u.ID==1);
 
            _context.Users.Add(user1);
            _context.SaveChanges();
            return _context.Users;
        }

        
    }
}
