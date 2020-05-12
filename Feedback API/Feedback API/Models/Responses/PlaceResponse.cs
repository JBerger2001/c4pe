using System.Collections.Generic;

namespace Feedback_API.Models.Responses
{
    public class PlaceResponse
    {
        public long ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }
        public bool UserIsOwner { get; set; }
        public float Rating { get; set; }
        public int ReviewCount { get; set; }
        public PlaceTypeResponse PlaceType { get; set; }
        public ICollection<OpeningTimeResponse> OpeningTimes { get; set; }
        public ICollection<PlaceImageResponse> Images { get; set; }
        public ICollection<PlaceOwnerResponse> Owners { get; set; }
    }
}
