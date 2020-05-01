using System.Collections.Generic;

namespace Feedback_API.Models.Requests
{
    public class PlaceRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public long PlaceTypeID { get; set; }
    }
}
