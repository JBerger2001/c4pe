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
        public TimeSpan Time_Open0 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open1 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open2 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open3 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open4 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open5 { get; set; }
        [BindProperty]
        public TimeSpan Time_Open6 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close0 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close1 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close2 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close3 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close4 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close5 { get; set; }
        [BindProperty]
        public TimeSpan Time_Close6 { get; set; }
        public void OnGet()
        {

        }
        public async void OnPost()
        {
            List<TimeSpan> openTimes = new List<TimeSpan>() { Time_Open0, Time_Open1, Time_Open2, Time_Open3, Time_Open4, Time_Open5, Time_Open6 };
            List<TimeSpan> closeTimes = new List<TimeSpan>() { Time_Close0, Time_Close1, Time_Close2, Time_Close3, Time_Close4, Time_Close5, Time_Close6 };
            HttpRequests req = new HttpRequests();
            for (int i = 0; i < 7; i++)
            {
                if(openTimes[i].ToString()!="00:00:00" && closeTimes[i].ToString() != "00:00:00")
                {
                    object opti = new { Day = i, Open = openTimes[i], Close = closeTimes[i] };
                    await req.CreateOpeningTime(opti, Convert.ToInt32(RouteData.Values["id"]), BaseController.GetToken());
                }
            }
        }
    }
}