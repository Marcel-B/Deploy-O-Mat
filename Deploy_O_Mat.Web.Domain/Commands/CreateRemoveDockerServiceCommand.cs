using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateRemoveDockerServiceCommand : RemoveDockerServiceCommand
    {
        public CreateRemoveDockerServiceCommand(
            string serviceName)
        {
            ServiceName = serviceName;
        }
    }
}
