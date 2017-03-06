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
            Assert.True(true);
        }
    }
}
