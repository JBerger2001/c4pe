using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class OverviewModel : PageModel
    {
        public List<Place> places { get; set; }

        public IActionResult OnGetAddPlace()
        {
            return RedirectToPage("/AddPlace");
        }
        public IActionResult OnGetAddPlaceType()
        {
            return RedirectToPage("/SignUp");
        }
    }
}