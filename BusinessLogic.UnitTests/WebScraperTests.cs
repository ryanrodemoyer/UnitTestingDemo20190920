﻿using NUnit.Framework;
using System.Threading.Tasks;
using TypeMock.ArrangeActAssert;

namespace BusinessLogic.UnitTests
{
    [TestFixture]
    public class WebScraperTests
    {
        [Test]
        public async Task Test01()
        {
            // arrange

            // act
            bool result = await WebScraper.GetAndSaveGoogleImageOfTheDay();

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Test02()
        {
            // arrange
            Isolate.WhenCalled(() => WebApiCalls.GetGoogleHomepageAsync())
                .WillReturn(Task.FromResult("fake data"));

            // act
            bool result = await WebScraper.GetAndSaveGoogleImageOfTheDay();

            // assert
            Assert.AreEqual(false, result);
        }
    }
}
