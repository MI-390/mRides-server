using Moq;
using mRides_server.Logic;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public void Test1() 
        {
            var mokCatalog = new Mock<RideCatalog>();
            mokCatalog.Setup(c=>c.getRides()).Returns()
            Assert.True(true);
        }
    }
}
