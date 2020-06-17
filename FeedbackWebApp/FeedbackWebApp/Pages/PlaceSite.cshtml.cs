using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class PlaceSiteModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPostEdit()
        {
            return RedirectToPage("/UpdatePlace", new { id = RouteData.Values["id"] });
        }
        public IActionResult OnPostReview()
        {
            return RedirectToPage("/AddReview", new { id = RouteData.Values["id"] });
        }
        public IActionResult OnPostUpdate()
        {
            return RedirectToPage("/UpdateReview", new { id = RouteData.Values["id"]});
        }
        public async Task<IActionResult> OnPostDeletePlace()
        {
            HttpRequests req = new HttpRequests();
            HttpStatusCode c = await req.DeletePlaceAsync(Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken());
            return RedirectToPage("/Overview", new { id =1 });
        }
        public async Task<IActionResult> OnPostDeleteReview()
        {
            HttpRequests req = new HttpRequests();
            HttpStatusCode c = await req.DeleteReviewAsync(Convert.ToInt32(RouteData.Values["id"]), BaseController.GetreviewID(),BaseController.GetToken());
            return RedirectToPage("/PlaceSite", new { id = RouteData.Values["id"] });
        }
    }
}