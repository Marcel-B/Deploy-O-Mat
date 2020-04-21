using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateDockerServiceCommand : Command
    {
        public string Name { get; set; }
        public string Repo { get; set; }
        public string Tag { get; set; }
        public string Network { get; set; }
        public string Script { get; set; }
    }
}
