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
    public class RequestController : Controller
    {
        private ServerContext _context;
        private RequestCatalog _rideCatalog;
        private UserCatalog _userCatalog;
        public RequestController(ServerContext context)
        {
            _context = context;
            _rideCatalog = new RequestCatalog(_context);
        }

        /// <summary>
        /// Used to find a user by user id
        /// </summary>
        [HttpGet("{id}")]
        public virtual Request getRequest(int id)
        {
            return _rideCatalog.getRequest(id);
        }

        [HttpPost]
        public Request createRequest([FromBody]Request request,[FromHeader]string id)
        { 
            return _rideCatalog.create(request, Convert.ToInt32(id));
        }

        [HttpGet("{id}")]
        public void deleteRequest(int ID)
        {
            
            _rideCatalog.deleteRequest(ID);
        }


    }
}
