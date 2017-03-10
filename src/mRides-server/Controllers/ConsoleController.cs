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

    public class ConsoleController : Controller
    {
        private ServerContext _context;
        private MatchingSession _matchingSession;
        private RideCatalog _rideCatalog;
        public ConsoleController(ServerContext context)
        {
            _context = context;
            _matchingSession = new MatchingSession(context);
            _rideCatalog = new RideCatalog(context);
        }

       [HttpPost]
       public object findDrivers([FromHeader]string id,[FromBody]Request request)
       {
            return _matchingSession.findDrivers(Convert.ToInt32(id), request, request.destinationCoordinates);
        }

        [HttpPost]
       public object findRiders([FromHeader]string id,[FromBody]Request request)
       {
            return _matchingSession.findRiders(Convert.ToInt32(id), request,request.destinationCoordinates);
       }
        [HttpPost]
        public Request confirm([FromHeader]string id, [FromBody]dynamic sentObj)
        {
            int userId = Convert.ToInt32(id);
            int driverRequestId = sentObj.driverRequestId;
            int riderRequestId = sentObj.riderRequestId;
            return _matchingSession.confirm(userId, driverRequestId,riderRequestId);
        }
        [HttpPost]
        public void createRide([FromBody]dynamic sentObject)
        {
            //var r = _context.Users.Include(c => c.RidesAsDriver).Single(u=>u.ID==1);
            Ride ride = sentObject.request.ToObject<Ride>();
            _rideCatalog.createNewRide(ride, (int)sentObject.userid, (string)sentObject.type);
        }
        /// <summary>
        /// Json object:{"rideid":1,"userid":1}
        /// </summary>
        /// <param name="sentObject"></param>
        [HttpPost]
        public void addRiderToRide([FromBody]dynamic sentObject)
        {
            _rideCatalog.addRiderToRide(sentObject.rideid,sentObject.userid);
        }
        




    }
}
