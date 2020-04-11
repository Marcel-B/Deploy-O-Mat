using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Application.DockerImages
{
    public class List
    {
        public class Query : IRequest<IEnumerable<DockerImage>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<DockerImage>>
        {
            private readonly IDockerImageService _dockerImageService;

            public Handler(
                IDockerImageService dockerImageService)
            {
                _dockerImageService = dockerImageService;
            }

            public async Task<IEnumerable<DockerImage>> Handle(
                Query request,
                CancellationToken cancellationToken)
                => await _dockerImageService.GetDockerImages();
        }
    }
}
