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
        
        private UserCatalog _userCatalog;
        public UserController(ICatalog<User> userCatalog)
        {
            _userCatalog = (UserCatalog) userCatalog;
        }


        /// <summary>
        /// This class is used to find a user by user id
        /// </summary>
        [HttpPost]
        public object getUserByFacebookId([FromBody]string facebookId)
        {
            //long fbId = long.Parse(facebookId);
            //return _context.Users.FirstOrDefault(u=>u.facebookID==fbId);
            return null;
        }
        [HttpGet("{id}")]
        public object getUser(int id)
        {
            return _userCatalog.get(id);
        }

        /// <summary>
        /// This class is used as an api for the search requests.
        /// </summary>
        // POST api/values
        [HttpPost]
        public object createUser([FromBody]User user)
        {
            _userCatalog.create(user);
            return user; 
        }
        /// <summary>
        /// Returns all the reviews of a user
        /// </summary>
        [HttpPost]
        public void leaveReview([FromHeader]string id,[FromBody]dynamic sentObject )
        {
            int userId = Convert.ToInt32(id);
            _userCatalog.leaveReview((int)sentObject.rideId, userId, (int)sentObject.revieweeId, (string)sentObject.review, (int)sentObject.star);
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
