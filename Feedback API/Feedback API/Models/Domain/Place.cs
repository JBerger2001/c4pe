using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("places")]
    public class Place
    {
        public long ID { get; set; }
        [Required]
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsVerified { get; set; }

        [Required]
        public long PlaceTypeID { get; set; }
        public PlaceType PlaceType { get; set; }

        public ICollection<OpeningTime> OpeningTimes { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PlaceOwner> Owners { get; set; }
        public ICollection<PlaceImage> Images { get; set; }
    }
}
