using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class PlaceImage
    {
        [Column(Order = 0)]
        public long PlaceID { get; set; }
        [Range(1,3)]
        [Column(Order = 1)]
        public long ID { get; set; }
        public string URI { get; set; }

        public Place Place { get; set; }
    }
}
