using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Repositories;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Contracts
{
    public class ExecutRRepository : IExecutRRepository
    {
        private readonly ExecutRContext _context;
        private readonly ILogger<ExecutRRepository> _logger;

        public ExecutRRepository(
            ExecutRContext context,
            ILogger<ExecutRRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task SaveCommandToLog(CommandLog commandLog)
        {
            _context.CommandLogs.Add(commandLog);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}