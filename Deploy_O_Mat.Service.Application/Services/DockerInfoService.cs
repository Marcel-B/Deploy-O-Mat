using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Service.Util.Contracts;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerInfoService : IDockerInfoService
    {
        private readonly IProcessor _processor;
        private readonly ILogger<DockerInfoService> _logger;
        private readonly IEventBus _bus;

        public DockerInfoService(
            IProcessor processor,
            IEventBus bus,
            ILogger<DockerInfoService> logger)
        {
            _processor = processor;
            _bus = bus;
            _logger = logger;
        }

        public async Task<string> GetServices()
        {
            var result = await _processor.Process(
                "docker",
                 $"service ls");

            if (!result.Success)
                _logger.LogError($"Error while get service info. '{result.ErrorMessage}' - Exit Code '{result.ReturnCode}'");

            return result.Result ?? result.ErrorMessage;
        }
    }
}
