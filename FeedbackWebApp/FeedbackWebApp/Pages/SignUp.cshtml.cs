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
        public void OnPost()
        {
            User u = new User() { Username = Request.Form["Username"], FirstName=Request.Form["fn"], LastName=Request.Form["ln"]};
            if(Request.Form["pwd"]== Request.Form["pwdC"])
            {
                u.Password = Request.Form["pwd"];
                HttpRequests r = new HttpRequests();
                //await r.CreateUserAsync(u);
            }
        }
    }
}