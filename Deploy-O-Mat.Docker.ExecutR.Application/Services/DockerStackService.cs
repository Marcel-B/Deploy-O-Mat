using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Repositories;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly IProcessor _processor;
        private readonly IEventBus _bus;
        private readonly IExecutRRepository _repo;
        private readonly ILogger<DockerStackService> _logger;

        public DockerStackService(
            IProcessor processor,
            IEventBus bus,
            IExecutRRepository repo,
            ILogger<DockerStackService> logger
            )
        {
            _processor = processor;
            _logger = logger;
            _bus = bus;
            _repo = repo;
        }

        public async Task<int> CreateStack(
            DockerStack stack)
        {
            var result = await _processor.Process("docker", $"stack deploy -c {stack.File} {stack.Name}");
            if (result.Success)
                _logger.LogInformation($"Docker Stack '{stack.Name}' with File '{stack.File}' created");
            else
                _logger.LogWarning($"Error while creating Stack '{stack.Name}' with file '{stack.File}': ({result.ReturnCode}) - {result.ErrorMessage}");
            
            await _repo.SaveCommandToLog(new CommandLog
            {
                Caller = GetType().Name,
                Created = DateTime.UtcNow,
                Message = $"docker stack deploy -c {stack.File} {stack.Name}",
                Success = result.Success,
                ResultCode = result.ReturnCode
            });
            
            _ = await _repo.SaveChanges();
            
            return result.ReturnCode;
        }

        public async Task<int> StartStack(DockerStack stack)
        {
            var result = await _processor.Process("docker", $"stack deploy -c {stack.File} {stack.Name}");
            if (result.Success)
                _logger.LogInformation($"Docker Stack '{stack.Name}' with File '{stack.File}' created");
            else
                _logger.LogWarning($"Error while creating Stack '{stack.Name}' with file '{stack.File}': ({result.ReturnCode}) - {result.ErrorMessage}");
            
            await _repo.SaveCommandToLog(new CommandLog
            {
                Caller = GetType().Name,
                Created = DateTime.UtcNow,
                Message = $"docker stack deploy -c {stack.File} {stack.Name}",
                Success = result.Success,
                ResultCode = result.ReturnCode
            });
            
            _ = await _repo.SaveChanges();
            
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
            
            await _repo.SaveCommandToLog(new CommandLog
            {
                Caller = GetType().Name,
                Created = DateTime.UtcNow,
                Message = $"docker stack rm {stack}",
                Success = result.Success,
                ResultCode = result.ReturnCode
            });
            
            _ = await _repo.SaveChanges();
            
            return result.ReturnCode;
        }
    }
}
