using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo
{
    public class Info : IRequest
    {

        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IEventBus bus;

            //private readonly IDockerImageRepository _repository;
            //private readonly ILogger<Handler> _logger;

            public Handler(
                IEventBus bus)
            {
                this.bus = bus;
                //_repository = repository;
                //_logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await bus.SendCommand(new DockerInfoCommand());
                return Unit.Value;

            }
        }
    }
}
