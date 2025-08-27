using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public static class HttpClientManager
    {
        public static readonly HttpClient Client = new HttpClient();

        static HttpClientManager()
        {
            // Initialize HttpClient
            Client.BaseAddress = new Uri("https://localhost:7264/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

}
