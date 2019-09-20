using NUnit.Framework;
using System.Threading.Tasks;

namespace BusinessLogic.UnitTests
{
    [TestFixture]
    public class WebScraperTests
    {
        [Test]
        public async Task Test01()
        {
            bool result = await WebScraper.GetAndSaveGoogleImageOfTheDay();
            Assert.IsTrue(result);
        }
    }
}
