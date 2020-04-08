using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Domain;
using com.b_velop.Deploy_O_Mat.Persistence;
using MediatR;

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
            private DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(
                DataContext dataContext,
                IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }


            public async Task<DockerHubWebhookCallbackDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dockerImage = _mapper.Map<DockerImage>(request);
                var tmpDockerImage = await _dataContext.DockerImages.FindAsync(dockerImage.Id);
                if (tmpDockerImage == null)
                    tmpDockerImage = _dataContext.DockerImages.Add(dockerImage).Entity;
                else
                {
                    tmpDockerImage.BuildId = Guid.NewGuid();
                    tmpDockerImage.Pusher = dockerImage.Pusher;
                    tmpDockerImage.Tag = dockerImage.Tag;
                    tmpDockerImage.Updated = dockerImage.Updated;
                }
                var success = await _dataContext.SaveChangesAsync();
                var response = new DockerHubWebhookCallbackDto
                {
                    Context = "Image received",
                    State = "success",
                    TargetUrl = $"https://deploy.qaybe.de/dockerImageDetails/{tmpDockerImage.Id}",
                    
                };
                if (success > 0)
                {
                    response.State = "success";
                    response.Description = "Image successfully added to deploy-O-mat service";
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
