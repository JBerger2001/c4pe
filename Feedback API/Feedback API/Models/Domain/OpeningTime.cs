using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class OpeningTime
    {
        public long ID { get; set; }
        public long PlaceID { get; set; }
        public int Day { get; set; }
        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }

        public Place Place { get; set; }
    }
}
