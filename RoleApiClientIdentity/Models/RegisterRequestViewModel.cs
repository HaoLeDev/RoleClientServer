﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleApiClientIdentity.Models
{
    public class RegisterRequestViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}