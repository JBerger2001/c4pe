using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class UserSiteModel : PageModel
    {
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
        public async Task<IActionResult> OnPostDel()
        {
            HttpRequests req = new HttpRequests();
            await req.DeleteUserAsync(BaseController.GetToken());
            BaseController.SetUserAndToken(new User() { Role = "nothing", Id = 0 }, "");
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            Object u = new 
            {
                firstName = Firstname,
                lastName = Lastname,
                description = Description,
                street = Street,
                zipCode = ZipCode,
                city = City,
                country = Country,
            };
            HttpStatusCode c = await req.UpdateUserAsync(u, BaseController.GetToken());
            if (c == HttpStatusCode.OK || c == HttpStatusCode.NoContent)
            {
                BaseController.SetUser(await req.GetUserAsync(BaseController.GetToken()));
                return RedirectToPage("/UserSite");
            }
            else
            {
                // Zeige Fehler an
                return null;
            }
        }
    }
}