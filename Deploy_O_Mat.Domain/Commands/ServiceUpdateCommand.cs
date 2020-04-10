using System;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Domain.Commands
{
    public abstract class ServiceUpdateCommand : Command
    {
        public string ServiceName { get; set; }
        public string RepoName { get; set; }
        public string Tag { get; set; }
        public Guid BuildId { get; set; }
    }
}
