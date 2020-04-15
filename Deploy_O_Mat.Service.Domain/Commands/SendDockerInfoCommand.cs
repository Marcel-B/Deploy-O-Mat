using System;
using MicroRabbit.Domain.Core.Commands;

namespace Deploy_O_Mat.Service.Domain.Commands
{
    public class SendDockerInfoCommand : Command
    {
        public string DockerInfo { get; set; }
    }
}
