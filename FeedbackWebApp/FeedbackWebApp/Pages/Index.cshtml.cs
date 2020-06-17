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
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
            BaseController.SetUserAndToken(new User() { Role = "nothing", Id = 0 }, "");
        }
        public async Task<IActionResult> OnPostLogIn()
        {
            HttpRequests r = new HttpRequests();
            LogIn x = new LogIn();
            if (Username != "" & Password != "")
            {
                object u = new { username = Username, password = Password };
                try
                {
                    x = await r.LoginUser(u);
                }
                catch (Exception)
                {
                    x = new LogIn() { userId = 0, Token = "" };
                }
            }
            if (x.Token != "" && x.userId!=0)
            {
                User us = await r.GetUserAsync(x.Token);
                BaseController.SetUserAndToken(us, x.Token);
                return RedirectToPage("/Overview", new { id = 1 });
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public IActionResult OnPostGuest()
        {
            return RedirectToPage("/Overview", new { id = 1 });
        }
    }
}
