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
            return _rideCatalog.createNewRide(ride, Convert.ToInt32(id));
        }
        [HttpGet("{rideId}")]
        public object getRide(int rideId)
        {
            return _rideCatalog.getRide(rideId);
        }


    }
}
