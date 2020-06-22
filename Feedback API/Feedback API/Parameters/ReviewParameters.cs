using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Parameters
{
    public class ReviewParameters : QueryStringParameters
    {
        public float MinRating { get; set; }
        public float MaxRating { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public ReviewParameters()
        {
            MinRating = 0;
            MaxRating = 5;
            From = DateTime.MinValue;
            To = DateTime.MaxValue;
            OrderBy = "PositiveReactions";
        }
    }
}
