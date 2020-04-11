using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Application.Images
{

    public class CreateOrUpdate
    {
        public class Command : IRequest<DockerHubWebhookCallbackDto>
        {
            public Guid Id { get; set; }

            [JsonPropertyName("push_data")]
            public PushData PushData { get; set; }

            [JsonPropertyName("callback_url")]
            public string CallbackUrl { get; set; }

            [JsonPropertyName("repository")]
            public Repository Repository { get; set; }
        }

        public class Handler : IRequestHandler<Command, DockerHubWebhookCallbackDto>
        {
            private readonly IDockerImageService _dockerImageService;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(
                IDockerImageService dockerImageService,
                ILogger<Handler> logger,
                IMapper mapper)
            {
                _dockerImageService = dockerImageService;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<DockerHubWebhookCallbackDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dockerImage = _mapper.Map<DockerImage>(request);
                var tmpDockerImage = await _dockerImageService.CreateOrUpdateDockerImage(dockerImage);

                var response = new DockerHubWebhookCallbackDto
                {
                    Context = "Image received",
                    State = "success",
                    TargetUrl = $"https://deploy.qaybe.de/dockerImageDetails/{tmpDockerImage.Id}",
                };
                if (tmpDockerImage != null)
                {
                    response.State = "success";
                    response.Description = "Image successfully added to deploy-O-mat service";
                    try
                    {
                        if (tmpDockerImage.DockerStackServices != null)
                            foreach (var dockerStackService in tmpDockerImage.DockerStackServices)
                            {
                                _dockerImageService.UpdateDockerService(new Models.DockerServiceUpdate
                                {
                                    BuildId = tmpDockerImage.BuildId,
                                    RepoName = tmpDockerImage.RepoName,
                                    Tag = tmpDockerImage.Tag,
                                    ServiceName = dockerStackService.Name
                                });
                            }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while calling rabbit");
                        response.Description = "Problems with Message Queue";
                    }
                }
                else
                {
                    response.State = "failure";
                    response.Description = "Image successfully added to deploy-O-mat service";
                }
                return response;
            }
        }
    }
}
