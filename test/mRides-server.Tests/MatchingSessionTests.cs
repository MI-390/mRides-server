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
            string[,] coordinates = new string[2, 2]
            {
                {"45.443246,-73.644613","45.452562,-73.625714"},
                {"45.443246,-73.644613","45.479196,-73.619449" }
            };

            for (int i = 0; i < 2; i++)
            {
                //Initialize user
                User user = new User
                {
                    ID = i
                };
                //Initialize Request
                Request request = new Request
                {
                    ID = i,
                    Driver = null
                };
                //Initialize RiderRequest Collection
                //All the requests with null driver will have a single RiderRequest
                List<RiderRequest> riderRequests = new List<RiderRequest>() {
                    new RiderRequest()
                    {
                        ID=i,
                        RiderID=user.ID,
                        Rider=user,
                        RequestID=request.ID,
                        Request=request,
                        destination= coordinates[i,0],
                        location= coordinates[i, 1],
                    }
                };
                
                request.RiderRequests = riderRequests;

            }


            return requests.AsQueryable();
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void findRiders()
        {
            var mockRequestCatalog = new Mock<RequestCatalog>();
            mockRequestCatalog.Setup(rc => rc.getNullDriver()).returns();
        }
    }
       
}
