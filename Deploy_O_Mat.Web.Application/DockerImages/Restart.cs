using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerImages
{
    public class Restart
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IDockerImageRepository _dockerImageRepository;
            private readonly IDockerImageService _dockerImageService;
            private readonly ILogger<Handler> _logger;

            public Handler(
                IDockerImageRepository dockerImageRepository,
                IDockerImageService dockerImageService,
                ILogger<Handler> logger)
            {
                _dockerImageService = dockerImageService;
                _dockerImageRepository = dockerImageRepository;
                _logger = logger;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var tmpDockerImage = await _dockerImageRepository.Get(request.Id);

                if (tmpDockerImage == null)
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"No Entity with Id '{request.Id}'");

                if (!tmpDockerImage.IsActive || tmpDockerImage.Tag != "latest")
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "Entity not valid for this action");
                try
                {
                    if (tmpDockerImage.IsActive)
                    {
                        if (tmpDockerImage.DockerStackServices != null)
                        {
                            foreach (var dockerStackService in tmpDockerImage.DockerStackServices)
                            {
                                tmpDockerImage.StartTime = DateTime.UtcNow;
                                _dockerImageService.UpdateDockerService(new Models.DockerServiceUpdate
                                {
                                    BuildId = tmpDockerImage.BuildId,
                                    RepoName = tmpDockerImage.RepoName,
                                    Tag = tmpDockerImage.Tag,
                                    ServiceName = dockerStackService.Name
                                });
                            }
                        }
                    }
                    await _dockerImageRepository.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calling rabbit");
                }
                return Unit.Value;
            }
        }
    }
}
