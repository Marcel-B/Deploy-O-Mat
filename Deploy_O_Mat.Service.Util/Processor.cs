using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Service.Util.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Service.Util
{
    public class Processor : IProcessor
    {
        private readonly ILogger<Processor> logger;

        public Processor(
            ILogger<Processor> logger)
        {
            this.logger = logger;
        }

        public async Task<ProcessResult> Process(
            string fileName,
            string arguments)
        {
            var result = new ProcessResult();
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process
            {
                StartInfo = psi
            };
            process.Start();
            var sb = new StringBuilder();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                sb.AppendLine(line);
            }
            while (!process.StandardError.EndOfStream)
            {
                var error = await process.StandardError.ReadLineAsync();
                sb.AppendLine(error);
            }

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                logger.LogWarning($"Error while get service info, error code {process.ExitCode}");
                result.ErrorMessage = sb.ToString();
                result.Success = false;
            }
            else
            {
                result.Result = sb.ToString();
                result.Success = true;
            }
            result.ReturnCode = process.ExitCode;
            return result;
        }
    }
}
