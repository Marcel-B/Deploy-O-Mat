using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class RemoveStackCommand : Command
    {
        public string StackName { get; set; }
    }
}
