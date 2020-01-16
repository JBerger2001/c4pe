using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("openingtimes")]
    public class OpeningTime
    {
        public long ID { get; set; }
        [Required]
        public long PlaceID { get; set; }
        [Required]
        [Range(0,6)]
        public int Day { get; set; }
        [Required]
        public TimeSpan Open { get; set; }
        [Required]
        public TimeSpan Close { get; set; }

        public Place Place { get; set; }
    }
}
