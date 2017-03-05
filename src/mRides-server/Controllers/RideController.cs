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
    public class RideController : Controller
    {
        private ServerContext _context;
        private RideCatalog _rideCatalog;
        public RideController(ServerContext context)
        {
            _context = context;
            _rideCatalog = new RideCatalog(_context);
        }



        [HttpPost]
        public Ride createRide([FromBody]Ride ride,[FromHeader]string id)
        { 
            return _rideCatalog.create(ride, Convert.ToInt32(id));
        }
        [HttpPost]
        public Ride addRiderToRide([FromBody]int rideId, [FromHeader]string id)
        {
            var userid = Convert.ToInt32(id);
            return _rideCatalog.addRiderToRide(rideId,userid);
        }
        [HttpGet("{rideId}")]
        public object getRide(int rideId)
        {
            return _rideCatalog.getRide(rideId);
        }


    }
}