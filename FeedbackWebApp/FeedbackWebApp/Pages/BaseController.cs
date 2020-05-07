using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackWebApp.Pages
{
    public class BaseController : Controller
    {
        private static User _instance = null;
        private static string _token = "";
        private static Place _place;
        public static User GetUser()
        {
            return _instance;
        }
        public static string GetToken()
        {
            return _token;
        }
        public static Place GetPlace()
        {
            return _place;
        }
        public static int GetPlaceID()
        {
            return Convert.ToInt32(_place.ID);
        }
        public static List<openingTimes> GetOpeningTimes()
        {
            return _place.OpeningTimes;
        }
        public static void SetToken(string t)
        {
            _token = t;
        }
        public static void SetUser(User u)
        {
            _instance = u;
        }
        public static void SetUserAndToken(User u, string t)
        {
            _instance = u;
            _token = t;
        }
        public static void SetPlace(Place p)
        {
            _place = p;
        }
    }
}