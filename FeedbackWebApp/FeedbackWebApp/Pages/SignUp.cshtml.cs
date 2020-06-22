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
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Firstname { get; set; }
        [BindProperty]
        public string Lastname { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string Street { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string ZipCode { get; set; }
        [BindProperty]
        public string Country { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if(Request.Form["username"]!="" && Request.Form["pwd"] != "")
            {
                Object u = new
                {
                    username = Username,
                    firstName = Firstname,
                    lastName = Lastname,
                    password = Password,
                    description = Description,
                    street = Street,
                    zipCode = ZipCode,
                    city = City,
                    country = Country
                };
                HttpRequests r = new HttpRequests();
                if(await r.CreateUserAsync(u) != HttpStatusCode.OK)
                {
                    // Zeige Fehler an
                }
            }
            return RedirectToPage("/Index");
        }
    }
}