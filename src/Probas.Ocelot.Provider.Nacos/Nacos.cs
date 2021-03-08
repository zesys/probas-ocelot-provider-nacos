using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
using Nacos.V2.Naming.Dtos;
using Service = Ocelot.Values.Service;
using Ocelot.Infrastructure.Extensions;
using Nacos.V2;
using Nacos.V2.Common;

namespace Probas.Ocelot.Provider.Nacos
{
    public class Nacos : IServiceDiscoveryProvider
    {
        private readonly string _serviceName;
        private readonly string _groupName;
        private readonly INacosNamingService _service;
        private const string VersionPrefix = "version-";

        public Nacos(string serviceName, INacosNamingService service)
        {
            var groupedName = serviceName.Split(Constants.SERVICE_INFO_SPLITER);
            _serviceName = groupedName.Length == 1 ? groupedName[0] : groupedName[1];
            _groupName = groupedName.Length == 2 ? groupedName[0] : Constants.DEFAULT_GROUP;
            _service = service;
        }

        public async Task<List<Service>> Get()
        {
            var services = new List<Service>();

            var instances = await _service.GetAllInstances(_serviceName, _groupName);

            if (instances != null && instances.Any())
            {
                foreach (var instance in instances)
                {
                    services.Add(BuildService(instance));
                }
            }

            return await Task.FromResult(services);
        }

        private Service BuildService(Instance instance)
        {
            var tags = GetTags(instance.Metadata);
            return new Service(instance.ServiceName, new ServiceHostAndPort(instance.Ip, instance.Port), instance.InstanceId, GetVersionFromStrings(tags), tags);
        }

        private List<string> GetTags(Dictionary<string, string> pairs)
        {
            var list = new List<string>();
            foreach (var key in pairs.Keys)
            {
                list.Add($"{key}-{pairs[key]}");
            }
            return list;
        }

        private string GetVersionFromStrings(IEnumerable<string> strings)
        {
            return strings
                ?.FirstOrDefault(x => x.StartsWith(VersionPrefix, StringComparison.Ordinal))
                .TrimStart(VersionPrefix);
        }
    }
}
