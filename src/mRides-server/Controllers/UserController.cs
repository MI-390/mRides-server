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
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ServerContext _context;
        public UserController(ServerContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet]
        public object Get()
        {
            return _context.Users;
        }

        // GET api/values/5
        [Route("getUserById={id}")]
        public object Get(int id)
        {
            return _context.Users.Find(id);
        }
    
        // POST api/values
        [Route("addPost")]
        [HttpPost]
        public object Post([FromBody]User user1)
        {
            var r = _context.Users.Include(c => c.Rides);
            _context.Users.Add(user1);
            _context.SaveChanges();
            return _context.Users;
        }

        // PUT api/values/5
        [Route("add")]
        public string addUser([FromQuery]string firstName, [FromQuery]string lastName)
        {
            _context.Users.Add(new User { FirstName = firstName, LastName = lastName });
            _context.SaveChanges();

            return "Added";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
