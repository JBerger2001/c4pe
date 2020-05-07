using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class SignUpModel : PageModel
    {
        public void OnGet()
        {

        }
        public async void OnPost()
        {
            if(Request.Form["username"]!="" && Request.Form["pwd"] != "")
            {
                Object u = new
                {
                    Username = Request.Form["username"].ToString(),
                    FirstName = Request.Form["fn"].ToString(),
                    LastName = Request.Form["ln"].ToString(),
                    Password = Request.Form["pwd"].ToString(),
                    Description = Request.Form["description"].ToString(),
                    Street = Request.Form["street"].ToString(),
                    ZipCode = Request.Form["zipCode"].ToString(),
                    City = Request.Form["city"].ToString(),
                    Country = Request.Form["country"].ToString(),
                    IsVerified = false
                };
                HttpRequests r = new HttpRequests();
                if(await r.CreateUserAsync(u) != HttpStatusCode.OK)
                {
                    // Zeige Fehler an
                }
            }

        }
    }
}