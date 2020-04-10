using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Application.Models;
using com.b_velop.Deploy_O_Mat.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Application.Services
{
    public class DockerImageService : IDockerImageService
    {
        private readonly IDockerImageRepository _dockerImageRepository;
        private readonly IEventBus _bus;

        public DockerImageService(
            IDockerImageRepository dockerImageRepository,
                IEventBus bus)
        {
            _dockerImageRepository = dockerImageRepository;
            _bus = bus;
        }

        public async Task<DockerImage> CreateOrUpdateDockerImage(
            DockerImage dockerImage)
        {
            var tmpDockerImage = await _dockerImageRepository.GetDockerImage(dockerImage.Id);

            if (tmpDockerImage != null)
                tmpDockerImage = await _dockerImageRepository.UpdateDockerImage(dockerImage.Id, dockerImage);
            else
                tmpDockerImage = _dockerImageRepository.CreateDockerImage(dockerImage);

            var success = await _dockerImageRepository.SaveChanges();
            if (success)
                return tmpDockerImage;

            return null;
        }

        public async IAsyncEnumerable<DockerImage> GetDockerImages()
        {
            var dockerImages = _dockerImageRepository.GetDockerImages();
            await foreach (var dockerImage in dockerImages)
            {
                dockerImage.Updated = dockerImage.Updated?.ToLocalTime();
                yield return dockerImage;
            }
        }

        public void UpdateDockerService(
            DockerServiceUpdate dockerServiceUpdate)
        {
            var createUpdateDockerServiceCommand = new CreateServiceUpdateCommand(
                "service_name",
                dockerServiceUpdate.RepoName,
                dockerServiceUpdate.Tag,
                dockerServiceUpdate.BuildId);
            _bus.SendCommand(createUpdateDockerServiceCommand);
        }
    }
}
