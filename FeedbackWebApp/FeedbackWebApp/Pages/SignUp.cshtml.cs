using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class SignUpModel : PageModel
    {
        public void OnGet()
        {

        }
        public async void OnPost()
        {
            User u = new User() { Username = Request.Form["username"], FirstName=Request.Form["fn"], LastName=Request.Form["ln"],
                                  Password=Request.Form["pwd"], Description=Request.Form["description"],
                                  Street =Request.Form["street"], ZipCode = Convert.ToInt32(Request.Form["zipCode"]),
                                  City = Request.Form["city"], Country = Request.Form["country"]
            };
            //if(Request.Form["check"]==true)
           // {
                HttpRequests r = new HttpRequests();
                await r.CreateUserAsync(u);
            //}
        }
    }
}