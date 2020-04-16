using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Application.Models;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerImageService : IDockerImageService
    {
        private readonly IDockerImageRepository _repository;
        private readonly IEventBus _bus;
        private readonly ILogger<DockerImageService> _logger;

        public DockerImageService(
            IDockerImageRepository repository,
            IEventBus bus,
            ILogger<DockerImageService> logger)
        {
            _repository = repository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<DockerImage> CreateOrUpdateDockerImage(
            DockerImage dockerImage)
        {
            var tmpDockerImage = await _repository.Get(dockerImage.Id);


            if (tmpDockerImage != null)
                tmpDockerImage = _repository.Update(dockerImage, tmpDockerImage);
            else
                tmpDockerImage = _repository.Create(dockerImage);

            tmpDockerImage.StartTime = DateTime.UtcNow;

            if (await _repository.SaveChangesAsync())
                return tmpDockerImage;

            _logger.LogError($"Error while persisting DockerImage '{dockerImage.Id} {dockerImage.RepoName}:{dockerImage.Tag}'");
            return null;
        }

        public async Task<DockerImage> GetDockerImage(Guid id)
            => await _repository.Get(id);

        public async Task<IEnumerable<DockerImage>> GetDockerImages()
        {
            var dockerImages = await _repository.GetAll();
            foreach (var dockerImage in dockerImages)
            {
                dockerImage.Updated = dockerImage.Updated?.ToLocalTime();
                dockerImage.StartTime = dockerImage.StartTime?.ToLocalTime();
            }
            return dockerImages;
        }

        public void UpdateDockerService(
            DockerServiceUpdate dockerServiceUpdate)
        {
            var createUpdateDockerServiceCommand = new CreateUpdateServiceCommand(
                dockerServiceUpdate.ServiceName,
                dockerServiceUpdate.RepoName,
                dockerServiceUpdate.Tag,
                dockerServiceUpdate.BuildId);
            _bus.SendCommand(createUpdateDockerServiceCommand);
        }
    }
}
