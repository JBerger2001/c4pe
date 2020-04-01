using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebApp
{
    public class Place
    {
        public long ID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }
        public float Rating { get; set; }
        public int ReviewCount { get; set; }
        public PlaceType PlaceType { get; set; }
        public List<openingTimes> OpeningTimes { get; set; }


    }
}
