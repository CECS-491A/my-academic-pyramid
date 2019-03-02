using System;
using System.Threading.Tasks;

namespace ServiceLayer.HttpClients
{
    // An http client to make http requests and
    // convert the response to a json string.
    public interface IHttpClient
    {
       Task<string> RequestData(Uri uri);
    }
}
