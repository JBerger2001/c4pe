using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class PlaceTypeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
