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
        public async void OnPost()
        {
            HttpRequests req = new HttpRequests();
            int placetypeID = 2;  //Request.Form["placetypes"].
            object place = new { Name=Request.Form["placeName"],
                              Address= Request.Form["zipCode"]+" "+Request.Form["City"]+", "+Request.Form["Street"], 
                              IsVerified=false,
                              PlaceTypeID = placetypeID 
            };
            await req.CreatePlaceAsync(place);
        }
    }
}