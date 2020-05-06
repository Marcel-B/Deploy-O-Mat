using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerActiveServiceController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<DockerActiveServiceDto>> List()
            => await Mediator.Send(new List.Query());
    }
}
