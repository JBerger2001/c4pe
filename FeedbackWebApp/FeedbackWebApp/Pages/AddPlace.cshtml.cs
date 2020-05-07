using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

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
            long placetypeID = 1;    //Request.Form["placetypes"].
            Object place = new
            {
                name = Request.Form["placeName"].ToString(),
                zipCode = Request.Form["zipCode"].ToString(),
                city = Request.Form["city"].ToString(),
                street = Request.Form["street"].ToString(),
                country = Request.Form["country"].ToString(),
                placeTypeID = placetypeID
            };
            Place p = await req.CreatePlaceAsync(place,BaseController.GetToken());
            int placeID = Convert.ToInt32(p.ID);
            openingTimes opti;
            for (int i = 0; i < 7; i++)
            {
                if (Request.Form["Open" + i].ToString() != "" && Request.Form["Close" + i].ToString() != "")
                {
                    opti = new openingTimes{ Day = i, Open = Request.Form["Open" + i].ToString(), Close = Request.Form["Close" + i].ToString() };
                    if(await req.CreateOpeningTime(opti, placeID, BaseController.GetToken()) != HttpStatusCode.OK)
                    {
                        // Zeig an, dass es nicht funktioniert hat
                    }
                }
            }
            BaseController.SetPlace(await req.GetPlaceAsync(placeID));
            return RedirectToPage("/PlaceSite");
        }
    }
}