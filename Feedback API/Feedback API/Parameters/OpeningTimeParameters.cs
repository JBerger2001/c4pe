using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Parameters
{
    public class OpeningTimeParameters : QueryStringParameters
    {
        public int Day { get; set; }

        public OpeningTimeParameters()
        {
            OrderBy = "Day";
        }
    }
}
