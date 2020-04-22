using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerImages
{
    public class Details
    {
        public class Query : IRequest<DockerImage>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DockerImage>
        {
            private readonly IDeployOMatWebRepository _repository;
            private readonly ILogger<Handler> _logger;

            public Handler(
                IDeployOMatWebRepository repository,
                ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public async Task<DockerImage> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var dockerImage = await _repository.GetDockerImage(request.Id);

                if (dockerImage == null)
                    throw new RestException(HttpStatusCode.NotFound, new { dockerImage = "Not found" });

                return dockerImage;
            }
        }
    }
}
