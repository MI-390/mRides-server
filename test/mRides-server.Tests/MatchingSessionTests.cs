    using Microsoft.EntityFrameworkCore;
using Moq;
using mRides_server.Data;
using mRides_server.Logic;
using mRides_server.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class MatchingSessionTests
    {
        UserCatalog catalog;
        [SetUp]
        public void Setup()
        {
            var mockRequestCatalog = new Mock<RequestCatalog>();
            //Setting up Request.getNullDriver
            mockRequestCatalog.Setup(rc => rc.getNullDriver()).Returns(sampleNullRequests());
        }
        public IQueryable sampleNullRequests()
        {
            //Initializing Requests
            var requests = new List<Request>();
            for(int i = 1; i <= 10; i++)
            {
                User user = new User
                {
                    ID = i
                };
                Request request = new Request
                {
                    ID = i,
                    Driver = null
                };
                RiderRequest riderRequest = new RiderRequest
                {
                    ID=i,
                    RiderID=user.ID,
                    Rider=user,
                    RequestID=request.ID,
                    Request=request,
                    destination=,
                    location=,                   
               
                };
            }


            return requests.AsQueryable();
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void findRiders()
        {

        }
    }
       
}
