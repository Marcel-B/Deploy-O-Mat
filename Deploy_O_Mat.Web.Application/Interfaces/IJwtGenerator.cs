using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;

namespace Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(AppUser user);
    }
}
