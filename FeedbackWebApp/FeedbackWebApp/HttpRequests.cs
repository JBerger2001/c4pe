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
        public async Task<Place> GetPlaceAsync(int placeId,string token)
        {
            path2 = path + "api/places/" + placeId;
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<Place>();
        }
        public async Task<PlaceType> GetPlaceTypeAsync(int typeId, string token)
        {
            path2 = path + "api/placetypes/" + typeId;
            return (await path2.WithOAuthBearerToken(token).GetJsonAsync<PlaceType>());
        }
        public async Task<List<PlaceType>> GetPlaceTypesAsync(string token)
        {
            path2 = path + "api/placetypes";
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<List<PlaceType>>();
        }
        public async Task<List<Place>> GetPlacesAsync(int page, string token)
        {
            path2 = path + "api/places?PageNumber=" + page;
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<List<Place>>();
        }
        public async Task<List<openingTimes>> GetOpenTimesAsync(int pID, string token)
        {
            path2 = path + "api/places/"+pID+"/OpeningTimes";
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<List<openingTimes>>();
        }
        public async Task<object> GetPagesAsync(string token)
        {
            path2 = path + "api/places?PageNumber=1";
            var head = (await path2.WithOAuthBearerToken(token).GetAsync()).Headers;
            //var h = head.Where((k)=> k.Key=="X-Pagination");
            return head;
        }
        public async Task<List<Review>> GetReviews(int placeId, string token)
        {
            path2 = path + "api/places/" + placeId + "/reviews";
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<List<Review>>();
        }
        public async Task<List<Review>> GetMyReviews(string token)
        {
            path2 = path + "api/users/me/reviews";
            return await path2.WithOAuthBearerToken(token).GetJsonAsync<List<Review>>();
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
        public async Task<HttpStatusCode> CreateOpeningTime(object opti,long pID,string token)
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
                return null;
        }
        #endregion POST Requests

        #region PUT Requests
        public async Task<HttpStatusCode> UpdateUserAsync(Object u, string token)
        {
            string path2 = path + "api/users/me";
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(u)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdatePlaceAsync(Object place,int pID, string token)
        {
            string path2 = path + "api/places/" + pID;
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(place)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdateOpeningTime(object r, int pID, int oID, string token)
        {
            string path2 = path + "api/places/" + pID + "/OpeningTimes/"+oID;
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(r)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdateReview(Object o, int pID, int opID, string token)
        {
            string path2 = path + "api/places/" + pID + "/reviews/" + opID;
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(o)).StatusCode;
        }
        public async Task<HttpStatusCode> UpdatePlaceType(Object t, int tID, string token)
        {
            string path2 = path + "api/placetypes/" + tID;
            return (await path2.WithOAuthBearerToken(token).PutJsonAsync(t)).StatusCode;
        }
        #endregion PUT Requests

        #region DELETE Requests
        public async Task<HttpStatusCode> DeletePlaceAsync(int pID, string token)
        {
            string path2 = path + "api/places/" + pID;
            return (await path2.WithOAuthBearerToken(token).DeleteAsync()).StatusCode;
        }
        public async Task<HttpStatusCode> DeletePlaceTypeAsync(int pID, string token)
        {
            string path2 = path + "api/placetypes/" + pID;
            return (await path2.WithOAuthBearerToken(token).DeleteAsync()).StatusCode;
        }
        public async Task<HttpStatusCode> DeleteOpeningTimeAsync(int pID, int oID, string token)
        {
            string path2 = path + "api/places/" + pID + "/OpeningTimes/" + oID;
            return (await path2.WithOAuthBearerToken(token).DeleteAsync()).StatusCode;
        }
        public async Task<HttpStatusCode> DeleteReviewAsync(int pID,int rID, string token)
        {
            string path2 = path + "api/places/" + pID+"/Reviews/"+rID;
            return (await path2.WithOAuthBearerToken(token).DeleteAsync()).StatusCode;
        }
        public async Task<HttpStatusCode> DeleteUserAsync(string token)
        {
            string path2 = path + "api/users/me";
            return (await path2.WithOAuthBearerToken(token).DeleteAsync()).StatusCode;
        }

        #endregion DELETE Requests
    }
}
