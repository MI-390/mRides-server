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
        Mock<UserCatalog> mockUserCatalog;
        MatchingSession matchingSession;
        [SetUp]
        public void Setup()
        {
            //MockRequestCatalog
            mockRequestCatalog = new Mock<RequestCatalog>();
            mockRequestCatalog.Setup(rc => rc.getNullDriver(It.IsAny<int>())).Returns(sampleNullRequests());
            mockRequestCatalog.Setup(rc => rc.getNullRiders(1)).Returns(sampleNullRequestsRiders());
            mockRequestCatalog.Setup(rc => rc.create(It.IsAny<Request>(),5)).Returns(new Request { ID = 5 });
            
            //MockUserCatalog
            mockUserCatalog = new Mock<UserCatalog>();
            mockUserCatalog.Setup(rc => rc.get(It.IsAny<int>())).Returns(new User { hasLuggage = true, hasPet = true, isHandicap = true, isSmoker = true });

            matchingSession = new MatchingSession(mockRequestCatalog.Object,mockUserCatalog.Object);
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
                    ID = i,
                    hasLuggage = true,
                    hasPet = true,
                    isHandicap = true,
                    isSmoker=true
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
            requests.First().RiderRequests.First().Rider.isSmoker = false;


            return requests.AsQueryable();
        }
        public IQueryable sampleNullRequestsRiders()
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
                    ID = i,
                    hasLuggage = true,
                    hasPet = true,
                    isHandicap = true,
                    isSmoker = true
                };
                //Initialize Request
                Request request = new Request
                {
                    ID = i,
                    Driver = user,
                    destination = coordinates[i, 0],
                    location = coordinates[i, 1],
                    destinationCoordinates= new List<DestinationCoordinate>
                    {
                    new DestinationCoordinate {coordinate= "45.443246,-73.644613"},
                    new DestinationCoordinate {coordinate="45.452554,-73.625753"}
                    }
                };
              
                requests.Add(request);
            }
            return requests.AsQueryable();
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void findRiders_takesRequest_returnsListofMarchedRequests()
        {
           
            Request request = new Request
            {
                ID = 2,
                Driver = null
            };
            //Initialize RiderRequest Collection
            //All the requests with null driver will have a single RiderRequest
            List<RiderRequest> riderRequests = new List<RiderRequest>() {
                    new RiderRequest()
                    {
                        ID=3,
                        Request=request,
                        destination= "45.442170,-73.664830",
                        location= "45.452715,-73.625920",
                    }
                };

            request.RiderRequests = riderRequests;
            MatchingSessionResponseRider sO =matchingSession.findDrivers(3,request);
            Assert.AreEqual(0,sO.Requests.First().ID);
        }
        [Test]
        public void findDrivers_takesRequest_returnsListOfMatchedRequests()
        {
            Request request1AvToAng = new Request
            {
                destinationCoordinates = new List<DestinationCoordinate>
                {
                    new DestinationCoordinate {coordinate= "45.443246,-73.644613"},
                    new DestinationCoordinate {coordinate="45.452554,-73.625753"}
                },
                location = "45.442170,-73.664830",
                destination = "45.452715,-73.625920",
            };

            MatchingSessionResponseDriver sO = matchingSession.findRiders(3, request1AvToAng);
            Assert.AreEqual(0, sO.Requests.First().ID);
        }
        [Test]
        public void filterByPreferences_takesRequest_returnsUserWithSameLuggage()
        {
            IQueryable<Request> requests = (IQueryable<Request>)sampleNullRequests();
            User actualUser = mockUserCatalog.Object.get(1);
            List<Request> expectedRequests = matchingSession.filterByPreferences(requests, 1);
            foreach(var request in expectedRequests)
            {
                User expectedUser = request.RiderRequests.First().Rider;
                Assert.AreEqual(expectedUser.hasLuggage,actualUser.hasLuggage);
            }   
        }
        [Test]
        public void filterByPreferences_takesRequest_returnsUserIsHandicapped()
        {
            IQueryable<Request> requests = (IQueryable<Request>)sampleNullRequests();
            User actualUser = mockUserCatalog.Object.get(1);
            List<Request> expectedRequests = matchingSession.filterByPreferences(requests, 1);
            foreach (var request in expectedRequests)
            {
                User expectedUser = request.RiderRequests.First().Rider;
                Assert.AreEqual(expectedUser.isHandicap, actualUser.isHandicap);
            }
        }
        [Test]
        public void filterByPreferences_takesRequest_returnsUserIsSameGander()
        {
            IQueryable<Request> requests = (IQueryable<Request>)sampleNullRequests();
            User actualUser = mockUserCatalog.Object.get(1);
            List<Request> expectedRequests = matchingSession.filterByPreferences(requests, 1);
            foreach (var request in expectedRequests)
            {
                User expectedUser = request.RiderRequests.First().Rider;
                Assert.AreEqual(expectedUser.genderPreference, actualUser.genderPreference);
            }
        }
        [Test]
        public void filterByPreferences_takesRequest_returnsUserHasPets()
        {
            IQueryable<Request> requests = (IQueryable<Request>)sampleNullRequests();
            User actualUser = mockUserCatalog.Object.get(1);
            List<Request> expectedRequests = matchingSession.filterByPreferences(requests, 1);
            foreach (var request in expectedRequests)
            {
                User expectedUser = request.RiderRequests.First().Rider;
                Assert.AreEqual(expectedUser.hasPet, actualUser.hasPet);
            }
        }
    }
}
       

