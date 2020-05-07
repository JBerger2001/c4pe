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
        public void OnGet()
        {
         
        }
        public IActionResult OnPutCancel()
        {
            return RedirectToPage("/Overview");
        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            Object u = new 
            {
                FirstName = Request.Form["fn"].ToString(),
                LastName = Request.Form["ln"].ToString(),
                Description = Request.Form["description"].ToString(),
                Street = Request.Form["street"].ToString(),
                ZipCode = Request.Form["zipCode"].ToString(),
                City = Request.Form["city"].ToString(),
                Country = Request.Form["country"].ToString(),
            };
            if(await req.UpdateUserAsync(u, BaseController.GetToken()) == HttpStatusCode.OK)
            {
                BaseController.SetUser(await req.GetUserAsync(BaseController.GetToken()));
                return RedirectToPage("/Overview");
            }
            else
            {
                // Zeige Fehler an
                return null;
            }
        }
    }
}