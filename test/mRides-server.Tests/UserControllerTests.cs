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
    public class UserControllerTests    
    {
        UserCatalog catalog;
        [SetUp]
        public void Setup() {
            int userId = 1;
            var mokSet = initializeMokSet();

            var mockContext = new Mock<ServerContext>();
            mockContext.Setup(c => c.Users).Returns(mokSet.Object);
            mockContext.Setup(c => c.Users.Find(It.IsAny<int>())).Returns(new User { GSD = 90 });
            mockContext.Setup(c => c.Users.Find(It.IsAny<int>())).Returns(new User { gender = "female" });
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

        [Test]
        public void getGSDtest()
        {
            long userGSD = catalog.getGSD(1);
            Assert.AreEqual(userGSD, 90);
        }

        [Test]
        public void setGSDTest()
        {
            catalog.setGSD(1, 100);
            long userGSD = catalog.getGSD(1);
            Assert.AreEqual(userGSD, 100);
            catalog.setGSD(1, 90);
        }

        [Test]
        public void getGenderTest()
        {
            string userGender = catalog.getGender(1);
            Assert.AreEqual(userGender, "female");
        }

        [Test]
        public void setGenderTest()
        {
            catalog.setGender(1, "male");
            string userGender = catalog.getGender(1);
            Assert.AreEqual(userGender, "male");
            catalog.setGender(1, "female");
        }

        public Mock<DbSet<User>> initializeMokSet()
        {
            var usersList = new List<User>();

            usersList.Add(new User
            {
                ID = 1,
                GSD = 90,
                gender = "female",
                RidesAsRider = new List<UserRides>
                {
                    new UserRides { RideId=1,RiderId=1,driverFeedback="Driver 2 to Rider 1",riderFeedback="Rider 1 to Driver 2"},

                },
                RidesAsDriver = new List<Ride>
                {
                   new Ride {ID=2,UserRides=new List<UserRides> {
                       new UserRides { RideId =2,RiderId=4,driverFeedback="Driver 1 to Rider 4",riderFeedback="Rider 4 to Driver 1" }
                   } }
                  
                }

            });
            
            var users=usersList.AsQueryable();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            return mockSet;
        }
    }
}
