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
            mockRequestCatalog.Setup(uc => uc.getNullDriver()).Returns(sampleNullRequests());
        }
        public IQueryable sampleNullRequests()
        {
            //Initializing Requests
            var requests = new List<Request>();
            for(int i = 0; i < 10; i++)
            {
                
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
