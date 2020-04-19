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
        public static User GetUser()
        {
            return _instance;
        }
        public static string GetToken()
        {
            return _token;
        }
        public static void SetUserAndToken(User u, string t)
        {
            _instance = u;
            _token = t;
        }
    }
}