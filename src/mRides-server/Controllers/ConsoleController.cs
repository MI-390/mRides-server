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
        private RequestCatalog _requestCatalog;
        public ConsoleController(ServerContext context)
        {
            _context = context;
            _requestCatalog = new RequestCatalog(_context);
        }



        [HttpPost]
        public void createRequest([FromBody]dynamic sentObject)
        {
            //var r = _context.Users.Include(c => c.RidesAsDriver).Single(u=>u.ID==1);
            Request request = sentObject.request.ToObject<Request>();
            _requestCatalog.createNewRequest(request, (int)sentObject.userid, (string)sentObject.type);
        }
        [HttpGet("{id}")]
        public void deleteRequest(int ID)
        {
            
            _requestCatalog.deleteRequest(ID);
        }


    }
}
