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
        HttpClient client = new HttpClient();
        private string path = "http://77.244.251.110/";

        public async Task<Place> GetPlaceAsync(int placeId)
        {
            Place place = null;
            HttpResponseMessage response = await client.GetAsync(path+ "api/places/"+placeId);
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<Place>(await client.GetStringAsync(path + "api/places"));
                place = data;
            }
            return place;
        }
        public async Task<string> GetPlaceTypeAsync(int typeId)
        {
            PlaceType placetype = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/placetype/" + typeId);
            if (response.IsSuccessStatusCode)
            {
                placetype = await response.Content.ReadAsAsync<PlaceType>();
            }
            return placetype.Name;
        }
        public async Task<List<PlaceType>> GetPlaceTypesAsync()
        {
            List<PlaceType> placetypes = new List<PlaceType>();
            string response = await client.GetStringAsync(path + "api/placetypes");
            var data = JsonConvert.DeserializeObject<PlaceType[]>(response);
            for (int i = 0; i < data.Length; i++)
            {
                placetypes.Add(data[i]);
            }
            return placetypes;
        }
        public async Task<List<Place>> GetPlacesAsync()
        {
            List<Place> places = new List<Place>();
            HttpResponseMessage response = await client.GetAsync(path + "api/places");
            if (response.IsSuccessStatusCode)
            {
                string response2 = await client.GetStringAsync(path + "api/places");
                var data = JsonConvert.DeserializeObject<Place[]>(response2);
                
                for (int i = 0; i < data.Length; i++)
                {
                    places.Add(data[i]);
                }
            }
            
            return places;
        }

        //public async Task<Uri> CreatePlaceAsync(Place place)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        path+"api/Places", place);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        //public async Task<Uri> CreateUserAsync(User u)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        path + "api/users", u);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        //public async void LoginUser(User user)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        path+"api/users", user);
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
