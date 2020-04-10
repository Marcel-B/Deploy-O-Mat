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

        public async Task<IEnumerable<DockerImage>> GetDockerImages()
        {
            return await _dockerImageRepository.GetDockerImages();
        }

        public void UpdateDockerService(DockerServiceUpdate dockerServiceUpdate)
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
