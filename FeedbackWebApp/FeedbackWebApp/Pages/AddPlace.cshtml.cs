using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace FeedbackWebApp.Pages
{
    public class AddPlaceModel : PageModel
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
        //[BindProperty]
        //public IFormFile Upload { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            HttpRequests req = new HttpRequests();
            Object place = new
            {
                name = Name,
                zipCode = ZipCode,
                city = City,
                street = Street,
                country = Country,
                placeTypeID = Placetype
            };
            Place p = await req.CreatePlaceAsync(place,BaseController.GetToken());
            return RedirectToPage("/PlaceSite",new { id = Convert.ToInt32(p.ID) });
        }
    }
}