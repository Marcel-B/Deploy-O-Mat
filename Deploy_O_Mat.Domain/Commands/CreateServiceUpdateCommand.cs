using System;
namespace com.b_velop.Deploy_O_Mat.Domain.Commands
{
    public class CreateServiceUpdateCommand : ServiceUpdateCommand
    {
        public CreateServiceUpdateCommand(
            string serviceName,
            string repoName,
            string tag,
            Guid buildId
            )
        {
            ServiceName = serviceName;
            RepoName = repoName;
            Tag = tag;
            BuildId = buildId;
        }
    }
}
