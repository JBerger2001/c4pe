using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPostLogIn()
        {
            User u = new User() { Username = Request.Form["username"], Password = Request.Form["pwd"] };
            HttpRequests r = new HttpRequests();
            r.LoginUser(u);
            return RedirectToPage("/Overview");
        }
        public IActionResult OnPostGuest()
        {
            return RedirectToPage("/Overview");
        }
    }
}
