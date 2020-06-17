using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class UpdatePlaceTypeModel : PageModel
    {
        [BindProperty]
        public string placetypeName { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostUp()
        {
            HttpRequests req = new HttpRequests();
            if(await req.UpdatePlaceType(new { name=placetypeName}, Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken()) == HttpStatusCode.NoContent)
            {
                return RedirectToPage("/PlaceTypeSite");
            }
            else
            {
                return null;
            }
        }
        public async Task<IActionResult> OnPostDel() 
        {
            HttpRequests req = new HttpRequests();
            HttpStatusCode c = await req.DeletePlaceTypeAsync(Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken());
            return RedirectToPage("/PlaceTypeSite");
        }
    }
}