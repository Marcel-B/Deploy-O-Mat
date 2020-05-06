using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IProcessor _processor;
        private readonly ILogger<DockerServiceService> _logger;
        private readonly IEventBus _bus;

        public DockerServiceService(
            IProcessor processor,
            ILogger<DockerServiceService> logger,
            IEventBus bus)
        {
            _processor = processor;
            _logger = logger;
            _bus = bus;
        }

        public async Task<int> Create(
            DockerService service)
        {
            var script = $"service create --name {service.Name} {service.Net} {service.Script} {service.Repo}";
            _logger.LogInformation($"Try to update:\n{script}");
            var result = await _processor.Process("docker", script);

            if (result.Success)
                _logger.LogInformation($"Create Docker Service '{service}' completed");
            else
                _logger.LogWarning($"Error while creating '{service}': ({result.ReturnCode}) - {result.ErrorMessage}");

            return result.ReturnCode;
        }

        public async Task<int> Remove(
            string service)
        {
            var result = await _processor.Process("docker", $"service rm {service}");

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
            var result = await _processor.Process("docker", $"service update --image {image} {service}");

            if (result.Success)
                _logger.LogInformation($"Update Docker Service '{service}' completed");
            else
                _logger.LogWarning($"Error while updating service '{service}' with image '{image}': ({result.ReturnCode}) - {result.ErrorMessage}");

            return result.ReturnCode;
        }
    }
}
