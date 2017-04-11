using System;
using System.Dynamic;
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
            _userCatalog = (UserCatalog)userCatalog;
        }

        /// <summary>
        /// Used to find a user by Facebook id
        /// </summary>
        [HttpPost]
        public object getUserByFacebookId([FromBody]string facebookId)
        {
            long fbId = long.Parse(facebookId);
            return _userCatalog.getUserByFacebookId(fbId);
        }

        /// <summary>
        /// Used to find a user by user id
        /// </summary>
        [HttpGet("{id}")]
        public virtual object getUser(int id)
        {
            return _userCatalog.get(id);
        }

        [HttpGet("{id}")]
        public virtual ICollection<Request> getRequests(int id)
        {
            return _userCatalog.getRequests(id);
        }

        [HttpPost]
        public Boolean updateFcmToken([FromHeader]string id, [FromBody]dynamic sentObj)
        {
            int userId = Convert.ToInt32(id);
            string fcmToken = (string) sentObj.fcmToken;
           return _userCatalog.updateFcmToken(userId, fcmToken);

        }

        /// <summary>
        /// This method is used as an api for the search requests.
        /// </summary>
        // POST api/values
        [HttpPost]
        public object createUser([FromBody]User user)
        {
            _userCatalog.create(user);
            return user;
        }

        /// <summary>
        /// This method is used as an api to update a user
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <return>bool, true if the operation was successful, false otherwise</return>
        [HttpPost]
        public bool updateUserSettings([FromHeader]string id, [FromBody] User user)
        {
            int userId = Convert.ToInt32(id);
           return  _userCatalog.updateUserSettings(userId, user);
        }

        /// <summary>
        /// Returns all the reviews of a user
        /// </summary>
        [HttpPost]
        public void leaveReview([FromHeader]string id, [FromBody]dynamic sentObject)
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

        /// <summary>
        /// Returns the GSD of the user with the corresponding id
        /// </summary>
        [HttpGet("{id}")]
        public long getGSD(int id)
        {
            return _userCatalog.get(id).GSD;
        }

        /// <summary>
        /// Used to modify the GSD of a user
        /// </summary>
        // POST api/values
        [HttpPost]
        public long setGSD([FromBody]dynamic sentObject)
        {
            int newUserId = sentObject.userId;
            int newAmountGSD = sentObject.amountGSD;
            _userCatalog.setGSD(newUserId, newAmountGSD);
            return _userCatalog.get(newUserId).GSD;
        }

        /// <summary>
        /// Returns the gender of the user with the corresponding id
        /// </summary>
        [HttpGet("{id}")]
        public string getGender(int id)
        {
            return _userCatalog.get(id).gender;
        }

        /// <summary>
        /// Used to modify the gender of a user
        /// </summary>
        // POST api/values
        [HttpPost]
        public string setGender([FromBody]dynamic sentObject)
        {
            int newUserId = sentObject.userId;
            string genderToChange = sentObject.userGender;
            _userCatalog.setGender(newUserId, genderToChange);
            return _userCatalog.get(newUserId).gender;
        }


    }
}
