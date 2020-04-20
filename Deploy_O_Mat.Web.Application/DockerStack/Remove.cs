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
            public string StackName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StackName).NotEmpty();
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
                _dockerStackService.RemoveStack(request.StackName);
                return Unit.Task;
            }
        }
    }
}
