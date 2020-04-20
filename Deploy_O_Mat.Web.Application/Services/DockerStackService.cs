using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly IEventBus eventBus;

        public DockerStackService(
            IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void CreateStack(Domain.Models.DockerStack dockerStack)
        {
            eventBus.SendCommand(new CreateCreateStackCommand(dockerStack.Name, dockerStack.File));
        }

        public void RemoveStack(string stackName)
        {
            eventBus.SendCommand(new CreateRemoveStackCommand(stackName));
        }
    }
}
