namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateUpdateDockerServiceCommand : UpdateDockerServiceCommand
    {
        public CreateUpdateDockerServiceCommand(
            string image,
            string service)
        {
            Service = service;
            Image = image;
        }
    }
}
