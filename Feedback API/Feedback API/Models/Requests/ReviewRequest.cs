using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Requests
{
    public class ReviewRequest
    {
        public long UserID { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
