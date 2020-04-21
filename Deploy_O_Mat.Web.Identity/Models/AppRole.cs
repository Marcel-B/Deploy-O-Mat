using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace com.b_velop.Deploy_O_Mat.Web.Identity.Models
{
    public class AppRole : IdentityRole<string>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}