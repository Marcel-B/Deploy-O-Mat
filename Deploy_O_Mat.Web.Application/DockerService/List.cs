using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
{
    public class List
    {
        public class Query : IRequest<IEnumerable<Domain.Models.DockerService>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<Domain.Models.DockerService>>
        {
            private readonly IDockerServiceService _service;

            public Handler(
                IDockerServiceService service)
            {
                _service = service;
            }

            public async Task<IEnumerable<Domain.Models.DockerService>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => await _service.Get(cancellationToken);
        }
    }
}
