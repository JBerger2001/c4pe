﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models.Responses
{
    public class UserResponse
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsVerified { get; set; }
    }
}
