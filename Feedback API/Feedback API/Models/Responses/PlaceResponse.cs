using System.Collections.Generic;

namespace Feedback_API.Models.Responses
{
    public class PlaceResponse
    {
        public long ID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }
        public float Rating { get; set; }
        public int RatingCount { get; set; }
        public PlaceTypeResponse PlaceType { get; set; }
        public ICollection<OpeningTimeResponse> OpeningTimes { get; set; }
    }
}
