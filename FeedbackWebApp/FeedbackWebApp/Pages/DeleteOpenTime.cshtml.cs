using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackWebApp.Pages
{
    public class DeleteOpenTimeModel : PageModel
    {
        [BindProperty]
        public List<int> CheckedDays { get; set; }
        public void OnGet()
        {
           
        }
        public async void OnPost()
        {
            int placeID = Convert.ToInt32(RouteData.Values["id"]);
            HttpRequests req = new HttpRequests();
            List<openingTimes> ops = await req.GetOpenTimesAsync(placeID, BaseController.GetToken());
            for (int i = 0; i < CheckedDays.Count; i++)
            {
                await req.DeleteOpeningTimeAsync(placeID, ops.Where((o) => o.Day == CheckedDays[i]).ToList()[0].ID, BaseController.GetToken());
            }
        }
    }
}