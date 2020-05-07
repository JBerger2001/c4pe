using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace FeedbackWebApp.Pages
{
    public class UpdatePlaceModel : PageModel
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
            long placetypeID = 2;  //Request.Form["placetypes"].
            object place = new 
            {
                Name = Request.Form["placeName"].ToString(),
                ZipCode = Request.Form["zipCode"].ToString(),
                City = Request.Form["city"].ToString(),
                Street = Request.Form["street"].ToString(),
                Country = Request.Form["country"].ToString(),
                placeTypeID = placetypeID
            };
            if(await req.UpdatePlaceAsync(place, Convert.ToInt32(BaseController.GetPlaceID())) == HttpStatusCode.OK)
            {
                int placeID = BaseController.GetPlaceID();
                openingTimes opti;
                int max = BaseController.GetOpeningTimes().Count;
                for (int i = 0; i < max; i++)
                {
                    int opID = Convert.ToInt32(BaseController.GetOpeningTimes()[i].ID);
                    if (Request.Form["Open" + i].ToString() != "" && Request.Form["Close" + i].ToString() != "")
                    {
                        opti = new openingTimes { Day = BaseController.GetOpeningTimes()[i].Day, Open = Request.Form["Open" + i].ToString(), Close = Request.Form["Close" + i].ToString() };
                        if(await req.UpdateOpeningTime(opti, placeID, opID) != HttpStatusCode.OK)
                        {
                            // Fehler mitteilen
                        }
                    }
                }
                return RedirectToPage("/PlaceSite");
            }
            else
            {
                // Fehler mitteilen
                return null;
            }
        }
    }
}