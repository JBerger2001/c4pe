using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("placeowner")]
    public class PlaceOwner
    {
        [Required]
        public long OwnerID { get; set; }
        [Required]
        public long PlaceID { get; set; }

        public User Owner { get; set; }
        public Place Place { get; set; }
    }
}
