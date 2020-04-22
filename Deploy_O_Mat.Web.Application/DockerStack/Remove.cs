using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Remove
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
            private readonly IDockerStackService _dockerStackService;

            public Handler(
                IDockerStackService dockerStackService)
            {
                _dockerStackService = dockerStackService;
            }

            public Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                _dockerStackService.RemoveStack(request.Id, cancellationToken);
                return Unit.Task;
            }
        }
    }
}
