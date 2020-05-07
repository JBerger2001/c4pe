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
        private string path2 = "";

        #region GET Requests
        public async Task<Place> GetPlaceAsync(int placeId)
        {
            path2 = path + "api/places/" + placeId;
            return await path2.GetJsonAsync<Place>();
        }
        public async Task<string> GetPlaceTypeAsync(int typeId)
        {
            path2 = path + "api/placetype/" + typeId;
            return (await path2.GetJsonAsync<PlaceType>()).Name;
        }
        public async Task<List<PlaceType>> GetPlaceTypesAsync()
        {
            path2 = path + "api/placetypes";
            return await path2.GetJsonAsync<List<PlaceType>>();
        }
        public async Task<List<Place>> GetPlacesAsync(int page)
        {
            path2 = path + "api/places?PageNumber=" + page;
            return await path2.GetJsonAsync<List<Place>>();
        }
        public async Task<List<Review>> GetReviews(int placeId)
        {
            path2 = path + "api/places/" + placeId + "/reviews";
            return await path2.GetJsonAsync<List<Review>>();
        }
        public async Task<User> GetUserAsync(string token)
        {
            path2 = path + "api/users/me";
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<User>();
        }
        #endregion GET Requests

        #region POST Requests
        public async Task<Place> CreatePlaceAsync(Object place, string token)
        {
            path2 = path + "api/places";
            return await path2.WithOAuthBearerToken(token).PostJsonAsync(place).ReceiveJson<Place>();
        }
        public async Task<HttpStatusCode> CreatePlaceTypeAsync(PlaceType placetype,string token)
        {
            path2 = path + "api/placetypes";
            return (await path2.WithOAuthBearerToken(token).PostJsonAsync(placetype)).StatusCode;
        }
        public async Task<HttpStatusCode> CreateUserAsync(object u)
        {
            path2 = path + "api/users/register";
            return (await path2.PostJsonAsync(u)).StatusCode;
        }
        public async Task<HttpStatusCode> CreateOpeningTime(openingTimes opti,long pID,string token)
        {
            string path2 = path + "api/places/" + pID + "/OpeningTimes";
            return (await path2.WithOAuthBearerToken(token).PostJsonAsync(opti)).StatusCode;
        }
        public async Task<HttpStatusCode> CreateReview(object review,long placeID,string token)
        {
            string path2 = path + "api/places/" + placeID + "/Reviews";
            return (await path2.WithOAuthBearerToken(token).PostJsonAsync(review)).StatusCode;
        }
        public async Task<LogIn> LoginUser(object user)
        {
            string path2 = path + "api/users/login";
            HttpStatusCode resp = (await path2.PostJsonAsync(user)).StatusCode;     // Problem!!!!
            if (resp == HttpStatusCode.OK)
                return await path2.PostJsonAsync(user).ReceiveJson<LogIn>();
            else
                return new LogIn() { Token = "", userId = 0 };
        }
        #endregion POST Requests

        #region PUT Requests
        public async Task<HttpStatusCode> UpdateUserAsync(Object u, string token)
        {
            string path2 = path + "api/users/me";
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(u)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdatePlaceAsync(Object place,int pID)
        {
            string path2 = path + "api/places/" + pID;
            return (await path2.PutJsonAsync(place)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdateOpeningTime(openingTimes o, int pID, int opID)
        {
            string path2 = path + "api/places/" + pID + "/OpeningTimes/"+opID;
            return (await path2.PutJsonAsync(o)).StatusCode;
        }
        #endregion PUT Requests
    }
}
