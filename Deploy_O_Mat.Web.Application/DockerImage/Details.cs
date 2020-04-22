using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerImage
{
    public class Details
    {
        public class Query : IRequest<DockerImageDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DockerImageDto>
        {
            private readonly IMapper _mapper;
            private readonly IDeployOMatWebRepository _repository;
            private readonly ILogger<Handler> _logger;

            public Handler(
                IMapper mapper,
                IDeployOMatWebRepository repository,
                ILogger<Handler> logger)
            {
                _mapper = mapper;
                _repository = repository;
                _logger = logger;
            }

            public async Task<DockerImageDto> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var dockerImage = await _repository.GetDockerImage(request.Id);

                if (dockerImage == null)
                    throw new RestException(HttpStatusCode.NotFound, new { dockerImage = "Not found" });

                var dto = _mapper.Map<DockerImageDto>(dockerImage);
                return dto;
            }
        }
    }
}
