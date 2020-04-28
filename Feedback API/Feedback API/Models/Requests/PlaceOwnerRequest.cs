using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Requests
{
    public class PlaceOwnerRequest
    {
        [Required]
        public long OwnerID { get; set; }
    }
}
