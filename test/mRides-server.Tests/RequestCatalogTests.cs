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
        Request catalog;
        [SetUp]
        public void Setup() {
            int userId = 1;
            var mokSet = initializeMokSet();

            var mockContext = new Mock<ServerContext>();
            mockContext.Setup(c => c.Requests).Returns(mokSet.Object);
            catalog = new UserCatalog(mockContext.Object);
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void GetReview() 
        {
            var feedbacks=catalog.getReviews(1);
            foreach(var feedback in feedbacks)
            {
                if (feedback.givenAs == "rider")
                {
                    Assert.AreEqual(feedback.feedbackText,"Driver 2 to Rider 1");
                }
                else if (feedback.givenAs == "driver")
                {
                    Assert.AreEqual(feedback.feedbackText,"Rider 4 to Driver 1");
                }
            } 
        }

        /*
         * Not run yet 
         */ 
        [Test]
        public void GetGSDTest()
        {
            User u = new User();
            u.ID = 1;
            u.GSD = 500;
            catalog.create(u);

            Assert.AreEqual(catalog.getGSD(1), 500);
        }

        /*
         * Not run yet
         */
        [Test]
        public void SetGSDTest()
        {

        }

        public Mock<DbSet<Request>> initializeMokSet()
        {
            var requestList = new List<Request>();
            //Driver Request
            Request request1 = new Request
            {
                ID = 1,
                Driver = { ID = 1 }
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
