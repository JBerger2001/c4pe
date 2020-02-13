using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class PlaceSiteModel : PageModel
    {
        [BindProperty(Name ="id", SupportsGet = true)]
        public int Id { get; set; }
        public void OnGet()
        {

        }
    }
}