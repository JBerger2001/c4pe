﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class AddPlaceTypeModel : PageModel
    {
        public void OnGet()
        {
        }
        public async void OnPost()
        {
            HttpRequests req = new HttpRequests();
            PlaceType placetype = new PlaceType(){ Name = Request.Form["placetypeName"] };
            await req.CreatePlaceTypeAsync(placetype);
        }
    }
}