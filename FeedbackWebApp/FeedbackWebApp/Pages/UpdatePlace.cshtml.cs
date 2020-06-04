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
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Street { get; set; }
        [BindProperty]
        public string ZipCode { get; set; }
        [BindProperty]
        public string Country { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public int Placetype { get; set; }
        [BindProperty]
        public string[] OpeningTimes_Open { get; set; }
        [BindProperty]
        public string[] OpeningTimes_Close { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/PlaceSite", new { id = RouteData.Values["id"] });
        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests(); 
            object place = new 
            {
                name = Name,
                zipCode = ZipCode,
                city = City,
                street = Street,
                country = Country,
                placeTypeID = Placetype
            };
            HttpStatusCode c = await req.UpdatePlaceAsync(place, Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken());
            if (c==HttpStatusCode.NoContent)
            {
                int placeID = Convert.ToInt32(RouteData.Values["id"]);
                List<openingTimes> ops = await req.GetOpenTimesAsync(placeID, BaseController.GetToken());
                openingTimes opti;
                for (int i = 0; i < 7; i++)
                {

                    if(ops.Where((o)=> o.Day==i).ToList().Count!=0)
                    {
                        opti = new openingTimes { Day = i, Open = Request.Form["Open" + i].ToString(), Close = Request.Form["Close" + i].ToString() };
                        if (await req.UpdateOpeningTime(opti, placeID, ops.Where((o) => o.Day == i).ToList()[0].ID, BaseController.GetToken()) != HttpStatusCode.OK)
                        {
                            // Fehler mitteilen
                        }
                    }
                }
                return RedirectToPage("/PlaceSite",new { id = RouteData.Values["id"] });
            }
            else
            {
                return null;
            }
        }
    }
}