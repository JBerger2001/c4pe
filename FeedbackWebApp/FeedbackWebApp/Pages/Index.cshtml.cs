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
            User u = new User() { Username = Request.Form["username"], Password = Request.Form["pwd"] };
            HttpRequests r = new HttpRequests();
            LogIn x= new LogIn();
            if (u.Username != "" & u.Password != "")
            {
                x = await r.LoginUser(u);
            }// Token wird gespeichert
            if (x.token != "")
            {
                u = await r.GetUserAsync(x.userId);
                BaseController.SetUserAndToken(u, x.token);
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
