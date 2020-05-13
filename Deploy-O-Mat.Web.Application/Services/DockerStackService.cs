using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Commands.DockerStack;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly IEventBus _eventBus;
        private readonly IDeployOMatWebRepository _repo;

        public DockerStackService(
            IEventBus eventBus,
            IDeployOMatWebRepository repo)
        {
            _eventBus = eventBus;
            _repo = repo;
        }

        public async Task StartStack(Guid id, CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);

            if (dockerStack == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new {dockerStack = "Not found", id});

            await _eventBus.SendCommand(new Application.Bus.Commands.DockerStack.Start.DockerStack {Name = dockerStack.Name, File = dockerStack.File});
        }

        public Task StopStack(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task CreateStack(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);
            if (dockerStack == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new
                {
                    dockerStack = "Not found", id
                });

            await _eventBus.SendCommand(new CreateCreateStackCommand(dockerStack.Name, dockerStack.File));
        }

        public async Task RemoveStack(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);
            if (dockerStack == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new
                {
                    dockerStack = "Not found", id
                });

            await _eventBus.SendCommand(new CreateRemoveStackCommand(dockerStack.Name));
        }

        public async Task<IServiceResponse> UpdateStack(
            Guid id,
            string serviceName,
            CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);
            if (dockerStack == null)
            {
                return new ServiceResponse
                {
                    Error = new {dockerStack = "Not found", id},
                    Message = $"DockerStack not found",
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                    Success = false
                };
            }

            //var service = dockerStack.
            //await _eventBus.SendCommand(new CreateUpdateDockerServiceCommand());
            return new ServiceResponse
            {
                Success = true
            };
        }
    }
}