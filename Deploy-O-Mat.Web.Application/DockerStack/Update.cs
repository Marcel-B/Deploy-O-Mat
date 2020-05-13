using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string ServiceName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEqual(Guid.Empty);
                RuleFor(x => x.ServiceName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private IDockerStackService _service;

            public Handler(
                IDockerStackService service)
            {
                _service = service;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                await _service.UpdateStack(request.Id, request.ServiceName);
                return Unit.Value;
            }
        }
    }
}
