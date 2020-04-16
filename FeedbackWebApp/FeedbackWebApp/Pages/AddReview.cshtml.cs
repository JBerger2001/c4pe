using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class AddReviewModel : PageModel
    {
        public void OnGet()
        {

        }
        public async void OnPost()
        {
            HttpRequests req = new HttpRequests();
            object r = new
            {
                UserID = 6,    //mgrau
                Rating = Convert.ToInt32(Request.Form["rating"].ToString()),
                Time = DateTime.Now.ToString(),
                Text = Request.Form["reviewContent"].ToString()
            };
            await req.CreateReview(r, 16);
        }
    }
}