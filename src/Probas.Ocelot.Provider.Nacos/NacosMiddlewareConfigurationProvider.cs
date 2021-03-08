using System.Threading.Tasks;
using Ocelot.Configuration.Repository;
using Ocelot.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Probas.Ocelot.Provider.Nacos
{
    public class NacosMiddlewareConfigurationProvider
    {
        public static OcelotMiddlewareConfigurationDelegate Get = builder =>
        {
            var internalConfigRepo = builder.ApplicationServices.GetService<IInternalConfigurationRepository>();
            var config = internalConfigRepo.Get();

            var hostLifetime = builder.ApplicationServices.GetService<IHostApplicationLifetime>();

            return Task.CompletedTask;
        };
    }
}
