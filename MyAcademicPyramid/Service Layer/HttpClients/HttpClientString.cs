using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceLayer.HttpClients
{
    /// <summary>
    /// An HttpClient implementation to make http requests
    /// to a server whose response is in the form of a string.
    /// </summary>
    public class HttpClientString : IHttpClient
    {
        public HttpClientString()
        {

        }

        /// <summary>
        /// HTTP GET Request
        /// </summary>
        /// <param name="uri">URL address</param>
        /// <returns>The string response of the request</returns>
        public async Task<string> RequestData(Uri uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}