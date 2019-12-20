using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class Review
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PlaceId { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
