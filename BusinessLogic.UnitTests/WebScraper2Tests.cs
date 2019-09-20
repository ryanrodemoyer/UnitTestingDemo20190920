using NUnit.Framework;
using System.Threading.Tasks;
using TypeMock.ArrangeActAssert;

namespace BusinessLogic.UnitTests
{
    [TestFixture]
    public class WebScraper2Tests
    {
        [Test]
        public async Task Test01()
        {
            // arrange
            var ws = new WebScraper2();

            // act
            bool result = await ws.GetAndSaveGoogleImageOfTheDay();

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Test02()
        {
            // arrange
            var ws = new WebScraper2();

            Isolate.WhenCalled(() => WebApiCalls.GetGoogleHomepageAsync())
                .WillReturn(Task.FromResult("fake data"));

            // act
            bool result = await WebScraper.GetAndSaveGoogleImageOfTheDay();

            // assert
            Assert.AreEqual(false, result);
        }
    }
}
