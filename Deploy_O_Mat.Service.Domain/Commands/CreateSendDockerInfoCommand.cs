using System;
namespace Deploy_O_Mat.Service.Domain.Commands
{
    public class CreateSendDockerInfoCommand : SendDockerInfoCommand
    {
        public CreateSendDockerInfoCommand(
            string dockerInfo)
        {
            DockerInfo = dockerInfo;
        }
    }
}
