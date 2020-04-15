using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerStack;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerStackController : BaseController
    {
        public DockerStackController()
        {
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(
            Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
