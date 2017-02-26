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
        public Request createRide([FromBody]Request request,[FromHeader]string id)
        { 
            return _rideCatalog.createNewRide(request, Convert.ToInt32(id));
        }



    }
}
