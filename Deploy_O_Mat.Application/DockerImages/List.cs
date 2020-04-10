using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Application.Images
{
    public class List
    {
        public class Query : IRequest<IAsyncEnumerable<DockerImage>> { }

        public class Handler : IRequestHandler<Query, IAsyncEnumerable<DockerImage>>
        {
            private readonly IDockerImageRepository _repository;

            public Handler(
                IDockerImageRepository repository)
            {
                _repository = repository;
            }

            public Task<IAsyncEnumerable<DockerImage>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => Task.FromResult(_repository.GetDockerImages());
        }
    }
}
