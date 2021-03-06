﻿using System;
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
        private RequestCatalog _requestCatalog;
        private UserCatalog _userCatalog;
        public ConsoleController(ServerContext context)
        {
            _context = context;
            _requestCatalog = new RequestCatalog(context);
            _userCatalog = new UserCatalog(context);
            _matchingSession = new MatchingSession(_requestCatalog,_userCatalog);
            _rideCatalog = new RideCatalog(context);
        }

       [HttpPost]
       public object findDrivers([FromHeader]string id,[FromBody]Request request)
       {
            return _matchingSession.findDrivers(Convert.ToInt32(id), request);
        }

        [HttpPost]
       public object findRiders([FromHeader]string id,[FromBody]Request request)
       {
            return _matchingSession.findRiders(Convert.ToInt32(id), request);
       }

        [HttpPost]
        public void TestFcmMessage([FromBody]dynamic sentObject)
        {
            _matchingSession.sendPushNotification(sentObject.id, sentObject.fcmToken);
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
        public void addRiderToRide([FromBody]dynamic sentObject)
        {
            _rideCatalog.addRiderToRide(sentObject.rideid,sentObject.userid);
        }
        




    }
}
