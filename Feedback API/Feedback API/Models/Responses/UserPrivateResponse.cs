using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class UserPrivateResponse
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsVerified { get; set; }
        public string AvatarURI { get; set; }
    }
}
