using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class Review
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long PlaceID { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public Place Place { get; set; }
    }
}
