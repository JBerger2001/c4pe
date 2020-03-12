using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class openingTimes
    {
        public int day { get { return day; } set { day = value;  } }    //0=Montag ConvertDay(day);
        public TimeSpan open { get; set; }
        public TimeSpan close { get; set; }
        //public string Day { get; set; }
        //private void ConvertDay(int id)
        //{
        //    switch (id)
        //    {
        //        case 0:
        //            Day = "Monday";
        //            break;
        //        case 1:
        //            Day = "Tuesday";
        //            break;
        //        case 2:
        //            Day = "Wednesday";
        //            break;
        //        case 3:
        //            Day = "Thursday";
        //            break;
        //        case 4:
        //            Day = "Friday";
        //            break;
        //        case 5:
        //            Day = "Saturday";
        //            break;
        //        case 6:
        //            Day = "Sunday";
        //            break;
        //    }
        //}
    }
}
