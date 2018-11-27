using System.Net.Http;
using System.Threading.Tasks;

namespace PasswordChecking
{
    class HttpClientMethods
    {
        /// <summary>
        /// HTTP GET Request
        /// </summary>
        /// <param name="uri">URL address</param>
        /// <returns>The string response of the request</returns>
        public static async Task<string> RequestData(string uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}
