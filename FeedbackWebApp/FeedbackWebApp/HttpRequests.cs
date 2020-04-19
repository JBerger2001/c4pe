using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Flurl;
using Flurl.Http;

namespace FeedbackWebApp
{
    public class HttpRequests
    {
        HttpClient client = new HttpClient();
        private string path = "http://77.244.251.110/";

        public async Task<Place> GetPlaceAsync(int placeId)
        {
            Place place = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/places/" + placeId);
            if (response.IsSuccessStatusCode)
            {
                place = await response.Content.ReadAsAsync<Place>();
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
            string response = await client.GetStringAsync(path + "api/places");
            var data = JsonConvert.DeserializeObject<Place[]>(response);
            for (int i = 0; i < data.Length; i++)
            {
                places.Add(data[i]);
            }

            return places;
        }
        public async Task<List<Review>> GetReviews(int placeId)
        {
            List<Review> places = new List<Review>();
            string response = await client.GetStringAsync(path + "api/places/"+placeId+"/reviews");
            var data = JsonConvert.DeserializeObject<Review[]>(response);
            for (int i = 0; i < data.Length; i++)
            {
                places.Add(data[i]);
            }
            return places;
        }
        public async Task<User> GetUserAsync(long userId)
        {
            User u = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/users/" + userId);
            if (response.IsSuccessStatusCode)
            {
                u = await response.Content.ReadAsAsync<User>();
            }
            return u;
        }

        public async Task<Place> CreatePlaceAsync(Object place)
        {
            Place response = await client.PostAsJsonAsync(
                path + "api/places", place).ReceiveJson<Place>();
            if(response!=null)
                return response;
            return null;
        }
        public async Task<Uri> CreatePlaceTypeAsync(PlaceType placetype)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path + "api/placetypes", placetype);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<string> CreateUserAsync(User u)
        {
            if (u.Username != "" && u.Password != "")
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                path + "api/users/register", u);
                response.EnsureSuccessStatusCode();
                return "Sucessfully registered!";
            }
            else
                return "A problem ...";
        }
        public async Task<openingTimes> CreateOpeningTime(object opti,long pID)
        {
            openingTimes response = await client.PostAsJsonAsync(
                path + "api/places/"+pID+"/OpeningTimes", opti).ReceiveJson<openingTimes>();
            return response;
        }

        public async Task<Uri> CreateReview(object review,long placeID)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path + "api/places/"+placeID+"/Reviews", review);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<LogIn> LoginUser(User user)
        {
            LogIn response = await client.PostAsJsonAsync(
            path + "api/users/login", user).ReceiveJson<LogIn>();
            //response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
