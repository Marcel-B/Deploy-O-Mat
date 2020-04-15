using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateUpdateServiceCommand : UpdateServiceCommand
    {
        public CreateUpdateServiceCommand(
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
