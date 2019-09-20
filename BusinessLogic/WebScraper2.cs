using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DefaultWebApiCalls : WebScraper2.IWebApiCalls
    {
        public async Task<string> GetGoogleHomepageAsync()
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync("https://google.com");
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }

        public async Task<byte[]> DownloadGoogleImageAsync(string imagePath)
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync($"https://google.com{imagePath}");
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                return data;
            }
        }
    }

    public class DefaultPersistantStorageCalls : WebScraper2.IPersistantStorageCalls
    {
        public Task<bool> SaveImageAsync(string filename, byte[] data)
        {
            File.WriteAllBytes($@"C:\Temp2\google-images\{filename}", data);
            return Task.FromResult(true);
        }
    }

    public class WebScraper2
    {
        private readonly IWebApiCalls _webApiCalls;
        private readonly IPersistantStorageCalls _persistantStorageCalls;

        public WebScraper2(IWebApiCalls webApiCalls, IPersistantStorageCalls persistantStorageCalls)
        {
            _webApiCalls = webApiCalls;
            _persistantStorageCalls = persistantStorageCalls;
        }

        public async Task<bool> GetAndSaveGoogleImageOfTheDay()
        {
            string homepage = await _webApiCalls.GetGoogleHomepageAsync();

            var match = Regex.Match(homepage, "[/a-z0-9-]+\\.png");
            if (match.Success)
            {
                string imagePath = match.Value;
                string filename = GetFileNameFromImagePath(imagePath);

                byte[] data = await _webApiCalls.DownloadGoogleImageAsync(imagePath);

                bool result = await _persistantStorageCalls.SaveImageAsync(filename, data);
                return result;
            }

            return false;
        }

        private string GetFileNameFromImagePath(string imagePath)
        {
            var match = Regex.Match(imagePath, "[a-z0-9-]+\\.png");
            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }

        public interface IWebApiCalls
        {
            Task<string> GetGoogleHomepageAsync();

            Task<byte[]> DownloadGoogleImageAsync(string imagePath);
        }

        public interface IPersistantStorageCalls
        {
            Task<bool> SaveImageAsync(string filename, byte[] data);
        }
    }
}
