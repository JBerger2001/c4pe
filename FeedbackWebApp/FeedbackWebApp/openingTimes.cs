using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class openingTimes
    {
        public long ID { get; set; }
        public int Day { get; set; }    //0=Montag ConvertDay(day);
        public string Open { get; set; }   
        public string Close { get; set; }
    }
}
