using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerStack;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Deploy_O_Mat.Web.Application.DockerStack;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerStackController : BaseController
    {
        public DockerStackController()
        { }

        [HttpPost("create")]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<ActionResult<Unit>> Create(
            Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("remove")]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<ActionResult<Unit>> Remove(
            Remove.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DockerStack>>> Get()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}
