using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class Review
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public DateTime? LastEdited { get; set; }
        public string Text { get; set; }
        public int positiveReactions { get; set; }
        public int negativeReactions { get; set; }
        public User2 User { get; set; }
    }
}
