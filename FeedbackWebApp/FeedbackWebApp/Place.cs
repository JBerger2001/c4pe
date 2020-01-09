using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class Place
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public openingTimes OpenTimes { get; set; }
        public string Type { get; set; }
        public bool IsVerified { get; set; }
    }
}
