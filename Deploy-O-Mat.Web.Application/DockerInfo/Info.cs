using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerInfo
{
    public class Info : IRequest
    {

        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IEventBus _bus;

            //private readonly IDockerImageRepository _repository;
            //private readonly ILogger<Handler> _logger;

            public Handler(
                IEventBus bus)
            {
                this._bus = bus;
                //_repository = repository;
                //_logger = logger;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // await _bus.SendCommand(new DockerInfoCommand());
                return  Unit.Task;

            }
        }
    }
}
