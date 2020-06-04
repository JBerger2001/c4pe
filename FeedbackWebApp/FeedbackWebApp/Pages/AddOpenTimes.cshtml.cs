using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class AddOpenTimesModel : PageModel
    {
        [BindProperty]
        public List<TimeSpan> Time_Open { get; set; }
        [BindProperty]
        public List<TimeSpan> Time_Close { get; set; }
        
        public void OnGet()
        {

        }
        public async void OnPost()
        {
            
            HttpRequests req = new HttpRequests();
            for (int i = 0; i < 7; i++)
            {
                if(Time_Open[i].ToString()!="00:00:00" && Time_Close[i].ToString() != "00:00:00")
                {
                    object opti = new { Day = i, Open = Time_Open[i], Close = Time_Close[i] };
                    await req.CreateOpeningTime(opti, Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken());
                }
            }
        }
    }
}