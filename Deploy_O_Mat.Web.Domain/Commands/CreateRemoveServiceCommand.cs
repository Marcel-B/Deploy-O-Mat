namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateRemoveServiceCommand : RemoveServiceCommand
    {
        public CreateRemoveServiceCommand(
            string serviceName)
        {
            ServiceName = serviceName;
        }
    }
}
