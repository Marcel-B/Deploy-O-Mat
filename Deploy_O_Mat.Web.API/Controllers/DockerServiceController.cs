using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerStackServices;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockerServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DockerServiceController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<DockerStackService>> List()
            => await _mediator.Send(new List.Query());
    }
}