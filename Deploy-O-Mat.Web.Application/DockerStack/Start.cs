using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using FluentValidation;
using MediatR;

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
            private readonly IDockerStackService _dockerStackService;

            public Handler(
                IDockerStackService dockerStackService)
            {
                _dockerStackService = dockerStackService;
            }

            public async Task<Unit> Handle(
                Start.Command request,
                CancellationToken cancellationToken)
            {
                await _dockerStackService.StartStack(request.Id, cancellationToken);
                return Unit.Value;
            }
        }
    }
}