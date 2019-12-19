using System.Collections.Generic;

namespace Feedback_API.Models.Requests
{
    public class PlaceRequest
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }
        public long PlaceTypeID { get; set; }
    }
}
