using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerInfoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Unit>> Get()
            => await Mediator.Send(new Info.Command());
        
        [HttpGet("stackLogs")]
        public async Task<IEnumerable<Domain.Models.DockerStackLog>> StackLog()
            => await Mediator.Send(new DockerStackLog.Query());
    }
}
