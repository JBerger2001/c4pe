using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Domain
{
    public class Reaction
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long ReviewID { get; set; }
        public bool IsHelpful { get; set; }

        public User User { get; set; }
        public Review Review { get; set; }
    }
}
