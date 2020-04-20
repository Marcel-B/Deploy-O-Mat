namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IDockerStackService
    {
        void CreateStack(Domain.Models.DockerStack dockerStack);
        void RemoveStack(string stackName);
    }
}
