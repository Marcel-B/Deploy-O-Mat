using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerImages;
using com.b_velop.Deploy_O_Mat.Web.Application.Images;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerImageController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<DockerImage>> List()
            => await Mediator.Send(new List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<DockerImage>> Details(Guid id)
            => await Mediator.Send(new Details.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult<DockerHubWebhookCallbackDto>> CreateOrUpdate(
            Guid id,
            CreateOrUpdate.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> ChangeState(
            bool state)
        {
            return await Task.FromResult(true);
        }
    }
}