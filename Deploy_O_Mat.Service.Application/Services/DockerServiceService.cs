using System.Collections.Generic;
using Deploy_O_Mat.Service.Application.Interfaces;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IDockerServiceRepository _dockerServiceRepository;
        private readonly IEventBus _bus;

        public DockerServiceService(
            IDockerServiceRepository dockerServiceRepository,
            IEventBus bus)
        {
            _dockerServiceRepository = dockerServiceRepository;
            _bus = bus;
        }

        public IEnumerable<DockerService> GetDockerServices()
        {
            return _dockerServiceRepository.GetDockerServices();
        }
    }
}
