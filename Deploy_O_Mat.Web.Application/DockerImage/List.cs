using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerImage
{
    public class List
    {
        public class Query : IRequest<IEnumerable<DockerImageDto>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<DockerImageDto>>
        {
            private readonly IMapper _mapper;
            private readonly IDockerImageService _dockerImageService;

            public Handler(
                IMapper mapper,
                IDockerImageService dockerImageService)
            {
                _mapper = mapper;
                _dockerImageService = dockerImageService;
            }

            public async Task<IEnumerable<DockerImageDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var dockerImages = await _dockerImageService.GetDockerImages();
                var dto = _mapper.Map<IEnumerable<DockerImageDto>>(dockerImages);
                return dto;
            }
        }
    }
}
