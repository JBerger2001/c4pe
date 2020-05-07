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
        public async Task<IActionResult> OnPostLogIn()
        {
            HttpRequests r = new HttpRequests();
            LogIn x = new LogIn();
            if (Request.Form["username"] != "" & Request.Form["pwd"] != "")
            {
                object u = new { username = Request.Form["username"].ToString(), password = Request.Form["pwd"].ToString() };
                x = await r.LoginUser(u);
            }
            if (x.Token != "" && x.userId!=0)
            {
                User us = await r.GetUserAsync(x.Token);
                BaseController.SetUserAndToken(us, x.Token);
                return RedirectToPage("/Overview");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public IActionResult OnPostGuest()
        {
            return RedirectToPage("/Overview");
        }
    }
}
