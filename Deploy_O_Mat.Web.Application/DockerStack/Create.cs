using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using FluentValidation;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStack
{
    public class Create
    {
        public class Command : IRequest
        {
            public string File { get; set; }
            public string Name { get; set; }
        }


        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.File).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly WebContext _context;
            private readonly IDockerStackService dockerStackService;

            public Handler(
                WebContext context,
                IDockerStackService dockerStackService)
            {
                _context = context;
                this.dockerStackService = dockerStackService;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                dockerStackService.CreateStack(new Domain.Models.DockerStack { File = request.File, Name = request.Name });
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
