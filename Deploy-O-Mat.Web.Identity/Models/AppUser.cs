﻿using Microsoft.AspNetCore.Identity;

namespace com.b_velop.Deploy_O_Mat.Web.Identity.Models
{
    public class AppUser : IdentityUser<string>
    {
        public string DisplayName { get; set; }
    }
}
