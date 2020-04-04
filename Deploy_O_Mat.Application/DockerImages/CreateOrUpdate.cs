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
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            [JsonPropertyName("push_data")]
            public PushData PushData { get; set; }

            [JsonPropertyName("callback_url")]
            public string CallbackUrl { get; set; }

            [JsonPropertyName("repository")]
            public Repository Repository { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private DataContext _dataContext;
            private IMapper _mapper;

            public Handler(
                DataContext dataContext,
                IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dockerImage = _mapper.Map<DockerImage>(request);
                var dImage = await _dataContext.DockerImages.FindAsync(dockerImage.Id);
                if (dImage == null)
                    _dataContext.DockerImages.Add(dockerImage);
                else
                {
                    dImage.BuildId = Guid.NewGuid();
                    dImage.Pusher = dockerImage.Pusher;
                    dImage.Tag = dockerImage.Tag;
                    dImage.Updated = dockerImage.Updated;
                }
                var success = await _dataContext.SaveChangesAsync();
                if (success > 0)
                    return Unit.Value;
                return default;
            }
        }
    }
}
