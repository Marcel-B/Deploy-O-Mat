using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Shared.Contracts
{
    public interface IProcessor
    {
        Task<ProcessResult> Process(string fileName, string arguments);
    }
}