using System;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class RemoveDockerServiceCommand : Command
    {
        public string ServiceName { get; set; }
    }
}
