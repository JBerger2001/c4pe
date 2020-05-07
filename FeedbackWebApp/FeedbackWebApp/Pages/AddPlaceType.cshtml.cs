using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class AddPlaceTypeModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            PlaceType placetype = new PlaceType(){ Name = Request.Form["placetypeName"] };
            if(await req.CreatePlaceTypeAsync(placetype, BaseController.GetToken()) != HttpStatusCode.OK)
            {
                // Zeig Fehler an
            }
            return RedirectToPage("/Overview");
        }
    }
}