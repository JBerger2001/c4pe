using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class PlaceType
    {
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
