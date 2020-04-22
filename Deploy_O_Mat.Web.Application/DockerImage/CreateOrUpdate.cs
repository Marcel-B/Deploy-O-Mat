﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Images
{

    public class CreateOrUpdate
    {
        public class Command : IRequest<DockerHubWebhookCallbackDto>
        {
            public Guid Id { get; set; }

            [JsonProperty("push_data")]
            public PushData PushData { get; set; }

            [JsonProperty("callback_url")]
            public string CallbackUrl { get; set; }

            [JsonProperty("repository")]
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

            private DockerHubWebhookCallbackDto BuildReturn(Guid id)
                => new DockerHubWebhookCallbackDto
                {
                    Context = "Image received",
                    State = "success",
                    TargetUrl = $"https://deploy.qaybe.de/dockerImageDetails/{id}",
                };

            public async Task<DockerHubWebhookCallbackDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dockerImage = _mapper.Map<DockerImage>(request);

                if (dockerImage.Tag != "latest")
                    return BuildReturn(request.Id);

                var tmpDockerImage = await _dockerImageService.CreateOrUpdateDockerImage(dockerImage);

                var response = BuildReturn(tmpDockerImage.Id);

                if (tmpDockerImage != null)
                {
                    response.State = "success";
                    response.Description = "Image successfully added to deploy-O-mat service";
                    try
                    {
                        if (tmpDockerImage.IsActive)
                            if (tmpDockerImage.DockerServices != null)
                                foreach (var dockerService in tmpDockerImage.DockerServices)
                                {
                                    await _dockerImageService.UpdateDockerService(
                                        dockerService.Name,
                                        $"{tmpDockerImage.RepoName}:{tmpDockerImage.Tag}");
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
