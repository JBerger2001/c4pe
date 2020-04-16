using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class AddPlaceModel : PageModel
    {
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            long placetypeID = 2;  //Request.Form["placetypes"].
            Object place = new
            {
                name = Request.Form["placeName"].ToString(),
                zipCode = Request.Form["zipCode"].ToString(),
                city = Request.Form["city"].ToString(),
                street = Request.Form["street"].ToString(),
                country = Request.Form["country"].ToString(),
                isVerified = false,
                placeTypeID = placetypeID
            };
            Place p = await req.CreatePlaceAsync(place);
            //long placeID = 16;         //p.ID;
            //object opti;
            //openingTimes o=new openingTimes();
            //for (int i = 0; i < 7; i++)
            //{
            //    if (Request.Form["Open" + i].ToString() != "--:--" && Request.Form["Close" + i].ToString() != "--:--")
            //    {
            //        opti = new { day = i, open = Request.Form["Open" + i].ToString(), close = Request.Form["Close" + i].ToString() };
            //        o = await req.CreateOpeningTime(opti, placeID);
            //    }
            //}
            return RedirectToPage("/PlaceSite");
        }
    }
}