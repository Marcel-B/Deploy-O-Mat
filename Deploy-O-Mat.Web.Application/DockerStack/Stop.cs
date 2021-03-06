using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Stop
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Stop.Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEqual(Guid.Empty);
            }
        }

        public class Handler : IRequestHandler<Stop.Command>
        {
            private readonly IDockerStackService _dockerStackService;
            private readonly ILogger<Handler> _logger;

            public Handler(
                IDockerStackService dockerStackService,
                ILogger<Handler> logger)
            {
                _dockerStackService = dockerStackService;
                _logger = logger;
            }

            public async Task<Unit> Handle(
                Stop.Command request,
                CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Stop Stack called");
                await _dockerStackService.StopStack(request.Id, cancellationToken);
                return Unit.Value;
            }
        }
    }
}