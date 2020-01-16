using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("placetypes")]
    public class PlaceType
    {
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
