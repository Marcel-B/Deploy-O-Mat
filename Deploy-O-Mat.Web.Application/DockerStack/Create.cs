﻿using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }


        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEqual(Guid.Empty);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly WebContext _context;
            private readonly IDockerStackService _dockerStackService;

            public Handler(
                WebContext context,
                IDockerStackService dockerStackService)
            {
                _context = context;
                _dockerStackService = dockerStackService;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                await _dockerStackService.CreateStack(request.Id, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
