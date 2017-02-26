using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mRides_server.Data;
using mRides_server.Models;
using Microsoft.EntityFrameworkCore;
using mRides_server.Logic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace mRides_server.Controllers
{
    [Route("api/[controller]/[action]")]

    public class UserController : Controller
    {
        private ServerContext _context;
        private UserCatalog _userCatalog;
        public UserController(ServerContext context)
        {
            _context = context;
            _userCatalog = new UserCatalog(context);
        }


        /// <summary>
        /// This class is used to find a user by user id
        /// </summary>
        [HttpPost]
        public object getUserByFacebookId([FromHeader]string id)
        {
            long fbId = long.Parse(id);
            return _context.Users.FirstOrDefault(u=>u.facebookID==fbId);
        }
        [HttpGet("{id}")]
        public object getUser(int id)
        {
            return _context.Users.Find(id);
        }

        /// <summary>
        /// This class is used as an api for the search requests.
        /// </summary>
        // POST api/values
        [HttpPost]
        public object createUser([FromBody]User user1)
        {
            _userCatalog.createUser(user1);
            //var r = _context.Users.Include(c => c.RidesAsDriver).Single(u=>u.ID==1);
            return user1; 
        }

        /// <summary>
        /// Returns all the reviews of a user
        /// </summary>
        [HttpGet("{userId}")]
        public object getReviews(int userId)
        {
            return _userCatalog.getReviews(userId);
        }


    }
}
