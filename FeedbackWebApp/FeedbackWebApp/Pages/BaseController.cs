using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackWebApp.Pages
{
    public class BaseController : Controller
    {
        private static User _instance = new User() { Role="nothing", Id=0};
        private static string _token = "";
        private static int _reviewID;
        public static User GetUser()
        {
            return _instance;
        }
        public static string GetToken()
        {
            return _token;
        }
        public static int GetreviewID()
        {
            return _reviewID;
        }
        public static void SetToken(string t)
        {
            _token = t;
        }
        public static void SetUser(User u)
        {
            _instance = u;
        }
        public static void SetreviewID(int i)
        {
            _reviewID = i;
        }
        public static void SetUserAndToken(User u, string t)
        {
            _instance = u;
            _token = t;
        }
    }
}