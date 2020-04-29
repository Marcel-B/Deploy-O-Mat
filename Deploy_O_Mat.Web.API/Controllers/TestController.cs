using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class TestController : BaseController
    {
        // GET
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}