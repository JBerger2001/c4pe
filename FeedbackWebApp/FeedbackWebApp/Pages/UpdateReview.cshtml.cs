using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class UpdateReviewModel : PageModel
    {
        [BindProperty]
        public int Rating { get; set; }
        [BindProperty]
        public string reviewContent { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            Object r = new { rating = Rating, text = reviewContent };
            HttpStatusCode c = await req.UpdateReview(r, Convert.ToInt32(RouteData.Values["id"]), BaseController.GetreviewID(), BaseController.GetToken());
            if (c == HttpStatusCode.NoContent)
            {
                return RedirectToPage("/PlaceSite", new { id = RouteData.Values["id"] });
            }
            else
            {
                return null;
            }
        }
    }
}