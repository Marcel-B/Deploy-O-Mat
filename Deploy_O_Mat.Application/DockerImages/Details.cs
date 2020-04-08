using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Errors;
using com.b_velop.Deploy_O_Mat.Domain;
using com.b_velop.Deploy_O_Mat.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Application.DockerImages
{
    public class Details
    {
        public class Query: IRequest<DockerImage>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DockerImage>
        {
            private readonly DataContext _dataContext;
            private readonly ILogger<Handler> _logger;

            public Handler(DataContext dataContext, ILogger<Handler> logger)
            {
                _dataContext = dataContext;
                _logger = logger;
            }

            public async Task<DockerImage> Handle(Query request, CancellationToken cancellationToken)
            {
                var dockerImage = await _dataContext.DockerImages.FindAsync(request.Id);

                if (dockerImage == null)
                    throw new RestException(HttpStatusCode.NotFound, new { dockerImage = "Not found" });

                return dockerImage;
            }
        }
    }
}
