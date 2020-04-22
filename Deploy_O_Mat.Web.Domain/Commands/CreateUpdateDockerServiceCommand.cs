namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateUpdateDockerServiceCommand : UpdateDockerServiceCommand
    {
        public CreateUpdateDockerServiceCommand(
            string service,
            string image)
        {
            Service = service;
            Image = image;
        }
    }
}
