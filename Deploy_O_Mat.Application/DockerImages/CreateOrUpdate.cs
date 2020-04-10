using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
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
            private readonly IDockerImageRepository _repository;
            private readonly IMapper _mapper;

            public Handler(
                IDockerImageService dockerImageService,
                ILogger<Handler> logger,
                IDockerImageRepository repository,
                IMapper mapper)
            {
                _dockerImageService = dockerImageService;
                _logger = logger;
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<DockerHubWebhookCallbackDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dockerImage = _mapper.Map<DockerImage>(request);
                var tmpDockerImage = await _repository.GetDockerImage(request.Id);

                if (tmpDockerImage != null)
                    tmpDockerImage = await _repository.UpdateDockerImage(request.Id, dockerImage);
                else
                    tmpDockerImage = _repository.CreateDockerImage(dockerImage);

                var success = await _repository.SaveChanges();

                var response = new DockerHubWebhookCallbackDto
                {
                    Context = "Image received",
                    State = "success",
                    TargetUrl = $"https://deploy.qaybe.de/dockerImageDetails/{tmpDockerImage.Id}",
                };
                if (success)
                {
                    response.State = "success";
                    response.Description = "Image successfully added to deploy-O-mat service";
                    try
                    {
                        _dockerImageService.UpdateDockerService(new Models.DockerServiceUpdate
                        {
                            BuildId = tmpDockerImage.BuildId,
                            RepoName = tmpDockerImage.RepoName,
                            Tag = tmpDockerImage.Tag,
                            ServiceName = "test_test"
                        });
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
