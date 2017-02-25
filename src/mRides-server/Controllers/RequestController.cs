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
        private RequestCatalog _requestCatalog;
        private UserCatalog _userCatalog;
        public RequestController(ServerContext context)
        {
            _context = context;
            _requestCatalog = new RequestCatalog(_context);
        }



        [HttpPost]
        public void createRequest([FromBody]Request request,[FromHeader]string id,[FromHeader]string type)
        { 
            _requestCatalog.createNewRequest(request, Convert.ToInt32(id), type);
        }

        [HttpDelete("{id}")]
        public void deleteRequest(int ID)
        {
            
            _requestCatalog.deleteRequest(ID);
        }


    }
}
