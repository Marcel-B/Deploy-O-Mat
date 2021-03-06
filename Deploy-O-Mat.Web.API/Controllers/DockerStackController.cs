﻿using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerStack;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Deploy_O_Mat.Web.API.Controllers
{
    public class DockerStackController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DockerStack>>> Get()
            => await Mediator.Send(new List.Query());

        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Create(
            Create.Command command)
            => await Mediator.Send(command);
        
        [HttpPost("start")]
        public async Task<ActionResult<Unit>> Start(
            Start.Command command)
            => await Mediator.Send(command);
            
        [HttpPost("stop")]
        public async Task<ActionResult<Unit>> Stop(
            Stop.Command command)
            => await Mediator.Send(command);

        [HttpPost("update")]
        public async Task<ActionResult<Unit>> Update(
           Update.Command command)
           => await Mediator.Send(command);

        [HttpPost("remove")]
        public async Task<ActionResult<Unit>> Remove(
            Remove.Command command)
            => await Mediator.Send(command);
    }
}
