using com.b_velop.Deploy_O_Mat.Web.Identity.Models;

namespace Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
