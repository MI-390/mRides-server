using mRides_server.Data;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mRides_server.Logic
{
    public class RequestCatalog:ICatalog<Request>
    {
        List<mRides_server.Models.Request> Requests;
        private ServerContext _context;

        public RequestCatalog(ServerContext context)
        {
            _context = context;
            
        }



        public List<mRides_server.Models.Request> getRequests()
        {
            Requests= _context.Requests.ToList();
            return Requests;
        }
        public List<mRides_server.Models.Request> getRequestsForDrivers()
        {
           return  _context.Requests
                .Include(s=>s.Driver)
                .Where(s => s.Driver== null)
                .ToList();
        }
        public List<mRides_server.Models.Request> getRequestsForRiders()
        {
            return null;
           // return _context.Requests
                 //.Where(s => s.type == "driver")
                 //.ToList();
        }
        public Request create(Request request, int userId)
        {
            
            if (request.type == "driver")
            {
                request.Driver = _context.Users.Find(userId);
                _context.Requests.Add(request);
            }
            else
            {
                User rider = _context.Users
                    .Include(s => s.RequestAsRider)
                    .First(s => s.ID == userId);
                
           
                RiderRequest r = new RiderRequest
                {
                    Request = request,
                    Rider = rider,
                    destination=request.destination,
                    location=request.location
                };
                request.destination = null;
                request.location = null;
                _context.RiderRequests.Add(r);
                
            }
           // 
            _context.SaveChanges();
            return request;
        }
        public Request mergeRiderRequestToRequest(int driverReqId,int riderReqId)
        {
            Request driverRequest = _context.Requests
                .Include(r=>r.RiderRequests)
                    .ThenInclude(rr=>rr.Rider)
                .First(r=>r.ID==driverReqId);
            Request riderRequest= _context.Requests
                .Include(r => r.RiderRequests)
                .First(r => r.ID == riderReqId);
            driverRequest.RiderRequests.Add(riderRequest.RiderRequests.FirstOrDefault());
            _context.Remove(riderRequest);
            _context.SaveChanges();
            return driverRequest;
        }

        public void deleteRequest(int ID)
        {
            _context.Remove(_context.Requests.Find(ID));
            _context.SaveChanges();
        }

        public void addDriver(int requestId,User driver)
        {

        }
        public void addRider(int requestId, User driver)
        {

        }

    }
}
