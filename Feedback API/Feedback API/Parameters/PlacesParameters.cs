using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Parameters
{
    public class PlacesParameters : QueryStringParameters
    {
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool? IsVerified { get; set; }
        public string Name { get; set; } = string.Empty;
        public float? MinRating { get; set; }
        public float? MaxRating { get; set; }
        public IList<long> PlaceType { get; set; } = new List<long>();
        public bool? IsOpen { get; set; }

        public PlacesParameters()
        {
            OrderBy = "Name";
        }
    }
}
