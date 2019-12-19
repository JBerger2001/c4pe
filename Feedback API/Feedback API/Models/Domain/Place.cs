using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class Place
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }

        public long PlaceTypeID { get; set; }
        public PlaceType PlaceType { get; set; }

        public ICollection<OpeningTime> OpeningTimes { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
