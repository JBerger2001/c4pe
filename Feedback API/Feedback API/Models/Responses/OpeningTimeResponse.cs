using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class OpeningTimeResponse
    {
        public long ID { get; set; }
        public int Day { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
    }
}
