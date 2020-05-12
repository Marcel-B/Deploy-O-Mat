using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
{
    public class Update
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
            IDockerServiceService _service;

            public Handler(
                IDockerServiceService service)
            {
                _service = service;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var result = await _service.UpdateDockerService(request.Id);

                if (!result.Success)
                    throw new RestException(result.HttpStatusCode, result.Error);

                return Unit.Value;
            }
        }
    }
}
