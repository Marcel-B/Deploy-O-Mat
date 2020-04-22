using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public abstract class UpdateDockerServiceCommand : Command
    {
        public string Service { get; set; }
        public string Image { get; set; }
    }
}
