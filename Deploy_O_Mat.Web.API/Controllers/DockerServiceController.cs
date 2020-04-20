using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerStackServices;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using MicroRabbit.Domain.Core.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerServiceController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<DockerStackService>> List()
            => await Mediator.Send(new List.Query());

        [HttpPost("remove")]
        public async Task<ActionResult<Unit>> Remove(
            Remove.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}