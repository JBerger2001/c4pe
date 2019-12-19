using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class ReviewResponse
    {
        public long ID { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public UserResponse User { get; set; }
    }
}
