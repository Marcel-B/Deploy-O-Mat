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
        public class Query : IRequest<List<DockerImage>> { }

        public class Handler : IRequestHandler<Query, List<DockerImage>>
        {
            private readonly IDockerImageRepository _repository;

            public Handler(
                IDockerImageRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<DockerImage>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => (await _repository.GetDockerImages()).ToList();
        }
    }
}
