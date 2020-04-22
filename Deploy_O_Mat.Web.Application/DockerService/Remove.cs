using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
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
            private readonly IDockerServiceService _service;

            public Handler(
                IDockerServiceService service)
            {
                _service = service;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                await _service.RemoveDockerService(request.Id);
                return Unit.Value;
            }
        }
    }
}
