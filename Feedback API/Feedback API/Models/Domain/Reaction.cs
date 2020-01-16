using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    [Table("reactions")]
    public class Reaction
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public long ReviewID { get; set; }
        [Required]
        public bool IsHelpful { get; set; }

        public User User { get; set; }
        public Review Review { get; set; }
    }
}
