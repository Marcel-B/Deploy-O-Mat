using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerImageService : IDockerImageService
    {
        private readonly IEventBus _bus;
        private readonly IDeployOMatWebRepository _repository;
        private readonly ILogger<DockerImageService> _logger;

        public DockerImageService(
            IEventBus bus,
            IDeployOMatWebRepository repository,
            ILogger<DockerImageService> logger)
        {
            _bus = bus;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Domain.Models.DockerImage> CreateOrUpdateDockerImage(
            Domain.Models.DockerImage dockerImage)
        {
            var tmpDockerImage = await _repository.GetDockerImage(dockerImage.Id);

            if (tmpDockerImage != null)
                tmpDockerImage = _repository.UpdateDockerImage(dockerImage, tmpDockerImage);
            else
                tmpDockerImage = _repository.CreateDockerImage(dockerImage);

            tmpDockerImage.StartTime = DateTime.UtcNow;

            if (await _repository.SaveChangesAsync())
                return tmpDockerImage;

            _logger.LogError($"Error while persisting DockerImage '{dockerImage.Id} {dockerImage.RepoName}:{dockerImage.Tag}'");
            return null;
        }

        public async Task<Domain.Models.DockerImage> GetDockerImage(
            Guid id)
            => await _repository.GetDockerImage(id);

        public async Task<IEnumerable<Domain.Models.DockerImage>> GetDockerImages()
        {
            var dockerImages = await _repository.GetDockerImages();
            foreach (var dockerImage in dockerImages)
            {
                dockerImage.Updated = dockerImage.Updated?.ToLocalTime();
                dockerImage.StartTime = dockerImage.StartTime?.ToLocalTime();
            }
            return dockerImages;
        }

        public async Task<IServiceResponse> UpdateDockerService(
            string image,
           string service)
        {
            var createUpdateDockerServiceCommand = new CreateUpdateDockerServiceCommand(
               image,
               service);
            await _bus.SendCommand(createUpdateDockerServiceCommand);
            return new ServiceResponse
            {
                Success = true
            };
        }
    }
}
