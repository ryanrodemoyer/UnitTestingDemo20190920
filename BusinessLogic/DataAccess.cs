using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class WebApiCalls
    {
        public static async Task<string> GetGoogleHomepageAsync()
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync("https://google.com");
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }

        public static async Task<byte[]> DownloadGoogleImageAsync(string imagePath)
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync($"https://google.com{imagePath}");
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                return data;
            }
        }
    }

    public static class FileSystemCalls
    {
        public static Task<bool> SaveImageToDiskAsync(string filename, byte[] data)
        {
            File.WriteAllBytes($@"C:\Temp2\google-images\{filename}", data);
            return Task.FromResult(true);
        }
    }

    public static class WebScraper
    {
        public static async Task<bool> GetAndSaveGoogleImageOfTheDay()
        {
            string homepage = await WebApiCalls.GetGoogleHomepageAsync();

            var match = Regex.Match(homepage, "[/a-z0-9-]+\\.png");
            if (match.Success)
            {
                string imagePath = match.Value;
                string filename = GetFileNameFromImagePath(imagePath);

                byte[] data = await WebApiCalls.DownloadGoogleImageAsync(imagePath);

                bool result = await FileSystemCalls.SaveImageToDiskAsync(filename, data);
                return result;
            }

            return false;
        }

        private static string GetFileNameFromImagePath(string imagePath)
        {
            var match = Regex.Match(imagePath, "[a-z0-9-]+\\.png");
            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }
    }
}
