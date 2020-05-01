using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("reviews")]
    public class Review
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public long PlaceID { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public DateTime? LastEdited { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public Place Place { get; set; }
    }
}
