using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Images;
using com.b_velop.Deploy_O_Mat.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockerImageController : ControllerBase
    {
        private IMediator _mediator;

        public DockerImageController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DockerImage>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateOrUpdate(
            Guid id,
            CreateOrUpdate.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}