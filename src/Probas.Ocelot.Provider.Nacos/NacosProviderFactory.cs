using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;
using Nacos.V2;
using Nacos.AspNetCore.V2;
using Microsoft.Extensions.Options;

namespace Probas.Ocelot.Provider.Nacos
{
    public static class NacosProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, route) =>
        {
            var service = provider.GetService<INacosNamingService>();
            if (config.Type?.ToLower() == "nacos" && service != null)
            {
                return new Nacos(route.ServiceName, service);
            }
            return null;
        };
    }
}
