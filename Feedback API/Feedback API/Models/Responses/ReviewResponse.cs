using System;

namespace Feedback_API.Models.Responses
{
    public class ReviewResponse
    {
        public long ID { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public DateTime? LastEdited { get; set; }
        public string Text { get; set; }
        public UserPublicResponse User { get; set; }
    }
}
