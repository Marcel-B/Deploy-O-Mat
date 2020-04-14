using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerStackServices
{
    public class List
    {
        public class Query : IRequest<IEnumerable<DockerStackService>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<DockerStackService>>
        {
            private readonly IDockerStackServiceRepository _repository;

            public Handler(
                IDockerStackServiceRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<DockerStackService>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => await _repository.GetAll(cancellationToken);
        }
    }
}
