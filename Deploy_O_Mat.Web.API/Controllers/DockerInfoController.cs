using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerInfoController : BaseController
    {
        public DockerInfoController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<Unit>> Get()
        {
            return await Mediator.Send(new Info.Command());
        }
    }
}
