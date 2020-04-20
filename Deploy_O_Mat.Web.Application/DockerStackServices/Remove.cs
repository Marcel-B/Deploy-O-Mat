using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStackServices
{
    public class Remove
    {
        public class Command : IRequest
        {
            public string ServiceName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ServiceName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDockerImageService dockerStackService;

            public Handler(
                IDockerImageService dockerStackService)
            {
                this.dockerStackService = dockerStackService;
            }

            public Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                dockerStackService.RemoveDockerService(request.ServiceName);
                return Unit.Task;
            }
        }
    }
}
