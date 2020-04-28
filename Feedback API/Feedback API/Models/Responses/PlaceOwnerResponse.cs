using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class PlaceOwnerResponse
    {
        public long OwnerID { get; set; }
        public long PlaceID { get; set; }
    }
}
