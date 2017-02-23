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
        public ConsoleController(ServerContext context)
        {
            _context = context;
            _matchingSession = new MatchingSession(context);
        }


       public object findDrivers([FromBody]Request request)
       {
            return null;
       }
       public object findRiders([FromBody]Request request)
       {
            return null;
       }
  
       

    }
}
