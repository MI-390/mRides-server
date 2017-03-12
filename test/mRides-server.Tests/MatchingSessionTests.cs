using Microsoft.EntityFrameworkCore;
using Moq;
using mRides_server.Data;
using mRides_server.Logic;
using mRides_server.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace Tests
{
    
    [TestFixture]
    public class MatchingSessionTests
    {
        UserCatalog catalog;
        Mock<RequestCatalog> mockRequestCatalog;
        MatchingSession matchingSession;
        [SetUp]
        public void Setup()
        {
            mockRequestCatalog = new Mock<RequestCatalog>();
            //Setting up Request.getNullDriver
            mockRequestCatalog.Setup(rc => rc.getNullDriver()).Returns(sampleNullRequests());
            mockRequestCatalog.Setup(rc => rc.create(It.IsAny<Request>(),5)).Returns(new Request { ID = 5 });
            matchingSession = new MatchingSession(mockRequestCatalog.Object);
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
                requests.Add(request);
            }


            return requests.AsQueryable();
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void findRiders()
        {
            Request request1AvToAng = new Request
            {
                destinationCoordinates = new List<string>
                {
                    "45.443246,-73.644613",
                    "45.452554,-73.625753"
                },
                location = "45.442170,-73.664830",
                destination = "45.452715,-73.625920",
            };

            MatchingSessionResponse sO =matchingSession.findRiders(3,request1AvToAng);
            Assert.AreEqual(0,sO.Requests.First().ID);
        }
     }
}
       

