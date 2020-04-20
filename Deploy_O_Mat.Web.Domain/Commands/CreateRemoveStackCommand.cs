namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateRemoveStackCommand : RemoveStackCommand
    {
        public CreateRemoveStackCommand(
            string stackName)
        {
            StackName = stackName;
        }
    }
}
