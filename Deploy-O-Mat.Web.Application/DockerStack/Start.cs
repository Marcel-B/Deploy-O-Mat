using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Start
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Start.Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEqual(Guid.Empty);
            }
        }

        public class Handler : IRequestHandler<Start.Command>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IDockerStackService _dockerStackService;

            public Handler(
                ILogger<Handler> logger,
                IDockerStackService dockerStackService)
            {
                _logger = logger;
                _dockerStackService = dockerStackService;
            }

            public async Task<Unit> Handle(
                Start.Command request,
                CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Stop Stack called");
                await _dockerStackService.StartStack(request.Id, cancellationToken);
                return Unit.Value;
            }
        }
    }
}