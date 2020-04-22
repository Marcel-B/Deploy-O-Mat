using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Service.Util.Contracts;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IProcessor processor;
        private readonly IDockerServiceRepository _dockerServiceRepository;
        private readonly ILogger<DockerServiceService> _logger;
        private readonly IEventBus _bus;

        public DockerServiceService(
            IProcessor processor,
            IDockerServiceRepository dockerServiceRepository,
            ILogger<DockerServiceService> logger,
            IEventBus bus)
        {
            this.processor = processor;
            _dockerServiceRepository = dockerServiceRepository;
            _logger = logger;
            _bus = bus;
        }

        public async Task<int> Create(
            DockerService service)
        {
            var script = $"service create --name {service.Name} {service.Net} {service.Script} {service.Repo}";
            _logger.LogInformation($"Try to update:\n{script}");
            var result = await processor.Process("docker", script);

            if (result.Success)
                _logger.LogInformation($"Create Docker Service '{service}' completed");
            else
                _logger.LogWarning($"Error while creating '{service}': ({result.ReturnCode}) - {result.ErrorMessage}");

            return result.ReturnCode;
        }

        public IEnumerable<DockerService> GetDockerServices()
            => _dockerServiceRepository.GetDockerServices();

        public async Task<int> Remove(
            string service)
        {
            var result = await processor.Process("docker", $"service rm {service}");

            if (result.Success)
                _logger.LogInformation($"Remove Docker Service '{service}' completed");
            else
                _logger.LogWarning($"Error while removing '{service}': ({result.ReturnCode}) - {result.ErrorMessage}");

            return result.ReturnCode;
        }

        public async Task<int> UpdateService(
            string image,
            string service)
        {
            var result = await processor.Process("docker", $"service update --image {image} {service}");

            if (result.Success)
                _logger.LogInformation($"Update Docker Service '{service}' completed");
            else
                _logger.LogWarning($"Error while updating '{service}' to Build '{image}': ({result.ReturnCode}) - {result.ErrorMessage}");

            return result.ReturnCode;
        }
    }
}
