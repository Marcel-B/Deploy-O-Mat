using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.DockerImages;
using com.b_velop.Deploy_O_Mat.Application.Images;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockerImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DockerImageController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DockerImage>>> List()
            => await _mediator.Send(new List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<DockerImage>> Details(Guid id)
            => await _mediator.Send(new Details.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult<DockerHubWebhookCallbackDto>> CreateOrUpdate(
            Guid id,
            CreateOrUpdate.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}