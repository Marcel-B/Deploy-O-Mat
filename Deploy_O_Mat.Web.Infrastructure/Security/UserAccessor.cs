using System.Linq;
using System.Security.Claims;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace com.b_velop.Deploy_O_Mat.Web.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUsername()
        {
            var username = _httpContextAccessor
                .HttpContext
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                .Value;

            return username;
        }
    }
}
