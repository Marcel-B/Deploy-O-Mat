using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Infra.Bus;
using com.b_velop.Deploy_O_Mat.Shared;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using com.b_velop.Utilities.Docker;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.Deploy_O_Mat.Queue.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<SecretProvider>();

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, sp.GetService<SecretProvider>());
            });
            
            // Utils
            services.AddScoped<IProcessor, Processor>();
        }
    }
}
