using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Requests
{
    public class OpeningTimeRequest
    {
        public int Day { get; set; }
        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }
    }
}
