using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class StatisticsResponse
    {
        public int AmountPlaces { get; set; }
        public int AmountUsers { get; set; }
        public int AmountReviews { get; set; }
    }
}
