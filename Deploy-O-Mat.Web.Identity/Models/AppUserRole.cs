using Microsoft.AspNetCore.Identity;

namespace com.b_velop.Deploy_O_Mat.Web.Identity.Models
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
