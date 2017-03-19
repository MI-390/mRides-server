    using Microsoft.EntityFrameworkCore;
using Moq;
using mRides_server.Data;
using mRides_server.Logic;
using mRides_server.Models;
using mRides_server.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class RequestCatalogTest    
    {
        RequestCatalog catalog;
        [SetUp]
        public void Setup() {
            int userId = 1;
            var mokSet = initializeMokSet();

            var mockContext = new Mock<ServerContext>();
            mockContext.Setup(c => c.Requests).Returns(mokSet.Object);
            catalog = new RequestCatalog(mockContext.Object);
        }

        [TearDown]
        public void Tear() { }


        /*
         * Not run yet
         */
        [TestCase(1,2)]
        public void mergeRiderRequestToRequest_takesTwoRequests_returnsMergedRequest(int i,int j)
        {
            Request expectedRequest=catalog.mergeRiderRequestToRequest(i, j);
            User expectedDriver = expectedRequest.Driver;
            User expectedRider = expectedRequest.RiderRequests.First().Rider;
            Assert.Equals(expectedDriver.ID,1);
        }

        public Mock<DbSet<Request>> initializeMokSet()
        {
            var requestList = new List<Request>();
            //Driver Request
            Request request1 = new Request
            {
                ID = 1,
                Driver = new User{ ID = 1 }
            };
            //Rider Request
            Request request2 = new Request
            {
                ID = 2,
                RiderRequests = new List<RiderRequest> { new RiderRequest { Rider = new User { ID = 1 } } }
            };
            
           
            var requests=requestList.AsQueryable();
            var mockSet = new Mock<DbSet<Request>>();
            mockSet.As<IQueryable<Request>>().Setup(m => m.Provider).Returns(requests.Provider);
            mockSet.As<IQueryable<Request>>().Setup(m => m.Expression).Returns(requests.Expression);
            mockSet.As<IQueryable<Request>>().Setup(m => m.ElementType).Returns(requests.ElementType);
            mockSet.As<IQueryable<Request>>().Setup(m => m.GetEnumerator()).Returns(requests.GetEnumerator());
            return mockSet;
        }
    }
}
