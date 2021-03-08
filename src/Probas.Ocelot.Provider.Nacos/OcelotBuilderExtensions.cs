using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore.V2;
using Ocelot.DependencyInjection;

namespace Probas.Ocelot.Provider.Nacos
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddNacos(this IOcelotBuilder builder)
        {
            builder.Services.AddNacosAspNet(builder.Configuration);
            builder.Services.AddSingleton(NacosProviderFactory.Get);
            builder.Services.AddSingleton(NacosMiddlewareConfigurationProvider.Get);
            return builder;
        }
    }
}
