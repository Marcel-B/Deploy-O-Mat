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
        private readonly IRepositoryWrapper _repository;
        private readonly IEventBus _bus;

        public DockerImageService(
            IRepositoryWrapper repository,
            IEventBus bus)
        {
            _repository = repository;
            _bus = bus;
        }

        public async Task<DockerImage> CreateOrUpdateDockerImage(
            DockerImage dockerImage)
        {
            var tmpDockerImage = _repository.DockerImages.SelectById(dockerImage.Id);

            if (tmpDockerImage != null && await _repository.DockerImages.UpdateAsync(dockerImage, tmpDockerImage))
            {
                return tmpDockerImage;
            }
            tmpDockerImage = _repository.DockerImages.Insert(dockerImage);
            var success = tmpDockerImage != null;
            if (success)
                return tmpDockerImage;

            return null;
        }

        public async Task<IEnumerable<DockerImage>> GetDockerImages()
        {
            var dockerImages = await _repository.DockerImages.SelectAllAsync();
            foreach (var dockerImage in dockerImages)
                dockerImage.Updated = dockerImage.Updated?.ToLocalTime();
            return dockerImages;
        }

        public void UpdateDockerService(
            DockerServiceUpdate dockerServiceUpdate)
        {
            var createUpdateDockerServiceCommand = new CreateServiceUpdateCommand(
                dockerServiceUpdate.ServiceName,
                dockerServiceUpdate.RepoName,
                dockerServiceUpdate.Tag,
                dockerServiceUpdate.BuildId);
            _bus.SendCommand(createUpdateDockerServiceCommand);
        }
    }
}
