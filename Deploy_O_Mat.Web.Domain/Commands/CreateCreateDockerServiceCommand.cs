namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateCreateDockerServiceCommand : CreateDockerServiceCommand
    {
        public CreateCreateDockerServiceCommand(
          string name,
          string repo,
          string tag,
          string network,
          string script)
        {
            Name = name;
            Repo = repo;
            Tag = tag;
            Network = network;
            Script = script;
        }
    }
}
