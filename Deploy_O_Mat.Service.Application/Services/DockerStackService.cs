using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Service.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly IProcessor _processor;
        private readonly IEventBus _bus;
        private readonly ILogger<DockerStackService> _logger;

        public DockerStackService(
            IProcessor processor,
            IEventBus bus,
            ILogger<DockerStackService> logger
            )
        {
            _processor = processor;
            _logger = logger;
            _bus = bus;
        }

        public async Task<int> CreateStack(
            DockerStack stack)
        {
            var result = await _processor.Process("docker", $"stack deploy -c {stack.File} {stack.Name}");
            if (result.Success)
                _logger.LogInformation($"Docker Stack '{stack.Name}' with File '{stack.File}' created");
            else
                _logger.LogWarning($"Error while creating Stack '{stack.Name}' with file '{stack.File}': ({result.ReturnCode}) - {result.ErrorMessage}");
            return result.ReturnCode;
        }

        public async Task<int> RemoveStack(
            string stack)
        {
            var result = await _processor.Process("docker", $"stack rm {stack}");
            if (result.Success)
                _logger.LogInformation($"Docker Stack '{stack}' removed");
            else
                _logger.LogWarning($"Error while removing Stack '{stack}': ({result.ReturnCode}) - {result.ErrorMessage}");
            return result.ReturnCode;
        }
    }
}
