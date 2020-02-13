using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace FeedbackWebApp
{
    public class HttpRequests
    {
        HttpClient client;
        public static int PlaceId { get; set; }

        public HttpRequests()
        {
            var proxy = new WebProxy
            {
                Address = new Uri($"http://192.168.1.96:3128"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
                // *** These creds are given to the proxy server, not the web server ***
                Credentials = new NetworkCredential( userName: "f.grausenburger", password: "franzy14" )
            };
            var httpClientHandler = new HttpClientHandler { Proxy = proxy };
            client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
        }

        public async Task<Place> GetProductAsync(string path)
        {
            Place place = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                place = await response.Content.ReadAsAsync<Place>();
            }
            return place;
        }
        public async Task<List<Place>> GetProductsAsync(string path)
        {
            List<Place> places = new List<Place>();
            string response = await client.GetStringAsync(path);
            var data = JsonConvert.DeserializeObject<Place[]>(response);
            for(int i = 0; i<data.Length; i++)
            {
                places.Add(data[i]);
            }
            return places;
        }

    //    public async Task<Uri> CreateProductAsync(Place place)
    //    {
    //        HttpResponseMessage response = await client.PostAsJsonAsync(
    //            "api/places", place);
    //        response.EnsureSuccessStatusCode();

    //        // return URI of the created resource.
    //        return response.Headers.Location;
    //    }
    }
}
